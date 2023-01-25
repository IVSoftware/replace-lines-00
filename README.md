I've read your code carefully and my understanding may not be perfect but it should be close enough that I can help. One way to achieve your objectives is with data binding and I'll demonstrate this step by step.

***
**"open a txt file to a combobox"**

In your main form, you'll move the `OpenFileDialog` so that it is now a member variable:

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

In the main form CTor
- Handle event `buttonLoad.Click` to display the dialog 
- Handle event `openFileDialog.FileOk` to read the file.
- The `DataSource` of `comboBox` will be set to the list of lines read from the file.

**Initialize**

    public MainForm()
    {
        InitializeComponent();
        buttonLoad.Click += onClickLoad;
        openFileDialog.FileOk += onFileOK;
        comboBox.DataSource = lines;
        .
        .
        .
        Disposed += (sender, e) => openFileDialog.Dispose();
    }
    private void onClickLoad(object? sender, EventArgs e) =>
        openFileDialog.ShowDialog();

In a minute, we'll look at those three things step by step. But first...
***
**Serialize and Deserialize**

There's a lot of loose code doing string splits to decode your serializer format. This might be better to encapsulate in a class that is suitable to be used in a `BindingList<Line>` that is the data source of your combo box.

    class Line : INotifyPropertyChanged
    {
        // Make a Line from serialized like firstName;;;100;;;0;
        public Line(string serialized)
        {
            _deserialized = serialized.Split(new string[] { ";;;" }, StringSplitOptions.None);
        }
        // Use array syntax to access the split array.
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
        // The array that is parsed in CTor
        private string[] _deserialized { get; }
        // Turn the array back into a form to store in file
        public string Serialized => string.Join(";;;", _deserialized);
        // Determine what will display in bound ComboBox
        public override string ToString() => _deserialized[1].ToString();
        // Send an event when any of the array values change in the item.
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

***
**Load**

Once the data file is selected, the raw text file will be shown and the combo box will be populated.

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

[![screenshot][1]][1]

***
**Replacement**
Going back to the main form CTor there are three more events we care about:


    public MainForm()
    {
        .
        .
        .
        comboBox.SelectedIndexChanged += onComboBoxSelectedIndexChanged;
        textBoxEditor.TextChanged += onEditorTextChanged;
        lines.ListChanged += onListChanged;
        .
        .
        .
    }

***
*When a new item is selected in the combo box, put it in the text editor.*

    private void onComboBoxSelectedIndexChanged(object sender, EventArgs e)
    {
        var item = (Line)comboBox.SelectedItem;
        if (item != null)
        {
            textBoxEditor.Text = item[0];
        }
    }

***
*When the textEditor text changes, modify the item.*

    private void onEditorTextChanged(object? sender, EventArgs e)
    {
        var item = (Line)comboBox.SelectedItem;
        if (item != null)
        {
            item[0] = textBoxEditor.Text;
        }
    }

***
*When the item changes, update the file display in the big textbox.*

    private void onListChanged(object? sender, ListChangedEventArgs e)
    {
        switch (e.ListChangedType)
        {
            case ListChangedType.ItemChanged:
                textBoxMultiline.Lines = lines.Select(_=>_.Serialized).ToArray();
                break;
        }
    }

[![replacement][2]][2]

I hope this is "close enough" to what you have described that if will give you some ideas to experiment with.


  [1]: https://i.stack.imgur.com/tt95h.png
  [2]: https://i.stack.imgur.com/uukTr.png