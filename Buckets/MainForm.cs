﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using System.Threading.Tasks;

namespace Buckets
{
    public partial class MainForm : Form
    {
        private static ProgressBar progBar = null;
        private static DateTime processingStart;
        private static DateTime processingFinish;
        private static Form main = null;
        private static RichTextBox outputBox;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //make certain things globally accesible, sloppy but quick
            progBar = pBar;
            main = this;
            outputBox = outputInfo;

            //preset radio buttons
            radBtnColorBW.Checked = true;
            radBtnThreadingSingle.Checked = true;
            radBtnGradient.Checked = true;
            radBtnKDTRandAlphaNumeric.Checked = true;

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
            comboBoxAlgos.Items.Add("MurmurHash2");
            comboBoxAlgos.Items.Add("MurmurHash3");
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
            if (loadMultiplier < 1) loadMultiplier = 1;

            int keyLength = 8;
            if (!int.TryParse(txtBoxKeyLength.Text, out keyLength) && radBtnKDTRandAlphaNumeric.Checked)
            {
                MessageBox.Show("Key Length must be a 32 bit integer!");
                return;
            }
            if (keyLength < 1) keyLength = 1;

            short aVal = 12;
            if (!short.TryParse(txtBoxAVal.Text, out aVal))
            {
                MessageBox.Show("Adjustment Value must be an 8 bit integer!");
                return;
            }
            if (aVal < 1) aVal = 1;

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
                case 9:
                    algo = StringHashes.MurmurHash2;
                    break;
                case 10:
                    algo = StringHashes.MurmurHash3;
                    break;
                default:
                    MessageBox.Show("Error getting algorithm!");
                    return;
            }

            if (radBtnColorBW.Checked) HashMatrix.BlackAndWhite = true;
            else HashMatrix.BlackAndWhite = false;

            //lock the main form, definitely not the best for usability but it doesn't matter for this project.
            this.Enabled = false;
            Invalidate();
            this.Refresh();

            //Get start time
            processingStart = DateTime.Now;

            //Process the hash
            HashMatrix.AdjustmentValue = aVal;
            HashMatrix.LoadMultiplier = loadMultiplier;
            if (radBtnKDTRandAlphaNumeric.Checked) new Action<int, Func<string, uint, uint>>(HashMatrix.ApplyHashRandomString).BeginInvoke(keyLength, algo, drawImage, null);
            if (radBtnKDTRandAlphaNumericSpec.Checked) new Action<int, Func<string, uint, uint>>(HashMatrix.ApplyHashRandomStringSpecial).BeginInvoke(keyLength, algo, drawImage, null);
            if (radBtnKDTIncNum.Checked) new Action<Func<string, uint, uint>>(HashMatrix.ApplyHashIncrementalNumerics).BeginInvoke(algo, drawImage, null);   
            if (radBtnKDTIncAlphaNumeric.Checked) new Action<Func<string, uint, uint>>(HashMatrix.ApplyHashIncrementalString).BeginInvoke(algo, drawImage, null);   
            if (radBtnKDTIncAlphaNumericSpec.Checked) new Action<Func<string, uint, uint>>(HashMatrix.ApplyHashIncrementalStringSpecial).BeginInvoke(algo, drawImage, null);   
        }

        private void drawImage(IAsyncResult result)
        {
            //Draw the image
            if (radBtnGradient.Checked) new Action<PictureBox>(HashMatrix.Draw2DGradiant).BeginInvoke(outputImage, completeProcessing, null);
            if (radBtnSurface.Checked) new Action<PictureBox>(HashMatrix.Draw3DSurface).BeginInvoke(outputImage, completeProcessing, null);
        }

        private void completeProcessing(IAsyncResult result)
        {
            //Get the end time
            DateTime processingFinish = DateTime.Now;

            //Write output information
            outputBox.InvokeEx(o => o.Clear());
            string info = String.Empty;
            info += "Compute Time: " + (processingFinish - processingStart).Duration().ToString() + Environment.NewLine;
            info += "Bucket Count: " + HashMatrix.Range + Environment.NewLine;
            info += "Key Count: " + HashMatrix.KeyCount + Environment.NewLine;
            info += "Heaviest Bucket: " + HashMatrix.HighVal + " keys." + Environment.NewLine;
            info += "Lightest Bucket: " + HashMatrix.LowVal + " keys." + Environment.NewLine;
            outputBox.InvokeEx(o => o.Text = info);

            //unlock the main form
            main.InvokeEx(mf => { mf.Enabled = true; mf.Invalidate(); mf.Refresh(); });
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


        public static void ProgressBarIncrement()
        {
            progBar.InvokeEx(c => { c.Value += 1; c.Invalidate(); c.Refresh(); });            
        }

        public static void ProgressBarReset()
        {
            progBar.InvokeEx(c => { c.Maximum = 100; c.Value = 0;});
        }

        
    }
}
