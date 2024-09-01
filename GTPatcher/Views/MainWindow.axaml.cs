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
using static Constants;

namespace GTPatcher.Views
{
    public class Patch
    {
        public string PatchName { get; set; }
        public string PatchShorthand { get; set; }
        public string PatchDescription { get; set; }
        public string PatchLink { get; set; }
        public string GameLink { get; set; }
        public long ManifestId { get; set; }
        public bool IsSteam { get; set; }
        public bool IsBeta { get; set; }
    }


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
        private string[] RegistriesValueName = {"InstallationPath", "SteamUsername", "DarkMode", "Transparency"};


        private string specificBuildPath;
        private IBrush defaultBackgroundColour;
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

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                using (var key = Registry.CurrentUser.OpenSubKey("Software", true))
                {
                    if (key != null)
                    {
                        if (key.OpenSubKey("GTPatcher") == null)
                        {
                            key.CreateSubKey("GTPatcher");
                        }
                    }
                }

                LoadInstallationPath();
                LoadUsername();
                LoadTransparency();
                //LoadDarkMode();
            }
            else
            {
                ShowMessageBox("Gorilla Tag Patcher Launcher", "You are not in windows, your settings wont save!");
            }

            //ApplyTheme(DarkModeCheckBox.IsChecked == true);

            LoadBuilds();

            MainTabControl.SelectionChanged += TabControl_SelectionChanged;
        }

        private List<Patch>? BuildsList;

        private async void LoadBuilds()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue { NoCache = true };
            BuildsList = JsonConvert.DeserializeObject<List<Patch>>(await client.GetStringAsync(INDEX_JSON));

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
        }


        private void LoadInstallationPath()
        {
            string savedPath = (string)Registry.GetValue(RegistryKeyPath, RegistriesValueName[0], string.Empty);
            if (!string.IsNullOrEmpty(savedPath))
            {
                PathTextBox.Text = savedPath;
            }
        }

        private void LoadUsername()
        {
            string savedUser = (string)Registry.GetValue(RegistryKeyPath, RegistriesValueName[1], string.Empty);
            if (!string.IsNullOrEmpty(savedUser))
            {
                UserTextBox.Text = savedUser;
            }
        }

        private void LoadDarkMode()
        {
            Console.WriteLine((string)Registry.GetValue(RegistryKeyPath, RegistriesValueName[2], "false"));
            bool savedDark = (string)Registry.GetValue(RegistryKeyPath, RegistriesValueName[2], "false") == "true" ? true : false;
            //DarkModeCheckBox.IsChecked = savedDark;
        }
        private void LoadTransparency()
        {
            bool savedTransparency = (string)Registry.GetValue(RegistryKeyPath, RegistriesValueName[3], "true") == "true" ? true : false;
            TransparencyCheckBox.IsChecked = savedTransparency;
        }

        private void SaveRegistry(string path, string registryValue)
        {
            Registry.SetValue(RegistryKeyPath, registryValue, path.ToString());
        }

        private void PathTextBox_TextChanged(object? sender, Avalonia.Controls.TextChangedEventArgs e)
        {
            SaveRegistry(PathTextBox.Text, RegistriesValueName[0]);
        }

        private void SteamUserTextBox_TextChanged(object? sender, Avalonia.Controls.TextChangedEventArgs e)
        {
            SaveRegistry(UserTextBox.Text, RegistriesValueName[1]);
        }

        private void DarkModeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            SaveRegistry("true", RegistriesValueName[2]);
            ApplyTheme(true);
            this.Background = new SolidColorBrush(Color.Parse("#FF1E1E1E"));
        }

        private void DarkModeCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            SaveRegistry("false", RegistriesValueName[2]);
            ApplyTheme(false);
            this.Background = new SolidColorBrush(Color.Parse("#FFFFFFFF"));
        }
        private void TransparencyCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            defaultBackgroundColour = this.Background;
            SaveRegistry("true", RegistriesValueName[3]);
            this.Background = new SolidColorBrush(Color.Parse("Transparent"));
        }

        private void TransparencyCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            SaveRegistry("false", RegistriesValueName[3]);
            this.Background = defaultBackgroundColour;
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //ApplyTheme(DarkModeCheckBox.IsChecked == true);
        }

        private void ApplyTheme(bool isDarkMode)
        {
            var backgroundColor = isDarkMode ? Avalonia.Media.Brushes.Black : Avalonia.Media.Brushes.White;
            var foregroundColor = isDarkMode ? Avalonia.Media.Brushes.White : Avalonia.Media.Brushes.Black;

            foreach (var textBox in VisualTreeHelperExtensions.FindVisualChildren<TextBox>(this))
            {
                textBox.Background = backgroundColor;
                textBox.Foreground = foregroundColor;
            }

            foreach (var textBlock in VisualTreeHelperExtensions.FindVisualChildren<TextBlock>(this))
            {
                textBlock.Foreground = foregroundColor;
            }

            foreach (var textBlock in VisualTreeHelperExtensions.FindVisualChildren<Button>(this))
            {
                textBlock.Foreground = foregroundColor;
            }
        }

        private void steamBuildBox_SelectedIndexChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedBuild = BuildsList.FirstOrDefault(x => x.PatchName == Builds.SelectedItem as string);
            if (selectedBuild == null) return;
            var specificBuildPath = $"{PathTextBox.Text}\\{selectedBuild.PatchShorthand}";
            manifestIdLabel.Text = string.IsNullOrEmpty(selectedBuild.ManifestId.ToString()) ? "No Steam manifest for this build" : selectedBuild.ManifestId.ToString();
            descriptionLabel.Text = string.IsNullOrEmpty(selectedBuild.PatchDescription) ? "No description for this patch" : selectedBuild.PatchDescription;
            if (Path.Exists(specificBuildPath))
            {
                runButton.Content = "Play!";
            }
            else
            {
                runButton.Content = "Install!";

            }
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

            var specificBuildPath = $"{PathTextBox.Text}\\{selectedBuild.PatchShorthand}";
            if (!Directory.Exists(specificBuildPath))
            {
                Directory.CreateDirectory(specificBuildPath);
                if (InstallGame(selectedBuild, specificBuildPath) != 0)
                {
                    ShowMessageBox("Eek...", "Something went wrong!\nMost likely your username or password is incorrect.\nTo prevent problems, the incomplete installation of the game will be deleted.");
                    Directory.Delete(specificBuildPath, true);
                    return;
                }
                PatchAssembly(selectedBuild, $"{specificBuildPath}\\Gorilla Tag_Data\\Managed");
                runButton.Content = "Play!";
                ShowMessageBox("Success!", "This version of the game has been installed, you can now play!");
                return;
            }
            if (File.Exists($"{specificBuildPath}\\Gorilla Tag.exe"))
                Process.Start($"{specificBuildPath}\\Gorilla Tag.exe");
        }

        private int InstallGame(Patch selectedBuild, string installPath)
        {
            if ((bool)selectedBuild.IsSteam)
            {
                var exitCode = SteamHelper.DownloadManifest((ulong)selectedBuild.ManifestId, installPath, UserTextBox.Text, (bool)selectedBuild.IsBeta);
                return exitCode;
            }
            else return -128;
        }

        private async void PatchAssembly(Patch selectedBuild, string managedPath)
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
                    SaveRegistry(selectedPath, RegistriesValueName[0]);
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
