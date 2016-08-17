using System;
using System.Collections.Generic;
using System.Text;
using log4net;
using SMarTICY;
using System.Configuration;

namespace SMarTICY_Test
{
    class Program
    {
        static ILog logger = LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {
            ICYServer server = new ICYServer(ConfigurationManager.AppSettings["mount_location"]);
            int port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);

            Console.CursorVisible = false;
            int spinnerCounter = 0;
            server.StartServer(port);
            while (!(Console.KeyAvailable && Console.ReadKey().Key == ConsoleKey.Q))
            {
                Console.CursorLeft = 0;
                WriteSpinner(spinnerCounter++ % 4);
                Console.CursorLeft = 0;

                System.Threading.Thread.Sleep(100);
            }
            server.StopServer();
        }


        static private void WriteSpinner(int state)
        {
            switch (state)
            {
                case 0:
                    Console.Write("Waiting for connections |");
                    break;

                case 1:
                    Console.Write("Waiting for connections /");
                    break;

                case 2:
                    Console.Write("Waiting for connections -");
                    break;

                case 3:
                    Console.Write("Waiting for connections \\");
                    break;
            }
        }

    }
}
