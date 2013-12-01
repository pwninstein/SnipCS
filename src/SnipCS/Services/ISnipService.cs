using System;

namespace SnipCS.Services
{
    public interface ISnipService
    {
        BuildResult Build(string code, Action<string> outputLine);

        void Run(string file, Action<string> outputLine);
    }

    public class BuildResult
    {
        public bool IsSuccess { get; set; }

        public string ExecutableName { get; set; }
    }
}
