namespace SudokuSolver.Engine.InitialState
{
    public class EmptyInitialStateProvider : IInitialStateProvider
    {
        public InitialFieldState GetInitialFieldState()
        {
            var emptyField = new int[Constants.FieldSize, Constants.FieldSize];
            return new InitialFieldState(emptyField);
        }
    }
}