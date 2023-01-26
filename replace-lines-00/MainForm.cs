using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using static System.Windows.Forms.LinkLabel;

namespace replace_lines_00
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            openFileDialog.FileOk += onOpenFileOK;
            saveFileDialog.FileOk += onSaveFileOK;
            buttonLoad.Click += onClickLoad;
            comboBox.DataSource = lines;
            comboBox.SelectedIndexChanged += onComboBoxSelectedIndexChanged;
            textBoxEditor.TextChanged += onEditorTextChanged;
            lines.ListChanged += onListChanged;
            buttonSave.Click += onClickSave;
            textBoxMultiline.ReadOnly= true;
            Disposed += (sender, e) => openFileDialog.Dispose();
            comboBox.DropDownStyle= ComboBoxStyle.DropDownList;
        }

        BindingList<Line> lines = new BindingList<Line>();

        private void onListChanged(object? sender, ListChangedEventArgs e)
        {
            switch (e.ListChangedType)
            {
                case ListChangedType.ItemChanged:
                    textBoxMultiline.Lines = lines.Select(_=>_.Serialized).ToArray();
                    break;
            }
        }

        private void onEditorTextChanged(object? sender, EventArgs e)
        {
            var item = (Line)comboBox.SelectedItem;
            if (item != null)
            {
                item[0] = textBoxEditor.Text;
            }
        }

        private OpenFileDialog openFileDialog = new OpenFileDialog
        {
            InitialDirectory = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "Files"),
            FileName = "Data.txt",
            Filter = "txt file (*.txt)|*.txt|All files (*.*)|*.*",
            FilterIndex = 2,
            RestoreDirectory = true,
        };
        private void onClickLoad(object? sender, EventArgs e) =>
            openFileDialog.ShowDialog(this);

        private void onOpenFileOK(object? sender, CancelEventArgs e)
        {
            lines.Clear();
            foreach (var serialized in File.ReadAllLines(openFileDialog.FileName))
            {
                lines.Add(new Line(serialized));
            }
            textBoxMultiline.Lines = lines.Select(_=>_.Serialized).ToArray();
            comboBox.SelectedIndex = -1;
            saveFileDialog.FileName= Path.GetFileName(openFileDialog.FileName);
        }
        SaveFileDialog saveFileDialog = new SaveFileDialog
        {
            InitialDirectory = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "Files"),
            FileName = "Data.txt",
            Filter = "txt file (*.txt)|*.txt|All files (*.*)|*.*",
            FilterIndex = 2,
            RestoreDirectory = true,
        };
        private void onClickSave(object? sender, EventArgs e) =>
            saveFileDialog.ShowDialog(this);
        private void onSaveFileOK(object? sender, CancelEventArgs e)
        {
            File.WriteAllLines(saveFileDialog.FileName, Line.ToAllLines(lines));
        }

        private void onComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            var item = (Line)comboBox.SelectedItem;
            if (item != null)
            {
                textBoxEditor.Text = item[0];
            }
        }
    }

    class Line : INotifyPropertyChanged
    {
        // Make a Line from serialized like firstName;;;100;;;0;
        // ** or use Json instead if you have a choice! **
        public Line(string serialized)
        {
            // Convert to a stored array
            _deserialized = serialized.Split(new string[] { ";;;" }, StringSplitOptions.None);
        }
        // Convert back to the format used in your file.
        public string Serialized => string.Join(";;;", _deserialized);
        private string[] _deserialized { get; } // Backing store.
        // Convert a list of Lines to a collection of strings (e.g. for Save).
        public static IEnumerable<string> ToAllLines(IEnumerable<Line> lines) =>
            lines.Select(_ => _.Serialized);
        // Determine how a Line will be displayed in the combo box (e.g. "0100").
        public override string ToString() => _deserialized[1].ToString();
        // Use array syntax to access elements of the split array.
        public string this[int index]
        {
            get => _deserialized[index];
            set
            {
                if(!Equals(_deserialized[index],value))
                {
                    // Send event when any array value changes.
                    _deserialized[index] = value;
                    OnPropertyChanged($"{index}");
                }
            }
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}