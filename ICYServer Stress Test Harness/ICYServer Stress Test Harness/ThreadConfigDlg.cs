using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ICYServer_Stress_Test_Harness
{
    public partial class ThreadConfigDlg : Form
    {
        public ThreadConfigDlg()
        {
            InitializeComponent();

            minRequestIntervalTextBox.ValidatingType = typeof(double);
            maxRequestIntervalTextBox.ValidatingType = typeof(double);
        }

        public int MaxThreads
        {
            get { return Convert.ToInt16(maxThreadsNumeric.Value); }
            set { maxThreadsNumeric.Value = value; }
        }

        public int MaxRequestsPerThread
        {
            get { return Convert.ToInt16(maxRequestNumeric.Value); }
            set { maxRequestNumeric.Value = value; }
        }

        public int BurstThreshold
        {
            get { return Convert.ToInt16(burstThresholdNumeric.Value); }
            set { burstThresholdNumeric.Value = value; }
        }

        public int BurstChance
        {
            get { return Convert.ToInt16(burstChanceNumeric.Value); }
            set { burstChanceNumeric.Value = value; }
        }


        public double MinRequestInterval
        {
            get 
            {
                string value = minRequestIntervalTextBox.MaskedTextProvider.ToString();

                return Convert.ToDouble(value);
            }

            set
            {
                string formattedText = string.Format("{0:0000.00}", value);
                minRequestIntervalTextBox.Text = formattedText;
            }
        }

        public double MaxRequestInterval
        {
            get
            {
                string value = maxRequestIntervalTextBox.MaskedTextProvider.ToString();

                return Convert.ToDouble(value);
            }

            set
            {
                string formattedText = string.Format("{0:0000.00}", value);
                maxRequestIntervalTextBox.Text = formattedText;
            }
        }

    }
}