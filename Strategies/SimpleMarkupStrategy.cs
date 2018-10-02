using SudokuSolver.Workers;
using System;
using System.Linq;


namespace SudokuSolver.Strategies
{
    class SimpleMarkupStrategy : ISudokuStrategy
    {
        private readonly SudokuMapper _sudokuMapper;
        public SimpleMarkupStrategy(SudokuMapper mapper)
        {
            _sudokuMapper = mapper;
        }

        public int[,] Solve(int[,] sudokuBoard)
        {
            for (int row = 0; row < sudokuBoard.GetUpperBound(0); row++)
            {
                for (int col = 0; col < sudokuBoard.GetUpperBound(1); col++)
                {
                    if (sudokuBoard[row,col] == 0 || sudokuBoard[row, col].ToString().Length > 1)
                    {
                        var possibilitiesInRowAndCol = GetPossibilitiesInRowAndCol(sudokuBoard, row, col);
                        var possibilitiesInBlock = GetPossibilitiesInBlock(sudokuBoard, row, col);
                        sudokuBoard[row, col] = GetPossibilityIntersection(possibilitiesInRowAndCol, possibilitiesInBlock);

                    }
                }
            }

            return sudokuBoard;
        }

        private int GetPossibilitiesInRowAndCol(int[,] sudokuBoard, int givenRow, int givenCol)
        {
            int[] possibilities = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            for (int row = 0; row < 9; row++) if (isValidSingle(sudokuBoard[row, givenCol])) possibilities[sudokuBoard[row, givenCol] - 1] = 0;
            for (int col = 0; col < 9; col++) if (isValidSingle(sudokuBoard[givenRow, col])) possibilities[sudokuBoard[givenRow, col] - 1] = 0;

            return Convert.ToInt32(string.Join(string.Empty, possibilities.Select(p => p).Where(p => p != 0)));
        }

        private int GetPossibilitiesInBlock(int[,] sudokuBoard, int givenRow, int givenCol)
        {
            int[] possibilities = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var sudokuMap = _sudokuMapper.Find(givenRow,givenCol);

            for (int row = sudokuMap.StartRow; row <= sudokuMap.StartRow + 2; row++)
            {
                for (int col = sudokuMap.StartCol; col <= sudokuMap.StartCol + 2; col++)
                {
                    if (isValidSingle(sudokuBoard[row, col])) possibilities[sudokuBoard[row, col] - 1] = 0;
                }
            }

            return Convert.ToInt32(string.Join(string.Empty, possibilities.Select(p => p).Where(p => p != 0)));
        }

        private int GetPossibilityIntersection(object possibilitiesInRowAndCol, object possibilitiesInBlock)
        {
            var possibilitiesInRowAndColCharArray = possibilitiesInRowAndCol.ToString().ToCharArray();
            var possibilitiesInBlockCharArray = possibilitiesInBlock.ToString().ToCharArray();
            var possibilitiesSubset = possibilitiesInBlockCharArray.Intersect(possibilitiesInRowAndColCharArray);

            return Convert.ToInt32(string.Join(string.Empty, possibilitiesSubset));

        }

        private bool isValidSingle(int v)
        {
            return v != 0 && v.ToString().Length == 1;
        }
    }
}
