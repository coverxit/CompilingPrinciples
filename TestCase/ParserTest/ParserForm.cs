﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CompilingPrinciples.TestCase
{
    public partial class ParserForm : Form
    {
        public ParserForm()
        {
            InitializeComponent();

            new GenerateWaitingForm().Show();
        }
    }
}
