using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverStorePermissions
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] commands = {
                "takeown /F {0}",
                "icacls {0} /reset",
                "icacls {0} /grant Administrators:f",
                "icacls {0} /setowner SYSTEM"
            };
            var fmtString = "/C " + string.Join(" && ", commands);

            var files = Directory.EnumerateFiles(@"C:\Windows\System32\DriverStore\FileRepository\", "*.*", SearchOption.AllDirectories);
            Parallel.ForEach(files, file =>
            {
                try
                {
                    using (var stream = File.Open(file, FileMode.Open, FileAccess.Read))
                    {
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    var pStartInfo = new ProcessStartInfo
                    {
                        FileName = "cmd",
                        Arguments = string.Format(fmtString, file),
                        WindowStyle = ProcessWindowStyle.Hidden,
                        RedirectStandardError = true,
                        RedirectStandardOutput = true,
                        UseShellExecute = false
                    };
                    var process = Process.Start(pStartInfo);
                    process.WaitForExit();
                    Console.WriteLine($"{file}\n-------------------\n{(process.StandardOutput.ReadToEnd() + "\n" + process.StandardError.ReadToEnd()).Trim()}");
                }
            });
            Console.ReadKey();
        }
    }
}
