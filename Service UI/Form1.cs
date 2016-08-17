using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.ServiceProcess;
using System.Reflection;
using System.IO;
using System.Diagnostics;

namespace Service_UI
{
    public partial class Form1 : Form
    {
        private delegate void UpdateLabelDelegate();
        private delegate void UpdateButtonDelegate();

        ManualResetEvent stopEvent = new ManualResetEvent(false);

        bool error = false;
        bool serverRunning = false;
        ServiceController icyController = null;
        string servicePath = string.Empty;
        public Form1()
        {
            InitializeComponent();

            // get system control information
            ServiceController[] controllers = ServiceController.GetServices();

            // see if our controller is in there
            foreach (ServiceController controller in controllers)
            {
                if (controller.ServiceName.Equals("ICYService"))
                {
                    icyController = controller;
                }
            }

            if (icyController == null)
            {
                error = true;
                editToolStripMenuItem1.Enabled = false;
            }
            else
            {
                error = false;
            }
        }

        private void UpdateThread()
        {
            while (!stopEvent.WaitOne(100, false))
            {
                if (icyController != null)
                {
                    icyController.Refresh();

                    if (icyController.Status == ServiceControllerStatus.Running)
                    {
                        serverRunning = true;
                    }
                    else
                    {
                        serverRunning = false;
                    }

                    Invalidate();
                }
            }
        }

        private void updateButton()
        {
            if (Visible)
            {
                if (buttonControl.InvokeRequired)
                {
                    buttonControl.Invoke(new UpdateButtonDelegate(updateButton));
                }
                else
                {
                    if (error)
                    {
                        buttonControl.Enabled = false;
                    }
                    else
                    {
                        if (serverRunning)
                        {
                            buttonControl.Text = "Stop Server";
                        }
                        else
                        {
                            buttonControl.Text = "Start Server";
                        }
                    }
                }
            }
        }

        private void updateStatusLabel()
        {
            if (Visible)
            {
                if (statusLabel.InvokeRequired)
                {
                    statusLabel.Invoke(new UpdateButtonDelegate(updateStatusLabel));
                }
                else
                {
                    if (error)
                    {
                        statusLabel.Text = "Error establishing connection to server.";
                        statusLabel.ForeColor = Color.Red;
                    }
                    else
                    {
                        string verb = "";
                        System.Drawing.Color color = Color.Red;
                        switch (icyController.Status)
                        {
                            case ServiceControllerStatus.ContinuePending:
                                color = Color.Red;
                                verb = "Continuing";
                                break;

                            case ServiceControllerStatus.Paused:
                                color = Color.Yellow;
                                verb = "Paused";
                                break;

                            case ServiceControllerStatus.PausePending:
                                color = Color.Yellow;
                                verb = "Pausing";
                                break;

                            case ServiceControllerStatus.StartPending:
                                color = Color.Red;
                                verb = "Starting";
                                break;

                            case ServiceControllerStatus.Running:
                                color = Color.Green;
                                verb = "Running";
                                break;

                            case ServiceControllerStatus.StopPending:
                                color = Color.Green;
                                verb = "Stopping";
                                break;

                            case ServiceControllerStatus.Stopped:
                                color = Color.Red;
                                verb = "Stopped";
                                break;
                        }

                        statusLabel.Text = string.Format("Server is {0}.", verb);
                        statusLabel.ForeColor = color;
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!error)
            {
                Thread thread = new Thread(new ThreadStart(UpdateThread));
                thread.Start();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            // signal ui thread to stop
            stopEvent.Set();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            updateButton();
            updateStatusLabel();
        }

        private void buttonControl_Click(object sender, EventArgs e)
        {
            icyController.Refresh();
            if (icyController.Status == ServiceControllerStatus.Running)
            {
                icyController.Stop();
            }
            else
            {
                icyController.Start();
            }
        }

        private void editToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (icyController != null && icyController.Status != ServiceControllerStatus.Stopped)
            {
                MessageBox.Show("Server must be stopped to edit file.");
            }
            else
            {
                string fileName = string.Format("{0}\\config\\server.xml", Application.StartupPath);
                if (File.Exists(fileName))
                {
                    Process.Start("notepad.exe", fileName);
                }
                else
                {
                    MessageBox.Show("Server configuration file not found.", "Invalid Configuration");
                }
            }
        }
    }
}