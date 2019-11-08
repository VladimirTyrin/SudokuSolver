using System;
using System.Collections.Generic;
using System.Linq;
using SudokuSolver.Engine.InitialState;

namespace SudokuSolver.Engine
{
    public class Game : IGame
    {
        private readonly Stack<FieldState> _states = new Stack<FieldState>();

        public Game(InitialFieldState initialFieldState)
        {
            _states.Push(new FieldState(initialFieldState));
        }

        public void Run()
        {
            var step = 0;
            while (_states.TryPop(out var state))
            {
                ++step;
                OnStep(state);
                if (state.IsCompleted)
                {
                    OnGameCompleted(state);
                    return;
                }

                var stepMoves = new List<(Move move, int moveCount)>();
                var badStateDetected = false;
                foreach (var (i, j) in state.GetEmptyCells())
                {
                    var possibleMoves = state.GetPossibleMoves(i, j).ToArray();
                    if (possibleMoves.Length == 0)
                    {
                        badStateDetected = true;
                        break;
                    }
                    stepMoves.AddRange(possibleMoves.Select(possibleMove => (new Move(i, j, possibleMove), possibleMoves.Length)));
                }

                if (badStateDetected)
                    continue;

                foreach (var (move, _) in stepMoves.OrderByDescending(m => m.moveCount))
                {
                    _states.Push(new FieldState(state, move, step));
                }
            }
        }

        public event EventHandler<FieldState> Step;
        public event EventHandler<FieldState> GameCompleted;

        protected virtual void OnStep(FieldState e)
        {
            Step?.Invoke(this, e);
        }

        protected virtual void OnGameCompleted(FieldState e)
        {
            GameCompleted?.Invoke(this, e);
        }
    }
}