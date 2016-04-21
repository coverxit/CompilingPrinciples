using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CompilingPrinciples.TestCase
{
    public partial class GenerateWaitingForm : Form
    {
        public GenerateWaitingForm()
        {
            InitializeComponent();
        }

        private void GenerateWaitingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
