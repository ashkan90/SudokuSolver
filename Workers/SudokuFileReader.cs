using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Workers
{
    class SudokuFileReader
    {
        public int[,] Read(string filename)
        {
            int[,] sudokuBoard = new int[9, 9];

            try
            {
                // Get all lines from text file.
                var sudokuBoardLines = File.ReadAllLines(filename);

                int row = 0;
                // Read all lines row by row
                foreach (var sudokuBoardLine in sudokuBoardLines)
                {
                    // Get readed row's splitted value as a array
                    string[] sudokuBoardLineElements = sudokuBoardLine.Split('|').Skip(1).Take(9).ToArray();

                    int col = 0;
                    // Read all numbers in rows
                    foreach (var sudokuBoardLineElement in sudokuBoardLineElements)
                    {
                        // Save the which row has what number and increase column
                        sudokuBoard[row, col] = sudokuBoardLineElement.Equals(" ") ? 0 : Convert.ToInt16(sudokuBoardLineElement);
                        col++;
                    }
                    row++;
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Something went wrong while reading the file: " + ex.Message);
            }

            return sudokuBoard;
        }
    }
}
