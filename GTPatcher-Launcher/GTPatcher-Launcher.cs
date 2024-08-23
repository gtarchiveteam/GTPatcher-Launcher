using Newtonsoft.Json;
using System.Diagnostics;
using static Constants;

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
            HttpClient jsonIndex = new HttpClient();
            Builds = JsonConvert.DeserializeObject<List<BuildInfo>>(await jsonIndex.GetStringAsync(INDEX_JSON));
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

        }
    }
}
