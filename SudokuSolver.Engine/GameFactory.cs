using SudokuSolver.Engine.Exceptions;
using SudokuSolver.Engine.InitialState;

namespace SudokuSolver.Engine
{
    public static class GameFactory
    {
        public static IGame Create(IInitialStateProvider initialStateProvider)
        {
            var initialState = initialStateProvider.GetInitialFieldState();

            if (!FieldValidator.ValidateState(initialState.State, out var errorMessage))
            {
                throw new GameConfigurationException(errorMessage);
            }

            return new Game(initialState);
        }
    }
}