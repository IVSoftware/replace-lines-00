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
            openFileDialog.FileOk += onFileOK;
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

        BindingList<Line> lines = new BindingList<Line>();

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
            openFileDialog.ShowDialog();

        private void onFileOK(object? sender, CancelEventArgs e)
        {
            lines.Clear();
            foreach (var serialized in File.ReadAllLines(openFileDialog.FileName))
            {
                lines.Add(new Line(serialized));
            }
            textBoxMultiline.Lines = lines.Select(_=>_.Serialized).ToArray();
            comboBox.SelectedIndex = -1;
        }
        private void onClickSave(object? sender, EventArgs e)
        {
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
        public Line(string serialized)
        {
            _deserialized = serialized.Split(new string[] { ";;;" }, StringSplitOptions.None);
        }
        public string this[int index]
        {
            get => _deserialized[index];
            set
            {
                if(!Equals(_deserialized[index],value))
                {
                    _deserialized[index] = value;
                    OnPropertyChanged($"{index}");
                }
            }
        }
        private string[] _deserialized { get; }
        public string Serialized => string.Join(";;;", _deserialized);
        public override string ToString() => _deserialized[1].ToString();
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}