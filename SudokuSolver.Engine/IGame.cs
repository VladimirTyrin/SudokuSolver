using System;

namespace SudokuSolver.Engine
{
    public interface IGame
    {
        void Run();

        event EventHandler<FieldState> Step;

        event EventHandler<FieldState> GameCompleted;
    }
}