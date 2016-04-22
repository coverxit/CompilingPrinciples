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
        private bool permitClose = false;

        public bool PermitClose
        {
            get { return permitClose; }
            set { permitClose = value; }
        }

        public GenerateWaitingForm()
        {
            InitializeComponent();
        }

        private void GenerateWaitingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!permitClose)
                e.Cancel = true;
        }
    }
}
