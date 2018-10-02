using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Workers
{
    class SudokuBoardStateManager
    {
        public string GenerateState(int[,] sudokuBoard)
        {
            StringBuilder key = new StringBuilder();

            for (int row = 0; row < sudokuBoard.GetUpperBound(0); row++)
            {
                for (int col = 0; col < sudokuBoard.GetUpperBound(1); col++)
                {
                    key.Append(sudokuBoard[row, col]);
                }
            }

            return key.ToString();

        }

        public bool isSolved(int[,] sudokuBoard)
        {
            for (int row = 0; row < sudokuBoard.GetUpperBound(0); row++)
            {
                for (int col = 0; col < sudokuBoard.GetUpperBound(1); col++)
                {
                    if (sudokuBoard[row, col] == 0 || sudokuBoard[row, col].ToString().Length > 1)
                        return false;
                }
            }

            return true;
        }
    }
}
