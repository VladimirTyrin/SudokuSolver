namespace SudokuSolver.Engine
{
    internal static class FieldValidator
    {
        public static bool ValidateState(int[,] state, out string errorMessage)
        {
            errorMessage = null;
            return true;
        }
    }
}