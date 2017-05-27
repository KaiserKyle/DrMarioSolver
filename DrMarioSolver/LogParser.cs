using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrMarioSolver
{
    enum ItemColor
    {
        Yellow,
        Red,
        Blue
    };

    struct Pill
    {
        public ItemColor Left;
        public ItemColor Right;

        public static Pill Create(string pill)
        {
            Pill returnValue = new Pill();

            string[] sides = pill.Split(':');
            Enum.TryParse(sides[0], out returnValue.Left);
            Enum.TryParse(sides[1], out returnValue.Right);

            return returnValue;
        }

        public override string ToString()
        {
            return Left.ToString() + ":" + Right.ToString();
        }
    };

    class Virusses
    {
        private List<string> map;
        private char[,] mapVector;
        public Dictionary<ItemColor, uint> VirusCounts;
        public uint TotalVirusCount;

        public List<string> Map
        {
            get
            {
                return map;
            }
        }

        public char[,] MapVector
        {
            get
            {
                return mapVector;
            }
        }

        public Virusses()
        {
            map = new List<string>();
            mapVector = new char[16,8];
            VirusCounts = new Dictionary<ItemColor, uint>();
            VirusCounts.Add(ItemColor.Red, 0);
            VirusCounts.Add(ItemColor.Blue, 0);
            VirusCounts.Add(ItemColor.Yellow, 0);
        }

        public void AddLineToMap(string line)
        {
            map.Add(line);
            int index = map.Count - 1;
            for(int j = 0; j < 8; j++)
            {
                mapVector[index, j] = line[j];
            }
        }
    }

    class Level
    {
        public int Number;
        public Virusses Virusses;
        public List<Pill> Pills;
        public Dictionary<ItemColor, uint> PillColorCounts;

        public Level()
        {
            Pills = new List<Pill>();
            Virusses = new Virusses();
            PillColorCounts = new Dictionary<ItemColor, uint>();
            PillColorCounts.Add(ItemColor.Red, 0);
            PillColorCounts.Add(ItemColor.Blue, 0);
            PillColorCounts.Add(ItemColor.Yellow, 0);
        }

        public void AddPill(Pill pill)
        {
            Pills.Add(pill);
            PillColorCounts[pill.Left]++;
            PillColorCounts[pill.Right]++;
        }

        public void ToConsole()
        {
            Console.WriteLine("Level: " + Number);
            Console.WriteLine("Total Virus Count: " + Virusses.TotalVirusCount);
            Console.WriteLine("  __  __");
            Console.WriteLine("   |  |");
            Console.WriteLine("+---  ---+");
            foreach (string str in Virusses.Map)
            {
                Console.WriteLine("|" + str.Replace('0', ' ') + "|");
            }
            Console.WriteLine("+--------+");
            Console.WriteLine("Blue Virus Count: " + Virusses.VirusCounts[ItemColor.Blue]);
            Console.WriteLine("Red Virus Count: " + Virusses.VirusCounts[ItemColor.Red]);
            Console.WriteLine("Yellow Virus Count: " + Virusses.VirusCounts[ItemColor.Yellow]);
            Console.WriteLine("Pills:");           
            foreach (Pill pill in Pills)
            {
                Console.WriteLine(pill.ToString());
            }
            Console.WriteLine("Blue Pill Count: " + PillColorCounts[ItemColor.Blue]);
            Console.WriteLine("Red Pill Count: " + PillColorCounts[ItemColor.Red]);
            Console.WriteLine("Yellow Pill Count: " + PillColorCounts[ItemColor.Yellow]);
        }
    };

    class LogParser
    {
        private StreamReader _logReader;
        private List<Level> _levels;

        public LogParser(string path)
        {
            FileStream logFile = File.Open(path, FileMode.Open, FileAccess.Read);

            _logReader = new StreamReader(logFile, Encoding.UTF8);
            _levels = new List<Level>();
        }

        public void Parse()
        {
            // Reset stream to beginning
            _logReader.BaseStream.Seek(0, SeekOrigin.Begin);

            Level currentLevel = new Level();
            currentLevel.Number = -1;
            while(!_logReader.EndOfStream)
            {
                string thisLine = _logReader.ReadLine();
                if (thisLine.StartsWith("NEW LEVEL"))
                {
                    if (currentLevel.Number != -1)
                    {
                        _levels.Add(currentLevel);
                    }
                    currentLevel = new Level();
                    currentLevel.Number = int.Parse(thisLine.Split(':')[1]);
                }
                else if (thisLine.StartsWith("Y") || thisLine.StartsWith("B") || thisLine.StartsWith("R"))
                {
                    currentLevel.AddPill(Pill.Create(thisLine));
                }
                else
                {
                    String current = "";
                    for (int i = 0; i < thisLine.Length; i++)
                    {
                        current += thisLine[i];
                        if ((i + 1) % 8 == 0)
                        {
                            currentLevel.Virusses.AddLineToMap(current);
                            current = "";
                        }
                        if (thisLine[i] == 'R')
                        {
                            currentLevel.Virusses.VirusCounts[ItemColor.Red]++;
                            currentLevel.Virusses.TotalVirusCount++;
                        }
                        else if (thisLine[i] == 'B')
                        {
                            currentLevel.Virusses.VirusCounts[ItemColor.Blue]++;
                            currentLevel.Virusses.TotalVirusCount++;
                        }
                        else if (thisLine[i] == 'Y')
                        {
                            currentLevel.Virusses.VirusCounts[ItemColor.Yellow]++;
                            currentLevel.Virusses.TotalVirusCount++;
                        }
                    }
                }
            }
        }

        public Level GetLevel(int level)
        {
            return _levels[level];
        }
    }
}
