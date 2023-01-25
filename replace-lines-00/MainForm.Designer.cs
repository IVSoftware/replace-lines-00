namespace replace_lines_00
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxMultiline = new System.Windows.Forms.TextBox();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.comboBox = new System.Windows.Forms.ComboBox();
            this.textBoxEditor = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBoxMultiline
            // 
            this.textBoxMultiline.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMultiline.BackColor = System.Drawing.Color.Azure;
            this.textBoxMultiline.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxMultiline.Location = new System.Drawing.Point(22, 72);
            this.textBoxMultiline.Multiline = true;
            this.textBoxMultiline.Name = "textBoxMultiline";
            this.textBoxMultiline.Size = new System.Drawing.Size(544, 414);
            this.textBoxMultiline.TabIndex = 0;
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(22, 13);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(88, 34);
            this.buttonLoad.TabIndex = 1;
            this.buttonLoad.Text = "Load";
            this.buttonLoad.UseVisualStyleBackColor = true;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(446, 13);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(105, 34);
            this.buttonSave.TabIndex = 1;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            // 
            // comboBox
            // 
            this.comboBox.FormattingEnabled = true;
            this.comboBox.Location = new System.Drawing.Point(116, 14);
            this.comboBox.Name = "comboBox";
            this.comboBox.Size = new System.Drawing.Size(153, 33);
            this.comboBox.TabIndex = 2;
            // 
            // textBoxEditor
            // 
            this.textBoxEditor.Location = new System.Drawing.Point(275, 16);
            this.textBoxEditor.Name = "textBoxEditor";
            this.textBoxEditor.Size = new System.Drawing.Size(165, 31);
            this.textBoxEditor.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 498);
            this.Controls.Add(this.textBoxEditor);
            this.Controls.Add(this.comboBox);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonLoad);
            this.Controls.Add(this.textBoxMultiline);
            this.Name = "MainForm";
            this.Text = "Main Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox textBoxMultiline;
        private Button buttonLoad;
        private Button buttonSave;
        private ComboBox comboBox;
        private TextBox textBoxEditor;
    }
}