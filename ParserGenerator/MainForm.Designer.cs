namespace CompilingPrinciples.ParserGenerator
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
            this.tableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.tablePanelControls = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxGrammar = new System.Windows.Forms.GroupBox();
            this.groupBoxSymbols = new System.Windows.Forms.GroupBox();
            this.groupBoxFirstFollow = new System.Windows.Forms.GroupBox();
            this.groupBoxParseTable = new System.Windows.Forms.GroupBox();
            this.tablePanelCode = new System.Windows.Forms.TableLayoutPanel();
            this.flowPanelButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAnalyze = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.textGrammar = new System.Windows.Forms.TextBox();
            this.tableLayout.SuspendLayout();
            this.tablePanelControls.SuspendLayout();
            this.groupBoxGrammar.SuspendLayout();
            this.tablePanelCode.SuspendLayout();
            this.flowPanelButtons.SuspendLayout();
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
            this.tableLayout.Size = new System.Drawing.Size(1690, 783);
            this.tableLayout.TabIndex = 0;
            // 
            // tablePanelControls
            // 
            this.tablePanelControls.ColumnCount = 5;
            this.tablePanelControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33332F));
            this.tablePanelControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tablePanelControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tablePanelControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tablePanelControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
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
            this.tablePanelControls.Size = new System.Drawing.Size(1644, 762);
            this.tablePanelControls.TabIndex = 0;
            // 
            // groupBoxGrammar
            // 
            this.groupBoxGrammar.Controls.Add(this.tablePanelCode);
            this.groupBoxGrammar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxGrammar.Location = new System.Drawing.Point(3, 3);
            this.groupBoxGrammar.Name = "groupBoxGrammar";
            this.tablePanelControls.SetRowSpan(this.groupBoxGrammar, 2);
            this.groupBoxGrammar.Size = new System.Drawing.Size(535, 756);
            this.groupBoxGrammar.TabIndex = 0;
            this.groupBoxGrammar.TabStop = false;
            this.groupBoxGrammar.Text = "Grammar";
            // 
            // groupBoxSymbols
            // 
            this.groupBoxSymbols.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxSymbols.Location = new System.Drawing.Point(554, 3);
            this.groupBoxSymbols.Name = "groupBoxSymbols";
            this.groupBoxSymbols.Size = new System.Drawing.Size(535, 298);
            this.groupBoxSymbols.TabIndex = 1;
            this.groupBoxSymbols.TabStop = false;
            this.groupBoxSymbols.Text = "Symbols";
            // 
            // groupBoxFirstFollow
            // 
            this.groupBoxFirstFollow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxFirstFollow.Location = new System.Drawing.Point(1105, 3);
            this.groupBoxFirstFollow.Name = "groupBoxFirstFollow";
            this.groupBoxFirstFollow.Size = new System.Drawing.Size(536, 298);
            this.groupBoxFirstFollow.TabIndex = 2;
            this.groupBoxFirstFollow.TabStop = false;
            this.groupBoxFirstFollow.Text = "FIRST / FOLLOW Set";
            // 
            // groupBoxParseTable
            // 
            this.tablePanelControls.SetColumnSpan(this.groupBoxParseTable, 3);
            this.groupBoxParseTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxParseTable.Location = new System.Drawing.Point(554, 307);
            this.groupBoxParseTable.Name = "groupBoxParseTable";
            this.groupBoxParseTable.Size = new System.Drawing.Size(1087, 452);
            this.groupBoxParseTable.TabIndex = 3;
            this.groupBoxParseTable.TabStop = false;
            this.groupBoxParseTable.Text = "Parsing Table";
            // 
            // tablePanelCode
            // 
            this.tablePanelCode.ColumnCount = 3;
            this.tablePanelCode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tablePanelCode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablePanelCode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tablePanelCode.Controls.Add(this.flowPanelButtons, 1, 2);
            this.tablePanelCode.Controls.Add(this.textGrammar, 1, 1);
            this.tablePanelCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanelCode.Location = new System.Drawing.Point(3, 27);
            this.tablePanelCode.Name = "tablePanelCode";
            this.tablePanelCode.RowCount = 4;
            this.tablePanelCode.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tablePanelCode.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablePanelCode.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tablePanelCode.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tablePanelCode.Size = new System.Drawing.Size(529, 726);
            this.tablePanelCode.TabIndex = 1;
            // 
            // flowPanelButtons
            // 
            this.flowPanelButtons.Controls.Add(this.btnAnalyze);
            this.flowPanelButtons.Controls.Add(this.btnOpen);
            this.flowPanelButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowPanelButtons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowPanelButtons.Location = new System.Drawing.Point(13, 664);
            this.flowPanelButtons.Name = "flowPanelButtons";
            this.flowPanelButtons.Size = new System.Drawing.Size(503, 54);
            this.flowPanelButtons.TabIndex = 1;
            // 
            // btnAnalyze
            // 
            this.btnAnalyze.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAnalyze.Location = new System.Drawing.Point(367, 2);
            this.btnAnalyze.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnAnalyze.Name = "btnAnalyze";
            this.btnAnalyze.Size = new System.Drawing.Size(132, 52);
            this.btnAnalyze.TabIndex = 3;
            this.btnAnalyze.Text = "Analyze";
            this.btnAnalyze.UseVisualStyleBackColor = true;
            // 
            // btnOpen
            // 
            this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpen.Location = new System.Drawing.Point(227, 2);
            this.btnOpen.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(132, 52);
            this.btnOpen.TabIndex = 4;
            this.btnOpen.Text = "Open...";
            this.btnOpen.UseVisualStyleBackColor = true;
            // 
            // textGrammar
            // 
            this.textGrammar.AcceptsReturn = true;
            this.textGrammar.AcceptsTab = true;
            this.textGrammar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textGrammar.Location = new System.Drawing.Point(13, 8);
            this.textGrammar.Multiline = true;
            this.textGrammar.Name = "textGrammar";
            this.textGrammar.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textGrammar.Size = new System.Drawing.Size(503, 650);
            this.textGrammar.TabIndex = 2;
            this.textGrammar.WordWrap = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1690, 783);
            this.Controls.Add(this.tableLayout);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Shindo\'s Parser Generator";
            this.tableLayout.ResumeLayout(false);
            this.tablePanelControls.ResumeLayout(false);
            this.groupBoxGrammar.ResumeLayout(false);
            this.tablePanelCode.ResumeLayout(false);
            this.tablePanelCode.PerformLayout();
            this.flowPanelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayout;
        private System.Windows.Forms.TableLayoutPanel tablePanelControls;
        private System.Windows.Forms.GroupBox groupBoxGrammar;
        private System.Windows.Forms.GroupBox groupBoxSymbols;
        private System.Windows.Forms.GroupBox groupBoxFirstFollow;
        private System.Windows.Forms.GroupBox groupBoxParseTable;
        private System.Windows.Forms.TableLayoutPanel tablePanelCode;
        private System.Windows.Forms.FlowLayoutPanel flowPanelButtons;
        private System.Windows.Forms.Button btnAnalyze;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.TextBox textGrammar;
    }
}

