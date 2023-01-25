using System.ComponentModel;
using System.Linq;
using static System.Windows.Forms.LinkLabel;

namespace replace_lines_00
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            openFileDialog.FileOk += onFileOK;
            Disposed += (sender, e) => openFileDialog.Dispose();
            comboBox1.DataSource = lines;

            buttonLoad.Click += onClickLoad;
            buttonReplace.Click += onClickModify;
            buttonRemove.Click += onClickRemove;      
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
            foreach (var encoded in File.ReadAllLines(openFileDialog.FileName))
            {
                lines.Add(new Line(encoded));
            }
            textBoxMultiline.Lines = lines.Select(_=>_.Raw).ToArray();
            comboBox1.SelectedIndex = -1;
        }








        private void onClickModify(object? sender, EventArgs e)
        {
            //lines[0] = "john;;;100;;;0;";
            //lines[1] = "Patrick;;;100;;;1;";
            //lines[2] = "firstName;;;100;;;2;";
            //textBoxMultiline.Lines = lines.ToArray();
        }

        private void onClickRemove(object? sender, EventArgs e)
        {
            //for (int i = 0; i < 3; i++)
            //{
            //    if (lines.Any())
            //    {
            //        lines.RemoveAt(lines.Count - 1);
            //    }
            //    else break;
            //}
            //textBoxMultiline.Lines = lines.ToArray();
        }

        private void onComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            var item = (string)comboBox1.SelectedItem;
            if (item != null)
            {
                string[] r = item.Split(new string[] { ";;;" }, StringSplitOptions.None);
                textBoxMultiline.AppendText($"{r[0]}{Environment.NewLine}");
            }
        }
    }

    class Line
    {
        public Line(string text)
        {
            Elements = text.Split(new string[] { ";;;" }, StringSplitOptions.None);
        }
        public string[] Elements { get; }
        public string Raw => string.Join(";;;", Elements);
        public string FormatForSave() => Raw;
        public override string ToString() => Elements[1].ToString();
    }
}