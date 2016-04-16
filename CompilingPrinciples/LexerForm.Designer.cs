namespace CompilingPrinciples
{
    partial class LexerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Keyword", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Identifier", System.Windows.Forms.HorizontalAlignment.Left);
            this.listTokens = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnOpen = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnAnalyze = new System.Windows.Forms.Button();
            this.listSymbols = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.textCode = new ScintillaNET.Scintilla();
            this.SuspendLayout();
            // 
            // listTokens
            // 
            this.listTokens.CausesValidation = false;
            this.listTokens.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listTokens.FullRowSelect = true;
            this.listTokens.Location = new System.Drawing.Point(698, 44);
            this.listTokens.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.listTokens.MultiSelect = false;
            this.listTokens.Name = "listTokens";
            this.listTokens.Size = new System.Drawing.Size(512, 630);
            this.listTokens.TabIndex = 3;
            this.listTokens.UseCompatibleStateImageBehavior = false;
            this.listTokens.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Type";
            this.columnHeader1.Width = 200;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Value";
            this.columnHeader2.Width = 270;
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(366, 626);
            this.btnOpen.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(132, 52);
            this.btnOpen.TabIndex = 1;
            this.btnOpen.Text = "Open...";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnAnalyze
            // 
            this.btnAnalyze.Location = new System.Drawing.Point(526, 626);
            this.btnAnalyze.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnAnalyze.Name = "btnAnalyze";
            this.btnAnalyze.Size = new System.Drawing.Size(132, 52);
            this.btnAnalyze.TabIndex = 2;
            this.btnAnalyze.Text = "Analyze";
            this.btnAnalyze.UseVisualStyleBackColor = true;
            this.btnAnalyze.Click += new System.EventHandler(this.btnAnalyze_Click);
            // 
            // listSymbols
            // 
            this.listSymbols.CausesValidation = false;
            this.listSymbols.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader3});
            this.listSymbols.FullRowSelect = true;
            listViewGroup1.Header = "Keyword";
            listViewGroup1.Name = "lvGroupKeyword";
            listViewGroup2.Header = "Identifier";
            listViewGroup2.Name = "lvGroupIdentifier";
            this.listSymbols.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2});
            this.listSymbols.Location = new System.Drawing.Point(1252, 44);
            this.listSymbols.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.listSymbols.MultiSelect = false;
            this.listSymbols.Name = "listSymbols";
            this.listSymbols.Size = new System.Drawing.Size(330, 630);
            this.listSymbols.TabIndex = 4;
            this.listSymbols.UseCompatibleStateImageBehavior = false;
            this.listSymbols.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Id";
            this.columnHeader4.Width = 65;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Symbol";
            this.columnHeader3.Width = 220;
            // 
            // textCode
            // 
            this.textCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textCode.EndAtLastLine = false;
            this.textCode.Lexer = ScintillaNET.Lexer.Cpp;
            this.textCode.Location = new System.Drawing.Point(42, 44);
            this.textCode.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.textCode.Name = "textCode";
            this.textCode.ScrollWidth = 200;
            this.textCode.Size = new System.Drawing.Size(614, 560);
            this.textCode.TabIndex = 0;
            this.textCode.UseTabs = false;
            this.textCode.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textCode_KeyUp);
            // 
            // LexerForm
            // 
            this.AcceptButton = this.btnAnalyze;
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1622, 718);
            this.Controls.Add(this.textCode);
            this.Controls.Add(this.listSymbols);
            this.Controls.Add(this.btnAnalyze);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.listTokens);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.MaximizeBox = false;
            this.Name = "LexerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lexer";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listTokens;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button btnAnalyze;
        private System.Windows.Forms.ListView listSymbols;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private ScintillaNET.Scintilla textCode;
        private System.Windows.Forms.ColumnHeader columnHeader4;
    }
}