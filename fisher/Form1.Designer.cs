namespace fisher
{
    partial class Form1
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.findLocationBtn = new System.Windows.Forms.Button();
			this.castCatchLocationLbl = new System.Windows.Forms.Label();
			this.rodChoiceGroupBox = new System.Windows.Forms.GroupBox();
			this.legRod = new System.Windows.Forms.RadioButton();
			this.flyRod = new System.Windows.Forms.RadioButton();
			this.spinningRod = new System.Windows.Forms.RadioButton();
			this.trollingRod = new System.Windows.Forms.RadioButton();
			this.woodFishingRod = new System.Windows.Forms.RadioButton();
			this.platformGroupBox = new System.Windows.Forms.GroupBox();
			this.steamButton = new System.Windows.Forms.RadioButton();
			this.kongButton = new System.Windows.Forms.RadioButton();
			this.getTimesBtn = new System.Windows.Forms.Button();
			this.autoBtn = new System.Windows.Forms.Button();
			this.debugAutoStepLbl = new System.Windows.Forms.Label();
			this.baitToUseLabel = new System.Windows.Forms.Label();
			this.baitToUseText = new System.Windows.Forms.NumericUpDown();
			this.backgroundThread = new System.ComponentModel.BackgroundWorker();
			this.cancelAutoModeBtn = new System.Windows.Forms.Button();
			this.debugOptions = new System.Windows.Forms.CheckBox();
			this.currentRodTimerLblDebug = new System.Windows.Forms.Label();
			this.rodTimerDebug = new System.Windows.Forms.NumericUpDown();
			this.rodTimerDebugToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.backgroundThreadGetTimes = new System.ComponentModel.BackgroundWorker();
			this.getTimesDedubToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.saveScreenshotButton = new System.Windows.Forms.Button();
			this.rodChoiceGroupBox.SuspendLayout();
			this.platformGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.baitToUseText)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.rodTimerDebug)).BeginInit();
			this.SuspendLayout();
			// 
			// findLocationBtn
			// 
			this.findLocationBtn.Location = new System.Drawing.Point(12, 190);
			this.findLocationBtn.Name = "findLocationBtn";
			this.findLocationBtn.Size = new System.Drawing.Size(160, 50);
			this.findLocationBtn.TabIndex = 0;
			this.findLocationBtn.Text = "Find Start Location";
			this.findLocationBtn.UseVisualStyleBackColor = true;
			this.findLocationBtn.Click += new System.EventHandler(this.CastCatchLocation_Click);
			// 
			// castCatchLocationLbl
			// 
			this.castCatchLocationLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.castCatchLocationLbl.Location = new System.Drawing.Point(187, 190);
			this.castCatchLocationLbl.Name = "castCatchLocationLbl";
			this.castCatchLocationLbl.Size = new System.Drawing.Size(160, 50);
			this.castCatchLocationLbl.TabIndex = 1;
			this.castCatchLocationLbl.Text = "Cast/Catch Location:";
			this.castCatchLocationLbl.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// rodChoiceGroupBox
			// 
			this.rodChoiceGroupBox.Controls.Add(this.legRod);
			this.rodChoiceGroupBox.Controls.Add(this.flyRod);
			this.rodChoiceGroupBox.Controls.Add(this.spinningRod);
			this.rodChoiceGroupBox.Controls.Add(this.trollingRod);
			this.rodChoiceGroupBox.Controls.Add(this.woodFishingRod);
			this.rodChoiceGroupBox.Location = new System.Drawing.Point(187, 12);
			this.rodChoiceGroupBox.Name = "rodChoiceGroupBox";
			this.rodChoiceGroupBox.Size = new System.Drawing.Size(160, 160);
			this.rodChoiceGroupBox.TabIndex = 8;
			this.rodChoiceGroupBox.TabStop = false;
			this.rodChoiceGroupBox.Text = "Rod: ";
			// 
			// legRod
			// 
			this.legRod.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
			this.legRod.Location = new System.Drawing.Point(7, 115);
			this.legRod.Name = "legRod";
			this.legRod.Size = new System.Drawing.Size(139, 32);
			this.legRod.TabIndex = 4;
			this.legRod.TabStop = true;
			this.legRod.Text = "Autoquatic Lightscoped Flycaster";
			this.legRod.UseVisualStyleBackColor = false;
			// 
			// flyRod
			// 
			this.flyRod.AutoSize = true;
			this.flyRod.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			this.flyRod.Location = new System.Drawing.Point(7, 91);
			this.flyRod.Name = "flyRod";
			this.flyRod.Size = new System.Drawing.Size(61, 17);
			this.flyRod.TabIndex = 3;
			this.flyRod.TabStop = true;
			this.flyRod.Text = "Fly Rod";
			this.flyRod.UseVisualStyleBackColor = false;
			// 
			// spinningRod
			// 
			this.spinningRod.AutoSize = true;
			this.spinningRod.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			this.spinningRod.Location = new System.Drawing.Point(7, 67);
			this.spinningRod.Name = "spinningRod";
			this.spinningRod.Size = new System.Drawing.Size(89, 17);
			this.spinningRod.TabIndex = 2;
			this.spinningRod.TabStop = true;
			this.spinningRod.Text = "Spinning Rod";
			this.spinningRod.UseVisualStyleBackColor = false;
			// 
			// trollingRod
			// 
			this.trollingRod.AutoSize = true;
			this.trollingRod.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
			this.trollingRod.Location = new System.Drawing.Point(7, 43);
			this.trollingRod.Name = "trollingRod";
			this.trollingRod.Size = new System.Drawing.Size(82, 17);
			this.trollingRod.TabIndex = 1;
			this.trollingRod.TabStop = true;
			this.trollingRod.Text = "Trolling Rod";
			this.trollingRod.UseVisualStyleBackColor = false;
			// 
			// woodFishingRod
			// 
			this.woodFishingRod.AutoSize = true;
			this.woodFishingRod.BackColor = System.Drawing.Color.White;
			this.woodFishingRod.Location = new System.Drawing.Point(6, 19);
			this.woodFishingRod.Name = "woodFishingRod";
			this.woodFishingRod.Size = new System.Drawing.Size(125, 17);
			this.woodFishingRod.TabIndex = 0;
			this.woodFishingRod.TabStop = true;
			this.woodFishingRod.Text = "Wooden Fishing Rod";
			this.woodFishingRod.UseVisualStyleBackColor = false;
			// 
			// platformGroupBox
			// 
			this.platformGroupBox.Controls.Add(this.steamButton);
			this.platformGroupBox.Controls.Add(this.kongButton);
			this.platformGroupBox.Location = new System.Drawing.Point(12, 12);
			this.platformGroupBox.Name = "platformGroupBox";
			this.platformGroupBox.Size = new System.Drawing.Size(160, 80);
			this.platformGroupBox.TabIndex = 5;
			this.platformGroupBox.TabStop = false;
			this.platformGroupBox.Text = "Platform:";
			// 
			// steamButton
			// 
			this.steamButton.AutoSize = true;
			this.steamButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(173)))), ((int)(((byte)(238)))));
			this.steamButton.Location = new System.Drawing.Point(7, 43);
			this.steamButton.Name = "steamButton";
			this.steamButton.Size = new System.Drawing.Size(55, 17);
			this.steamButton.TabIndex = 1;
			this.steamButton.Text = "Steam";
			this.steamButton.UseVisualStyleBackColor = false;
			// 
			// kongButton
			// 
			this.kongButton.AutoSize = true;
			this.kongButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.kongButton.ForeColor = System.Drawing.Color.White;
			this.kongButton.Location = new System.Drawing.Point(7, 19);
			this.kongButton.Name = "kongButton";
			this.kongButton.Size = new System.Drawing.Size(80, 17);
			this.kongButton.TabIndex = 0;
			this.kongButton.Text = "Kongregate";
			this.kongButton.UseVisualStyleBackColor = false;
			// 
			// getTimesBtn
			// 
			this.getTimesBtn.Location = new System.Drawing.Point(366, 67);
			this.getTimesBtn.Name = "getTimesBtn";
			this.getTimesBtn.Size = new System.Drawing.Size(100, 25);
			this.getTimesBtn.TabIndex = 10;
			this.getTimesBtn.Text = "Get Times";
			this.getTimesBtn.UseVisualStyleBackColor = true;
			this.getTimesBtn.Click += new System.EventHandler(this.getTimesBtn_Click);
			// 
			// autoBtn
			// 
			this.autoBtn.Location = new System.Drawing.Point(12, 246);
			this.autoBtn.Name = "autoBtn";
			this.autoBtn.Size = new System.Drawing.Size(75, 50);
			this.autoBtn.TabIndex = 11;
			this.autoBtn.Text = "Auto mode";
			this.autoBtn.UseVisualStyleBackColor = true;
			this.autoBtn.Click += new System.EventHandler(this.autoBtn_Click);
			// 
			// debugAutoStepLbl
			// 
			this.debugAutoStepLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.debugAutoStepLbl.Location = new System.Drawing.Point(187, 246);
			this.debugAutoStepLbl.Name = "debugAutoStepLbl";
			this.debugAutoStepLbl.Size = new System.Drawing.Size(160, 50);
			this.debugAutoStepLbl.TabIndex = 12;
			this.debugAutoStepLbl.Text = "0/0 bait used.";
			this.debugAutoStepLbl.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// baitToUseLabel
			// 
			this.baitToUseLabel.Location = new System.Drawing.Point(9, 95);
			this.baitToUseLabel.Name = "baitToUseLabel";
			this.baitToUseLabel.Size = new System.Drawing.Size(121, 16);
			this.baitToUseLabel.TabIndex = 14;
			this.baitToUseLabel.Text = "Bait to use:";
			// 
			// baitToUseText
			// 
			this.baitToUseText.Location = new System.Drawing.Point(12, 114);
			this.baitToUseText.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.baitToUseText.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.baitToUseText.Name = "baitToUseText";
			this.baitToUseText.Size = new System.Drawing.Size(143, 20);
			this.baitToUseText.TabIndex = 15;
			this.baitToUseText.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// backgroundThread
			// 
			this.backgroundThread.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundThread_DoWork);
			// 
			// cancelAutoModeBtn
			// 
			this.cancelAutoModeBtn.Location = new System.Drawing.Point(97, 246);
			this.cancelAutoModeBtn.Name = "cancelAutoModeBtn";
			this.cancelAutoModeBtn.Size = new System.Drawing.Size(75, 50);
			this.cancelAutoModeBtn.TabIndex = 16;
			this.cancelAutoModeBtn.Text = "Cancel Auto";
			this.cancelAutoModeBtn.UseVisualStyleBackColor = true;
			this.cancelAutoModeBtn.Click += new System.EventHandler(this.cancelAutoModeBtn_Click);
			// 
			// debugOptions
			// 
			this.debugOptions.AutoSize = true;
			this.debugOptions.Location = new System.Drawing.Point(12, 155);
			this.debugOptions.Name = "debugOptions";
			this.debugOptions.Size = new System.Drawing.Size(97, 17);
			this.debugOptions.TabIndex = 17;
			this.debugOptions.Text = "Debug Options";
			this.debugOptions.UseVisualStyleBackColor = true;
			this.debugOptions.CheckedChanged += new System.EventHandler(this.debugOptions_CheckedChanged);
			// 
			// currentRodTimerLblDebug
			// 
			this.currentRodTimerLblDebug.Location = new System.Drawing.Point(366, 12);
			this.currentRodTimerLblDebug.Name = "currentRodTimerLblDebug";
			this.currentRodTimerLblDebug.Size = new System.Drawing.Size(100, 15);
			this.currentRodTimerLblDebug.TabIndex = 18;
			this.currentRodTimerLblDebug.Text = "Current Rod Timer:";
			// 
			// rodTimerDebug
			// 
			this.rodTimerDebug.Location = new System.Drawing.Point(366, 31);
			this.rodTimerDebug.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
			this.rodTimerDebug.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.rodTimerDebug.Name = "rodTimerDebug";
			this.rodTimerDebug.Size = new System.Drawing.Size(100, 20);
			this.rodTimerDebug.TabIndex = 19;
			this.rodTimerDebug.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.rodTimerDebug.ValueChanged += new System.EventHandler(this.rodTimerDebug_ValueChanged);
			// 
			// rodTimerDebugToolTip
			// 
			this.rodTimerDebugToolTip.AutomaticDelay = 100;
			this.rodTimerDebugToolTip.AutoPopDelay = 10000;
			this.rodTimerDebugToolTip.InitialDelay = 100;
			this.rodTimerDebugToolTip.ReshowDelay = 20;
			this.rodTimerDebugToolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
			this.rodTimerDebugToolTip.ToolTipTitle = "Casting Timer Adjustment";
			// 
			// backgroundThreadGetTimes
			// 
			this.backgroundThreadGetTimes.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundThreadGetTimes_DoWork);
			// 
			// getTimesDedubToolTip
			// 
			this.getTimesDedubToolTip.AutomaticDelay = 100;
			this.getTimesDedubToolTip.AutoPopDelay = 10000;
			this.getTimesDedubToolTip.InitialDelay = 100;
			this.getTimesDedubToolTip.ReshowDelay = 20;
			this.getTimesDedubToolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
			this.getTimesDedubToolTip.ToolTipTitle = "Get Times Function";
			// 
			// saveScreenshotButton
			// 
			this.saveScreenshotButton.Location = new System.Drawing.Point(366, 103);
			this.saveScreenshotButton.Name = "saveScreenshotButton";
			this.saveScreenshotButton.Size = new System.Drawing.Size(100, 23);
			this.saveScreenshotButton.TabIndex = 20;
			this.saveScreenshotButton.Text = "Save screenshot";
			this.saveScreenshotButton.UseVisualStyleBackColor = true;
			this.saveScreenshotButton.Click += new System.EventHandler(this.saveScreenshotButton_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(484, 311);
			this.Controls.Add(this.saveScreenshotButton);
			this.Controls.Add(this.rodTimerDebug);
			this.Controls.Add(this.currentRodTimerLblDebug);
			this.Controls.Add(this.debugOptions);
			this.Controls.Add(this.cancelAutoModeBtn);
			this.Controls.Add(this.baitToUseText);
			this.Controls.Add(this.baitToUseLabel);
			this.Controls.Add(this.debugAutoStepLbl);
			this.Controls.Add(this.autoBtn);
			this.Controls.Add(this.getTimesBtn);
			this.Controls.Add(this.platformGroupBox);
			this.Controls.Add(this.rodChoiceGroupBox);
			this.Controls.Add(this.castCatchLocationLbl);
			this.Controls.Add(this.findLocationBtn);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Form1";
			this.Text = "Fisher v1.2.3";
			this.rodChoiceGroupBox.ResumeLayout(false);
			this.rodChoiceGroupBox.PerformLayout();
			this.platformGroupBox.ResumeLayout(false);
			this.platformGroupBox.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.baitToUseText)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.rodTimerDebug)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button findLocationBtn;
        private System.Windows.Forms.Label castCatchLocationLbl;
        private System.Windows.Forms.GroupBox rodChoiceGroupBox;
        private System.Windows.Forms.RadioButton flyRod;
        private System.Windows.Forms.RadioButton spinningRod;
        private System.Windows.Forms.RadioButton trollingRod;
        private System.Windows.Forms.RadioButton woodFishingRod;
        private System.Windows.Forms.RadioButton legRod;
        private System.Windows.Forms.GroupBox platformGroupBox;
        private System.Windows.Forms.RadioButton steamButton;
        private System.Windows.Forms.RadioButton kongButton;
        private System.Windows.Forms.Button getTimesBtn;
        private System.Windows.Forms.Button autoBtn;
        private System.Windows.Forms.Label debugAutoStepLbl;
        private System.Windows.Forms.Label baitToUseLabel;
        private System.Windows.Forms.NumericUpDown baitToUseText;
		private System.ComponentModel.BackgroundWorker backgroundThread;
		private System.Windows.Forms.Button cancelAutoModeBtn;
		private System.Windows.Forms.CheckBox debugOptions;
		private System.Windows.Forms.Label currentRodTimerLblDebug;
		private System.Windows.Forms.NumericUpDown rodTimerDebug;
		private System.Windows.Forms.ToolTip rodTimerDebugToolTip;
		private System.ComponentModel.BackgroundWorker backgroundThreadGetTimes;
		private System.Windows.Forms.ToolTip getTimesDedubToolTip;
		private System.Windows.Forms.Button saveScreenshotButton;
	}
}

