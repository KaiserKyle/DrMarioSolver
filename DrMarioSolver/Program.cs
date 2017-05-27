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
            LogParser log = new LogParser(@"C:\Users\kyle\Source\Repos\DrMarioSolver\DrMarioSolver\drmario-60.log");
            log.Parse();

            Level level = log.GetLevel(0);
            level.ToConsole();

            Console.ReadLine();

            LevelSolver solve = new LevelSolver(level);
            solve.FindAvailableMoves();

            Console.ReadLine();
        }
    }
}
