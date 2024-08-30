using Newtonsoft.Json;
using System.Diagnostics;
using static Constants;
using System.Net.Http.Headers;
using GTPatcher_Launcher.Utilities;
using Microsoft.Win32;

namespace GTPatcher_Launcher
{
    public partial class Form1 : Form
    {
        public static List<BuildInfo>? Builds;

        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            LoadSettings();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue { NoCache = true }; // This is such a small amount of data that not caching it is probably okay
            Builds = JsonConvert.DeserializeObject<List<BuildInfo>>(await client.GetStringAsync(INDEX_JSON));
            if (Builds == null)
            {
                MessageBox.Show("Failed to retrieve builds list. Check your internet connection!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Debug.WriteLine($"We got {Builds.Count} build(s) from URL.");
            foreach (BuildInfo build in Builds)
            {
                steamBuildBox.Items.Add(build);
            }
        }

        private void steamBuildBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedBuild = (BuildInfo)steamBuildBox.SelectedItem;
            if (selectedBuild == null) return;
            manifestIdLabel.Text = string.IsNullOrEmpty(selectedBuild.ManifestId.ToString()) ? "No Steam manifest for this build" : selectedBuild.ManifestId.ToString();
            descriptionLabel.Text = string.IsNullOrEmpty(selectedBuild.PatchDescription) ? "No description for this patch" : selectedBuild.PatchDescription;
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                pathBox.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(steamAccountUsername.Text))
            {
                MessageBox.Show("You need to put in your Steam account username in the Settings tab.", "Cannot download game", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(pathBox.Text))
            {
                MessageBox.Show("You don't have an installation path set. Set a folder in the Settings.", "Cannot download game", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedBuild = (BuildInfo)steamBuildBox.SelectedItem;
            if (selectedBuild == null)
            {
                MessageBox.Show("You need to select a game version first.", "Cannot download game", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveSettings();

            var specificBuildPath = $"{pathBox.Text}\\{selectedBuild.PatchShorthand}";
            if (!Directory.Exists(specificBuildPath))
            {
                Directory.CreateDirectory(specificBuildPath);
                if (InstallGame(selectedBuild, specificBuildPath) != 0)
                {
                    MessageBox.Show("Something went wrong!\nMost likely your username or password is incorrect.\nTo prevent problems, the incomplete installation of the game will be deleted.", "Eek...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Directory.Delete(specificBuildPath, true);
                    return;
                }
                PatchAssembly(selectedBuild, $"{specificBuildPath}\\Gorilla Tag_Data\\Managed");
            }
            if (File.Exists($"{specificBuildPath}\\Gorilla Tag.exe"))
                Process.Start($"{specificBuildPath}\\Gorilla Tag.exe");
        }

        private void LoadSettings()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\GTPatcherLauncher");

            if (key != null)
            {
                pathBox.Text = (string)key.GetValue("installPath");
                steamAccountUsername.Text = (string)key.GetValue("steamUsername");
                key.Close();
            }
        }

        private void SaveSettings()
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\GTPatcherLauncher");

            key.SetValue("installPath", pathBox.Text);
            key.SetValue("steamUsername", steamAccountUsername.Text);
            key.Close();
        }

        private int InstallGame(BuildInfo selectedBuild, string installPath)
        {
            if ((bool)selectedBuild.IsSteam)
            {
                var exitCode = SteamHelper.DownloadManifest((ulong)selectedBuild.ManifestId, installPath, steamAccountUsername.Text, (bool)selectedBuild.IsBeta);
                return exitCode;
            }
            else return -128;
        }

        private async void PatchAssembly(BuildInfo selectedBuild, string managedPath)
        {
            File.Move($"{managedPath}\\Assembly-CSharp.dll", $"{managedPath}\\Assembly-CSharp.bak");
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue { NoCache = true };

            using var input = new FileStream($"{managedPath}\\Assembly-CSharp.bak", FileMode.Open);
            var file = File.Create($"{managedPath}\\patch.xdelta");
            var stream = await client.GetStreamAsync(selectedBuild.PatchLink);
            stream.CopyTo(file);
            file.Close();
            using var patch = new FileStream($"{managedPath}\\patch.xdelta", FileMode.Open);
            using var output = new FileStream($"{managedPath}\\Assembly-CSharp.dll", FileMode.Create);

            using var decoder = new PleOps.XdeltaSharp.Decoder.Decoder(input, patch, output);

            decoder.Run();
        }

        private void versionLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
