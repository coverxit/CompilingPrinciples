namespace CompilingPrinciples.GUICodeGen
{
    partial class CodeGenForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CodeGenForm));
            this.tableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.tablePanelControls = new System.Windows.Forms.TableLayoutPanel();
            this.panelToken = new System.Windows.Forms.Panel();
            this.groupBoxThreeAddrCode = new System.Windows.Forms.GroupBox();
            this.tablePanelThreeAddrCode = new System.Windows.Forms.TableLayoutPanel();
            this.panelTokensInner = new System.Windows.Forms.Panel();
            this.listInterCode = new CompilingPrinciples.Utility.WindowThemeListView();
            this.interCodeHeaderLabel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.interCodeHeaderCode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panelSymbolTable = new System.Windows.Forms.Panel();
            this.groupBoxSymbolTable = new System.Windows.Forms.GroupBox();
            this.tablePanelSymbolTable = new System.Windows.Forms.TableLayoutPanel();
            this.panelSymbolTableInner = new System.Windows.Forms.Panel();
            this.listSymbolTable = new CompilingPrinciples.Utility.WindowThemeListView();
            this.symbolHeaderId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.symbolHeaderSymbol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.symbolHeaderType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.symbolTypeOffset = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panelCode = new System.Windows.Forms.Panel();
            this.groupBoxCode = new System.Windows.Forms.GroupBox();
            this.tablePanelCode = new System.Windows.Forms.TableLayoutPanel();
            this.flowPanelButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAnalyze = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.panelCodeInner = new System.Windows.Forms.Panel();
            this.textCode = new ScintillaNET.Scintilla();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.openCtxDialog = new System.Windows.Forms.OpenFileDialog();
            this.tableLayout.SuspendLayout();
            this.tablePanelControls.SuspendLayout();
            this.panelToken.SuspendLayout();
            this.groupBoxThreeAddrCode.SuspendLayout();
            this.tablePanelThreeAddrCode.SuspendLayout();
            this.panelTokensInner.SuspendLayout();
            this.panelSymbolTable.SuspendLayout();
            this.groupBoxSymbolTable.SuspendLayout();
            this.tablePanelSymbolTable.SuspendLayout();
            this.panelSymbolTableInner.SuspendLayout();
            this.panelCode.SuspendLayout();
            this.groupBoxCode.SuspendLayout();
            this.tablePanelCode.SuspendLayout();
            this.flowPanelButtons.SuspendLayout();
            this.panelCodeInner.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayout
            // 
            this.tableLayout.ColumnCount = 3;
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayout.Controls.Add(this.tablePanelControls, 1, 1);
            this.tableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayout.Location = new System.Drawing.Point(0, 0);
            this.tableLayout.Name = "tableLayout";
            this.tableLayout.RowCount = 3;
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayout.Size = new System.Drawing.Size(1909, 922);
            this.tableLayout.TabIndex = 8;
            // 
            // tablePanelControls
            // 
            this.tablePanelControls.ColumnCount = 5;
            this.tablePanelControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 665F));
            this.tablePanelControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tablePanelControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablePanelControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tablePanelControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 595F));
            this.tablePanelControls.Controls.Add(this.panelToken, 2, 0);
            this.tablePanelControls.Controls.Add(this.panelSymbolTable, 4, 0);
            this.tablePanelControls.Controls.Add(this.panelCode, 0, 0);
            this.tablePanelControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanelControls.Location = new System.Drawing.Point(23, 13);
            this.tablePanelControls.Name = "tablePanelControls";
            this.tablePanelControls.RowCount = 1;
            this.tablePanelControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablePanelControls.Size = new System.Drawing.Size(1863, 901);
            this.tablePanelControls.TabIndex = 5;
            // 
            // panelToken
            // 
            this.panelToken.Controls.Add(this.groupBoxThreeAddrCode);
            this.panelToken.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelToken.Location = new System.Drawing.Point(678, 3);
            this.panelToken.Name = "panelToken";
            this.panelToken.Size = new System.Drawing.Size(577, 895);
            this.panelToken.TabIndex = 2;
            // 
            // groupBoxThreeAddrCode
            // 
            this.groupBoxThreeAddrCode.Controls.Add(this.tablePanelThreeAddrCode);
            this.groupBoxThreeAddrCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxThreeAddrCode.Location = new System.Drawing.Point(0, 0);
            this.groupBoxThreeAddrCode.Name = "groupBoxThreeAddrCode";
            this.groupBoxThreeAddrCode.Size = new System.Drawing.Size(577, 895);
            this.groupBoxThreeAddrCode.TabIndex = 0;
            this.groupBoxThreeAddrCode.TabStop = false;
            this.groupBoxThreeAddrCode.Text = "Three Address Code";
            // 
            // tablePanelThreeAddrCode
            // 
            this.tablePanelThreeAddrCode.ColumnCount = 3;
            this.tablePanelThreeAddrCode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tablePanelThreeAddrCode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablePanelThreeAddrCode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tablePanelThreeAddrCode.Controls.Add(this.panelTokensInner, 1, 1);
            this.tablePanelThreeAddrCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanelThreeAddrCode.Location = new System.Drawing.Point(3, 27);
            this.tablePanelThreeAddrCode.Name = "tablePanelThreeAddrCode";
            this.tablePanelThreeAddrCode.RowCount = 3;
            this.tablePanelThreeAddrCode.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tablePanelThreeAddrCode.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablePanelThreeAddrCode.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tablePanelThreeAddrCode.Size = new System.Drawing.Size(571, 865);
            this.tablePanelThreeAddrCode.TabIndex = 0;
            // 
            // panelTokensInner
            // 
            this.panelTokensInner.Controls.Add(this.listInterCode);
            this.panelTokensInner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTokensInner.Location = new System.Drawing.Point(13, 8);
            this.panelTokensInner.Name = "panelTokensInner";
            this.panelTokensInner.Size = new System.Drawing.Size(545, 849);
            this.panelTokensInner.TabIndex = 0;
            // 
            // listInterCode
            // 
            this.listInterCode.CausesValidation = false;
            this.listInterCode.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.interCodeHeaderLabel,
            this.interCodeHeaderCode});
            this.listInterCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listInterCode.FullRowSelect = true;
            this.listInterCode.Location = new System.Drawing.Point(0, 0);
            this.listInterCode.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.listInterCode.MultiSelect = false;
            this.listInterCode.Name = "listInterCode";
            this.listInterCode.Size = new System.Drawing.Size(545, 849);
            this.listInterCode.TabIndex = 0;
            this.listInterCode.UseCompatibleStateImageBehavior = false;
            this.listInterCode.View = System.Windows.Forms.View.Details;
            // 
            // interCodeHeaderLabel
            // 
            this.interCodeHeaderLabel.Text = "Label";
            this.interCodeHeaderLabel.Width = 100;
            // 
            // interCodeHeaderCode
            // 
            this.interCodeHeaderCode.Text = "Code";
            this.interCodeHeaderCode.Width = 400;
            // 
            // panelSymbolTable
            // 
            this.panelSymbolTable.Controls.Add(this.groupBoxSymbolTable);
            this.panelSymbolTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSymbolTable.Location = new System.Drawing.Point(1271, 3);
            this.panelSymbolTable.Name = "panelSymbolTable";
            this.panelSymbolTable.Size = new System.Drawing.Size(589, 895);
            this.panelSymbolTable.TabIndex = 3;
            // 
            // groupBoxSymbolTable
            // 
            this.groupBoxSymbolTable.Controls.Add(this.tablePanelSymbolTable);
            this.groupBoxSymbolTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxSymbolTable.Location = new System.Drawing.Point(0, 0);
            this.groupBoxSymbolTable.Name = "groupBoxSymbolTable";
            this.groupBoxSymbolTable.Size = new System.Drawing.Size(589, 895);
            this.groupBoxSymbolTable.TabIndex = 6;
            this.groupBoxSymbolTable.TabStop = false;
            this.groupBoxSymbolTable.Text = "Symbol Table";
            // 
            // tablePanelSymbolTable
            // 
            this.tablePanelSymbolTable.ColumnCount = 3;
            this.tablePanelSymbolTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tablePanelSymbolTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablePanelSymbolTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tablePanelSymbolTable.Controls.Add(this.panelSymbolTableInner, 1, 1);
            this.tablePanelSymbolTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanelSymbolTable.Location = new System.Drawing.Point(3, 27);
            this.tablePanelSymbolTable.Name = "tablePanelSymbolTable";
            this.tablePanelSymbolTable.RowCount = 3;
            this.tablePanelSymbolTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tablePanelSymbolTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablePanelSymbolTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tablePanelSymbolTable.Size = new System.Drawing.Size(583, 865);
            this.tablePanelSymbolTable.TabIndex = 0;
            // 
            // panelSymbolTableInner
            // 
            this.panelSymbolTableInner.Controls.Add(this.listSymbolTable);
            this.panelSymbolTableInner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSymbolTableInner.Location = new System.Drawing.Point(13, 8);
            this.panelSymbolTableInner.Name = "panelSymbolTableInner";
            this.panelSymbolTableInner.Size = new System.Drawing.Size(557, 849);
            this.panelSymbolTableInner.TabIndex = 0;
            // 
            // listSymbolTable
            // 
            this.listSymbolTable.CausesValidation = false;
            this.listSymbolTable.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.symbolHeaderId,
            this.symbolHeaderSymbol,
            this.symbolHeaderType,
            this.symbolTypeOffset});
            this.listSymbolTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listSymbolTable.FullRowSelect = true;
            listViewGroup1.Header = "Keyword";
            listViewGroup1.Name = "lvGroupKeyword";
            listViewGroup2.Header = "Identifier";
            listViewGroup2.Name = "lvGroupIdentifier";
            this.listSymbolTable.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2});
            this.listSymbolTable.Location = new System.Drawing.Point(0, 0);
            this.listSymbolTable.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.listSymbolTable.MultiSelect = false;
            this.listSymbolTable.Name = "listSymbolTable";
            this.listSymbolTable.Size = new System.Drawing.Size(557, 849);
            this.listSymbolTable.TabIndex = 0;
            this.listSymbolTable.UseCompatibleStateImageBehavior = false;
            this.listSymbolTable.View = System.Windows.Forms.View.Details;
            // 
            // symbolHeaderId
            // 
            this.symbolHeaderId.Text = "Id";
            this.symbolHeaderId.Width = 65;
            // 
            // symbolHeaderSymbol
            // 
            this.symbolHeaderSymbol.Text = "Symbol";
            this.symbolHeaderSymbol.Width = 190;
            // 
            // symbolHeaderType
            // 
            this.symbolHeaderType.Text = "Type (Width)";
            this.symbolHeaderType.Width = 150;
            // 
            // symbolTypeOffset
            // 
            this.symbolTypeOffset.Text = "Offset";
            this.symbolTypeOffset.Width = 100;
            // 
            // panelCode
            // 
            this.panelCode.Controls.Add(this.groupBoxCode);
            this.panelCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCode.Location = new System.Drawing.Point(3, 3);
            this.panelCode.Name = "panelCode";
            this.panelCode.Size = new System.Drawing.Size(659, 895);
            this.panelCode.TabIndex = 4;
            // 
            // groupBoxCode
            // 
            this.groupBoxCode.Controls.Add(this.tablePanelCode);
            this.groupBoxCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxCode.Location = new System.Drawing.Point(0, 0);
            this.groupBoxCode.Name = "groupBoxCode";
            this.groupBoxCode.Size = new System.Drawing.Size(659, 895);
            this.groupBoxCode.TabIndex = 3;
            this.groupBoxCode.TabStop = false;
            this.groupBoxCode.Text = "Code";
            // 
            // tablePanelCode
            // 
            this.tablePanelCode.ColumnCount = 3;
            this.tablePanelCode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tablePanelCode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablePanelCode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tablePanelCode.Controls.Add(this.flowPanelButtons, 1, 2);
            this.tablePanelCode.Controls.Add(this.panelCodeInner, 1, 1);
            this.tablePanelCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanelCode.Location = new System.Drawing.Point(3, 27);
            this.tablePanelCode.Name = "tablePanelCode";
            this.tablePanelCode.RowCount = 4;
            this.tablePanelCode.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tablePanelCode.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablePanelCode.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 67F));
            this.tablePanelCode.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tablePanelCode.Size = new System.Drawing.Size(653, 865);
            this.tablePanelCode.TabIndex = 0;
            // 
            // flowPanelButtons
            // 
            this.flowPanelButtons.Controls.Add(this.btnAnalyze);
            this.flowPanelButtons.Controls.Add(this.btnOpen);
            this.flowPanelButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowPanelButtons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowPanelButtons.Location = new System.Drawing.Point(13, 796);
            this.flowPanelButtons.Name = "flowPanelButtons";
            this.flowPanelButtons.Size = new System.Drawing.Size(627, 61);
            this.flowPanelButtons.TabIndex = 3;
            // 
            // btnAnalyze
            // 
            this.btnAnalyze.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAnalyze.Location = new System.Drawing.Point(491, 2);
            this.btnAnalyze.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnAnalyze.Name = "btnAnalyze";
            this.btnAnalyze.Size = new System.Drawing.Size(132, 52);
            this.btnAnalyze.TabIndex = 1;
            this.btnAnalyze.Text = "Analyze";
            this.btnAnalyze.UseVisualStyleBackColor = true;
            this.btnAnalyze.Click += new System.EventHandler(this.btnAnalyze_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpen.Location = new System.Drawing.Point(351, 2);
            this.btnOpen.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(132, 52);
            this.btnOpen.TabIndex = 0;
            this.btnOpen.Text = "Open...";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // panelCodeInner
            // 
            this.panelCodeInner.Controls.Add(this.textCode);
            this.panelCodeInner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCodeInner.Location = new System.Drawing.Point(13, 8);
            this.panelCodeInner.Name = "panelCodeInner";
            this.panelCodeInner.Size = new System.Drawing.Size(627, 782);
            this.panelCodeInner.TabIndex = 0;
            // 
            // textCode
            // 
            this.textCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textCode.Lexer = ScintillaNET.Lexer.Cpp;
            this.textCode.Location = new System.Drawing.Point(0, 0);
            this.textCode.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.textCode.Name = "textCode";
            this.textCode.ScrollWidth = 200;
            this.textCode.Size = new System.Drawing.Size(627, 782);
            this.textCode.TabIndex = 0;
            this.textCode.Text = "int a;\r\nint b;\r\nint c;\r\n\r\na = 2;\r\nb = 1;\r\n\r\nif (a>b)\r\n\tc=a+b;\r\nelse\r\n\tc=a-b;";
            this.textCode.UseTabs = false;
            // 
            // openCtxDialog
            // 
            this.openCtxDialog.Filter = "Shindo\'s Parser Context|*.ctx";
            this.openCtxDialog.Title = "Open context...";
            // 
            // CodeGenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1909, 922);
            this.Controls.Add(this.tableLayout);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CodeGenForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Shindo\'s Intermediate Code Generator";
            this.Shown += new System.EventHandler(this.ParserForm_Shown);
            this.tableLayout.ResumeLayout(false);
            this.tablePanelControls.ResumeLayout(false);
            this.panelToken.ResumeLayout(false);
            this.groupBoxThreeAddrCode.ResumeLayout(false);
            this.tablePanelThreeAddrCode.ResumeLayout(false);
            this.panelTokensInner.ResumeLayout(false);
            this.panelSymbolTable.ResumeLayout(false);
            this.groupBoxSymbolTable.ResumeLayout(false);
            this.tablePanelSymbolTable.ResumeLayout(false);
            this.panelSymbolTableInner.ResumeLayout(false);
            this.panelCode.ResumeLayout(false);
            this.groupBoxCode.ResumeLayout(false);
            this.tablePanelCode.ResumeLayout(false);
            this.flowPanelButtons.ResumeLayout(false);
            this.panelCodeInner.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayout;
        private System.Windows.Forms.TableLayoutPanel tablePanelControls;
        private System.Windows.Forms.Panel panelToken;
        private System.Windows.Forms.GroupBox groupBoxThreeAddrCode;
        private System.Windows.Forms.TableLayoutPanel tablePanelThreeAddrCode;
        private System.Windows.Forms.Panel panelTokensInner;
        private CompilingPrinciples.Utility.WindowThemeListView listInterCode;
        private System.Windows.Forms.Panel panelSymbolTable;
        private System.Windows.Forms.GroupBox groupBoxSymbolTable;
        private System.Windows.Forms.TableLayoutPanel tablePanelSymbolTable;
        private System.Windows.Forms.Panel panelSymbolTableInner;
        private CompilingPrinciples.Utility.WindowThemeListView listSymbolTable;
        private System.Windows.Forms.ColumnHeader symbolHeaderId;
        private System.Windows.Forms.ColumnHeader symbolHeaderSymbol;
        private System.Windows.Forms.Panel panelCode;
        private System.Windows.Forms.GroupBox groupBoxCode;
        private System.Windows.Forms.TableLayoutPanel tablePanelCode;
        private System.Windows.Forms.Panel panelCodeInner;
        private ScintillaNET.Scintilla textCode;
        private System.Windows.Forms.ColumnHeader interCodeHeaderLabel;
        private System.Windows.Forms.ColumnHeader interCodeHeaderCode;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.OpenFileDialog openCtxDialog;
        private System.Windows.Forms.ColumnHeader symbolHeaderType;
        private System.Windows.Forms.ColumnHeader symbolTypeOffset;
        private System.Windows.Forms.FlowLayoutPanel flowPanelButtons;
        private System.Windows.Forms.Button btnAnalyze;
        private System.Windows.Forms.Button btnOpen;
    }
}