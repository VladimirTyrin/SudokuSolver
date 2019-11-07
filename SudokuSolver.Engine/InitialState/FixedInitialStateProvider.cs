namespace SudokuSolver.Engine.InitialState
{
    public class FixedInitialStateProvider : IInitialStateProvider
    {
        private readonly int[,] _state;

        public FixedInitialStateProvider(int[,] state)
        {
            _state = state;
        }

        public InitialFieldState GetInitialFieldState()
        {
            return new InitialFieldState(_state);
        }
    }
}
