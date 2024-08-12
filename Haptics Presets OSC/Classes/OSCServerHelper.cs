using BuildSoft.OscCore;

namespace Haptics_Presets_OSC.Classes
{
    public class OSCServerHelper(ConnectionSettings connectionSettings) : IDisposable
    {
        private readonly OscClient client = new(connectionSettings.TargetIP, connectionSettings.SenderPort);
        private readonly OscServer server = new(connectionSettings.ReceiverPort);
        private bool disposed;
        public bool IsRunning
        {
            get
            {
                return !disposed;
            }
        }

        public EventHandler<OSCMessageEventArgs>? OnMessageReceived;

        public void Start(IEnumerable<HapticPattern> patterns)
        {
            // Start the OSC server
            foreach (HapticPattern pattern in patterns)
            {
                server.TryAddMethod(
                    pattern.Parameter,
                    (OscMessageValues values) => OnMessageReceived?.Invoke(this, new OSCMessageEventArgs(pattern, values))
                );
            }

            server.Start();
        }

        public void Send(HapticPattern pattern, bool value)
        {
            client.Send(pattern.Parameter, value);
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