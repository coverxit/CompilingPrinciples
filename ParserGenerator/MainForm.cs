using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using Microsoft.CSharp;
using System.CodeDom;
using System.CodeDom.Compiler;

using CompilingPrinciples.LexerModule;
using CompilingPrinciples.ParserModule;
using CompilingPrinciples.SymbolEnvironment;
using CompilingPrinciples.Utility;

namespace CompilingPrinciples.ParserGenerator
{
    public partial class MainForm : Form
    {
        private bool useSLR = true;
        private Grammar grammar = null;
        private ParseTable parseTable = null;

        public MainForm()
        {
            InitializeComponent();

            // Show Line Number
            textGrammar.Margins[0].Width = 36;

            textGrammar.StyleResetDefault();
            textGrammar.Styles[ScintillaNET.Style.Default].Font = "Consolas";
            textGrammar.Styles[ScintillaNET.Style.Default].Size = 10;
            textGrammar.StyleClearAll();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == openFileDialog.ShowDialog())
            {
                string fileName = openFileDialog.FileName;
                textGrammar.Text = File.ReadAllText(fileName);

                btnAnalyse.Focus();
            }
        }

        private void UpdateListSymbol(List<ProductionSymbol> symbols, ListView listView)
        {
            listView.BeginUpdate();
            foreach (var s in symbols)
            {
                var item = new ListViewItem();
                item.Text = s.Id.ToString();
                item.SubItems.Add(s.ToString());
                listView.Items.Add(item);
            }
            listView.EndUpdate();
        }

        private void UpdateListFirstFollow(List<ProductionSymbol> nonTerms, FirstFollowSet set, ListView listView)
        {
            listView.BeginUpdate();
            foreach (var sym in nonTerms)
            {
                var item = new ListViewItem();
                item.Text = sym.ToString();

                var sb = new StringBuilder();
                foreach (var s in set.Get(sym))
                    sb.Append(s.ToString() + ", ");

                item.SubItems.Add(sb.ToString(0, sb.Length - 2));
                listView.Items.Add(item);
            }
            listView.EndUpdate();
        }

        private void btnAnalyse_Click(object sender, EventArgs e)
        {
            listNonTerminals.Items.Clear();
            listTerminals.Items.Clear();
            listFirst.Items.Clear();
            listFollow.Items.Clear();
            
            gridAction.Rows.Clear();
            gridAction.Columns.Clear();
            gridGoto.Rows.Clear();
            gridGoto.Columns.Clear();
            
            var inputArray = Encoding.ASCII.GetBytes(textGrammar.Text);
            useSLR = !rbLR1.Checked;

            // Show waiting form
            var waitingForm = new GenerateWaitingForm();
            if (useSLR) waitingForm.DisableLR1();
            else waitingForm.DisableSLR();

            // Generate
            var reporter = new ProgressReporter(this, useSLR ? waitingForm.lblSLRProcess : waitingForm.lblLR1Process);
            grammar = new Grammar(new SymbolTable(), reporter);

            var analyseTask = new Task<bool>(() =>
            {
                grammar.Parse(new MemoryStream(inputArray));

                // Check if augmented
                if (grammar.FirstProduction.IsRightEpsilon() || grammar.FirstProduction.Right.Count != 1 ||
                    (grammar.FirstProduction.Right.Count == 1 && grammar.FirstProduction.Right[0].Type != ProductionSymbol.SymbolType.NonTerminal))
                {
                    MessageBox.Show("The grammar is NOT augmented!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }

                return true;
            });

            analyseTask.ContinueWith((lastTask) =>
            {
                // failed in grammar parse
                if (!lastTask.Result)
                    return;

                var showFormTask = Task.Run(() => { this.Invoke((MethodInvoker)delegate { waitingForm.ShowDialog(this); }); });

                this.Invoke((MethodInvoker)delegate
                {
                    UpdateListSymbol(grammar.NonTerminals, listNonTerminals);
                    UpdateListSymbol(grammar.Terminals, listTerminals);
                });

                if (useSLR)
                {
                    var collection = new LR0Collection(grammar, reporter);
                    this.Invoke((MethodInvoker)delegate
                    {
                        UpdateListFirstFollow(grammar.NonTerminals, collection.First, listFirst);
                        UpdateListFirstFollow(grammar.NonTerminals, collection.Follow, listFollow);
                    });

                    parseTable = SLRParseTable.Create(collection, reporter);
                    this.Invoke((MethodInvoker)delegate
                    {
                        waitingForm.lblSLRProcess.Text = "Refreshing UI...";
                        waitingForm.lblSLRProcess.ForeColor = Color.Green;
                    });
                }
                else
                {
                    var collection = new LR1Collection(grammar, reporter);
                    this.Invoke((MethodInvoker)delegate
                    {
                        UpdateListFirstFollow(grammar.NonTerminals, collection.First, listFirst);
                        UpdateListFirstFollow(grammar.NonTerminals, collection.Follow, listFollow);
                    });

                    parseTable = LR1ParseTable.Create(collection, reporter);

                    this.Invoke((MethodInvoker)delegate
                    {
                        waitingForm.lblLR1Process.Text = "Refreshing UI...";
                        waitingForm.lblLR1Process.ForeColor = Color.Green;
                    });
                }

                // State Grid
                var stateTable = new DataTable();
                stateTable.Columns.Add("State", typeof(int));
                stateTable.Columns.Add("Items", typeof(string));

                foreach (var s in parseTable.StringItems().Select((value, index) => new { index, value }))
                    stateTable.Rows.Add(s.index, s.value);

                this.Invoke((MethodInvoker)delegate { gridStates.DataSource = stateTable; });

                // Action Grid
                this.Invoke((MethodInvoker)delegate
                {
                    foreach (var s in grammar.TerminalsWithoutEpsilonWithEndMarker)
                    {
                        var col = new DataGridViewTextBoxColumn();
                        col.HeaderText = s.ToString();
                        col.SortMode = DataGridViewColumnSortMode.NotSortable;
                        col.Resizable = DataGridViewTriState.False;
                        gridAction.Columns.Add(col);
                    }

                    foreach (var r in parseTable.Action)
                    {
                        var row = new DataGridViewRow();
                        row.HeaderCell.Value = r.Key.ToString();

                        foreach (var s in grammar.TerminalsWithoutEpsilonWithEndMarker)
                        {
                            var cell = new DataGridViewTextBoxCell();

                            if (r.Value[s].PreferedEntrySpecified)
                            {
                                cell.Value = r.Value[s].PreferredEntry.ToShortString();
                                switch (r.Value[s].PreferredEntry.Type)
                                {
                                    case ActionTableEntry.ActionType.Accept:
                                        cell.Style.ForeColor = Color.Green;
                                        break;

                                    case ActionTableEntry.ActionType.Error:
                                        cell.Style.ForeColor = Color.Red;
                                        break;

                                    case ActionTableEntry.ActionType.Shift:
                                        cell.Style.ForeColor = Color.Blue;
                                        break;

                                    case ActionTableEntry.ActionType.Reduce:
                                        cell.Style.ForeColor = Color.MediumPurple;
                                        break;
                                }
                            }
                            else
                            {
                                StringBuilder sb = new StringBuilder();
                                foreach (var el in r.Value[s].Entries)
                                    sb.Append(el.ToShortString() + "/");
                                cell.Value = sb.ToString(0, sb.Length - 1);

                                cell.Style.BackColor = Color.OrangeRed;
                                cell.Style.ForeColor = Color.White;
                                cell.Style.SelectionBackColor = Color.DarkOrange;
                                cell.Style.SelectionForeColor = Color.White;

                                cell.Tag = new Tuple<int, ProductionSymbol, MultipleEntry>(r.Key, s, r.Value[s]); // Save the tag for solving conflicts
                            }
                            
                            row.Cells.Add(cell);
                        }

                        gridAction.Rows.Add(row);
                    }
                });

                // Goto Grid
                this.Invoke((MethodInvoker)delegate
                {
                    foreach (var s in grammar.NonTerminalsWithoutAugmentedS)
                    {
                        var col = new DataGridViewTextBoxColumn();
                        col.HeaderText = s.ToString();
                        col.SortMode = DataGridViewColumnSortMode.NotSortable;
                        col.Resizable = DataGridViewTriState.False;
                        gridGoto.Columns.Add(col);
                    }
                    
                    foreach (var r in parseTable.Goto)
                    {
                        var row = new DataGridViewRow();
                        row.HeaderCell.Value = r.Key.ToString();

                        foreach (var s in grammar.NonTerminalsWithoutAugmentedS)
                        {
                            var cell = new DataGridViewTextBoxCell();
                            cell.Value = r.Value[s] == -1 ? "err" : r.Value[s].ToString();
                            cell.Style.ForeColor = r.Value[s] == -1 ? Color.Red : Color.Black;
                            row.Cells.Add(cell);
                        }
                        
                        gridGoto.Rows.Add(row);
                    }
                });
            }).ContinueWith((lastTask) => {
                // Cleanup
                this.Invoke((MethodInvoker)delegate
                {
                    // Close waiting form
                    waitingForm.PermitClose = true;
                    waitingForm.Close();

                    btnGenerate.Enabled = true;
                    btnGenerate.Focus();
                });
            });

            analyseTask.Start();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            int unsolvedConflicts = 0;
            foreach (var a in parseTable.Action)
                foreach (var p in a.Value)
                    unsolvedConflicts += p.Value.PreferedEntrySpecified ? 0 : 1;

            if (unsolvedConflicts > 0)
            {
                MessageBox.Show("There are " + unsolvedConflicts.ToString() + " shift/shift or shift/reduce conflicts in ACTION table unsolved!",
                                "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (DialogResult.OK == saveFileDialog.ShowDialog())
            {
                var fileName = saveFileDialog.FileName;
                var outputWriter = new StreamWriter(new FileStream(fileName, FileMode.Create));

                var parser = useSLR ? new Parser<LR0Item>(new SymbolTable(), grammar, parseTable as ParseTable<LR0Item>, null) as Parser :
                                   new Parser<LR1Item>(new SymbolTable(), grammar, parseTable as ParseTable<LR1Item>, null) as Parser;

                using (var ctxStream = new FileStream(Path.ChangeExtension(fileName, ".ctx"), FileMode.Create))
                    parser.SaveContext(ctxStream);
                
                outputWriter.WriteLine(CompilerHelper.CompileParser(fileName));
                outputWriter.Close();
                
                MessageBox.Show("Three files are generated:\r\n\r\n" + 
                                Path.GetFileName(fileName) + " - Source code of the parser.\r\n" +
                                Path.GetFileNameWithoutExtension(fileName) + ".ctx - Parser's context.\r\n" +
                                Path.GetFileNameWithoutExtension(fileName) + ".exe - Executable parser.", 
                                "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gridAction_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var cell = gridAction.Rows[e.RowIndex].Cells[e.ColumnIndex];
            var tag = cell.Tag as Tuple<int, ProductionSymbol, MultipleEntry>;

            if (tag != null)
            {
                SolveConflict solveForm;
                var shiftTo = new List<int>();

                foreach (var el in tag.Item3.Entries.Where(el => el.Type == ActionTableEntry.ActionType.Shift))
                    shiftTo.Add(el.ShiftState);
                
                if (useSLR)
                {
                    var slrParseTable = parseTable as SLRParseTable;
                    var shiftItems = new Dictionary<int, HashSet<LR0Item>>();

                    foreach (var s in shiftTo)
                        shiftItems.Add(s, slrParseTable.Items[s]);

                    solveForm = new SolveConflict(new SolveConflictParams(tag.Item3, tag.Item1, tag.Item2, slrParseTable.Items[tag.Item1], shiftItems));
                }
                else
                {
                    var lr1ParseTable = parseTable as LR1ParseTable;
                    var shiftItems = new Dictionary<int, HashSet<LR1Item>>();

                    foreach (var s in shiftTo)
                        shiftItems.Add(s, lr1ParseTable.Items[s]);

                    solveForm = new SolveConflict(new SolveConflictParams(tag.Item3, tag.Item1, tag.Item2, lr1ParseTable.Items[tag.Item1], shiftItems));
                }
                solveForm.ShowDialog(this);

                if (tag.Item3.PreferedEntrySpecified)
                {
                    // Solved color
                    cell.Style.BackColor = Color.LightGray;
                    switch (tag.Item3.PreferredEntry.Type)
                    {
                        case ActionTableEntry.ActionType.Reduce:
                            cell.Style.ForeColor = Color.MediumPurple;
                            break;

                        case ActionTableEntry.ActionType.Shift:
                            cell.Style.ForeColor = Color.Blue;
                            break;

                        default:
                            cell.Style.ForeColor = Color.Black;
                            break;
                    }
                    cell.Style.SelectionBackColor = Color.LightSlateGray;
                    cell.Style.SelectionForeColor = Color.Black;

                    cell.Value = tag.Item3.PreferredEntry.ToShortString();
                }   
                else
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var el in tag.Item3.Entries)
                        sb.Append(el.ToShortString() + "/");
                    cell.Value = sb.ToString(0, sb.Length - 1);

                    cell.Style.BackColor = Color.OrangeRed;
                    cell.Style.ForeColor = Color.White;
                    cell.Style.SelectionBackColor = Color.DarkOrange;
                    cell.Style.SelectionForeColor = Color.White;
                }
            }
        }

        private void copyCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var row = gridStates.SelectedRows[0];
            Clipboard.SetText("State " + row.Cells[0].Value.ToString() + 
                              ":\r\n" + row.Cells[1].Value.ToString() + 
                              "\r\n");
        }

        private void gridStates_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                gridStates.CurrentCell = gridStates.Rows[e.RowIndex].Cells[e.ColumnIndex];
                stateContextMenu.Show(MousePosition.X, MousePosition.Y);
            }
        }

        private void gridAction_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                gridAction.CurrentCell = gridAction.Rows[e.RowIndex].Cells[e.ColumnIndex];
                
                if (gridAction.CurrentCell.Tag != null) // only on conflict cells
                    actionContextMenu.Show(MousePosition.X, MousePosition.Y);
            }
        }

        private void solveSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gridAction_CellContentDoubleClick(sender, 
                new DataGridViewCellEventArgs(gridAction.CurrentCell.ColumnIndex, gridAction.CurrentCell.RowIndex));
        }
    }
}
