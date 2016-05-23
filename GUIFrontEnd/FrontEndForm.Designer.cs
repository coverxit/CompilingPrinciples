namespace CompilingPrinciples.GUIFrontEnd
{
    partial class FrontEndForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrontEndForm));
            this.tableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.tablePanelControls = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxParseStep = new System.Windows.Forms.GroupBox();
            this.tablePanelParse = new System.Windows.Forms.TableLayoutPanel();
            this.listParse = new CompilingPrinciples.Utility.WindowThemeListView();
            this.parseHeaderStack = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.parseHeaderSymbol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.parseHeaderAction = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tablePanelThreeAndSymbol = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxThreeAddrCode = new System.Windows.Forms.GroupBox();
            this.tablePanelThreeAddrCode = new System.Windows.Forms.TableLayoutPanel();
            this.panelTokensInner = new System.Windows.Forms.Panel();
            this.listInterCode = new CompilingPrinciples.Utility.WindowThemeListView();
            this.interCodeHeaderLabel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.interCodeHeaderCode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.flowPanelOptions = new System.Windows.Forms.FlowLayoutPanel();
            this.rbPseudo = new System.Windows.Forms.RadioButton();
            this.rbAddressed = new System.Windows.Forms.RadioButton();
            this.groupBoxSymbolTable = new System.Windows.Forms.GroupBox();
            this.tablePanelSymbolTable = new System.Windows.Forms.TableLayoutPanel();
            this.panelSymbolTableInner = new System.Windows.Forms.Panel();
            this.listSymbolTable = new CompilingPrinciples.Utility.WindowThemeListView();
            this.symbolHeaderId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.symbolHeaderSymbol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.symbolHeaderType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.symbolTypeOffset = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tablePanelCodeAndToken = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxTokens = new System.Windows.Forms.GroupBox();
            this.tablePanelTokens = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.listTokens = new CompilingPrinciples.Utility.WindowThemeListView();
            this.tokenHeaderType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tokenHeaderValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBoxCode = new System.Windows.Forms.GroupBox();
            this.tablePanelCode = new System.Windows.Forms.TableLayoutPanel();
            this.tablePanelFunctions = new System.Windows.Forms.TableLayoutPanel();
            this.flowPanelButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAnalyze = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.tablePanelParser = new System.Windows.Forms.TableLayoutPanel();
            this.rbLR1 = new System.Windows.Forms.RadioButton();
            this.rbSLR = new System.Windows.Forms.RadioButton();
            this.panelCodeInner = new System.Windows.Forms.Panel();
            this.textCode = new ScintillaNET.Scintilla();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tableLayout.SuspendLayout();
            this.tablePanelControls.SuspendLayout();
            this.groupBoxParseStep.SuspendLayout();
            this.tablePanelParse.SuspendLayout();
            this.tablePanelThreeAndSymbol.SuspendLayout();
            this.groupBoxThreeAddrCode.SuspendLayout();
            this.tablePanelThreeAddrCode.SuspendLayout();
            this.panelTokensInner.SuspendLayout();
            this.flowPanelOptions.SuspendLayout();
            this.groupBoxSymbolTable.SuspendLayout();
            this.tablePanelSymbolTable.SuspendLayout();
            this.panelSymbolTableInner.SuspendLayout();
            this.tablePanelCodeAndToken.SuspendLayout();
            this.groupBoxTokens.SuspendLayout();
            this.tablePanelTokens.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBoxCode.SuspendLayout();
            this.tablePanelCode.SuspendLayout();
            this.tablePanelFunctions.SuspendLayout();
            this.flowPanelButtons.SuspendLayout();
            this.tablePanelParser.SuspendLayout();
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
            this.tableLayout.Size = new System.Drawing.Size(2486, 1207);
            this.tableLayout.TabIndex = 9;
            // 
            // tablePanelControls
            // 
            this.tablePanelControls.ColumnCount = 5;
            this.tablePanelControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36F));
            this.tablePanelControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tablePanelControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 64F));
            this.tablePanelControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tablePanelControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 595F));
            this.tablePanelControls.Controls.Add(this.groupBoxParseStep, 2, 0);
            this.tablePanelControls.Controls.Add(this.tablePanelThreeAndSymbol, 4, 0);
            this.tablePanelControls.Controls.Add(this.tablePanelCodeAndToken, 0, 0);
            this.tablePanelControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanelControls.Location = new System.Drawing.Point(23, 13);
            this.tablePanelControls.Name = "tablePanelControls";
            this.tablePanelControls.RowCount = 1;
            this.tablePanelControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablePanelControls.Size = new System.Drawing.Size(2440, 1186);
            this.tablePanelControls.TabIndex = 5;
            // 
            // groupBoxParseStep
            // 
            this.groupBoxParseStep.Controls.Add(this.tablePanelParse);
            this.groupBoxParseStep.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxParseStep.Location = new System.Drawing.Point(670, 3);
            this.groupBoxParseStep.Name = "groupBoxParseStep";
            this.groupBoxParseStep.Size = new System.Drawing.Size(1162, 1180);
            this.groupBoxParseStep.TabIndex = 6;
            this.groupBoxParseStep.TabStop = false;
            this.groupBoxParseStep.Text = "Parse Step";
            // 
            // tablePanelParse
            // 
            this.tablePanelParse.ColumnCount = 3;
            this.tablePanelParse.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tablePanelParse.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablePanelParse.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tablePanelParse.Controls.Add(this.listParse, 1, 1);
            this.tablePanelParse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanelParse.Location = new System.Drawing.Point(3, 27);
            this.tablePanelParse.Name = "tablePanelParse";
            this.tablePanelParse.RowCount = 3;
            this.tablePanelParse.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tablePanelParse.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablePanelParse.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tablePanelParse.Size = new System.Drawing.Size(1156, 1150);
            this.tablePanelParse.TabIndex = 0;
            // 
            // listParse
            // 
            this.listParse.CausesValidation = false;
            this.listParse.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.parseHeaderStack,
            this.parseHeaderSymbol,
            this.parseHeaderAction});
            this.listParse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listParse.FullRowSelect = true;
            this.listParse.Location = new System.Drawing.Point(14, 7);
            this.listParse.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.listParse.MultiSelect = false;
            this.listParse.Name = "listParse";
            this.listParse.Size = new System.Drawing.Size(1128, 1136);
            this.listParse.TabIndex = 1;
            this.listParse.UseCompatibleStateImageBehavior = false;
            this.listParse.View = System.Windows.Forms.View.Details;
            // 
            // parseHeaderStack
            // 
            this.parseHeaderStack.Text = "Stack";
            this.parseHeaderStack.Width = 470;
            // 
            // parseHeaderSymbol
            // 
            this.parseHeaderSymbol.Text = "Symbols";
            this.parseHeaderSymbol.Width = 400;
            // 
            // parseHeaderAction
            // 
            this.parseHeaderAction.Text = "Action";
            this.parseHeaderAction.Width = 350;
            // 
            // tablePanelThreeAndSymbol
            // 
            this.tablePanelThreeAndSymbol.ColumnCount = 1;
            this.tablePanelThreeAndSymbol.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tablePanelThreeAndSymbol.Controls.Add(this.groupBoxThreeAddrCode, 0, 0);
            this.tablePanelThreeAndSymbol.Controls.Add(this.groupBoxSymbolTable, 0, 1);
            this.tablePanelThreeAndSymbol.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanelThreeAndSymbol.Location = new System.Drawing.Point(1848, 3);
            this.tablePanelThreeAndSymbol.Name = "tablePanelThreeAndSymbol";
            this.tablePanelThreeAndSymbol.RowCount = 2;
            this.tablePanelThreeAndSymbol.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tablePanelThreeAndSymbol.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tablePanelThreeAndSymbol.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tablePanelThreeAndSymbol.Size = new System.Drawing.Size(589, 1180);
            this.tablePanelThreeAndSymbol.TabIndex = 5;
            // 
            // groupBoxThreeAddrCode
            // 
            this.groupBoxThreeAddrCode.Controls.Add(this.tablePanelThreeAddrCode);
            this.groupBoxThreeAddrCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxThreeAddrCode.Location = new System.Drawing.Point(3, 3);
            this.groupBoxThreeAddrCode.Name = "groupBoxThreeAddrCode";
            this.groupBoxThreeAddrCode.Size = new System.Drawing.Size(583, 584);
            this.groupBoxThreeAddrCode.TabIndex = 8;
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
            this.tablePanelThreeAddrCode.Controls.Add(this.flowPanelOptions, 1, 2);
            this.tablePanelThreeAddrCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanelThreeAddrCode.Location = new System.Drawing.Point(3, 27);
            this.tablePanelThreeAddrCode.Name = "tablePanelThreeAddrCode";
            this.tablePanelThreeAddrCode.RowCount = 4;
            this.tablePanelThreeAddrCode.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tablePanelThreeAddrCode.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablePanelThreeAddrCode.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tablePanelThreeAddrCode.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tablePanelThreeAddrCode.Size = new System.Drawing.Size(577, 554);
            this.tablePanelThreeAddrCode.TabIndex = 0;
            // 
            // panelTokensInner
            // 
            this.panelTokensInner.Controls.Add(this.listInterCode);
            this.panelTokensInner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTokensInner.Location = new System.Drawing.Point(13, 8);
            this.panelTokensInner.Name = "panelTokensInner";
            this.panelTokensInner.Size = new System.Drawing.Size(551, 498);
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
            this.listInterCode.Size = new System.Drawing.Size(551, 498);
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
            // flowPanelOptions
            // 
            this.flowPanelOptions.Controls.Add(this.rbPseudo);
            this.flowPanelOptions.Controls.Add(this.rbAddressed);
            this.flowPanelOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowPanelOptions.Location = new System.Drawing.Point(13, 512);
            this.flowPanelOptions.Name = "flowPanelOptions";
            this.flowPanelOptions.Size = new System.Drawing.Size(551, 34);
            this.flowPanelOptions.TabIndex = 1;
            // 
            // rbPseudo
            // 
            this.rbPseudo.AutoSize = true;
            this.rbPseudo.Checked = true;
            this.rbPseudo.Location = new System.Drawing.Point(3, 3);
            this.rbPseudo.Name = "rbPseudo";
            this.rbPseudo.Size = new System.Drawing.Size(173, 29);
            this.rbPseudo.TabIndex = 0;
            this.rbPseudo.TabStop = true;
            this.rbPseudo.Text = "Pseudo Code";
            this.rbPseudo.UseVisualStyleBackColor = true;
            this.rbPseudo.CheckedChanged += new System.EventHandler(this.RadioButtonAddressType_CheckedChanged);
            // 
            // rbAddressed
            // 
            this.rbAddressed.AutoSize = true;
            this.rbAddressed.Location = new System.Drawing.Point(182, 3);
            this.rbAddressed.Name = "rbAddressed";
            this.rbAddressed.Size = new System.Drawing.Size(203, 29);
            this.rbAddressed.TabIndex = 1;
            this.rbAddressed.Text = "Addressed Code";
            this.rbAddressed.UseVisualStyleBackColor = true;
            this.rbAddressed.CheckedChanged += new System.EventHandler(this.RadioButtonAddressType_CheckedChanged);
            // 
            // groupBoxSymbolTable
            // 
            this.groupBoxSymbolTable.Controls.Add(this.tablePanelSymbolTable);
            this.groupBoxSymbolTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxSymbolTable.Location = new System.Drawing.Point(3, 593);
            this.groupBoxSymbolTable.Name = "groupBoxSymbolTable";
            this.groupBoxSymbolTable.Size = new System.Drawing.Size(583, 584);
            this.groupBoxSymbolTable.TabIndex = 7;
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
            this.tablePanelSymbolTable.Size = new System.Drawing.Size(577, 554);
            this.tablePanelSymbolTable.TabIndex = 0;
            // 
            // panelSymbolTableInner
            // 
            this.panelSymbolTableInner.Controls.Add(this.listSymbolTable);
            this.panelSymbolTableInner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSymbolTableInner.Location = new System.Drawing.Point(13, 8);
            this.panelSymbolTableInner.Name = "panelSymbolTableInner";
            this.panelSymbolTableInner.Size = new System.Drawing.Size(551, 538);
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
            this.listSymbolTable.Size = new System.Drawing.Size(551, 538);
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
            // tablePanelCodeAndToken
            // 
            this.tablePanelCodeAndToken.ColumnCount = 1;
            this.tablePanelCodeAndToken.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablePanelCodeAndToken.Controls.Add(this.groupBoxTokens, 0, 1);
            this.tablePanelCodeAndToken.Controls.Add(this.groupBoxCode, 0, 0);
            this.tablePanelCodeAndToken.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanelCodeAndToken.Location = new System.Drawing.Point(3, 3);
            this.tablePanelCodeAndToken.Name = "tablePanelCodeAndToken";
            this.tablePanelCodeAndToken.RowCount = 2;
            this.tablePanelCodeAndToken.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tablePanelCodeAndToken.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tablePanelCodeAndToken.Size = new System.Drawing.Size(651, 1180);
            this.tablePanelCodeAndToken.TabIndex = 7;
            // 
            // groupBoxTokens
            // 
            this.groupBoxTokens.Controls.Add(this.tablePanelTokens);
            this.groupBoxTokens.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxTokens.Location = new System.Drawing.Point(3, 593);
            this.groupBoxTokens.Name = "groupBoxTokens";
            this.groupBoxTokens.Size = new System.Drawing.Size(645, 584);
            this.groupBoxTokens.TabIndex = 5;
            this.groupBoxTokens.TabStop = false;
            this.groupBoxTokens.Text = "Tokens";
            // 
            // tablePanelTokens
            // 
            this.tablePanelTokens.ColumnCount = 3;
            this.tablePanelTokens.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tablePanelTokens.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablePanelTokens.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tablePanelTokens.Controls.Add(this.panel1, 1, 1);
            this.tablePanelTokens.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanelTokens.Location = new System.Drawing.Point(3, 27);
            this.tablePanelTokens.Name = "tablePanelTokens";
            this.tablePanelTokens.RowCount = 3;
            this.tablePanelTokens.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tablePanelTokens.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablePanelTokens.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tablePanelTokens.Size = new System.Drawing.Size(639, 554);
            this.tablePanelTokens.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.listTokens);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(13, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(613, 538);
            this.panel1.TabIndex = 0;
            // 
            // listTokens
            // 
            this.listTokens.CausesValidation = false;
            this.listTokens.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.tokenHeaderType,
            this.tokenHeaderValue});
            this.listTokens.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listTokens.FullRowSelect = true;
            this.listTokens.Location = new System.Drawing.Point(0, 0);
            this.listTokens.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.listTokens.MultiSelect = false;
            this.listTokens.Name = "listTokens";
            this.listTokens.Size = new System.Drawing.Size(613, 538);
            this.listTokens.TabIndex = 0;
            this.listTokens.UseCompatibleStateImageBehavior = false;
            this.listTokens.View = System.Windows.Forms.View.Details;
            // 
            // tokenHeaderType
            // 
            this.tokenHeaderType.Text = "Type";
            this.tokenHeaderType.Width = 200;
            // 
            // tokenHeaderValue
            // 
            this.tokenHeaderValue.Text = "Value";
            this.tokenHeaderValue.Width = 350;
            // 
            // groupBoxCode
            // 
            this.groupBoxCode.Controls.Add(this.tablePanelCode);
            this.groupBoxCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxCode.Location = new System.Drawing.Point(3, 3);
            this.groupBoxCode.Name = "groupBoxCode";
            this.groupBoxCode.Size = new System.Drawing.Size(645, 584);
            this.groupBoxCode.TabIndex = 4;
            this.groupBoxCode.TabStop = false;
            this.groupBoxCode.Text = "Code";
            // 
            // tablePanelCode
            // 
            this.tablePanelCode.ColumnCount = 3;
            this.tablePanelCode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tablePanelCode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablePanelCode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tablePanelCode.Controls.Add(this.tablePanelFunctions, 1, 2);
            this.tablePanelCode.Controls.Add(this.panelCodeInner, 1, 1);
            this.tablePanelCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanelCode.Location = new System.Drawing.Point(3, 27);
            this.tablePanelCode.Name = "tablePanelCode";
            this.tablePanelCode.RowCount = 4;
            this.tablePanelCode.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tablePanelCode.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablePanelCode.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 67F));
            this.tablePanelCode.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tablePanelCode.Size = new System.Drawing.Size(639, 554);
            this.tablePanelCode.TabIndex = 0;
            // 
            // tablePanelFunctions
            // 
            this.tablePanelFunctions.ColumnCount = 2;
            this.tablePanelFunctions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tablePanelFunctions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tablePanelFunctions.Controls.Add(this.flowPanelButtons, 1, 0);
            this.tablePanelFunctions.Controls.Add(this.tablePanelParser, 0, 0);
            this.tablePanelFunctions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanelFunctions.Location = new System.Drawing.Point(13, 485);
            this.tablePanelFunctions.Name = "tablePanelFunctions";
            this.tablePanelFunctions.RowCount = 1;
            this.tablePanelFunctions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablePanelFunctions.Size = new System.Drawing.Size(613, 61);
            this.tablePanelFunctions.TabIndex = 2;
            // 
            // flowPanelButtons
            // 
            this.flowPanelButtons.Controls.Add(this.btnAnalyze);
            this.flowPanelButtons.Controls.Add(this.btnOpen);
            this.flowPanelButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowPanelButtons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowPanelButtons.Location = new System.Drawing.Point(278, 3);
            this.flowPanelButtons.Name = "flowPanelButtons";
            this.flowPanelButtons.Size = new System.Drawing.Size(332, 55);
            this.flowPanelButtons.TabIndex = 2;
            // 
            // btnAnalyze
            // 
            this.btnAnalyze.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAnalyze.Location = new System.Drawing.Point(196, 2);
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
            this.btnOpen.Location = new System.Drawing.Point(56, 2);
            this.btnOpen.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(132, 52);
            this.btnOpen.TabIndex = 0;
            this.btnOpen.Text = "Open...";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // tablePanelParser
            // 
            this.tablePanelParser.ColumnCount = 2;
            this.tablePanelParser.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tablePanelParser.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tablePanelParser.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tablePanelParser.Controls.Add(this.rbLR1, 0, 0);
            this.tablePanelParser.Controls.Add(this.rbSLR, 0, 0);
            this.tablePanelParser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanelParser.Location = new System.Drawing.Point(3, 3);
            this.tablePanelParser.Name = "tablePanelParser";
            this.tablePanelParser.RowCount = 1;
            this.tablePanelParser.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablePanelParser.Size = new System.Drawing.Size(269, 55);
            this.tablePanelParser.TabIndex = 3;
            // 
            // rbLR1
            // 
            this.rbLR1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbLR1.Location = new System.Drawing.Point(137, 3);
            this.rbLR1.Name = "rbLR1";
            this.rbLR1.Size = new System.Drawing.Size(129, 49);
            this.rbLR1.TabIndex = 2;
            this.rbLR1.Text = "LR(1)";
            this.rbLR1.UseVisualStyleBackColor = true;
            // 
            // rbSLR
            // 
            this.rbSLR.Checked = true;
            this.rbSLR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbSLR.Location = new System.Drawing.Point(3, 3);
            this.rbSLR.Name = "rbSLR";
            this.rbSLR.Size = new System.Drawing.Size(128, 49);
            this.rbSLR.TabIndex = 0;
            this.rbSLR.TabStop = true;
            this.rbSLR.Text = "SLR";
            this.rbSLR.UseVisualStyleBackColor = true;
            // 
            // panelCodeInner
            // 
            this.panelCodeInner.Controls.Add(this.textCode);
            this.panelCodeInner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCodeInner.Location = new System.Drawing.Point(13, 8);
            this.panelCodeInner.Name = "panelCodeInner";
            this.panelCodeInner.Size = new System.Drawing.Size(613, 471);
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
            this.textCode.Size = new System.Drawing.Size(613, 471);
            this.textCode.TabIndex = 1;
            this.textCode.Text = "int a;\r\nint b;\r\nint c;\r\n\r\na = 2;\r\nb = 1;\r\n\r\nif (a<b)\r\n\tc=a+b;\r\nelse\r\n\tc=a-b;";
            this.textCode.UseTabs = false;
            // 
            // FrontEndForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2486, 1207);
            this.Controls.Add(this.tableLayout);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrontEndForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Shindo\'s Compiler Front End";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Shown += new System.EventHandler(this.FrontEndForm_Shown);
            this.tableLayout.ResumeLayout(false);
            this.tablePanelControls.ResumeLayout(false);
            this.groupBoxParseStep.ResumeLayout(false);
            this.tablePanelParse.ResumeLayout(false);
            this.tablePanelThreeAndSymbol.ResumeLayout(false);
            this.groupBoxThreeAddrCode.ResumeLayout(false);
            this.tablePanelThreeAddrCode.ResumeLayout(false);
            this.panelTokensInner.ResumeLayout(false);
            this.flowPanelOptions.ResumeLayout(false);
            this.flowPanelOptions.PerformLayout();
            this.groupBoxSymbolTable.ResumeLayout(false);
            this.tablePanelSymbolTable.ResumeLayout(false);
            this.panelSymbolTableInner.ResumeLayout(false);
            this.tablePanelCodeAndToken.ResumeLayout(false);
            this.groupBoxTokens.ResumeLayout(false);
            this.tablePanelTokens.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBoxCode.ResumeLayout(false);
            this.tablePanelCode.ResumeLayout(false);
            this.tablePanelFunctions.ResumeLayout(false);
            this.flowPanelButtons.ResumeLayout(false);
            this.tablePanelParser.ResumeLayout(false);
            this.panelCodeInner.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayout;
        private System.Windows.Forms.TableLayoutPanel tablePanelControls;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TableLayoutPanel tablePanelThreeAndSymbol;
        private System.Windows.Forms.GroupBox groupBoxSymbolTable;
        private System.Windows.Forms.TableLayoutPanel tablePanelSymbolTable;
        private System.Windows.Forms.Panel panelSymbolTableInner;
        private Utility.WindowThemeListView listSymbolTable;
        private System.Windows.Forms.ColumnHeader symbolHeaderId;
        private System.Windows.Forms.ColumnHeader symbolHeaderSymbol;
        private System.Windows.Forms.ColumnHeader symbolHeaderType;
        private System.Windows.Forms.ColumnHeader symbolTypeOffset;
        private System.Windows.Forms.GroupBox groupBoxThreeAddrCode;
        private System.Windows.Forms.TableLayoutPanel tablePanelThreeAddrCode;
        private System.Windows.Forms.Panel panelTokensInner;
        private Utility.WindowThemeListView listInterCode;
        private System.Windows.Forms.ColumnHeader interCodeHeaderLabel;
        private System.Windows.Forms.ColumnHeader interCodeHeaderCode;
        private System.Windows.Forms.FlowLayoutPanel flowPanelOptions;
        private System.Windows.Forms.RadioButton rbPseudo;
        private System.Windows.Forms.RadioButton rbAddressed;
        private System.Windows.Forms.GroupBox groupBoxParseStep;
        private System.Windows.Forms.TableLayoutPanel tablePanelParse;
        private System.Windows.Forms.TableLayoutPanel tablePanelCodeAndToken;
        private System.Windows.Forms.GroupBox groupBoxCode;
        private System.Windows.Forms.TableLayoutPanel tablePanelCode;
        private System.Windows.Forms.TableLayoutPanel tablePanelFunctions;
        private System.Windows.Forms.FlowLayoutPanel flowPanelButtons;
        private System.Windows.Forms.Button btnAnalyze;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.TableLayoutPanel tablePanelParser;
        private System.Windows.Forms.RadioButton rbLR1;
        private System.Windows.Forms.RadioButton rbSLR;
        private System.Windows.Forms.Panel panelCodeInner;
        private Utility.WindowThemeListView listParse;
        private System.Windows.Forms.ColumnHeader parseHeaderStack;
        private System.Windows.Forms.ColumnHeader parseHeaderSymbol;
        private System.Windows.Forms.ColumnHeader parseHeaderAction;
        private System.Windows.Forms.GroupBox groupBoxTokens;
        private System.Windows.Forms.TableLayoutPanel tablePanelTokens;
        private System.Windows.Forms.Panel panel1;
        private Utility.WindowThemeListView listTokens;
        private System.Windows.Forms.ColumnHeader tokenHeaderType;
        private System.Windows.Forms.ColumnHeader tokenHeaderValue;
        private ScintillaNET.Scintilla textCode;
    }
}

