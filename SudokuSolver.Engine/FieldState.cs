using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using SudokuSolver.Engine.InitialState;

namespace SudokuSolver.Engine
{
    public class FieldState
    {
        public int[] State { get; }

        public int FilledCellsCount { get; }

        public ImmutableArray<Move> History { get; }

        public int AlgorithmStep { get; }

        public FieldState(InitialFieldState initialFieldState)
        {
            State = new int[Constants.FieldCellCount];
            for (var i = 0; i < Constants.FieldSize; ++i)
            {
                for (var j = 0; j < Constants.FieldSize; ++j)
                {
                    this[i, j] = initialFieldState.State[i, j];
                    if (initialFieldState.State[i, j] > 0)
                    {
                        FilledCellsCount++;
                    }
                }
            }

            History = ImmutableArray<Move>.Empty;
            AlgorithmStep = 0;
        }

        public FieldState(FieldState prev, Move move, int step)
        {
            State = new int[Constants.FieldCellCount];
            Array.Copy(prev.State, State, Constants.FieldCellCount);
            this[move.I, move.J] = move.Value;
            FilledCellsCount = prev.FilledCellsCount + 1;
            History = prev.History.Add(move);
            AlgorithmStep = step;
        }

        public int this[int i, int j]
        {
            get => State[i * Constants.FieldSize + j];
            private set => State[i * Constants.FieldSize + j] = value;
        }

        public IEnumerable<(int i, int j)> GetEmptyCells()
        {
            for (var i = 0; i < Constants.FieldSize; ++i)
            {
                for (var j = 0; j < Constants.FieldSize; ++j)
                {
                    if (this[i, j] == 0)
                        yield return (i, j);
                }
            }
        }

        public IEnumerable<int> GetPossibleMoves(int i, int j)
        {
            var filled = new int[Constants.FieldSize + 1];

            for (var ii = 0; ii < Constants.FieldSize; ++ii)
            {
                if (ii == i)
                    continue;

                filled[this[ii, j]]++;
            }

            for (var jj = 0; jj < Constants.FieldSize; ++jj)
            {
                if (jj == j)
                    continue;

                filled[this[i, jj]]++;
            }

            var iIndex = (i / Constants.SquareSize) * Constants.SquareSize;
            var jIndex = (j / Constants.SquareSize) * Constants.SquareSize;

            for (var ii = 0; ii < Constants.SquareSize; ++ii)
            {
                for (var jj = 0; jj < Constants.SquareSize; ++jj)
                {
                    filled[this[iIndex + ii, jIndex + jj]]++;
                }
            }

            for (var possibleMove = 1; possibleMove <= Constants.FieldSize; ++possibleMove)
            {
                if (filled[possibleMove] > 0)
                    continue;

                yield return possibleMove;
            }
        }

        public bool IsCompleted => FilledCellsCount == Constants.FieldCellCount;
    }
}