using Newtonsoft.Json;

namespace Haptics_Presets_OSC.Classes
{
    public class HapticPattern
    {
        public bool Enabled { get; set; } = true;
        public string Name { get; }
        public readonly Uri PatternFile;

        [JsonIgnore]
        public string Path
        {
            get
            {
                return PatternFile.LocalPath;
            }
        }
        public string Parameter { get; set; }

        public HapticPattern(string name, Uri path, string parameter)
        {
            Name = name;
            PatternFile = path;
            Parameter = parameter;
        }

        [JsonConstructor]
        public HapticPattern(bool enabled, string name, Uri patternFile, string parameter)
        {
            Enabled = enabled;
            Name = name;
            PatternFile = patternFile;
            Parameter = parameter;
        }
    }
}
