namespace ICYServer_Stress_Test_Harness
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.simulationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.requestsOutstandingStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.requestCompleteStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.requestTotalBytesStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.requestTotalTimeStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.requestAverageTimeStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.throughputStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.requestErrorCountLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.workingThreadCountStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.simulationToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(952, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // simulationToolStripMenuItem
            // 
            this.simulationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configurationToolStripMenuItem,
            this.startToolStripMenuItem,
            this.stopToolStripMenuItem});
            this.simulationToolStripMenuItem.Name = "simulationToolStripMenuItem";
            this.simulationToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.simulationToolStripMenuItem.Text = "Simulation";
            // 
            // configurationToolStripMenuItem
            // 
            this.configurationToolStripMenuItem.Name = "configurationToolStripMenuItem";
            this.configurationToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.configurationToolStripMenuItem.Text = "Configuration...";
            this.configurationToolStripMenuItem.Click += new System.EventHandler(this.configurationToolStripMenuItem_Click);
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Enabled = false;
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // textBox1
            // 
            this.textBox1.AcceptsReturn = true;
            this.textBox1.AcceptsTab = true;
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(0, 24);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(952, 512);
            this.textBox1.TabIndex = 1;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.requestsOutstandingStatusLabel,
            this.requestCompleteStatusLabel,
            this.requestTotalBytesStatusLabel,
            this.requestTotalTimeStatusLabel,
            this.requestAverageTimeStatusLabel,
            this.throughputStatusLabel,
            this.requestErrorCountLabel,
            this.workingThreadCountStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 536);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(952, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // requestsOutstandingStatusLabel
            // 
            this.requestsOutstandingStatusLabel.Name = "requestsOutstandingStatusLabel";
            this.requestsOutstandingStatusLabel.Size = new System.Drawing.Size(109, 17);
            this.requestsOutstandingStatusLabel.Text = "toolStripStatusLabel1";
            // 
            // requestCompleteStatusLabel
            // 
            this.requestCompleteStatusLabel.Name = "requestCompleteStatusLabel";
            this.requestCompleteStatusLabel.Size = new System.Drawing.Size(109, 17);
            this.requestCompleteStatusLabel.Text = "toolStripStatusLabel1";
            // 
            // requestTotalBytesStatusLabel
            // 
            this.requestTotalBytesStatusLabel.Name = "requestTotalBytesStatusLabel";
            this.requestTotalBytesStatusLabel.Size = new System.Drawing.Size(109, 17);
            this.requestTotalBytesStatusLabel.Text = "toolStripStatusLabel2";
            // 
            // requestTotalTimeStatusLabel
            // 
            this.requestTotalTimeStatusLabel.Name = "requestTotalTimeStatusLabel";
            this.requestTotalTimeStatusLabel.Size = new System.Drawing.Size(109, 17);
            this.requestTotalTimeStatusLabel.Text = "toolStripStatusLabel3";
            // 
            // requestAverageTimeStatusLabel
            // 
            this.requestAverageTimeStatusLabel.Name = "requestAverageTimeStatusLabel";
            this.requestAverageTimeStatusLabel.Size = new System.Drawing.Size(109, 17);
            this.requestAverageTimeStatusLabel.Text = "toolStripStatusLabel4";
            // 
            // throughputStatusLabel
            // 
            this.throughputStatusLabel.Name = "throughputStatusLabel";
            this.throughputStatusLabel.Size = new System.Drawing.Size(109, 17);
            this.throughputStatusLabel.Text = "toolStripStatusLabel1";
            // 
            // requestErrorCountLabel
            // 
            this.requestErrorCountLabel.Name = "requestErrorCountLabel";
            this.requestErrorCountLabel.Size = new System.Drawing.Size(109, 17);
            this.requestErrorCountLabel.Text = "toolStripStatusLabel1";
            // 
            // workingThreadCountStatusLabel
            // 
            this.workingThreadCountStatusLabel.Name = "workingThreadCountStatusLabel";
            this.workingThreadCountStatusLabel.Size = new System.Drawing.Size(109, 17);
            this.workingThreadCountStatusLabel.Text = "toolStripStatusLabel1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(952, 558);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ToolStripMenuItem simulationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel requestsOutstandingStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel requestTotalBytesStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel requestTotalTimeStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel requestAverageTimeStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel requestErrorCountLabel;
        private System.Windows.Forms.ToolStripMenuItem configurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel workingThreadCountStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel requestCompleteStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel throughputStatusLabel;
    }
}

