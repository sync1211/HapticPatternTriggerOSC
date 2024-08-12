﻿using BuildSoft.OscCore;
using System;
using System.Linq;

namespace Haptics_Presets_OSC.Classes
{
    public class OSCMessageEventArgs(HapticPattern pattern, OscMessageValues values)
    {
        public readonly HapticPattern Pattern = pattern;
        public readonly OscMessageValues Values = values;
    }
}
