namespace CompilingPrinciples.ParserGenerator
{
    partial class SolveConflict
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.tablePanelControls = new System.Windows.Forms.TableLayoutPanel();
            this.gridSelect = new CompilingPrinciples.Utility.DoubleBufferDataGridView();
            this.ColumnSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColumnOperation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnItems = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblAction = new System.Windows.Forms.Label();
            this.listItems = new CompilingPrinciples.Utility.DoubleBufferedListBox();
            this.lblState = new System.Windows.Forms.Label();
            this.flowPanelButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.tableLayout.SuspendLayout();
            this.tablePanelControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSelect)).BeginInit();
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
            this.tableLayout.Size = new System.Drawing.Size(1034, 921);
            this.tableLayout.TabIndex = 2;
            // 
            // tablePanelControls
            // 
            this.tablePanelControls.ColumnCount = 1;
            this.tablePanelControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablePanelControls.Controls.Add(this.gridSelect, 0, 3);
            this.tablePanelControls.Controls.Add(this.lblAction, 0, 2);
            this.tablePanelControls.Controls.Add(this.listItems, 0, 1);
            this.tablePanelControls.Controls.Add(this.lblState, 0, 0);
            this.tablePanelControls.Controls.Add(this.flowPanelButtons, 0, 4);
            this.tablePanelControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanelControls.Location = new System.Drawing.Point(23, 13);
            this.tablePanelControls.Name = "tablePanelControls";
            this.tablePanelControls.RowCount = 5;
            this.tablePanelControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tablePanelControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tablePanelControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tablePanelControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tablePanelControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tablePanelControls.Size = new System.Drawing.Size(988, 900);
            this.tablePanelControls.TabIndex = 0;
            // 
            // gridSelect
            // 
            this.gridSelect.AllowUserToAddRows = false;
            this.gridSelect.AllowUserToDeleteRows = false;
            this.gridSelect.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.gridSelect.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridSelect.BackgroundColor = System.Drawing.SystemColors.Window;
            this.gridSelect.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridSelect.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridSelect.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSelect.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnSelect,
            this.ColumnOperation,
            this.ColumnItems});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridSelect.DefaultCellStyle = dataGridViewCellStyle4;
            this.gridSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSelect.Location = new System.Drawing.Point(3, 455);
            this.gridSelect.MultiSelect = false;
            this.gridSelect.Name = "gridSelect";
            this.gridSelect.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.gridSelect.RowHeadersVisible = false;
            this.gridSelect.RowTemplate.Height = 33;
            this.gridSelect.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridSelect.Size = new System.Drawing.Size(982, 381);
            this.gridSelect.StandardTab = true;
            this.gridSelect.TabIndex = 5;
            this.gridSelect.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridSelect_CellValueChanged);
            this.gridSelect.CurrentCellDirtyStateChanged += new System.EventHandler(this.gridSelect_CurrentCellDirtyStateChanged);
            // 
            // ColumnSelect
            // 
            this.ColumnSelect.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ColumnSelect.Frozen = true;
            this.ColumnSelect.HeaderText = "Select";
            this.ColumnSelect.Name = "ColumnSelect";
            this.ColumnSelect.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnSelect.Width = 78;
            // 
            // ColumnOperation
            // 
            this.ColumnOperation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ColumnOperation.DefaultCellStyle = dataGridViewCellStyle2;
            this.ColumnOperation.HeaderText = "Operation";
            this.ColumnOperation.Name = "ColumnOperation";
            this.ColumnOperation.ReadOnly = true;
            this.ColumnOperation.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnOperation.Width = 112;
            // 
            // ColumnItems
            // 
            this.ColumnItems.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnItems.DefaultCellStyle = dataGridViewCellStyle3;
            this.ColumnItems.HeaderText = "Shift To Items";
            this.ColumnItems.Name = "ColumnItems";
            this.ColumnItems.ReadOnly = true;
            this.ColumnItems.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // lblAction
            // 
            this.lblAction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAction.Font = new System.Drawing.Font("Segoe UI", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAction.Location = new System.Drawing.Point(3, 412);
            this.lblAction.Name = "lblAction";
            this.lblAction.Size = new System.Drawing.Size(982, 40);
            this.lblAction.TabIndex = 4;
            this.lblAction.Text = "Select for";
            this.lblAction.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listItems
            // 
            this.listItems.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.listItems.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listItems.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listItems.FormattingEnabled = true;
            this.listItems.ItemHeight = 28;
            this.listItems.Location = new System.Drawing.Point(3, 28);
            this.listItems.Name = "listItems";
            this.listItems.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.listItems.Size = new System.Drawing.Size(982, 381);
            this.listItems.TabIndex = 3;
            // 
            // lblState
            // 
            this.lblState.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblState.Location = new System.Drawing.Point(3, 0);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(982, 25);
            this.lblState.TabIndex = 2;
            this.lblState.Text = "State 0:";
            this.lblState.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // flowPanelButtons
            // 
            this.flowPanelButtons.Controls.Add(this.btnCancel);
            this.flowPanelButtons.Controls.Add(this.btnOK);
            this.flowPanelButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowPanelButtons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowPanelButtons.Location = new System.Drawing.Point(3, 842);
            this.flowPanelButtons.Name = "flowPanelButtons";
            this.flowPanelButtons.Size = new System.Drawing.Size(982, 55);
            this.flowPanelButtons.TabIndex = 6;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(846, 2);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(132, 52);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(706, 2);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(132, 52);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // SolveConflict
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1034, 921);
            this.Controls.Add(this.tableLayout);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SolveConflict";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Solve Conflict";
            this.Load += new System.EventHandler(this.SolveConflict_Load);
            this.tableLayout.ResumeLayout(false);
            this.tablePanelControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridSelect)).EndInit();
            this.flowPanelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayout;
        private System.Windows.Forms.TableLayoutPanel tablePanelControls;
        private System.Windows.Forms.Label lblState;
        private Utility.DoubleBufferedListBox listItems;
        private System.Windows.Forms.Label lblAction;
        private Utility.DoubleBufferDataGridView gridSelect;
        private System.Windows.Forms.FlowLayoutPanel flowPanelButtons;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnOperation;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnItems;
    }
}