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

using CompilingPrinciples.LexicalAnalyzer;
using CompilingPrinciples.SyntaxAnalyzer;
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

        private string HashSetToMultilineString<T>(HashSet<T> hashSet)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var e in hashSet)
                sb.Append(e.ToString() + '\n');
            return sb.ToString(0, sb.Length - 1);
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

            var showFormTask = Task.Run(() => { this.Invoke((MethodInvoker)delegate { waitingForm.ShowDialog(this); }); });

            // Generate
            var reporter = new ProgressReporter(this, useSLR ? waitingForm.lblSLRProcess : waitingForm.lblLR1Process);
            grammar = new Grammar(new SymbolTable(), reporter);
            

            var analyseTask = new Task(() =>
            {
                grammar.Parse(new MemoryStream(inputArray));
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
                        waitingForm.lblSLRProcess.Text = "Done";
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
                        waitingForm.lblLR1Process.Text = "Done";
                        waitingForm.lblLR1Process.ForeColor = Color.Green;
                    });
                }
                
                // State Grid
                var stateTable = new DataTable();
                stateTable.Columns.Add("State", typeof(int));
                stateTable.Columns.Add("Items", typeof(string));

                if (useSLR)
                    foreach (var s in (parseTable as SLRParseTable).Items.Select((value, index) => new { index, value }))
                        stateTable.Rows.Add(s.index, HashSetToMultilineString(s.value));
                else
                    foreach (var s in (parseTable as LR1ParseTable).Items.Select((value, index) => new { index, value }))
                        stateTable.Rows.Add(s.index, HashSetToMultilineString(s.value));

                this.Invoke((MethodInvoker)delegate { gridStates.DataSource = stateTable; });

                // Action Grid
                this.Invoke((MethodInvoker)delegate
                {
                    foreach (var s in grammar.Terminals)
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

                        foreach (var s in grammar.Terminals)
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
                                        cell.Style.ForeColor = Color.Green;
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

                                cell.Tag = r.Value[s]; // Save the tag for solving conflicts
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
            });

            analyseTask.ContinueWith((lastTask) => {
                // Cleanup
                this.Invoke((MethodInvoker)delegate
                {
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
                MessageBox.Show("There are " + unsolvedConflicts.ToString() + " shift/shift or shift/reduce conflicts unsolved!", 
                                "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        private void gridAction_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var cell = gridAction.Rows[e.RowIndex].Cells[e.ColumnIndex];
            var entry = cell.Tag as MultipleEntry;

            if (entry != null)
            {
                if (entry.PreferedEntrySpecified)
                    cell.Value = entry.PreferredEntry.ToShortString();
                else
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var el in entry.Entries)
                        sb.Append(el.ToShortString() + "/");
                    cell.Value = sb.ToString(0, sb.Length - 1);
                }

                // Solved color
                cell.Style.BackColor = Color.LightGray;
                cell.Style.ForeColor = Color.Black;
                cell.Style.SelectionBackColor = Color.LightSlateGray;
                cell.Style.SelectionForeColor = Color.Black;
            }
        }
    }
}
