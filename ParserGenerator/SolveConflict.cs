using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CompilingPrinciples.LexerModule;
using CompilingPrinciples.ParserModule;
using CompilingPrinciples.SymbolEnvironment;
using CompilingPrinciples.Utility;

namespace CompilingPrinciples.ParserGenerator
{
    public partial class SolveConflict : Form
    {
        private SolveConflictParams param;

        public SolveConflict(SolveConflictParams param)
        {
            this.param = param;
            InitializeComponent();
        }

        private void SolveConflict_Load(object sender, EventArgs e)
        {
            this.Text = "Solve Conflict: ACTION[" + param.State.ToString() + ", " + param.Symbol.ToString() + "]";
            lblState.Text = "State " + param.State.ToString() + ":";
            lblAction.Text = "Select for ACTION[" + param.State.ToString() + ", " + param.Symbol.ToString() + "]";

            if (param.IsSLR)
                foreach (var el in param.SLRItems)
                    listItems.Items.Add(el.ToString());
            else
                foreach (var el in param.LR1Items)
                    listItems.Items.Add(el.ToString());

            foreach (var el in param.Entry.Entries)
            {
                var row = new DataGridViewRow();

                var selectCell = new DataGridViewCheckBoxCell();
                selectCell.Value = param.Entry.PreferredEntry != null ? param.Entry.PreferredEntry.Equals(el) : false;
                row.Cells.Add(selectCell);

                var opCell = new DataGridViewTextBoxCell();
                opCell.Value = el.ToString();
                row.Cells.Add(opCell);
                opCell.ReadOnly = true;

                var itemCell = new DataGridViewTextBoxCell();
                itemCell.Value =
                    el.Type == ActionTableEntry.ActionType.Shift ?
                        param.IsSLR ? ParseTable<LR0Item>.ItemSetToString(param.SLRShiftItems[el.ShiftState])
                                    : ParseTable<LR1Item>.ItemSetToString(param.LR1ShiftItems[el.ShiftState])
                        : string.Empty;
                row.Cells.Add(itemCell);
                itemCell.ReadOnly = true;

                gridSelect.Rows.Add(row);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            var selectedIndex = -1;

            for (var i = 0; i < gridSelect.Rows.Count; i++)
                if (gridSelect.Rows[i].Cells[0].EditedFormattedValue.ToString() == "True")
                    selectedIndex = i;

            param.Entry.SetPreferEntry(selectedIndex == -1 ? null : param.Entry.Entries.ToList()[selectedIndex]);
            this.Close();
        }

        private void gridSelect_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // cancel select on other rows
            for (var i = 0; i < gridSelect.Rows.Count; i++)
                if (e.RowIndex != i && gridSelect.Rows[i].Cells[0].EditedFormattedValue.ToString() == "True")
                    gridSelect.Rows[i].Cells[0].Value = false;
        }

        private void gridSelect_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (gridSelect.IsCurrentCellDirty)
                gridSelect.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void gridSelect_SelectionChanged(object sender, EventArgs e)
        {
            gridSelect.ClearSelection();
        }
    }

    public class SolveConflictParams
    {
        private MultipleEntry entry;
        private bool slr;
        private int state;
        private ProductionSymbol symbol;

        private HashSet<LR0Item> slrItems;
        private Dictionary<int, HashSet<LR0Item>> slrShiftItems;
        private HashSet<LR1Item> lr1Items;
        private Dictionary<int, HashSet<LR1Item>> lr1ShiftItems;

        public MultipleEntry Entry
        {
            get { return entry; }
        }

        public bool IsSLR
        {
            get { return slr; }
        }

        public int State
        {
            get { return state; }
        }

        public ProductionSymbol Symbol
        {
            get { return new ProductionSymbol(symbol); }
        }

        public HashSet<LR0Item> SLRItems
        {
            get { return slrItems; }
        }

        public Dictionary<int, HashSet<LR0Item>> SLRShiftItems
        {
            get { return slrShiftItems; }
        }

        public HashSet<LR1Item> LR1Items
        {
            get { return lr1Items; }
        }

        public Dictionary<int, HashSet<LR1Item>> LR1ShiftItems
        {
            get { return lr1ShiftItems; }
        }

        public SolveConflictParams(MultipleEntry entry, int state, ProductionSymbol symbol, HashSet<LR0Item> slrItems, Dictionary<int, HashSet<LR0Item>> slrShiftItems = null)
        {
            this.slr = true;
            this.state = state;
            this.symbol = symbol;
            this.entry = entry;
            this.slrItems = slrItems;
            this.slrShiftItems = slrShiftItems;
        }

        public SolveConflictParams(MultipleEntry entry, int state, ProductionSymbol symbol, HashSet<LR1Item> lr1Items, Dictionary<int, HashSet<LR1Item>> lr1ShiftItems = null)
        {
            this.slr = false;
            this.state = state;
            this.symbol = symbol;
            this.entry = entry;
            this.lr1Items = lr1Items;
            this.lr1ShiftItems = lr1ShiftItems;
        }
    }
}
