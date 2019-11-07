namespace SudokuSolver.Engine.InitialState
{
    public interface IInitialStateProvider
    {
        InitialFieldState GetInitialFieldState();
    }
}