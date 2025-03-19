using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using static Constants;

namespace GTPatcher_Launcher.Utilities
{
    public static class DownloadHelper
    {
        public static int DownloadManifest(ulong manifestId, string directory, string steamUsername, string branch)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                var proc = Process.Start("DepotDownloader.exe", $"-app {APP_ID} -depot {DEPOT_ID} -manifest {manifestId.ToString()} -branch {branch} -username {steamUsername} -remember-password -dir \"{directory}\"");
                proc.WaitForExit();
                return proc.ExitCode;
            }
            else
            {
                var proc = Process.Start("DepotDownloader", $"-app {APP_ID} -depot {DEPOT_ID} -manifest {manifestId.ToString()} -branch {branch} -username {steamUsername} -remember-password -dir \"{directory}\"");
                proc.WaitForExit();
                return proc.ExitCode;
            }
        }

        public static int DownloadUrl(string directory, string url)
        {
            try
            {
                using (var client = new WebClient())
                {
                    client.DownloadFile(url, @$"{directory}/game.zip");
                }
                System.IO.Compression.ZipFile.ExtractToDirectory(@$"{directory}/game.zip", directory);
                File.Delete(@$"{directory}/game.zip");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 1;
            }

            try
            {
                // HACK: this is so inefficient, but its the best way i can think of doing it for now
                foreach (var dir in Directory.GetDirectories(directory))
                {
                    if (Directory.GetFiles(dir, "*.exe").Length > 0)
                    {
                        CopyFolder(dir, directory);
                        Directory.Delete(dir, true);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 1;
            }

            return 0;
        }
        
        static public void CopyFolder( string sourceFolder, string destFolder )
        {
            if (!Directory.Exists( destFolder ))
                Directory.CreateDirectory( destFolder );
            string[] files = Directory.GetFiles( sourceFolder );
            foreach (string file in files)
            {
                string name = Path.GetFileName( file );
                string dest = Path.Combine( destFolder, name );
                File.Copy( file, dest );
            }
            string[] folders = Directory.GetDirectories( sourceFolder );
            foreach (string folder in folders)
            {
                string name = Path.GetFileName( folder );
                string dest = Path.Combine( destFolder, name );
                CopyFolder( folder, dest );
            }
        }
    }
}
