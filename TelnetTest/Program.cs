using System;
using System.Collections.Generic;
using System.Text;
using ObjectEventQueue;
using SocketServer;

namespace TelnetTest
{
    class Program
    {
        static bool done = false;

        static SimulationManager simulation = new SimulationManager();
        static void Main(string[] args)
        {
            simulation.StartSimulation();

            Console.WriteLine("Server started and listening on port 4000.");

            while (!done)
            {
                if (Console.KeyAvailable && Console.ReadKey().Key == ConsoleKey.Q)
                {
                    done = true;
                }
                else
                {
                    System.Threading.Thread.Sleep(250);
                }
            }
        }
    }
}
