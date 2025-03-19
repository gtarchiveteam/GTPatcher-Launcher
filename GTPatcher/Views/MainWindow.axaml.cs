using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.VisualTree;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Diagnostics;
using System.IO;
using GTPatcher_Launcher.Utilities;
using GTPatcher.Types;
using static Constants;

namespace GTPatcher.Views
{
    
    public static class VisualTreeHelperExtensions
    {
        public static IEnumerable<T> FindVisualChildren<T>(this Control control) where T : Control
        {
            var results = new List<T>();
            if (control == null) return results;

            foreach (var child in control.GetVisualChildren())
            {
                if (child is T tChild)
                {
                    results.Add(tChild);
                }
                
                if (child is Control childControl)
                {
                    results.AddRange(FindVisualChildren<T>(childControl));
                }
            }

            return results;
        }
    }

    public partial class MainWindow : Window
    {
        private const string RegistryKeyPath = @"HKEY_CURRENT_USER\SOFTWARE\GTPatcher";
        private string SettingsPath;
        private Settings Settings;

        private async void ShowMessageBox(string title, string message)
        {
            var box = MessageBoxManager
                  .GetMessageBoxStandard(title, message,
                      ButtonEnum.Ok);

            var result = await box.ShowAsync();
        }

        public MainWindow()
        {
            InitializeComponent();
            if (!Path.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "GTPatcher")))
            {
                Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "GTPatcher"));
            }
            SettingsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "GTPatcher/settings.json");
            Settings = File.Exists(SettingsPath) ? JsonConvert.DeserializeObject<Settings>(File.ReadAllText(SettingsPath))! : new Settings();
            PathTextBox.Text = Settings.Path;
            UserTextBox.Text = Settings.Username;
            LoadBuilds();
        }

        private List<Patch>? BuildsList;

        private async void LoadBuilds()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue { NoCache = true };
            BuildsList = JsonConvert.DeserializeObject<List<Patch>>(await client.GetStringAsync(INDEX_JSON), new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });

            if (Builds == null)
            {
                ShowMessageBox("Error", "Failed to retrieve builds list. Check your internet connection!");
                return;
            }

            Builds.ItemsSource = BuildsList.Select(p => p.PatchName).ToList();

            if (BuildsList.Any())
            {
                Builds.SelectedItem = BuildsList.First().PatchName;
                var selectedBuild = BuildsList.FirstOrDefault(x => x.PatchName == Builds.SelectedItem as string);
                manifestIdLabel.Text = string.IsNullOrEmpty(selectedBuild.ManifestId.ToString()) ? "No Steam manifest for this build" : selectedBuild.ManifestId.ToString();
                descriptionLabel.Text = string.IsNullOrEmpty(selectedBuild.PatchDescription) ? "No description for this patch" : selectedBuild.PatchDescription;
            }
            
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                ShowMessageBox("Hello there, fellow penguin!", "I see you're on Linux.\nBefore you go ahead and download a build, running this application from a terminal is REQUIRED to type your Steam password into DepotDownloader\nGood luck, have fun! :3");
            }
        }

        private void SaveSettings()
        {
            Settings.Path = PathTextBox.Text;
            Settings.Username = UserTextBox.Text;
            if (!File.Exists(SettingsPath)) File.Create(SettingsPath).Close();
            File.WriteAllText(SettingsPath, JsonConvert.SerializeObject(Settings));
        }

        private void PathTextBox_TextChanged(object? sender, Avalonia.Controls.TextChangedEventArgs e)
        {
            SaveSettings();
        }

        private void SteamUserTextBox_TextChanged(object? sender, Avalonia.Controls.TextChangedEventArgs e)
        {
            SaveSettings();
        }

        private void steamBuildBox_SelectedIndexChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedBuild = BuildsList.FirstOrDefault(x => x.PatchName == Builds.SelectedItem as string);
            if (selectedBuild == null) return;
            manifestIdLabel.Text = string.IsNullOrEmpty(selectedBuild.ManifestId.ToString()) ? "No Steam manifest for this build" : selectedBuild.ManifestId.ToString();
            descriptionLabel.Text = string.IsNullOrEmpty(selectedBuild.PatchDescription) ? "No description for this patch" : selectedBuild.PatchDescription;
        }

        private async void PlayButton(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(UserTextBox.Text))
            {
                ShowMessageBox("Cannot download game", "Put in your Steam account username in the Settings tab.");
                return;
            }

            if (string.IsNullOrEmpty(PathTextBox.Text))
            {
                ShowMessageBox("Cannot download game", "You don't have an installation path set. Set a folder in the Settings.");
                return;
            }

            var selectedBuild = BuildsList.FirstOrDefault(x => x.PatchName == Builds.SelectedItem as string);
            if (selectedBuild == null)
            {
                ShowMessageBox("Cannot download game", "You need to select a game version first.");
                return;
            }

            var specificBuildPath = $"{PathTextBox.Text}/{selectedBuild.PatchShorthand}";
            if (!Directory.Exists(specificBuildPath))
            {
                Directory.CreateDirectory(specificBuildPath);
                if (InstallGame(selectedBuild, specificBuildPath) != 0)
                {
                    ShowMessageBox("Eek...", "Something went wrong!\nMost likely your username or password is incorrect.\nTo prevent problems, the incomplete installation of the game will be deleted.");
                    Directory.Delete(specificBuildPath, true);
                    return;
                }
                PatchAssembly(selectedBuild, $"{specificBuildPath}/{selectedBuild.GameName}_Data/Managed");
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                if (File.Exists($"{specificBuildPath}/{selectedBuild.GameName}.exe"))
                    Process.Start($"{specificBuildPath}/{selectedBuild.GameName}.exe");
            }
            else
            {
                ShowMessageBox("Manual action required", "Linux builds of the launcher will not auto-start the game due to complexities with Wine/Proton.\nPlease add the build as a non-Steam game in your Steam library, and run it under Proton.");
            }
        }

        private int InstallGame(Patch selectedBuild, string installPath)
        {
            if (selectedBuild.IsSteam)
            {
                return DownloadHelper.DownloadManifest((ulong)selectedBuild.ManifestId, installPath, UserTextBox.Text, selectedBuild.Branch);
            }
            else
            {
                return DownloadHelper.DownloadUrl(installPath, selectedBuild.GameLink);
            }
        }

        private async void PatchAssembly(Patch selectedBuild, string managedPath)
        {
            File.Move($"{managedPath}/Assembly-CSharp.dll", $"{managedPath}/Assembly-CSharp.bak");
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue { NoCache = true };

            using var input = new FileStream($"{managedPath}/Assembly-CSharp.bak", FileMode.Open);
            var file = File.Create($"{managedPath}/patch.xdelta");
            var stream = await client.GetStreamAsync(selectedBuild.PatchLink);
            stream.CopyTo(file);
            file.Close();
            using var patch = new FileStream($"{managedPath}/patch.xdelta", FileMode.Open);
            using var output = new FileStream($"{managedPath}/Assembly-CSharp.dll", FileMode.Create);

            using var decoder = new PleOps.XdeltaSharp.Decoder.Decoder(input, patch, output);

            decoder.Run();
        }

        private async void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFolderDialog
            {
                Title = "Select Installation Path"
            };

            var result = dialog.ShowAsync(this);
            await result.ContinueWith(task =>
            {
                if (task.Result != null)
                {
                    var selectedPath = task.Result;
                    PathTextBox.Text = selectedPath;
                    SaveSettings();
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
