using BuildSoft.OscCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haptics_Presets_OSC.Classes
{
    public class OSCMessageEventArgs
    {
        public readonly HapticPattern Pattern;
        public readonly OscMessageValues Values;

        public OSCMessageEventArgs(HapticPattern pattern, OscMessageValues values)
        {
            Pattern = pattern;
            Values = values;
        }
    }
}
