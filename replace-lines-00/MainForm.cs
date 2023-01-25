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

        BindingList<string> lines = new BindingList<string>();

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
            foreach (var line in File.ReadAllLines(openFileDialog.FileName))
            {
                lines.Add(line);
            }
#if false
            // Doesn't seem to do anything
            List<string> result = new List<string>();
            string[] par = new string[1];
            par[0] = ";;;";
            for (int i = 0; i < lines.Count; i++)
            {
                string[] row = lines[i].Split(par, StringSplitOptions.None);
                if (row.Length > 2)
                {
                    if (!result.Contains(row[1]))
                        result.Add(row[1]);
                }
            }
#endif

            comboBox1.SelectedIndex = -1;
        }








        private void onClickModify(object? sender, EventArgs e)
        {
            // The list can be modified with indexer[N] syntax.
            lines[0] = "john;;;100;;;0;";
            lines[1] = "Patrick;;;100;;;1;";
            lines[2] = "firstName;;;100;;;2;";
            textBoxMultiline.Lines = lines.ToArray();
        }

        private void onClickRemove(object? sender, EventArgs e)
        {
            for (int i = 0; i < 3; i++)
            {
                if (lines.Any())
                {
                    lines.RemoveAt(lines.Count - 1);
                }
                else break;
            }
            textBoxMultiline.Lines = lines.ToArray();
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
}