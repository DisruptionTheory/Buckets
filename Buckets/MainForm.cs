using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Buckets
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HashMatrix.LoadMultiplier = 1000;
            HashMatrix.ApplyHash(4, StringHashes.SAX);
            HashMatrix.DrawImage(pictureBox1);
        }
    }
}
