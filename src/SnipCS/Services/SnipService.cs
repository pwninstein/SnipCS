using System;
using System.Diagnostics;
using System.IO;

namespace SnipCS.Services
{
    public class SnipService : ISnipService
    {
        public BuildResult Build(string code, Action<string> outputLine)
        {
            BuildResult result = null;

            var file = Path.GetTempFileName();
            var inputFile = string.Format("{0}.cs", file);
            var outputFile = string.Format("{0}.exe", file);

            File.WriteAllText(inputFile, code);

            using (var compiler = new Process())
            {

                compiler.StartInfo = new ProcessStartInfo(@"c:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe", string.Format("/out:{0} {1}", outputFile, inputFile));
                compiler.StartInfo.CreateNoWindow = true;
                compiler.StartInfo.RedirectStandardOutput = true;
                compiler.StartInfo.UseShellExecute = false;
                compiler.OutputDataReceived += (s, e) => { outputLine(e.Data); };
                compiler.Start();
                compiler.BeginOutputReadLine();
                compiler.WaitForExit();
                
                if (compiler.ExitCode == 0)
                {
                    result = new BuildResult { IsSuccess = true, ExecutableName = outputFile };
                }
                else
                {
                    result = new BuildResult { IsSuccess = false };
                }
            }

            return result;
        }

        public void Run(string file, Action<string> outputLine)
        {
            using (var runner = new Process())
            {
                runner.StartInfo = new ProcessStartInfo(file);
                runner.StartInfo.CreateNoWindow = true;
                runner.StartInfo.RedirectStandardOutput = true;
                runner.StartInfo.UseShellExecute = false;
                runner.OutputDataReceived += (s, e) => { outputLine(e.Data); };
                runner.Start();
                runner.BeginOutputReadLine();
                runner.WaitForExit();
            }
        }
    }
}