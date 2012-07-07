using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;

namespace Buckets
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //preset radio buttons
            radBtnColorBW.Checked = true;
            radBtnThreadingSingle.Checked = true;
            radBtnGradient.Checked = true;

            //fill algorithms box
            comboBoxAlgos.Items.Add("Additive");
            comboBoxAlgos.Items.Add("ExclusiveOr");
            comboBoxAlgos.Items.Add("SAX");
            comboBoxAlgos.Items.Add("SDMB");
            comboBoxAlgos.Items.Add("Bernstein");
            comboBoxAlgos.Items.Add("BernsteinModified");
            comboBoxAlgos.Items.Add("JenkinsOneAtATime");
            comboBoxAlgos.Items.Add("FNV1");
            comboBoxAlgos.Items.Add("FNV1A");
            //Set default to bernstein
            comboBoxAlgos.SelectedIndex = 4;

            //make form non resizable
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            //we need to capture the mouse wheel since the picture box never actually has focus
            this.MouseWheel += MainForm_MouseWheel;

        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            //get values
            int loadMultiplier = 1;
            if (!int.TryParse(txtBoxLoadMultiplier.Text, out loadMultiplier))
            {
                MessageBox.Show("Load Multiplier must be a 32 bit integer!");
                return;
            }

            int keyLength = 8;
            if (!int.TryParse(txtBoxKeyLength.Text, out keyLength))
            {
                MessageBox.Show("Key Length must be a 32 bit integer!");
                return;
            }

            short aVal = 12;
            if (!short.TryParse(txtBoxAVal.Text, out aVal))
            {
                MessageBox.Show("Adjustment Value must be an 8 bit integer!");
                return;
            }

            Func<string, uint, uint> algo;
            switch (comboBoxAlgos.SelectedIndex)
            {
                case 0:
                    algo = StringHashes.Additive;
                    break;
                case 1:
                    algo = StringHashes.ExclusiveOr;
                    break;
                case 2:
                    algo = StringHashes.SAX;
                    break;
                case 3:
                    algo = StringHashes.SDMB;
                    break;
                case 4:
                    algo = StringHashes.Bernstein;
                    break;
                case 5:
                    algo = StringHashes.BernsteinModified;
                    break;
                case 6:
                    algo = StringHashes.JenkinsOneAtATime;
                    break;
                case 7:
                    algo = StringHashes.FNV1;
                    break;
                case 8:
                    algo = StringHashes.FNV1A;
                    break;
                default:
                    MessageBox.Show("Error getting algorithm!");
                    return;
            }

            if (radBtnColorBW.Checked) HashMatrix.BlackAndWhite = true;
            else HashMatrix.BlackAndWhite = false;


            //lock the main form, definitely not the best for usability but it doesn't matter for this project.
            this.Enabled = false;

            //Get start time
            DateTime processingStart = DateTime.Now;

            //Process the hash
            HashMatrix.AdjustmentValue = aVal;
            HashMatrix.LoadMultiplier = loadMultiplier;
            HashMatrix.ApplyHash(keyLength, algo);

            //Draw the image
            if (radBtnGradient.Checked) HashMatrix.Draw2DGradiant(outputImage);
            if (radBtnSurface.Checked) HashMatrix.Draw3DSurface(outputImage);

            //Get the end time
            DateTime processingFinish = DateTime.Now;

            //Write output information
            outputInfo.Clear();
            string info = String.Empty;
            info += "Elapsed Time: " + (processingFinish - processingStart).Duration().ToString() + Environment.NewLine;

            outputInfo.Text = info;


            //unlock the main form
            this.Enabled = true;

        }

        private void MainForm_MouseWheel(Object sender, MouseEventArgs e)
        {
            if (HashMatrix.CapturingMouse_3DSurface) HashMatrix.Draw3DSurface_MouseWheel(outputImage, e);
        }

        private void radBtnThreadingMulti_CheckedChanged(object sender, EventArgs e)
        {
            HashMatrix.MultiThreaded = true;
        }

        private void radBtnThreadingSingle_CheckedChanged(object sender, EventArgs e)
        {
            HashMatrix.MultiThreaded = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saver = new SaveFileDialog();
            saver.Filter = "Jpeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
            saver.Title = "Save Rendered Bitmap";
            saver.ShowDialog();
            if (saver.FileName != String.Empty)
            {
                FileStream stream = (FileStream)saver.OpenFile();
                switch (saver.FilterIndex)
                {
                    case 1:
                        outputImage.Image.Save(stream, ImageFormat.Jpeg);
                        break;
                    case 2:
                        outputImage.Image.Save(stream, ImageFormat.Bmp);
                        break;
                    case 3:
                        outputImage.Image.Save(stream, ImageFormat.Gif);
                        break;
                    default:
                        MessageBox.Show("Unknown file format! Cannot Save!");
                        break;
                }
                stream.Close();
            }
        }

        
    }
}
