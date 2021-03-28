using System;
using System.Diagnostics;
using System.IO;
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
        /// <param name="fileName">It is a file:/// fileName of the generate word ladder file to be opened.</param>
        public void OpenFile(string fileName)
        {
            try
            {
                var urlfileName = $"file:///{fileName}";

                Console.WriteLine();
                Console.WriteLine($"Answer file created in {fileName}");
                Console.WriteLine();
                Process.Start(urlfileName);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    fileName = fileName.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {fileName}") {CreateNoWindow = true});
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", fileName);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", fileName);
                }
                else
                {
                    Console.WriteLine(
                        $"This app has failed to open the file, please go to the file location at {fileName}.");
                }
            }
        }
    }
}