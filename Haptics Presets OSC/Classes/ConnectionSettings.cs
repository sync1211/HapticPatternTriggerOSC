using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haptics_Presets_OSC.Classes
{
    public class ConnectionSettings
    {
        public string TargetIP { get; set; } = "127.0.0.1";
        public int SenderPort { get; set; } = 9009;
        public int ReceiverPort { get; set; } = 9008;
    }
}
