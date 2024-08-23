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

        }

        private void browseButton_Click(object sender, EventArgs e)
        {

        }

        private void settingsTab_Click(object sender, EventArgs e)
        {

        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }
    }
}
