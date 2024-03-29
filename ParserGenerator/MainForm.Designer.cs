﻿namespace CompilingPrinciples.ParserGenerator
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.tablePanelControls = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxGrammar = new System.Windows.Forms.GroupBox();
            this.tablePanelGrammar = new System.Windows.Forms.TableLayoutPanel();
            this.textGrammar = new ScintillaNET.Scintilla();
            this.tablePanelFunctions = new System.Windows.Forms.TableLayoutPanel();
            this.flowPanelButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnAnalyze = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.tablePanelParserType = new System.Windows.Forms.TableLayoutPanel();
            this.rbLR1 = new System.Windows.Forms.RadioButton();
            this.rbSLR = new System.Windows.Forms.RadioButton();
            this.lblGrammarNote = new System.Windows.Forms.Label();
            this.groupBoxSymbols = new System.Windows.Forms.GroupBox();
            this.tablePanelSymbols = new System.Windows.Forms.TableLayoutPanel();
            this.tabSymbol = new System.Windows.Forms.TabControl();
            this.tabPageNonTerminals = new System.Windows.Forms.TabPage();
            this.listNonTerminals = new CompilingPrinciples.Utility.WindowThemeListView();
            this.nonTerminalsHeaderId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.nonTerminalsHeaderSymbol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPageTerminals = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.listTerminals = new CompilingPrinciples.Utility.WindowThemeListView();
            this.terminalsHeaderId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.terminalsHeaderSymbol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxFirstFollow = new System.Windows.Forms.GroupBox();
            this.tablePanelFirstFollow = new System.Windows.Forms.TableLayoutPanel();
            this.tabFirstFollow = new System.Windows.Forms.TabControl();
            this.tabPageFirst = new System.Windows.Forms.TabPage();
            this.listFirst = new CompilingPrinciples.Utility.WindowThemeListView();
            this.firstHeaderSymbol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.firstHeaderElement = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPageFollow = new System.Windows.Forms.TabPage();
            this.listFollow = new CompilingPrinciples.Utility.WindowThemeListView();
            this.followHeaderSymbol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.followHeaderElements = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.flowPanelGrammarType = new System.Windows.Forms.FlowLayoutPanel();
            this.lblLLGrammarText = new System.Windows.Forms.Label();
            this.lblIsLLGrammar = new System.Windows.Forms.Label();
            this.lblLRGrammarText = new System.Windows.Forms.Label();
            this.lblIsLRGrammar = new System.Windows.Forms.Label();
            this.groupBoxParseTable = new System.Windows.Forms.GroupBox();
            this.tablePanelParsingTable = new System.Windows.Forms.TableLayoutPanel();
            this.tabParsingTable = new System.Windows.Forms.TabControl();
            this.tabPageStates = new System.Windows.Forms.TabPage();
            this.gridStates = new CompilingPrinciples.Utility.DoubleBufferDataGridView();
            this.ColumnId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnItems = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPageActionTable = new System.Windows.Forms.TabPage();
            this.gridAction = new CompilingPrinciples.Utility.DoubleBufferDataGridView();
            this.tabPageGotoAction = new System.Windows.Forms.TabPage();
            this.gridGoto = new CompilingPrinciples.Utility.DoubleBufferDataGridView();
            this.stateContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.actionContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.solveSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.tableLayout.SuspendLayout();
            this.tablePanelControls.SuspendLayout();
            this.groupBoxGrammar.SuspendLayout();
            this.tablePanelGrammar.SuspendLayout();
            this.tablePanelFunctions.SuspendLayout();
            this.flowPanelButtons.SuspendLayout();
            this.tablePanelParserType.SuspendLayout();
            this.groupBoxSymbols.SuspendLayout();
            this.tablePanelSymbols.SuspendLayout();
            this.tabSymbol.SuspendLayout();
            this.tabPageNonTerminals.SuspendLayout();
            this.tabPageTerminals.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBoxFirstFollow.SuspendLayout();
            this.tablePanelFirstFollow.SuspendLayout();
            this.tabFirstFollow.SuspendLayout();
            this.tabPageFirst.SuspendLayout();
            this.tabPageFollow.SuspendLayout();
            this.flowPanelGrammarType.SuspendLayout();
            this.groupBoxParseTable.SuspendLayout();
            this.tablePanelParsingTable.SuspendLayout();
            this.tabParsingTable.SuspendLayout();
            this.tabPageStates.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridStates)).BeginInit();
            this.tabPageActionTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridAction)).BeginInit();
            this.tabPageGotoAction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridGoto)).BeginInit();
            this.stateContextMenu.SuspendLayout();
            this.actionContextMenu.SuspendLayout();
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
            this.tableLayout.Size = new System.Drawing.Size(2352, 1114);
            this.tableLayout.TabIndex = 0;
            // 
            // tablePanelControls
            // 
            this.tablePanelControls.ColumnCount = 5;
            this.tablePanelControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 800F));
            this.tablePanelControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tablePanelControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tablePanelControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tablePanelControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tablePanelControls.Controls.Add(this.groupBoxGrammar, 0, 0);
            this.tablePanelControls.Controls.Add(this.groupBoxSymbols, 2, 0);
            this.tablePanelControls.Controls.Add(this.groupBoxFirstFollow, 4, 0);
            this.tablePanelControls.Controls.Add(this.groupBoxParseTable, 2, 1);
            this.tablePanelControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanelControls.Location = new System.Drawing.Point(23, 13);
            this.tablePanelControls.Name = "tablePanelControls";
            this.tablePanelControls.RowCount = 2;
            this.tablePanelControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tablePanelControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tablePanelControls.Size = new System.Drawing.Size(2306, 1093);
            this.tablePanelControls.TabIndex = 0;
            // 
            // groupBoxGrammar
            // 
            this.groupBoxGrammar.Controls.Add(this.tablePanelGrammar);
            this.groupBoxGrammar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxGrammar.Location = new System.Drawing.Point(3, 3);
            this.groupBoxGrammar.Name = "groupBoxGrammar";
            this.tablePanelControls.SetRowSpan(this.groupBoxGrammar, 2);
            this.groupBoxGrammar.Size = new System.Drawing.Size(794, 1087);
            this.groupBoxGrammar.TabIndex = 0;
            this.groupBoxGrammar.TabStop = false;
            this.groupBoxGrammar.Text = "Grammar";
            // 
            // tablePanelGrammar
            // 
            this.tablePanelGrammar.ColumnCount = 3;
            this.tablePanelGrammar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tablePanelGrammar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablePanelGrammar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tablePanelGrammar.Controls.Add(this.textGrammar, 1, 2);
            this.tablePanelGrammar.Controls.Add(this.tablePanelFunctions, 1, 3);
            this.tablePanelGrammar.Controls.Add(this.lblGrammarNote, 1, 1);
            this.tablePanelGrammar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanelGrammar.Location = new System.Drawing.Point(3, 27);
            this.tablePanelGrammar.Name = "tablePanelGrammar";
            this.tablePanelGrammar.RowCount = 5;
            this.tablePanelGrammar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tablePanelGrammar.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tablePanelGrammar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablePanelGrammar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 67F));
            this.tablePanelGrammar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tablePanelGrammar.Size = new System.Drawing.Size(788, 1057);
            this.tablePanelGrammar.TabIndex = 1;
            // 
            // textGrammar
            // 
            this.textGrammar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textGrammar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textGrammar.Location = new System.Drawing.Point(14, 82);
            this.textGrammar.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.textGrammar.Name = "textGrammar";
            this.textGrammar.ScrollWidth = 241;
            this.textGrammar.Size = new System.Drawing.Size(760, 901);
            this.textGrammar.TabIndex = 0;
            this.textGrammar.Text = resources.GetString("textGrammar.Text");
            this.textGrammar.UseTabs = false;
            // 
            // tablePanelFunctions
            // 
            this.tablePanelFunctions.ColumnCount = 2;
            this.tablePanelFunctions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tablePanelFunctions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tablePanelFunctions.Controls.Add(this.flowPanelButtons, 1, 0);
            this.tablePanelFunctions.Controls.Add(this.tablePanelParserType, 0, 0);
            this.tablePanelFunctions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanelFunctions.Location = new System.Drawing.Point(13, 988);
            this.tablePanelFunctions.Name = "tablePanelFunctions";
            this.tablePanelFunctions.RowCount = 1;
            this.tablePanelFunctions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablePanelFunctions.Size = new System.Drawing.Size(762, 61);
            this.tablePanelFunctions.TabIndex = 3;
            // 
            // flowPanelButtons
            // 
            this.flowPanelButtons.Controls.Add(this.btnGenerate);
            this.flowPanelButtons.Controls.Add(this.btnAnalyze);
            this.flowPanelButtons.Controls.Add(this.btnOpen);
            this.flowPanelButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowPanelButtons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowPanelButtons.Location = new System.Drawing.Point(307, 3);
            this.flowPanelButtons.Name = "flowPanelButtons";
            this.flowPanelButtons.Size = new System.Drawing.Size(452, 55);
            this.flowPanelButtons.TabIndex = 2;
            // 
            // btnGenerate
            // 
            this.btnGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerate.Enabled = false;
            this.btnGenerate.Location = new System.Drawing.Point(316, 2);
            this.btnGenerate.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(132, 52);
            this.btnGenerate.TabIndex = 2;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnAnalyze
            // 
            this.btnAnalyze.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAnalyze.Location = new System.Drawing.Point(176, 2);
            this.btnAnalyze.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnAnalyze.Name = "btnAnalyze";
            this.btnAnalyze.Size = new System.Drawing.Size(132, 52);
            this.btnAnalyze.TabIndex = 1;
            this.btnAnalyze.Text = "Analyze";
            this.btnAnalyze.UseVisualStyleBackColor = true;
            this.btnAnalyze.Click += new System.EventHandler(this.btnAnalyse_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpen.Location = new System.Drawing.Point(36, 2);
            this.btnOpen.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(132, 52);
            this.btnOpen.TabIndex = 0;
            this.btnOpen.Text = "Open...";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // tablePanelParserType
            // 
            this.tablePanelParserType.ColumnCount = 2;
            this.tablePanelParserType.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tablePanelParserType.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tablePanelParserType.Controls.Add(this.rbLR1, 1, 0);
            this.tablePanelParserType.Controls.Add(this.rbSLR, 0, 0);
            this.tablePanelParserType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanelParserType.Location = new System.Drawing.Point(3, 3);
            this.tablePanelParserType.Name = "tablePanelParserType";
            this.tablePanelParserType.RowCount = 1;
            this.tablePanelParserType.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tablePanelParserType.Size = new System.Drawing.Size(298, 55);
            this.tablePanelParserType.TabIndex = 3;
            // 
            // rbLR1
            // 
            this.rbLR1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbLR1.Location = new System.Drawing.Point(152, 3);
            this.rbLR1.Name = "rbLR1";
            this.rbLR1.Size = new System.Drawing.Size(143, 49);
            this.rbLR1.TabIndex = 1;
            this.rbLR1.Text = "LR(1)";
            this.rbLR1.UseVisualStyleBackColor = true;
            // 
            // rbSLR
            // 
            this.rbSLR.Checked = true;
            this.rbSLR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbSLR.Location = new System.Drawing.Point(3, 3);
            this.rbSLR.Name = "rbSLR";
            this.rbSLR.Size = new System.Drawing.Size(143, 49);
            this.rbSLR.TabIndex = 0;
            this.rbSLR.TabStop = true;
            this.rbSLR.Text = "SLR";
            this.rbSLR.UseVisualStyleBackColor = true;
            // 
            // lblGrammarNote
            // 
            this.lblGrammarNote.AutoSize = true;
            this.lblGrammarNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGrammarNote.Location = new System.Drawing.Point(13, 5);
            this.lblGrammarNote.Name = "lblGrammarNote";
            this.lblGrammarNote.Size = new System.Drawing.Size(760, 75);
            this.lblGrammarNote.TabIndex = 4;
            this.lblGrammarNote.Text = "Grammar should be augmented, use @ to represent ε.\r\nSymbols in the body of produc" +
    "tion should be separated by whitespace.\r\nFor productions like A -> α | β, separa" +
    "te them into multiple productions.";
            // 
            // groupBoxSymbols
            // 
            this.groupBoxSymbols.Controls.Add(this.tablePanelSymbols);
            this.groupBoxSymbols.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxSymbols.Location = new System.Drawing.Point(813, 3);
            this.groupBoxSymbols.Name = "groupBoxSymbols";
            this.groupBoxSymbols.Size = new System.Drawing.Size(588, 431);
            this.groupBoxSymbols.TabIndex = 1;
            this.groupBoxSymbols.TabStop = false;
            this.groupBoxSymbols.Text = "Symbols";
            // 
            // tablePanelSymbols
            // 
            this.tablePanelSymbols.ColumnCount = 3;
            this.tablePanelSymbols.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tablePanelSymbols.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablePanelSymbols.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tablePanelSymbols.Controls.Add(this.tabSymbol, 1, 1);
            this.tablePanelSymbols.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanelSymbols.Location = new System.Drawing.Point(3, 27);
            this.tablePanelSymbols.Name = "tablePanelSymbols";
            this.tablePanelSymbols.RowCount = 3;
            this.tablePanelSymbols.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tablePanelSymbols.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablePanelSymbols.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tablePanelSymbols.Size = new System.Drawing.Size(582, 401);
            this.tablePanelSymbols.TabIndex = 1;
            // 
            // tabSymbol
            // 
            this.tabSymbol.Controls.Add(this.tabPageNonTerminals);
            this.tabSymbol.Controls.Add(this.tabPageTerminals);
            this.tabSymbol.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabSymbol.Location = new System.Drawing.Point(8, 8);
            this.tabSymbol.Name = "tabSymbol";
            this.tabSymbol.SelectedIndex = 0;
            this.tabSymbol.Size = new System.Drawing.Size(566, 385);
            this.tabSymbol.TabIndex = 0;
            // 
            // tabPageNonTerminals
            // 
            this.tabPageNonTerminals.Controls.Add(this.listNonTerminals);
            this.tabPageNonTerminals.Location = new System.Drawing.Point(8, 39);
            this.tabPageNonTerminals.Name = "tabPageNonTerminals";
            this.tabPageNonTerminals.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageNonTerminals.Size = new System.Drawing.Size(550, 338);
            this.tabPageNonTerminals.TabIndex = 0;
            this.tabPageNonTerminals.Text = "Non Terminals";
            this.tabPageNonTerminals.UseVisualStyleBackColor = true;
            // 
            // listNonTerminals
            // 
            this.listNonTerminals.CausesValidation = false;
            this.listNonTerminals.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nonTerminalsHeaderId,
            this.nonTerminalsHeaderSymbol});
            this.listNonTerminals.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listNonTerminals.FullRowSelect = true;
            this.listNonTerminals.Location = new System.Drawing.Point(3, 3);
            this.listNonTerminals.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.listNonTerminals.MultiSelect = false;
            this.listNonTerminals.Name = "listNonTerminals";
            this.listNonTerminals.Size = new System.Drawing.Size(544, 332);
            this.listNonTerminals.TabIndex = 0;
            this.listNonTerminals.UseCompatibleStateImageBehavior = false;
            this.listNonTerminals.View = System.Windows.Forms.View.Details;
            // 
            // nonTerminalsHeaderId
            // 
            this.nonTerminalsHeaderId.Text = "Id";
            this.nonTerminalsHeaderId.Width = 65;
            // 
            // nonTerminalsHeaderSymbol
            // 
            this.nonTerminalsHeaderSymbol.Text = "Symbol";
            this.nonTerminalsHeaderSymbol.Width = 220;
            // 
            // tabPageTerminals
            // 
            this.tabPageTerminals.Controls.Add(this.tableLayoutPanel1);
            this.tabPageTerminals.Location = new System.Drawing.Point(8, 39);
            this.tabPageTerminals.Name = "tabPageTerminals";
            this.tabPageTerminals.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTerminals.Size = new System.Drawing.Size(550, 338);
            this.tabPageTerminals.TabIndex = 1;
            this.tabPageTerminals.Text = "Terminals";
            this.tabPageTerminals.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.listTerminals, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(544, 332);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // listTerminals
            // 
            this.listTerminals.CausesValidation = false;
            this.listTerminals.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.terminalsHeaderId,
            this.terminalsHeaderSymbol});
            this.listTerminals.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listTerminals.FullRowSelect = true;
            this.listTerminals.Location = new System.Drawing.Point(4, 2);
            this.listTerminals.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.listTerminals.MultiSelect = false;
            this.listTerminals.Name = "listTerminals";
            this.listTerminals.Size = new System.Drawing.Size(536, 303);
            this.listTerminals.TabIndex = 3;
            this.listTerminals.UseCompatibleStateImageBehavior = false;
            this.listTerminals.View = System.Windows.Forms.View.Details;
            // 
            // terminalsHeaderId
            // 
            this.terminalsHeaderId.Text = "Id";
            this.terminalsHeaderId.Width = 65;
            // 
            // terminalsHeaderSymbol
            // 
            this.terminalsHeaderSymbol.Text = "Symbol";
            this.terminalsHeaderSymbol.Width = 220;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 307);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(224, 25);
            this.label1.TabIndex = 4;
            this.label1.Text = "Note: 0 for ε, 1 for $";
            // 
            // groupBoxFirstFollow
            // 
            this.groupBoxFirstFollow.Controls.Add(this.tablePanelFirstFollow);
            this.groupBoxFirstFollow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxFirstFollow.Location = new System.Drawing.Point(1417, 3);
            this.groupBoxFirstFollow.Name = "groupBoxFirstFollow";
            this.groupBoxFirstFollow.Size = new System.Drawing.Size(886, 431);
            this.groupBoxFirstFollow.TabIndex = 2;
            this.groupBoxFirstFollow.TabStop = false;
            this.groupBoxFirstFollow.Text = "FIRST / FOLLOW Set";
            // 
            // tablePanelFirstFollow
            // 
            this.tablePanelFirstFollow.ColumnCount = 3;
            this.tablePanelFirstFollow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tablePanelFirstFollow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablePanelFirstFollow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tablePanelFirstFollow.Controls.Add(this.tabFirstFollow, 1, 1);
            this.tablePanelFirstFollow.Controls.Add(this.flowPanelGrammarType, 1, 2);
            this.tablePanelFirstFollow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanelFirstFollow.Location = new System.Drawing.Point(3, 27);
            this.tablePanelFirstFollow.Name = "tablePanelFirstFollow";
            this.tablePanelFirstFollow.RowCount = 3;
            this.tablePanelFirstFollow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tablePanelFirstFollow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablePanelFirstFollow.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tablePanelFirstFollow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tablePanelFirstFollow.Size = new System.Drawing.Size(880, 401);
            this.tablePanelFirstFollow.TabIndex = 2;
            // 
            // tabFirstFollow
            // 
            this.tabFirstFollow.Controls.Add(this.tabPageFirst);
            this.tabFirstFollow.Controls.Add(this.tabPageFollow);
            this.tabFirstFollow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabFirstFollow.Location = new System.Drawing.Point(8, 8);
            this.tabFirstFollow.Name = "tabFirstFollow";
            this.tabFirstFollow.SelectedIndex = 0;
            this.tabFirstFollow.Size = new System.Drawing.Size(864, 353);
            this.tabFirstFollow.TabIndex = 0;
            // 
            // tabPageFirst
            // 
            this.tabPageFirst.Controls.Add(this.listFirst);
            this.tabPageFirst.Location = new System.Drawing.Point(8, 39);
            this.tabPageFirst.Name = "tabPageFirst";
            this.tabPageFirst.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageFirst.Size = new System.Drawing.Size(848, 306);
            this.tabPageFirst.TabIndex = 0;
            this.tabPageFirst.Text = "FIRST Set";
            this.tabPageFirst.UseVisualStyleBackColor = true;
            // 
            // listFirst
            // 
            this.listFirst.CausesValidation = false;
            this.listFirst.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.firstHeaderSymbol,
            this.firstHeaderElement});
            this.listFirst.Cursor = System.Windows.Forms.Cursors.Default;
            this.listFirst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listFirst.FullRowSelect = true;
            this.listFirst.Location = new System.Drawing.Point(3, 3);
            this.listFirst.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.listFirst.MultiSelect = false;
            this.listFirst.Name = "listFirst";
            this.listFirst.Size = new System.Drawing.Size(842, 300);
            this.listFirst.TabIndex = 0;
            this.listFirst.UseCompatibleStateImageBehavior = false;
            this.listFirst.View = System.Windows.Forms.View.Details;
            // 
            // firstHeaderSymbol
            // 
            this.firstHeaderSymbol.Text = "Symbol";
            this.firstHeaderSymbol.Width = 150;
            // 
            // firstHeaderElement
            // 
            this.firstHeaderElement.Text = "Elements";
            this.firstHeaderElement.Width = 425;
            // 
            // tabPageFollow
            // 
            this.tabPageFollow.Controls.Add(this.listFollow);
            this.tabPageFollow.Location = new System.Drawing.Point(8, 39);
            this.tabPageFollow.Name = "tabPageFollow";
            this.tabPageFollow.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageFollow.Size = new System.Drawing.Size(848, 306);
            this.tabPageFollow.TabIndex = 1;
            this.tabPageFollow.Text = "FOLLOW Set";
            this.tabPageFollow.UseVisualStyleBackColor = true;
            // 
            // listFollow
            // 
            this.listFollow.CausesValidation = false;
            this.listFollow.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.followHeaderSymbol,
            this.followHeaderElements});
            this.listFollow.Cursor = System.Windows.Forms.Cursors.Default;
            this.listFollow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listFollow.FullRowSelect = true;
            this.listFollow.Location = new System.Drawing.Point(3, 3);
            this.listFollow.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.listFollow.MultiSelect = false;
            this.listFollow.Name = "listFollow";
            this.listFollow.Size = new System.Drawing.Size(842, 300);
            this.listFollow.TabIndex = 3;
            this.listFollow.UseCompatibleStateImageBehavior = false;
            this.listFollow.View = System.Windows.Forms.View.Details;
            // 
            // followHeaderSymbol
            // 
            this.followHeaderSymbol.Text = "Symbol";
            this.followHeaderSymbol.Width = 150;
            // 
            // followHeaderElements
            // 
            this.followHeaderElements.Text = "Elements";
            this.followHeaderElements.Width = 425;
            // 
            // flowPanelGrammarType
            // 
            this.flowPanelGrammarType.Controls.Add(this.lblLLGrammarText);
            this.flowPanelGrammarType.Controls.Add(this.lblIsLLGrammar);
            this.flowPanelGrammarType.Controls.Add(this.lblLRGrammarText);
            this.flowPanelGrammarType.Controls.Add(this.lblIsLRGrammar);
            this.flowPanelGrammarType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowPanelGrammarType.Location = new System.Drawing.Point(8, 367);
            this.flowPanelGrammarType.Name = "flowPanelGrammarType";
            this.flowPanelGrammarType.Size = new System.Drawing.Size(864, 31);
            this.flowPanelGrammarType.TabIndex = 1;
            // 
            // lblLLGrammarText
            // 
            this.lblLLGrammarText.AutoSize = true;
            this.lblLLGrammarText.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLLGrammarText.Location = new System.Drawing.Point(3, 0);
            this.lblLLGrammarText.Name = "lblLLGrammarText";
            this.lblLLGrammarText.Size = new System.Drawing.Size(207, 25);
            this.lblLLGrammarText.TabIndex = 0;
            this.lblLLGrammarText.Text = "Is LL(1) Grammar?";
            // 
            // lblIsLLGrammar
            // 
            this.lblIsLLGrammar.AutoSize = true;
            this.lblIsLLGrammar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblIsLLGrammar.Location = new System.Drawing.Point(216, 0);
            this.lblIsLLGrammar.Name = "lblIsLLGrammar";
            this.lblIsLLGrammar.Size = new System.Drawing.Size(47, 25);
            this.lblIsLLGrammar.TabIndex = 1;
            this.lblIsLLGrammar.Text = "N/A";
            // 
            // lblLRGrammarText
            // 
            this.lblLRGrammarText.AutoSize = true;
            this.lblLRGrammarText.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLRGrammarText.Location = new System.Drawing.Point(269, 0);
            this.lblLRGrammarText.Name = "lblLRGrammarText";
            this.lblLRGrammarText.Size = new System.Drawing.Size(181, 25);
            this.lblLRGrammarText.TabIndex = 2;
            this.lblLRGrammarText.Text = "Is LR Grammar?";
            // 
            // lblIsLRGrammar
            // 
            this.lblIsLRGrammar.AutoSize = true;
            this.lblIsLRGrammar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblIsLRGrammar.Location = new System.Drawing.Point(456, 0);
            this.lblIsLRGrammar.Name = "lblIsLRGrammar";
            this.lblIsLRGrammar.Size = new System.Drawing.Size(47, 25);
            this.lblIsLRGrammar.TabIndex = 3;
            this.lblIsLRGrammar.Text = "N/A";
            // 
            // groupBoxParseTable
            // 
            this.tablePanelControls.SetColumnSpan(this.groupBoxParseTable, 3);
            this.groupBoxParseTable.Controls.Add(this.tablePanelParsingTable);
            this.groupBoxParseTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxParseTable.Location = new System.Drawing.Point(813, 440);
            this.groupBoxParseTable.Name = "groupBoxParseTable";
            this.groupBoxParseTable.Size = new System.Drawing.Size(1490, 650);
            this.groupBoxParseTable.TabIndex = 3;
            this.groupBoxParseTable.TabStop = false;
            this.groupBoxParseTable.Text = "Parsing Table";
            // 
            // tablePanelParsingTable
            // 
            this.tablePanelParsingTable.ColumnCount = 3;
            this.tablePanelParsingTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tablePanelParsingTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablePanelParsingTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tablePanelParsingTable.Controls.Add(this.tabParsingTable, 1, 1);
            this.tablePanelParsingTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanelParsingTable.Location = new System.Drawing.Point(3, 27);
            this.tablePanelParsingTable.Name = "tablePanelParsingTable";
            this.tablePanelParsingTable.RowCount = 3;
            this.tablePanelParsingTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tablePanelParsingTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablePanelParsingTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tablePanelParsingTable.Size = new System.Drawing.Size(1484, 620);
            this.tablePanelParsingTable.TabIndex = 2;
            // 
            // tabParsingTable
            // 
            this.tabParsingTable.Controls.Add(this.tabPageStates);
            this.tabParsingTable.Controls.Add(this.tabPageActionTable);
            this.tabParsingTable.Controls.Add(this.tabPageGotoAction);
            this.tabParsingTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabParsingTable.Location = new System.Drawing.Point(8, 8);
            this.tabParsingTable.Name = "tabParsingTable";
            this.tabParsingTable.SelectedIndex = 0;
            this.tabParsingTable.Size = new System.Drawing.Size(1468, 604);
            this.tabParsingTable.TabIndex = 0;
            // 
            // tabPageStates
            // 
            this.tabPageStates.Controls.Add(this.gridStates);
            this.tabPageStates.Location = new System.Drawing.Point(8, 39);
            this.tabPageStates.Name = "tabPageStates";
            this.tabPageStates.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageStates.Size = new System.Drawing.Size(1452, 557);
            this.tabPageStates.TabIndex = 2;
            this.tabPageStates.Text = "States";
            this.tabPageStates.UseVisualStyleBackColor = true;
            // 
            // gridStates
            // 
            this.gridStates.AllowUserToAddRows = false;
            this.gridStates.AllowUserToDeleteRows = false;
            this.gridStates.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.gridStates.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridStates.BackgroundColor = System.Drawing.SystemColors.Window;
            this.gridStates.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridStates.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridStates.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridStates.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnId,
            this.ColumnItems});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridStates.DefaultCellStyle = dataGridViewCellStyle3;
            this.gridStates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridStates.Location = new System.Drawing.Point(3, 3);
            this.gridStates.MultiSelect = false;
            this.gridStates.Name = "gridStates";
            this.gridStates.ReadOnly = true;
            this.gridStates.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.gridStates.RowHeadersVisible = false;
            this.gridStates.RowTemplate.Height = 33;
            this.gridStates.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridStates.Size = new System.Drawing.Size(1446, 551);
            this.gridStates.StandardTab = true;
            this.gridStates.TabIndex = 1;
            this.gridStates.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridStates_CellMouseDown);
            // 
            // ColumnId
            // 
            this.ColumnId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnId.DataPropertyName = "State";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ColumnId.DefaultCellStyle = dataGridViewCellStyle2;
            this.ColumnId.Frozen = true;
            this.ColumnId.HeaderText = "State";
            this.ColumnId.Name = "ColumnId";
            this.ColumnId.ReadOnly = true;
            this.ColumnId.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColumnId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnId.Width = 110;
            // 
            // ColumnItems
            // 
            this.ColumnItems.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnItems.DataPropertyName = "Items";
            this.ColumnItems.HeaderText = "Items";
            this.ColumnItems.Name = "ColumnItems";
            this.ColumnItems.ReadOnly = true;
            this.ColumnItems.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColumnItems.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // tabPageActionTable
            // 
            this.tabPageActionTable.Controls.Add(this.gridAction);
            this.tabPageActionTable.Location = new System.Drawing.Point(8, 39);
            this.tabPageActionTable.Name = "tabPageActionTable";
            this.tabPageActionTable.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageActionTable.Size = new System.Drawing.Size(1452, 557);
            this.tabPageActionTable.TabIndex = 0;
            this.tabPageActionTable.Text = "ACTION";
            this.tabPageActionTable.UseVisualStyleBackColor = true;
            // 
            // gridAction
            // 
            this.gridAction.AllowUserToAddRows = false;
            this.gridAction.AllowUserToDeleteRows = false;
            this.gridAction.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.gridAction.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridAction.BackgroundColor = System.Drawing.SystemColors.Window;
            this.gridAction.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridAction.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.gridAction.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridAction.DefaultCellStyle = dataGridViewCellStyle5;
            this.gridAction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridAction.Location = new System.Drawing.Point(3, 3);
            this.gridAction.MultiSelect = false;
            this.gridAction.Name = "gridAction";
            this.gridAction.ReadOnly = true;
            this.gridAction.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.gridAction.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.gridAction.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.gridAction.RowTemplate.Height = 33;
            this.gridAction.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.gridAction.Size = new System.Drawing.Size(1446, 551);
            this.gridAction.StandardTab = true;
            this.gridAction.TabIndex = 3;
            this.gridAction.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridAction_CellContentDoubleClick);
            this.gridAction.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridAction_CellMouseDown);
            // 
            // tabPageGotoAction
            // 
            this.tabPageGotoAction.Controls.Add(this.gridGoto);
            this.tabPageGotoAction.Location = new System.Drawing.Point(8, 39);
            this.tabPageGotoAction.Name = "tabPageGotoAction";
            this.tabPageGotoAction.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGotoAction.Size = new System.Drawing.Size(1452, 557);
            this.tabPageGotoAction.TabIndex = 1;
            this.tabPageGotoAction.Text = "GOTO";
            this.tabPageGotoAction.UseVisualStyleBackColor = true;
            // 
            // gridGoto
            // 
            this.gridGoto.AllowUserToAddRows = false;
            this.gridGoto.AllowUserToDeleteRows = false;
            this.gridGoto.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.gridGoto.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridGoto.BackgroundColor = System.Drawing.SystemColors.Window;
            this.gridGoto.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridGoto.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.gridGoto.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridGoto.DefaultCellStyle = dataGridViewCellStyle8;
            this.gridGoto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridGoto.Location = new System.Drawing.Point(3, 3);
            this.gridGoto.MultiSelect = false;
            this.gridGoto.Name = "gridGoto";
            this.gridGoto.ReadOnly = true;
            this.gridGoto.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.gridGoto.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.gridGoto.RowsDefaultCellStyle = dataGridViewCellStyle9;
            this.gridGoto.RowTemplate.Height = 33;
            this.gridGoto.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.gridGoto.Size = new System.Drawing.Size(1446, 551);
            this.gridGoto.StandardTab = true;
            this.gridGoto.TabIndex = 2;
            // 
            // stateContextMenu
            // 
            this.stateContextMenu.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.stateContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyCToolStripMenuItem});
            this.stateContextMenu.Name = "stateContextMenu";
            this.stateContextMenu.Size = new System.Drawing.Size(214, 42);
            // 
            // copyCToolStripMenuItem
            // 
            this.copyCToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.copyCToolStripMenuItem.Name = "copyCToolStripMenuItem";
            this.copyCToolStripMenuItem.Size = new System.Drawing.Size(213, 38);
            this.copyCToolStripMenuItem.Text = "Copy (&C)";
            this.copyCToolStripMenuItem.Click += new System.EventHandler(this.copyCToolStripMenuItem_Click);
            // 
            // actionContextMenu
            // 
            this.actionContextMenu.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.actionContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.solveSToolStripMenuItem});
            this.actionContextMenu.Name = "actionContextMenu";
            this.actionContextMenu.Size = new System.Drawing.Size(332, 42);
            // 
            // solveSToolStripMenuItem
            // 
            this.solveSToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.solveSToolStripMenuItem.Name = "solveSToolStripMenuItem";
            this.solveSToolStripMenuItem.Size = new System.Drawing.Size(331, 38);
            this.solveSToolStripMenuItem.Text = "Solve Conflict... (&S)";
            this.solveSToolStripMenuItem.Click += new System.EventHandler(this.solveSToolStripMenuItem_Click);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "cs";
            this.saveFileDialog.Filter = "C# files|*.cs|VB files|*.vb";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2352, 1114);
            this.Controls.Add(this.tableLayout);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Shindo\'s Parser Generator";
            this.tableLayout.ResumeLayout(false);
            this.tablePanelControls.ResumeLayout(false);
            this.groupBoxGrammar.ResumeLayout(false);
            this.tablePanelGrammar.ResumeLayout(false);
            this.tablePanelGrammar.PerformLayout();
            this.tablePanelFunctions.ResumeLayout(false);
            this.flowPanelButtons.ResumeLayout(false);
            this.tablePanelParserType.ResumeLayout(false);
            this.groupBoxSymbols.ResumeLayout(false);
            this.tablePanelSymbols.ResumeLayout(false);
            this.tabSymbol.ResumeLayout(false);
            this.tabPageNonTerminals.ResumeLayout(false);
            this.tabPageTerminals.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBoxFirstFollow.ResumeLayout(false);
            this.tablePanelFirstFollow.ResumeLayout(false);
            this.tabFirstFollow.ResumeLayout(false);
            this.tabPageFirst.ResumeLayout(false);
            this.tabPageFollow.ResumeLayout(false);
            this.flowPanelGrammarType.ResumeLayout(false);
            this.flowPanelGrammarType.PerformLayout();
            this.groupBoxParseTable.ResumeLayout(false);
            this.tablePanelParsingTable.ResumeLayout(false);
            this.tabParsingTable.ResumeLayout(false);
            this.tabPageStates.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridStates)).EndInit();
            this.tabPageActionTable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridAction)).EndInit();
            this.tabPageGotoAction.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridGoto)).EndInit();
            this.stateContextMenu.ResumeLayout(false);
            this.actionContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayout;
        private System.Windows.Forms.TableLayoutPanel tablePanelControls;
        private System.Windows.Forms.GroupBox groupBoxGrammar;
        private System.Windows.Forms.GroupBox groupBoxSymbols;
        private System.Windows.Forms.GroupBox groupBoxFirstFollow;
        private System.Windows.Forms.GroupBox groupBoxParseTable;
        private System.Windows.Forms.TableLayoutPanel tablePanelGrammar;
        private System.Windows.Forms.TableLayoutPanel tablePanelSymbols;
        private System.Windows.Forms.TableLayoutPanel tablePanelFunctions;
        private System.Windows.Forms.FlowLayoutPanel flowPanelButtons;
        private System.Windows.Forms.Button btnAnalyze;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.TableLayoutPanel tablePanelParserType;
        private System.Windows.Forms.RadioButton rbLR1;
        private System.Windows.Forms.RadioButton rbSLR;
        private System.Windows.Forms.TableLayoutPanel tablePanelFirstFollow;
        private System.Windows.Forms.TabControl tabFirstFollow;
        private System.Windows.Forms.TabPage tabPageFirst;
        private System.Windows.Forms.TabPage tabPageFollow;
        private Utility.WindowThemeListView listFirst;
        private System.Windows.Forms.ColumnHeader firstHeaderSymbol;
        private System.Windows.Forms.ColumnHeader firstHeaderElement;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TabControl tabSymbol;
        private System.Windows.Forms.TabPage tabPageNonTerminals;
        private System.Windows.Forms.TabPage tabPageTerminals;
        private Utility.WindowThemeListView listFollow;
        private System.Windows.Forms.ColumnHeader followHeaderSymbol;
        private System.Windows.Forms.ColumnHeader followHeaderElements;
        private ScintillaNET.Scintilla textGrammar;
        private System.Windows.Forms.TableLayoutPanel tablePanelParsingTable;
        private System.Windows.Forms.TabControl tabParsingTable;
        private System.Windows.Forms.TabPage tabPageActionTable;
        private System.Windows.Forms.TabPage tabPageGotoAction;
        private System.Windows.Forms.TabPage tabPageStates;
        private Utility.DoubleBufferDataGridView gridStates;
        private Utility.DoubleBufferDataGridView gridGoto;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnItems;
        private Utility.DoubleBufferDataGridView gridAction;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.ContextMenuStrip stateContextMenu;
        private System.Windows.Forms.ToolStripMenuItem copyCToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip actionContextMenu;
        private System.Windows.Forms.ToolStripMenuItem solveSToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Label lblGrammarNote;
        private System.Windows.Forms.FlowLayoutPanel flowPanelGrammarType;
        private System.Windows.Forms.Label lblLLGrammarText;
        private System.Windows.Forms.Label lblIsLLGrammar;
        private Utility.WindowThemeListView listNonTerminals;
        private System.Windows.Forms.ColumnHeader nonTerminalsHeaderId;
        private System.Windows.Forms.ColumnHeader nonTerminalsHeaderSymbol;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Utility.WindowThemeListView listTerminals;
        private System.Windows.Forms.ColumnHeader terminalsHeaderId;
        private System.Windows.Forms.ColumnHeader terminalsHeaderSymbol;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblLRGrammarText;
        private System.Windows.Forms.Label lblIsLRGrammar;
    }
}

