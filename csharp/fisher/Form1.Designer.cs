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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.findLocationBtn = new System.Windows.Forms.Button();
			this.castCatchLocationLbl = new System.Windows.Forms.Label();
			this.platformGroupBox = new System.Windows.Forms.GroupBox();
			this.kartridgeButton = new System.Windows.Forms.RadioButton();
			this.steamButton = new System.Windows.Forms.RadioButton();
			this.kongButton = new System.Windows.Forms.RadioButton();
			this.autoBtn = new System.Windows.Forms.Button();
			this.debugAutoStepLbl = new System.Windows.Forms.Label();
			this.baitToUseLabel = new System.Windows.Forms.Label();
			this.baitToUseText = new System.Windows.Forms.NumericUpDown();
			this.backgroundThread = new System.ComponentModel.BackgroundWorker();
			this.cancelAutoModeBtn = new System.Windows.Forms.Button();
			this.backgroundThreadGetTimes = new System.ComponentModel.BackgroundWorker();
			this.platformGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.baitToUseText)).BeginInit();
			this.SuspendLayout();
			//
			// findLocationBtn
			//
			this.findLocationBtn.Location = new System.Drawing.Point(12, 113);
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
			this.castCatchLocationLbl.Location = new System.Drawing.Point(187, 113);
			this.castCatchLocationLbl.Name = "castCatchLocationLbl";
			this.castCatchLocationLbl.Size = new System.Drawing.Size(160, 50);
			this.castCatchLocationLbl.TabIndex = 1;
			this.castCatchLocationLbl.Text = "Cast/Catch Location:";
			this.castCatchLocationLbl.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			//
			// platformGroupBox
			//
			this.platformGroupBox.Controls.Add(this.kartridgeButton);
			this.platformGroupBox.Controls.Add(this.steamButton);
			this.platformGroupBox.Controls.Add(this.kongButton);
			this.platformGroupBox.Location = new System.Drawing.Point(12, 12);
			this.platformGroupBox.Name = "platformGroupBox";
			this.platformGroupBox.Size = new System.Drawing.Size(160, 96);
			this.platformGroupBox.TabIndex = 5;
			this.platformGroupBox.TabStop = false;
			this.platformGroupBox.Text = "Platform:";
			//
			// kartridgeButton
			//
			this.kartridgeButton.AutoSize = true;
			this.kartridgeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(22)))), ((int)(((byte)(220)))));
			this.kartridgeButton.ForeColor = System.Drawing.Color.White;
			this.kartridgeButton.Location = new System.Drawing.Point(7, 42);
			this.kartridgeButton.Name = "kartridgeButton";
			this.kartridgeButton.Size = new System.Drawing.Size(67, 17);
			this.kartridgeButton.TabIndex = 2;
			this.kartridgeButton.Text = "Kartridge";
			this.kartridgeButton.UseVisualStyleBackColor = false;
			//
			// steamButton
			//
			this.steamButton.AutoSize = true;
			this.steamButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(173)))), ((int)(((byte)(238)))));
			this.steamButton.Location = new System.Drawing.Point(7, 65);
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
			// autoBtn
			//
			this.autoBtn.Location = new System.Drawing.Point(12, 169);
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
			this.debugAutoStepLbl.Location = new System.Drawing.Point(187, 169);
			this.debugAutoStepLbl.Name = "debugAutoStepLbl";
			this.debugAutoStepLbl.Size = new System.Drawing.Size(160, 50);
			this.debugAutoStepLbl.TabIndex = 12;
			this.debugAutoStepLbl.Text = "0/0 bait used.";
			this.debugAutoStepLbl.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			//
			// baitToUseLabel
			//
			this.baitToUseLabel.Location = new System.Drawing.Point(187, 31);
			this.baitToUseLabel.Name = "baitToUseLabel";
			this.baitToUseLabel.Size = new System.Drawing.Size(103, 16);
			this.baitToUseLabel.TabIndex = 14;
			this.baitToUseLabel.Text = "Bait to use:";
			//
			// baitToUseText
			//
			this.baitToUseText.Location = new System.Drawing.Point(187, 48);
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
			this.baitToUseText.Size = new System.Drawing.Size(160, 20);
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
			this.cancelAutoModeBtn.Location = new System.Drawing.Point(97, 169);
			this.cancelAutoModeBtn.Name = "cancelAutoModeBtn";
			this.cancelAutoModeBtn.Size = new System.Drawing.Size(75, 50);
			this.cancelAutoModeBtn.TabIndex = 16;
			this.cancelAutoModeBtn.Text = "Cancel Auto";
			this.cancelAutoModeBtn.UseVisualStyleBackColor = true;
			this.cancelAutoModeBtn.Click += new System.EventHandler(this.cancelAutoModeBtn_Click);
			//
			// Form1
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(355, 227);
			this.Controls.Add(this.cancelAutoModeBtn);
			this.Controls.Add(this.baitToUseText);
			this.Controls.Add(this.baitToUseLabel);
			this.Controls.Add(this.debugAutoStepLbl);
			this.Controls.Add(this.autoBtn);
			this.Controls.Add(this.platformGroupBox);
			this.Controls.Add(this.castCatchLocationLbl);
			this.Controls.Add(this.findLocationBtn);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Form1";
			this.Text = "Fisher v1.4.0";
			this.platformGroupBox.ResumeLayout(false);
			this.platformGroupBox.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.baitToUseText)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button findLocationBtn;
        private System.Windows.Forms.Label castCatchLocationLbl;
        private System.Windows.Forms.GroupBox platformGroupBox;
        private System.Windows.Forms.RadioButton steamButton;
        private System.Windows.Forms.RadioButton kongButton;
        private System.Windows.Forms.Button autoBtn;
        private System.Windows.Forms.Label debugAutoStepLbl;
        private System.Windows.Forms.Label baitToUseLabel;
        private System.Windows.Forms.NumericUpDown baitToUseText;
		private System.ComponentModel.BackgroundWorker backgroundThread;
		private System.Windows.Forms.Button cancelAutoModeBtn;
		private System.ComponentModel.BackgroundWorker backgroundThreadGetTimes;
		private System.Windows.Forms.RadioButton kartridgeButton;
	}
}
