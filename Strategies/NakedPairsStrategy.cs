using SudokuSolver.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Strategies
{
    class NakedPairsStrategy : ISudokuStrategy
    {
        private readonly SudokuMapper _sudokuMapper;

        public NakedPairsStrategy(SudokuMapper mapper)
        {
            _sudokuMapper = mapper;
        }
        public int[,] Solve(int[,] sudokuBoard)
        {
            for (int row = 0; row < sudokuBoard.GetUpperBound(0); row++)
            {
                for (int col = 0; col < sudokuBoard.GetUpperBound(1); col++)
                {
                    EliminateNakedPairFromOthersInRow(sudokuBoard, row, col);
                    EliminateNakedPairFromOthersInCol(sudokuBoard, row, col);
                    EliminateNakedPairFromOthersInBlock(sudokuBoard, row, col);
                }
            }
            return sudokuBoard;
        }

        private void EliminateNakedPairFromOthersInRow(int[,] sudokuBoard, int givenRow, int givenCol)
        {
            if (hasNakedPairInRow(sudokuBoard, givenRow, givenCol)) return;

            for (int col = 0; col < sudokuBoard.GetUpperBound(1); col++)
            {
                if (sudokuBoard[givenRow, col] != sudokuBoard[givenRow, givenCol] && sudokuBoard[givenRow, col].ToString().Length > 1)
                {
                    EliminateNakedPair(sudokuBoard, sudokuBoard[givenRow, givenCol], givenRow, col);
                }
            }
        }


        private void EliminateNakedPairFromOthersInCol(int[,] sudokuBoard, int givenRow, int givenCol)
        {
            if (hasNakedPairInCol(sudokuBoard, givenRow, givenCol)) return;

            for (int row = 0; row < sudokuBoard.GetUpperBound(0); row++)
            {
                if (sudokuBoard[row, givenCol] != sudokuBoard[givenRow, givenCol] && sudokuBoard[row, givenCol].ToString().Length > 1)
                {
                    EliminateNakedPair(sudokuBoard, sudokuBoard[givenRow, givenCol], row, givenCol);
                }
            }
        }  

        private void EliminateNakedPair(int[,] sudokuBoard, int valuesToEliminate, int eliminateFromRow, int eliminateFromCol)
        {
            var valuesToEliminateArray = valuesToEliminate.ToString().ToCharArray();
            foreach (var valueToEliminate in valuesToEliminateArray)
            {
                sudokuBoard[eliminateFromRow, eliminateFromCol] = Convert.ToInt32(sudokuBoard[eliminateFromRow, eliminateFromCol].ToString().Replace(valuesToEliminate.ToString(), string.Empty));
            }
        }

        private void EliminateNakedPairFromOthersInBlock(int[,] sudokuBoard, int givenRow, int givenCol)
        {
            if (!HasNakedPairInBlock(sudokuBoard, givenRow, givenCol)) return;

            var sudokuMap = _sudokuMapper.Find(givenRow, givenCol);
            for (int row = sudokuMap.StartRow; row <= sudokuMap.StartRow + 2; row++)
            {
                for (int col = sudokuMap.StartCol; col <= sudokuMap.StartCol + 2; col++)
                {
                    if (sudokuBoard[row, col].ToString().Length > 1 && sudokuBoard[row, col] != sudokuBoard[givenRow, givenCol])
                    {
                        EliminateNakedPair(sudokuBoard, sudokuBoard[givenRow, givenCol], row, col);
                    }
                }
            }
        }  

        private bool isNakedPair(int firstPair, int secondPair)
        {
            return firstPair.ToString().Length == 2 && firstPair == secondPair;
        }

        private bool hasNakedPairInRow(int[,] sudokuBoard, int givenRow, int givenCol)
        {
            for (int col = 0; col < sudokuBoard.GetUpperBound(1); col++)
            {
                if (givenCol != col && isNakedPair(sudokuBoard[givenRow, col], sudokuBoard[givenRow, givenCol])) return true;
            }
            return false;
        }

        private bool hasNakedPairInCol(int[,] sudokuBoard, int givenRow, int givenCol)
        {
            for (int row = 0; row < sudokuBoard.GetUpperBound(0); row++)
            {
                if (givenRow != row && isNakedPair(sudokuBoard[row, givenCol], sudokuBoard[givenRow, givenCol])) return true;
            }
            return false;
        }

        private bool HasNakedPairInBlock(int[,] sudokuBoard, int givenRow, int givenCol)
        {
            for (int row = 0; row < sudokuBoard.GetUpperBound(0); row++)
            {
                for (int col = 0; col < sudokuBoard.GetUpperBound(1); col++)
                {
                    var elementSame = givenRow == row && givenCol == col;
                    var elementInSameBlock = _sudokuMapper.Find(givenRow, givenCol).StartRow == _sudokuMapper.Find(row, col).StartRow &&
                        _sudokuMapper.Find(givenRow, givenCol).StartCol == _sudokuMapper.Find(row, col).StartCol;

                    if (!elementSame && elementInSameBlock && isNakedPair(sudokuBoard[givenRow, givenCol], sudokuBoard[row, col])) return true;
                }
            }
            return false;
        }

    }
}
