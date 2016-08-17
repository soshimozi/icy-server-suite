using System;
using System.Collections.Generic;
using System.Text;
using SocketServer;
using ObjectEventQueue;

namespace TelnetTest
{
    public class SimulationManager
    {
        ConnectionManager<TelnetProtocol> _connectionManager;
        ListeningManager _listenManager;

        public SimulationManager()
        {
            AsynchObjectEvent<SocketMessage> serverEvent = new AsynchObjectEvent<SocketMessage>();
            ObjectEventManager.SubscribeEvent<SocketMessage>(serverEvent, new ObjectEventDelegate<SocketMessage>(MessageHandler), null);

            _connectionManager = new ConnectionManager<TelnetProtocol>();
            _listenManager = new ListeningManager();
        }

        public void StartSimulation()
        {
            _listenManager.AddPort(4000);
        }

        public void StopSimulation()
        {
        }

        private void MessageHandler(string eventName, SocketMessage message)
        {
            switch (message.MessageCode)
            {
                case ServerEvent.IncommingData:
                    break;
            }
        }

    }
}
