using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnipCS.Services
{
    public interface ICompileService
    {
        CompileResult Compile(string code);
    }

    public class CompileService : ICompileService
    {
        public CompileResult Compile(string code)
        {
            CompileResult result = null;

            result = new CompileResult { IsSuccess = true };

            return result;
        }
    }

    public class CompileResult
    {
        public bool IsSuccess { get; set; }
    }
}