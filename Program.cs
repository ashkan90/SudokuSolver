using SudokuSolver.Strategies;
using SudokuSolver.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                SudokuMapper sudokuMapper = new SudokuMapper();
                SudokuBoardStateManager sudokuBoardStateManager = new SudokuBoardStateManager();
                SudokuBoardDisplayer sudokuBoardDisplayer = new SudokuBoardDisplayer();
                SudokuSolverEngine engine = new SudokuSolverEngine(sudokuBoardStateManager, sudokuMapper);
                SudokuFileReader sudokuFileReader = new SudokuFileReader();

                Console.WriteLine("Please enter the filename containing Sudoku Puzzle:");
                var filename = Console.ReadLine();

                var sudokuBoard = sudokuFileReader.Read(filename);
                sudokuBoardDisplayer.Display("Initial State", sudokuBoard);

                bool isSolved = engine.Solve(sudokuBoard);
                sudokuBoardDisplayer.Display("Final State", sudokuBoard);

                Console.WriteLine(isSolved 
                    ? "You have successfully solved this Sudoku Puzzle"
                    : "Unfortunately, current algorithm(s) were not enough to solve this current Sudoku Puzzle!");

                Console.ReadKey();
            }
            catch (Exception ex)
            {
                // In real world apps there will be log.
                Console.WriteLine("{0} : {1}", "Sudoku Puzzle cannot be solved because there was an error: ", ex.Message);
            }
        }
    }

}
