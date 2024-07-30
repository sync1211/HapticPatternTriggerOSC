using bHapticsLib;
using Haptics_Presets_OSC.Classes;
using BuildSoft.OscCore;
using Newtonsoft.Json;
using System.Net;

namespace Haptics_Presets_OSC
{
    public partial class PresetsForm : Form
    {
        private const string SettingsFile = "settings.json";
        private readonly HapticsManager manager = new();
        private ConnectionSettings connectionSettings = new();
        private OSCServerHelper? oscServer;

        public PresetsForm()
        {
            InitializeComponent();

            IPInput.ValidatingType = typeof(System.Net.IPAddress);
            SenderPortInput.ValidatingType = typeof(int);
            ReceiverPortInput.ValidatingType = typeof(int);
            dataGridView1.AllowUserToDeleteRows = true;

            dataGridView1.CellContentClick += DataGridView1_CellContentClick;
        }

        private void AddPatternButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new()
            {
                Filter = "Pattern Files (*.tact)|",
                Title = "Select a pattern file"
            };

            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            string selectedFile = openFileDialog.FileName;
            manager.ImportPattern(new Uri(selectedFile));

            RefreshGrid();
        }

        private void RefreshGrid()
        {
            dataGridView1.DataSource = (List<Classes.HapticPattern>)([.. manager.Patterns.Values]);

            DataGridViewButtonColumn uninstallButtonColumn = new()
            {
                Name = "deleteColumn",
                Text = "X",
                HeaderText = "Delete",
                Width = 50,
                Resizable = DataGridViewTriState.False,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            };
            DataGridViewButtonColumn testButtonColumn = new()
            {
                Name = "testColumn",
                Text = "T",
                HeaderText = "Test",
                Width = 50,
                Resizable = DataGridViewTriState.False,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            };



            if (dataGridView1.Columns["testColumn"] == null)
            {
                dataGridView1.Columns.Insert(0, testButtonColumn);
            }

            if (dataGridView1.Columns["deleteColumn"] == null)
            {
                dataGridView1.Columns.Insert(5, uninstallButtonColumn);
            }
        }

        private void PresetsForm_Load(object sender, EventArgs e)
        {
            manager.LoadPatterns();
            LoadSettings();

            IPInput.Text = connectionSettings.TargetIP;
            SenderPortInput.Text = connectionSettings.SenderPort.ToString();
            ReceiverPortInput.Text = connectionSettings.ReceiverPort.ToString();
            bHapticsManager.Connect("bHapticsLib", "HapticsPresetsOSC", maxRetries: 0);

            RefreshGrid();
        }

        private void SaveSettings()
        {
            string settingsFilePath = Path.Combine(Directory.GetCurrentDirectory(), SettingsFile);
            string settingsString = JsonConvert.SerializeObject(connectionSettings);
            File.WriteAllText(settingsFilePath, settingsString);
        }

        private void LoadSettings()
        {
            string settingsFilePath = Path.Combine(Directory.GetCurrentDirectory(), SettingsFile);

            if (!File.Exists(settingsFilePath))
            {
                return;
            }

            string settingsString = File.ReadAllText(settingsFilePath);

            ConnectionSettings? settings = JsonConvert.DeserializeObject<ConnectionSettings>(settingsString);
            if (settings == null)
            {
                MessageBox.Show("Unable to load saved settings");
                return;
            }

            connectionSettings = settings;
        }

        private void PortInput_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(SenderPortInput.Text, out int clientPort))
            {
                SenderPortInput.Text = connectionSettings.SenderPort.ToString();
                return;
            }

            connectionSettings.SenderPort = clientPort;
            SaveSettings();
        }

        private void ServerPortInput_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(ReceiverPortInput.Text, out int serverPort))
            {
                ReceiverPortInput.Text = connectionSettings.ReceiverPort.ToString();
                return;
            }

            connectionSettings.ReceiverPort = serverPort;
            SaveSettings();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            foreach (Classes.HapticPattern pattern in manager.Patterns.Values)
            {
                bHapticsManager.RegisterPatternFromFile(pattern.Name, pattern.Path);
            }

            oscServer = new OSCServerHelper(connectionSettings);
            oscServer.OnMessageReceived += HandleOSCMessageReceived;
            oscServer.Start([.. manager.Patterns.Values]);
            RefreshButtons();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            if (oscServer == null)
            {
                return;
            }

            oscServer.OnMessageReceived -= HandleOSCMessageReceived;
            oscServer.Dispose();
            oscServer = null;

            RefreshButtons();
        }

        private void RefreshButtons()
        {
            bool isRunning = oscServer?.IsRunning ?? false;
            StartButton.Visible = !isRunning;
            StopButton.Visible = isRunning;
            SenderPortInput.Enabled = !isRunning;
            ReceiverPortInput.Enabled = !isRunning;
            IPInput.Enabled = !isRunning;
            dataGridView1.ReadOnly = !isRunning;
            AddPatternButton.Enabled = !isRunning;
        }

        private void IPInput_TextChanged(object sender, EventArgs e)
        {
            if (!IPAddress.TryParse(IPInput.Text, out IPAddress? ipAddress))
            {
                MessageBox.Show($"Invalid IP address '{IPInput.Text}'!", "Invalid input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            connectionSettings.TargetIP = ipAddress.ToString();
            SaveSettings();
        }

        private void HandleOSCMessageReceived(object? sender, OSCMessageEventArgs e)
        {
            Classes.HapticPattern pattern = e.Pattern;
            OscMessageValues values = e.Values;

            // Do something with the pattern and values
            if (values.ReadBooleanElement(0))
            {
                bHapticsManager.PlayRegistered(pattern.Name);
            }
        }

        private void DataGridView1_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            if (dataGridView1.Columns[e.ColumnIndex].Name == "deleteColumn")
            {
                if (oscServer?.IsRunning ?? false)
                {
                    MessageBox.Show("Cannot delete patterns while OSC server is running", "Server is running!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DialogResult result = MessageBox.Show("Do you really want to delete this pattern?", "Delete pattern?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
                {
                    return;
                }

                Classes.HapticPattern pattern = (Classes.HapticPattern)dataGridView1.Rows[e.RowIndex].DataBoundItem;
                manager.RemovePattern(pattern.Name);
                RefreshGrid();
            }
            else if (dataGridView1.Columns[e.ColumnIndex].Name == "testColumn")
            {
                if (!(oscServer?.IsRunning ?? false))
                {
                    MessageBox.Show("Please start the server before testing patterns.", "Patterns not yet registered!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Classes.HapticPattern pattern = (Classes.HapticPattern)dataGridView1.Rows[e.RowIndex].DataBoundItem;
                bHapticsManager.PlayRegistered(pattern.Name);
            }
        }

        private void PresetsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopButton_Click(sender, e);
            bHapticsManager.Disconnect();
        }
    }
}
