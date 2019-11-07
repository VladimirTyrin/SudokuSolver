using SudokuSolver.Engine.Exceptions;

namespace SudokuSolver.Engine.InitialState
{
    public readonly struct InitialFieldState
    {
        public InitialFieldState(int[,] state)
        {
            if (state.GetUpperBound(0) != Constants.FieldSize - 1)
                throw new GameConfigurationException("Field size must be " + Constants.FieldSize + "x" + Constants.FieldSize);
            if (state.GetUpperBound(1) != Constants.FieldSize - 1)
                throw new GameConfigurationException("Field size must be " + Constants.FieldSize + "x" + Constants.FieldSize);

            State = state;
        }

        public int[,] State { get; }
    }
}
