using Newtonsoft.Json;

namespace Haptics_Presets_OSC.Classes
{
    public class HapticsManager
    {
        private const string PatternDirectory = ".Patterns";
        private const string ParameterBasePath = "/avatar/parameters/Haptics/Patterns/";
        public readonly Dictionary<string, HapticPattern> Patterns = [];

        private Uri CreatePatternDirectory()
        {
            Uri patternDirectoryPath = new(Path.Combine(Directory.GetCurrentDirectory(), PatternDirectory));
            if (!Directory.Exists(patternDirectoryPath.LocalPath))
            {
                Directory.CreateDirectory(patternDirectoryPath.LocalPath);
            }

            return patternDirectoryPath;
        }

        public void SavePatterns()
        {
            Uri patternDirectoryPath = CreatePatternDirectory();

            List<HapticPattern> patterns = [.. Patterns.Values];

            string patternsJson = JsonConvert.SerializeObject(patterns, Formatting.Indented);

            string patternsJsonPath = Path.Combine(patternDirectoryPath.LocalPath, "patterns.json");
            File.WriteAllText(patternsJsonPath, patternsJson);
        }

        public void LoadPatterns()
        {
            Uri patternDirectoryPath = CreatePatternDirectory();

            string patternsJsonPath = Path.Combine(patternDirectoryPath.LocalPath, "patterns.json");

            if (!File.Exists(patternsJsonPath))
            {
                return;
            }

            string patternsJson = File.ReadAllText(patternsJsonPath);

            List<HapticPattern>? patterns = JsonConvert.DeserializeObject<List<HapticPattern>>(patternsJson);

            if (patterns == null)
            {
                MessageBox.Show("Unable to load saved patterns");
                return;
            }

            foreach (HapticPattern pattern in patterns)
            {
                Patterns.Add(pattern.Name, pattern);
            }
        }

        public void ImportPattern(Uri patternFile)
        {
            Uri patternDirectoryPath = CreatePatternDirectory();

            string patternName = Path.GetFileNameWithoutExtension(patternFile.LocalPath);
            string patternFileName = Path.GetFileName(patternFile.LocalPath);
            string patternPath = Path.Combine(patternDirectoryPath.LocalPath, patternFileName);

            // Check if the pattern already exists
            if (Patterns.ContainsKey(patternName))
            {
                MessageBox.Show("Pattern already exists");
                return;
            }

            // Check if the pattern file already exists
            if (File.Exists(patternPath))
            {
                DialogResult result = MessageBox.Show($"Pattern already exists.{Environment.NewLine}Do you want to replace the existing file?", "File exists", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
                {
                    return;
                }

                File.Delete(patternPath);
            }

            // Copy pattern to the pattern directory
            File.Copy(patternFile.LocalPath, patternPath);

            // Save pattern info
            Patterns.Add(patternName, new HapticPattern(patternName, new Uri(patternPath), ParameterBasePath + patternName));

            SavePatterns();
        }

        public void RemovePattern(string patternName)
        {
            if (!Patterns.ContainsKey(patternName))
            {
                return;
            }

            HapticPattern pattern = Patterns[patternName];
            Patterns.Remove(patternName);

            File.Delete(pattern.Path);

            SavePatterns();
        }
    }
}
