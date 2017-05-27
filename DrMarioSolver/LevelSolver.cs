using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrMarioSolver
{
    class LevelSolver
    {
        private Level OriginalLevel;
        private Level CurrentState;

        public LevelSolver(Level level)
        {
            OriginalLevel = level;
            CurrentState = level;
        }

        public void FindAvailableMoves()
        {
            for (int i = 1; i < 15; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (CurrentState.Virusses.MapVector[i,j] != '0')
                    {
                        if (CurrentState.Virusses.MapVector[i - 1,j] == '0')
                        {
                            CurrentState.Virusses.MapVector[i - 1, j] = 'X';
                        }
                    }
                }
            }

            for (int k = 0; k < 8; k++)
            {
                if (CurrentState.Virusses.MapVector[15, k] == '0')
                {
                    CurrentState.Virusses.MapVector[15, k] = 'X';
                }
            }

            CurrentState.ToConsole();
        }
    }
}
