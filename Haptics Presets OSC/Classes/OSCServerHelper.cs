using BuildSoft.OscCore;

namespace Haptics_Presets_OSC.Classes
{
    public class OSCServerHelper: IDisposable
    {
        private readonly OscClient client;
        private readonly OscServer server;
        private bool disposed;
        public bool IsRunning
        {
            get
            {
                return !disposed;
            }
        }

        public EventHandler<OSCMessageEventArgs>? OnMessageReceived;

        public OSCServerHelper(ConnectionSettings connectionSettings)
        {
            server = new OscServer(connectionSettings.ReceiverPort);
            client = new OscClient(connectionSettings.TargetIP, connectionSettings.SenderPort);
        }

        public void Start(IEnumerable<HapticPattern> patterns)
        {
            // Start the OSC server
            foreach (HapticPattern pattern in patterns)
            {
                server.TryAddMethod(
                    pattern.Parameter,
                    (OscMessageValues values) => 
                    {
                        OnMessageReceived?.Invoke(this, new OSCMessageEventArgs(pattern, values));
                        client.Send(pattern.Parameter, false);
                    }
                );
            }

            server.Start();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                client.Dispose();
                server.Dispose();
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }
    }
}