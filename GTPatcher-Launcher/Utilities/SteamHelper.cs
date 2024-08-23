using System.Diagnostics;
using static Constants;

namespace GTPatcher_Launcher.Utilities
{
    public static class SteamHelper
    {
        public static int DownloadManifest(ulong manifestId, string directory, string steamUsername)
        {
            var proc = Process.Start("DepotDownloader.exe", $"-app {APP_ID} -depot {DEPOT_ID} -manifest {manifestId.ToString()} -username {steamUsername} -remember-password -dir \"{directory}\"");
            proc.WaitForExit();
            return proc.ExitCode;
        }
    }
}
