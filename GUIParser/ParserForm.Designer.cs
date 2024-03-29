﻿namespace CompilingPrinciples.GUIParser
{
    partial class ParserForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ParserForm));
            this.tableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.tablePanelControls = new System.Windows.Forms.TableLayoutPanel();
            this.panelParse = new System.Windows.Forms.Panel();
            this.groupBoxThreeAddrCode = new System.Windows.Forms.GroupBox();
            this.tablePanelParse = new System.Windows.Forms.TableLayoutPanel();
            this.panelTokensInner = new System.Windows.Forms.Panel();
            this.panelSymbolTable = new System.Windows.Forms.Panel();
            this.groupBoxSymbolTable = new System.Windows.Forms.GroupBox();
            this.tablePanelSymbolTable = new System.Windows.Forms.TableLayoutPanel();
            this.panelSymbolTableInner = new System.Windows.Forms.Panel();
            this.panelCode = new System.Windows.Forms.Panel();
            this.groupBoxCode = new System.Windows.Forms.GroupBox();
            this.tablePanelCode = new System.Windows.Forms.TableLayoutPanel();
            this.panelCodeInner = new System.Windows.Forms.Panel();
            this.textCode = new ScintillaNET.Scintilla();
            this.tablePanelFunctions = new System.Windows.Forms.TableLayoutPanel();
            this.flowPanelButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAnalyze = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.tablePanelParser = new System.Windows.Forms.TableLayoutPanel();
            this.rbLR1 = new System.Windows.Forms.RadioButton();
            this.rbCustom = new System.Windows.Forms.RadioButton();
            this.rbSLR = new System.Windows.Forms.RadioButton();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.openCtxDialog = new System.Windows.Forms.OpenFileDialog();
            this.listParse = new CompilingPrinciples.Utility.WindowThemeListView();
            this.parseHeaderStack = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.parseHeaderSymbol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.parseHeaderAction = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listSymbolTable = new CompilingPrinciples.Utility.WindowThemeListView();
            this.symbolHeaderId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.symbolHeaderSymbol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tableLayout.SuspendLayout();
            this.tablePanelControls.SuspendLayout();
            this.panelParse.SuspendLayout();
            this.groupBoxThreeAddrCode.SuspendLayout();
            this.tablePanelParse.SuspendLayout();
            this.panelTokensInner.SuspendLayout();
            this.panelSymbolTable.SuspendLayout();
            this.groupBoxSymbolTable.SuspendLayout();
            this.tablePanelSymbolTable.SuspendLayout();
            this.panelSymbolTableInner.SuspendLayout();
            this.panelCode.SuspendLayout();
            this.groupBoxCode.SuspendLayout();
            this.tablePanelCode.SuspendLayout();
            this.panelCodeInner.SuspendLayout();
            this.tablePanelFunctions.SuspendLayout();
            this.flowPanelButtons.SuspendLayout();
            this.tablePanelParser.SuspendLayout();
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
            this.tableLayout.Size = new System.Drawing.Size(2255, 1110);
            this.tableLayout.TabIndex = 8;
            // 
            // tablePanelControls
            // 
            this.tablePanelControls.ColumnCount = 5;
            this.tablePanelControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 665F));
            this.tablePanelControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tablePanelControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablePanelControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tablePanelControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 365F));
            this.tablePanelControls.Controls.Add(this.panelParse, 2, 0);
            this.tablePanelControls.Controls.Add(this.panelSymbolTable, 4, 0);
            this.tablePanelControls.Controls.Add(this.panelCode, 0, 0);
            this.tablePanelControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanelControls.Location = new System.Drawing.Point(23, 13);
            this.tablePanelControls.Name = "tablePanelControls";
            this.tablePanelControls.RowCount = 1;
            this.tablePanelControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablePanelControls.Size = new System.Drawing.Size(2209, 1089);
            this.tablePanelControls.TabIndex = 5;
            // 
            // panelParse
            // 
            this.panelParse.Controls.Add(this.groupBoxThreeAddrCode);
            this.panelParse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelParse.Location = new System.Drawing.Point(678, 3);
            this.panelParse.Name = "panelParse";
            this.panelParse.Size = new System.Drawing.Size(1153, 1083);
            this.panelParse.TabIndex = 2;
            // 
            // groupBoxThreeAddrCode
            // 
            this.groupBoxThreeAddrCode.Controls.Add(this.tablePanelParse);
            this.groupBoxThreeAddrCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxThreeAddrCode.Location = new System.Drawing.Point(0, 0);
            this.groupBoxThreeAddrCode.Name = "groupBoxThreeAddrCode";
            this.groupBoxThreeAddrCode.Size = new System.Drawing.Size(1153, 1083);
            this.groupBoxThreeAddrCode.TabIndex = 0;
            this.groupBoxThreeAddrCode.TabStop = false;
            this.groupBoxThreeAddrCode.Text = "Parse Step";
            // 
            // tablePanelParse
            // 
            this.tablePanelParse.ColumnCount = 3;
            this.tablePanelParse.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tablePanelParse.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablePanelParse.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tablePanelParse.Controls.Add(this.panelTokensInner, 1, 1);
            this.tablePanelParse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanelParse.Location = new System.Drawing.Point(3, 27);
            this.tablePanelParse.Name = "tablePanelParse";
            this.tablePanelParse.RowCount = 3;
            this.tablePanelParse.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tablePanelParse.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablePanelParse.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tablePanelParse.Size = new System.Drawing.Size(1147, 1053);
            this.tablePanelParse.TabIndex = 0;
            // 
            // panelTokensInner
            // 
            this.panelTokensInner.Controls.Add(this.listParse);
            this.panelTokensInner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTokensInner.Location = new System.Drawing.Point(13, 8);
            this.panelTokensInner.Name = "panelTokensInner";
            this.panelTokensInner.Size = new System.Drawing.Size(1121, 1037);
            this.panelTokensInner.TabIndex = 0;
            // 
            // panelSymbolTable
            // 
            this.panelSymbolTable.Controls.Add(this.groupBoxSymbolTable);
            this.panelSymbolTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSymbolTable.Location = new System.Drawing.Point(1847, 3);
            this.panelSymbolTable.Name = "panelSymbolTable";
            this.panelSymbolTable.Size = new System.Drawing.Size(359, 1083);
            this.panelSymbolTable.TabIndex = 3;
            // 
            // groupBoxSymbolTable
            // 
            this.groupBoxSymbolTable.Controls.Add(this.tablePanelSymbolTable);
            this.groupBoxSymbolTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxSymbolTable.Location = new System.Drawing.Point(0, 0);
            this.groupBoxSymbolTable.Name = "groupBoxSymbolTable";
            this.groupBoxSymbolTable.Size = new System.Drawing.Size(359, 1083);
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
            this.tablePanelSymbolTable.Size = new System.Drawing.Size(353, 1053);
            this.tablePanelSymbolTable.TabIndex = 0;
            // 
            // panelSymbolTableInner
            // 
            this.panelSymbolTableInner.Controls.Add(this.listSymbolTable);
            this.panelSymbolTableInner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSymbolTableInner.Location = new System.Drawing.Point(13, 8);
            this.panelSymbolTableInner.Name = "panelSymbolTableInner";
            this.panelSymbolTableInner.Size = new System.Drawing.Size(327, 1037);
            this.panelSymbolTableInner.TabIndex = 0;
            // 
            // panelCode
            // 
            this.panelCode.Controls.Add(this.groupBoxCode);
            this.panelCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCode.Location = new System.Drawing.Point(3, 3);
            this.panelCode.Name = "panelCode";
            this.panelCode.Size = new System.Drawing.Size(659, 1083);
            this.panelCode.TabIndex = 4;
            // 
            // groupBoxCode
            // 
            this.groupBoxCode.Controls.Add(this.tablePanelCode);
            this.groupBoxCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxCode.Location = new System.Drawing.Point(0, 0);
            this.groupBoxCode.Name = "groupBoxCode";
            this.groupBoxCode.Size = new System.Drawing.Size(659, 1083);
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
            this.tablePanelCode.Controls.Add(this.panelCodeInner, 1, 1);
            this.tablePanelCode.Controls.Add(this.tablePanelFunctions, 1, 2);
            this.tablePanelCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanelCode.Location = new System.Drawing.Point(3, 27);
            this.tablePanelCode.Name = "tablePanelCode";
            this.tablePanelCode.RowCount = 4;
            this.tablePanelCode.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tablePanelCode.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablePanelCode.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 67F));
            this.tablePanelCode.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tablePanelCode.Size = new System.Drawing.Size(653, 1053);
            this.tablePanelCode.TabIndex = 0;
            // 
            // panelCodeInner
            // 
            this.panelCodeInner.Controls.Add(this.textCode);
            this.panelCodeInner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCodeInner.Location = new System.Drawing.Point(13, 8);
            this.panelCodeInner.Name = "panelCodeInner";
            this.panelCodeInner.Size = new System.Drawing.Size(627, 970);
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
            this.textCode.Size = new System.Drawing.Size(627, 970);
            this.textCode.TabIndex = 0;
            this.textCode.Text = "int a;\r\nint b;\r\nint c;\r\n\r\na = 2;\r\nb = 1;\r\n\r\nif (a>b)\r\n\tc=a+b;\r\nelse\r\n\tc=a-b;";
            this.textCode.UseTabs = false;
            // 
            // tablePanelFunctions
            // 
            this.tablePanelFunctions.ColumnCount = 2;
            this.tablePanelFunctions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52.38095F));
            this.tablePanelFunctions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.61905F));
            this.tablePanelFunctions.Controls.Add(this.flowPanelButtons, 1, 0);
            this.tablePanelFunctions.Controls.Add(this.tablePanelParser, 0, 0);
            this.tablePanelFunctions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanelFunctions.Location = new System.Drawing.Point(13, 984);
            this.tablePanelFunctions.Name = "tablePanelFunctions";
            this.tablePanelFunctions.RowCount = 1;
            this.tablePanelFunctions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablePanelFunctions.Size = new System.Drawing.Size(627, 61);
            this.tablePanelFunctions.TabIndex = 1;
            // 
            // flowPanelButtons
            // 
            this.flowPanelButtons.Controls.Add(this.btnAnalyze);
            this.flowPanelButtons.Controls.Add(this.btnOpen);
            this.flowPanelButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowPanelButtons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowPanelButtons.Location = new System.Drawing.Point(331, 3);
            this.flowPanelButtons.Name = "flowPanelButtons";
            this.flowPanelButtons.Size = new System.Drawing.Size(293, 55);
            this.flowPanelButtons.TabIndex = 2;
            // 
            // btnAnalyze
            // 
            this.btnAnalyze.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAnalyze.Location = new System.Drawing.Point(157, 2);
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
            this.btnOpen.Location = new System.Drawing.Point(17, 2);
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
            this.tablePanelParser.ColumnCount = 3;
            this.tablePanelParser.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tablePanelParser.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tablePanelParser.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tablePanelParser.Controls.Add(this.rbLR1, 0, 0);
            this.tablePanelParser.Controls.Add(this.rbCustom, 1, 0);
            this.tablePanelParser.Controls.Add(this.rbSLR, 0, 0);
            this.tablePanelParser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanelParser.Location = new System.Drawing.Point(3, 3);
            this.tablePanelParser.Name = "tablePanelParser";
            this.tablePanelParser.RowCount = 1;
            this.tablePanelParser.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablePanelParser.Size = new System.Drawing.Size(322, 55);
            this.tablePanelParser.TabIndex = 3;
            // 
            // rbLR1
            // 
            this.rbLR1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbLR1.Location = new System.Drawing.Point(110, 3);
            this.rbLR1.Name = "rbLR1";
            this.rbLR1.Size = new System.Drawing.Size(101, 49);
            this.rbLR1.TabIndex = 2;
            this.rbLR1.Text = "LR(1)";
            this.rbLR1.UseVisualStyleBackColor = true;
            // 
            // rbCustom
            // 
            this.rbCustom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbCustom.Location = new System.Drawing.Point(217, 3);
            this.rbCustom.Name = "rbCustom";
            this.rbCustom.Size = new System.Drawing.Size(102, 49);
            this.rbCustom.TabIndex = 1;
            this.rbCustom.Text = "Cus.";
            this.rbCustom.UseVisualStyleBackColor = true;
            this.rbCustom.CheckedChanged += new System.EventHandler(this.rbCustom_CheckedChanged);
            // 
            // rbSLR
            // 
            this.rbSLR.Checked = true;
            this.rbSLR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbSLR.Location = new System.Drawing.Point(3, 3);
            this.rbSLR.Name = "rbSLR";
            this.rbSLR.Size = new System.Drawing.Size(101, 49);
            this.rbSLR.TabIndex = 0;
            this.rbSLR.TabStop = true;
            this.rbSLR.Text = "SLR";
            this.rbSLR.UseVisualStyleBackColor = true;
            // 
            // openCtxDialog
            // 
            this.openCtxDialog.Filter = "Shindo\'s Parser Context|*.ctx";
            this.openCtxDialog.Title = "Open context...";
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
            this.listParse.Location = new System.Drawing.Point(0, 0);
            this.listParse.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.listParse.MultiSelect = false;
            this.listParse.Name = "listParse";
            this.listParse.Size = new System.Drawing.Size(1121, 1037);
            this.listParse.TabIndex = 0;
            this.listParse.UseCompatibleStateImageBehavior = false;
            this.listParse.View = System.Windows.Forms.View.Details;
            // 
            // parseHeaderStack
            // 
            this.parseHeaderStack.Text = "Stack";
            this.parseHeaderStack.Width = 375;
            // 
            // parseHeaderSymbol
            // 
            this.parseHeaderSymbol.Text = "Symbols";
            this.parseHeaderSymbol.Width = 305;
            // 
            // parseHeaderAction
            // 
            this.parseHeaderAction.Text = "Action";
            this.parseHeaderAction.Width = 400;
            // 
            // listSymbolTable
            // 
            this.listSymbolTable.CausesValidation = false;
            this.listSymbolTable.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.symbolHeaderId,
            this.symbolHeaderSymbol});
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
            this.listSymbolTable.Size = new System.Drawing.Size(327, 1037);
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
            this.symbolHeaderSymbol.Width = 220;
            // 
            // ParserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2255, 1110);
            this.Controls.Add(this.tableLayout);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ParserForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Shindo\'s Syntax Analyzer";
            this.Shown += new System.EventHandler(this.ParserForm_Shown);
            this.tableLayout.ResumeLayout(false);
            this.tablePanelControls.ResumeLayout(false);
            this.panelParse.ResumeLayout(false);
            this.groupBoxThreeAddrCode.ResumeLayout(false);
            this.tablePanelParse.ResumeLayout(false);
            this.panelTokensInner.ResumeLayout(false);
            this.panelSymbolTable.ResumeLayout(false);
            this.groupBoxSymbolTable.ResumeLayout(false);
            this.tablePanelSymbolTable.ResumeLayout(false);
            this.panelSymbolTableInner.ResumeLayout(false);
            this.panelCode.ResumeLayout(false);
            this.groupBoxCode.ResumeLayout(false);
            this.tablePanelCode.ResumeLayout(false);
            this.panelCodeInner.ResumeLayout(false);
            this.tablePanelFunctions.ResumeLayout(false);
            this.flowPanelButtons.ResumeLayout(false);
            this.tablePanelParser.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayout;
        private System.Windows.Forms.TableLayoutPanel tablePanelControls;
        private System.Windows.Forms.Panel panelParse;
        private System.Windows.Forms.GroupBox groupBoxThreeAddrCode;
        private System.Windows.Forms.TableLayoutPanel tablePanelParse;
        private System.Windows.Forms.Panel panelTokensInner;
        private CompilingPrinciples.Utility.WindowThemeListView listParse;
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
        private System.Windows.Forms.ColumnHeader parseHeaderStack;
        private System.Windows.Forms.ColumnHeader parseHeaderSymbol;
        private System.Windows.Forms.ColumnHeader parseHeaderAction;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TableLayoutPanel tablePanelFunctions;
        private System.Windows.Forms.FlowLayoutPanel flowPanelButtons;
        private System.Windows.Forms.Button btnAnalyze;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.TableLayoutPanel tablePanelParser;
        private System.Windows.Forms.RadioButton rbSLR;
        private System.Windows.Forms.RadioButton rbCustom;
        private System.Windows.Forms.RadioButton rbLR1;
        private System.Windows.Forms.OpenFileDialog openCtxDialog;
    }
}