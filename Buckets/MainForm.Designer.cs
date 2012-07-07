namespace Buckets
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.outputImage = new System.Windows.Forms.PictureBox();
            this.btnProcess = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxAlgos = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBoxLoadMultiplier = new System.Windows.Forms.TextBox();
            this.grpBoxColorPallete = new System.Windows.Forms.GroupBox();
            this.radBtnColorFullCollor = new System.Windows.Forms.RadioButton();
            this.radBtnColorBW = new System.Windows.Forms.RadioButton();
            this.grpBoxThreads = new System.Windows.Forms.GroupBox();
            this.radBtnThreadingMulti = new System.Windows.Forms.RadioButton();
            this.radBtnThreadingSingle = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBoxKeyLength = new System.Windows.Forms.TextBox();
            this.grpBoxImageType = new System.Windows.Forms.GroupBox();
            this.radBtnSurface = new System.Windows.Forms.RadioButton();
            this.radBtnGradient = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBoxAVal = new System.Windows.Forms.TextBox();
            this.outputInfo = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.outputImage)).BeginInit();
            this.grpBoxColorPallete.SuspendLayout();
            this.grpBoxThreads.SuspendLayout();
            this.grpBoxImageType.SuspendLayout();
            this.SuspendLayout();
            // 
            // outputImage
            // 
            this.outputImage.BackColor = System.Drawing.SystemColors.Control;
            this.outputImage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.outputImage.Location = new System.Drawing.Point(12, 12);
            this.outputImage.Name = "outputImage";
            this.outputImage.Size = new System.Drawing.Size(500, 500);
            this.outputImage.TabIndex = 0;
            this.outputImage.TabStop = false;
            // 
            // btnProcess
            // 
            this.btnProcess.Location = new System.Drawing.Point(545, 489);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(237, 23);
            this.btnProcess.TabIndex = 1;
            this.btnProcess.Text = "Process Image";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(545, 460);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(237, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save Image";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(539, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Hashing Algorithm";
            // 
            // comboBoxAlgos
            // 
            this.comboBoxAlgos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAlgos.FormattingEnabled = true;
            this.comboBoxAlgos.Location = new System.Drawing.Point(548, 25);
            this.comboBoxAlgos.Name = "comboBoxAlgos";
            this.comboBoxAlgos.Size = new System.Drawing.Size(237, 21);
            this.comboBoxAlgos.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(539, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(252, 26);
            this.label2.TabIndex = 6;
            this.label2.Text = "Load Multiplier\r\n(Load Multiplier * Bucket Count = #Computed Keys )";
            // 
            // txtBoxLoadMultiplier
            // 
            this.txtBoxLoadMultiplier.Location = new System.Drawing.Point(548, 78);
            this.txtBoxLoadMultiplier.Name = "txtBoxLoadMultiplier";
            this.txtBoxLoadMultiplier.Size = new System.Drawing.Size(234, 20);
            this.txtBoxLoadMultiplier.TabIndex = 7;
            this.txtBoxLoadMultiplier.Text = "1";
            // 
            // grpBoxColorPallete
            // 
            this.grpBoxColorPallete.Controls.Add(this.radBtnColorFullCollor);
            this.grpBoxColorPallete.Controls.Add(this.radBtnColorBW);
            this.grpBoxColorPallete.Location = new System.Drawing.Point(548, 156);
            this.grpBoxColorPallete.Name = "grpBoxColorPallete";
            this.grpBoxColorPallete.Size = new System.Drawing.Size(234, 49);
            this.grpBoxColorPallete.TabIndex = 8;
            this.grpBoxColorPallete.TabStop = false;
            this.grpBoxColorPallete.Text = "Color Palette";
            // 
            // radBtnColorFullCollor
            // 
            this.radBtnColorFullCollor.AutoSize = true;
            this.radBtnColorFullCollor.Location = new System.Drawing.Point(130, 19);
            this.radBtnColorFullCollor.Name = "radBtnColorFullCollor";
            this.radBtnColorFullCollor.Size = new System.Drawing.Size(68, 17);
            this.radBtnColorFullCollor.TabIndex = 1;
            this.radBtnColorFullCollor.TabStop = true;
            this.radBtnColorFullCollor.Text = "Full Color";
            this.radBtnColorFullCollor.UseVisualStyleBackColor = true;
            // 
            // radBtnColorBW
            // 
            this.radBtnColorBW.AutoSize = true;
            this.radBtnColorBW.Location = new System.Drawing.Point(16, 19);
            this.radBtnColorBW.Name = "radBtnColorBW";
            this.radBtnColorBW.Size = new System.Drawing.Size(72, 17);
            this.radBtnColorBW.TabIndex = 0;
            this.radBtnColorBW.TabStop = true;
            this.radBtnColorBW.Text = "Grayscale";
            this.radBtnColorBW.UseVisualStyleBackColor = true;
            // 
            // grpBoxThreads
            // 
            this.grpBoxThreads.Controls.Add(this.radBtnThreadingMulti);
            this.grpBoxThreads.Controls.Add(this.radBtnThreadingSingle);
            this.grpBoxThreads.Location = new System.Drawing.Point(548, 221);
            this.grpBoxThreads.Name = "grpBoxThreads";
            this.grpBoxThreads.Size = new System.Drawing.Size(234, 48);
            this.grpBoxThreads.TabIndex = 9;
            this.grpBoxThreads.TabStop = false;
            this.grpBoxThreads.Text = "Process Threading";
            // 
            // radBtnThreadingMulti
            // 
            this.radBtnThreadingMulti.AutoSize = true;
            this.radBtnThreadingMulti.Location = new System.Drawing.Point(130, 19);
            this.radBtnThreadingMulti.Name = "radBtnThreadingMulti";
            this.radBtnThreadingMulti.Size = new System.Drawing.Size(96, 17);
            this.radBtnThreadingMulti.TabIndex = 1;
            this.radBtnThreadingMulti.TabStop = true;
            this.radBtnThreadingMulti.Text = "Multi Threaded";
            this.radBtnThreadingMulti.UseVisualStyleBackColor = true;
            this.radBtnThreadingMulti.CheckedChanged += new System.EventHandler(this.radBtnThreadingMulti_CheckedChanged);
            // 
            // radBtnThreadingSingle
            // 
            this.radBtnThreadingSingle.AutoSize = true;
            this.radBtnThreadingSingle.Location = new System.Drawing.Point(16, 19);
            this.radBtnThreadingSingle.Name = "radBtnThreadingSingle";
            this.radBtnThreadingSingle.Size = new System.Drawing.Size(103, 17);
            this.radBtnThreadingSingle.TabIndex = 0;
            this.radBtnThreadingSingle.TabStop = true;
            this.radBtnThreadingSingle.Text = "Single Threaded";
            this.radBtnThreadingSingle.UseVisualStyleBackColor = true;
            this.radBtnThreadingSingle.CheckedChanged += new System.EventHandler(this.radBtnThreadingSingle_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(545, 284);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(149, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Generated Key Length (chars)";
            // 
            // txtBoxKeyLength
            // 
            this.txtBoxKeyLength.Location = new System.Drawing.Point(548, 301);
            this.txtBoxKeyLength.Name = "txtBoxKeyLength";
            this.txtBoxKeyLength.Size = new System.Drawing.Size(234, 20);
            this.txtBoxKeyLength.TabIndex = 11;
            this.txtBoxKeyLength.Text = "8";
            // 
            // grpBoxImageType
            // 
            this.grpBoxImageType.Controls.Add(this.radBtnSurface);
            this.grpBoxImageType.Controls.Add(this.radBtnGradient);
            this.grpBoxImageType.Location = new System.Drawing.Point(548, 328);
            this.grpBoxImageType.Name = "grpBoxImageType";
            this.grpBoxImageType.Size = new System.Drawing.Size(234, 45);
            this.grpBoxImageType.TabIndex = 12;
            this.grpBoxImageType.TabStop = false;
            this.grpBoxImageType.Text = "Image Type";
            // 
            // radBtnSurface
            // 
            this.radBtnSurface.AutoSize = true;
            this.radBtnSurface.Location = new System.Drawing.Point(130, 19);
            this.radBtnSurface.Name = "radBtnSurface";
            this.radBtnSurface.Size = new System.Drawing.Size(79, 17);
            this.radBtnSurface.TabIndex = 1;
            this.radBtnSurface.TabStop = true;
            this.radBtnSurface.Text = "3D Surface";
            this.radBtnSurface.UseVisualStyleBackColor = true;
            // 
            // radBtnGradient
            // 
            this.radBtnGradient.AutoSize = true;
            this.radBtnGradient.Location = new System.Drawing.Point(16, 20);
            this.radBtnGradient.Name = "radBtnGradient";
            this.radBtnGradient.Size = new System.Drawing.Size(89, 17);
            this.radBtnGradient.TabIndex = 0;
            this.radBtnGradient.TabStop = true;
            this.radBtnGradient.Text = "Gradient Map";
            this.radBtnGradient.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(539, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(180, 26);
            this.label4.TabIndex = 13;
            this.label4.Text = "Adjustment Value\r\n(Proportionally flattens output values)\r\n";
            // 
            // txtBoxAVal
            // 
            this.txtBoxAVal.Location = new System.Drawing.Point(548, 130);
            this.txtBoxAVal.Name = "txtBoxAVal";
            this.txtBoxAVal.Size = new System.Drawing.Size(234, 20);
            this.txtBoxAVal.TabIndex = 14;
            this.txtBoxAVal.Text = "12";
            // 
            // outputInfo
            // 
            this.outputInfo.Location = new System.Drawing.Point(548, 400);
            this.outputInfo.Name = "outputInfo";
            this.outputInfo.Size = new System.Drawing.Size(226, 54);
            this.outputInfo.TabIndex = 15;
            this.outputInfo.Text = "";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(548, 381);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Output Information";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 530);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.outputInfo);
            this.Controls.Add(this.txtBoxAVal);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.grpBoxImageType);
            this.Controls.Add(this.txtBoxKeyLength);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.grpBoxThreads);
            this.Controls.Add(this.grpBoxColorPallete);
            this.Controls.Add(this.txtBoxLoadMultiplier);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxAlgos);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnProcess);
            this.Controls.Add(this.outputImage);
            this.Name = "MainForm";
            this.Text = "Buckets - Disruption Theory";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.outputImage)).EndInit();
            this.grpBoxColorPallete.ResumeLayout(false);
            this.grpBoxColorPallete.PerformLayout();
            this.grpBoxThreads.ResumeLayout(false);
            this.grpBoxThreads.PerformLayout();
            this.grpBoxImageType.ResumeLayout(false);
            this.grpBoxImageType.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox outputImage;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxAlgos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBoxLoadMultiplier;
        private System.Windows.Forms.GroupBox grpBoxColorPallete;
        private System.Windows.Forms.RadioButton radBtnColorFullCollor;
        private System.Windows.Forms.RadioButton radBtnColorBW;
        private System.Windows.Forms.GroupBox grpBoxThreads;
        private System.Windows.Forms.RadioButton radBtnThreadingMulti;
        private System.Windows.Forms.RadioButton radBtnThreadingSingle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBoxKeyLength;
        private System.Windows.Forms.GroupBox grpBoxImageType;
        private System.Windows.Forms.RadioButton radBtnSurface;
        private System.Windows.Forms.RadioButton radBtnGradient;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBoxAVal;
        private System.Windows.Forms.RichTextBox outputInfo;
        private System.Windows.Forms.Label label5;
    }
}

