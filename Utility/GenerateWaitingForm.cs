using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CompilingPrinciples.Utility
{
    public partial class GenerateWaitingForm : Form
    {
        private bool permitClose = false;

        public bool PermitClose
        {
            get { return permitClose; }
            set { permitClose = value; }
        }

        public void DisableSLR()
        {
            lblSLRProcess.Text = "N/A";
            lblSLRProcess.ForeColor = Color.Black;
        }

        public void DisableLR1()
        {
            lblLR1Process.Text = "N/A";
            lblLR1Process.ForeColor = Color.Black;
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
