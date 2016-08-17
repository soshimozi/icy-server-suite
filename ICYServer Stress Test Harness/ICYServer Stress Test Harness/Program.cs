using System;
using System.Collections.Generic;
using System.Windows.Forms;
using log4net;

namespace ICYServer_Stress_Test_Harness
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                Application.Run(new Form1());
            }
            catch (Exception exception)
            {
                ILog logger = LogManager.GetLogger(typeof(Program));
                logger.Error(exception);
            }
        }
    }
}