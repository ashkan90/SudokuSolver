using SudokuSolver.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Strategies
{
    class SudokuSolverEngine
    {
        private readonly SudokuMapper _sudokuMapper;
        private readonly SudokuBoardStateManager _sudokuBoardStateManager;

        public SudokuSolverEngine(SudokuBoardStateManager state, SudokuMapper mapper)
        {
            _sudokuBoardStateManager = state;
            _sudokuMapper = mapper;
        }

        public bool Solve(int[,] sudokuBoard)
        {
            List<ISudokuStrategy> strategies = new List<ISudokuStrategy>()
            {
                new SimpleMarkupStrategy(_sudokuMapper),
                new NakedPairsStrategy(_sudokuMapper)
            };

            var currentState = _sudokuBoardStateManager.GenerateState(sudokuBoard);
            var nextState = _sudokuBoardStateManager.GenerateState(strategies.First().Solve(sudokuBoard));

            while (!_sudokuBoardStateManager.isSolved(sudokuBoard) && currentState != nextState)
            {
                currentState = nextState;
                foreach (var strategy in strategies)
                    nextState = _sudokuBoardStateManager.GenerateState(strategy.Solve(sudokuBoard));
            }

            return _sudokuBoardStateManager.isSolved(sudokuBoard);
        }
    }
}
