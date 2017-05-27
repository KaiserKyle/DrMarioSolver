using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DrMarioSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            LogParser log = new LogParser(@"C:\Users\kaiserkyle\Documents\drmario\60\drmario-60.log");
            log.Parse();

            Level level = log.GetLevel(0);
            level.ToConsole();
        }
    }
}
