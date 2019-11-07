using System;

namespace SudokuSolver.Engine.InitialState
{
    public class FuncInitialStateProvider : IInitialStateProvider
    {
        private readonly Func<InitialFieldState> _func;

        public FuncInitialStateProvider(Func<InitialFieldState> func)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        public InitialFieldState GetInitialFieldState()
        {
            return _func();
        }
    }
}
