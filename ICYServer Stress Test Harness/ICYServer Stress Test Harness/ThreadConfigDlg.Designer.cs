namespace ICYServer_Stress_Test_Harness
{
    partial class ThreadConfigDlg
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.maxThreadsNumeric = new System.Windows.Forms.NumericUpDown();
            this.maxRequestNumeric = new System.Windows.Forms.NumericUpDown();
            this.burstThresholdNumeric = new System.Windows.Forms.NumericUpDown();
            this.acceptButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.burstChanceNumeric = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.minRequestIntervalTextBox = new System.Windows.Forms.MaskedTextBox();
            this.maxRequestIntervalTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.maxThreadsNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxRequestNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.burstThresholdNumeric)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.burstChanceNumeric)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(79, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Threads In Pool:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Max Request Per Thread:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(80, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Burst Threshold:";
            // 
            // maxThreadsNumeric
            // 
            this.maxThreadsNumeric.Location = new System.Drawing.Point(163, 16);
            this.maxThreadsNumeric.Name = "maxThreadsNumeric";
            this.maxThreadsNumeric.Size = new System.Drawing.Size(82, 20);
            this.maxThreadsNumeric.TabIndex = 3;
            // 
            // maxRequestNumeric
            // 
            this.maxRequestNumeric.Location = new System.Drawing.Point(163, 46);
            this.maxRequestNumeric.Name = "maxRequestNumeric";
            this.maxRequestNumeric.Size = new System.Drawing.Size(82, 20);
            this.maxRequestNumeric.TabIndex = 4;
            // 
            // burstThresholdNumeric
            // 
            this.burstThresholdNumeric.Location = new System.Drawing.Point(163, 76);
            this.burstThresholdNumeric.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.burstThresholdNumeric.Name = "burstThresholdNumeric";
            this.burstThresholdNumeric.Size = new System.Drawing.Size(82, 20);
            this.burstThresholdNumeric.TabIndex = 5;
            // 
            // acceptButton
            // 
            this.acceptButton.Cursor = System.Windows.Forms.Cursors.Default;
            this.acceptButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.acceptButton.Location = new System.Drawing.Point(25, 248);
            this.acceptButton.Name = "acceptButton";
            this.acceptButton.Size = new System.Drawing.Size(75, 23);
            this.acceptButton.TabIndex = 6;
            this.acceptButton.Text = "Accept";
            this.acceptButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(193, 248);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.burstChanceNumeric);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.maxRequestNumeric);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.burstThresholdNumeric);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.maxThreadsNumeric);
            this.groupBox1.Location = new System.Drawing.Point(6, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(280, 136);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thread Configuration";
            // 
            // burstChanceNumeric
            // 
            this.burstChanceNumeric.Location = new System.Drawing.Point(163, 106);
            this.burstChanceNumeric.Name = "burstChanceNumeric";
            this.burstChanceNumeric.Size = new System.Drawing.Size(82, 20);
            this.burstChanceNumeric.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(90, 110);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Burst Chance:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.minRequestIntervalTextBox);
            this.groupBox2.Controls.Add(this.maxRequestIntervalTextBox);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(6, 160);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(280, 80);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Request Configuration";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(205, 56);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "seconds";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(205, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "seconds";
            // 
            // minRequestIntervalTextBox
            // 
            this.minRequestIntervalTextBox.Location = new System.Drawing.Point(141, 16);
            this.minRequestIntervalTextBox.Mask = "0000.00";
            this.minRequestIntervalTextBox.Name = "minRequestIntervalTextBox";
            this.minRequestIntervalTextBox.Size = new System.Drawing.Size(56, 20);
            this.minRequestIntervalTextBox.TabIndex = 4;
            // 
            // maxRequestIntervalTextBox
            // 
            this.maxRequestIntervalTextBox.Location = new System.Drawing.Point(141, 52);
            this.maxRequestIntervalTextBox.Mask = "0000.00";
            this.maxRequestIntervalTextBox.Name = "maxRequestIntervalTextBox";
            this.maxRequestIntervalTextBox.Size = new System.Drawing.Size(56, 20);
            this.maxRequestIntervalTextBox.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Max Request Interval:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Min Request Interval:";
            // 
            // ThreadConfigDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 276);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.acceptButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ThreadConfigDlg";
            this.Text = "Simulation Configuration";
            ((System.ComponentModel.ISupportInitialize)(this.maxThreadsNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxRequestNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.burstThresholdNumeric)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.burstChanceNumeric)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown maxThreadsNumeric;
        private System.Windows.Forms.NumericUpDown maxRequestNumeric;
        private System.Windows.Forms.NumericUpDown burstThresholdNumeric;
        private System.Windows.Forms.Button acceptButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.MaskedTextBox minRequestIntervalTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.MaskedTextBox maxRequestIntervalTextBox;
        private System.Windows.Forms.NumericUpDown burstChanceNumeric;
        private System.Windows.Forms.Label label8;
    }
}