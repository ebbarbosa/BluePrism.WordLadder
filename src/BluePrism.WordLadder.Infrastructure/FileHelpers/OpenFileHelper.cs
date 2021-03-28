using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace BluePrism.WordLadder.Infrastructure.FileHelpers
{
    /// <summary>
    /// This class, in case of success, will open the generated .txt file for the user in the end of the program execution.
    /// </summary>
    public class OpenFileHelper : IOpenFileHelper
    {
        /// <summary>
        /// Code found at https://brockallen.com/2016/09/24/process-start-for-urls-on-net-core/
        /// Method starts a process to open the generated file for the word ladder.
        /// </summary>
        /// <param name="url">It is a file:/// url of the generate word ladder file to be opened.</param>
        public void OpenFile(string url)
        {
            try
            {
                Console.WriteLine();
                Console.WriteLine($"Answer file created in {url}");
                Console.WriteLine();
                Process.Start(url);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    Console.WriteLine($"This app has failed to open the file, please go to the file location at {url}.");
                }
            }
        }
    }
}
