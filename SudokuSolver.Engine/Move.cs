namespace SudokuSolver.Engine
{
    public struct Move
    {
        public Move(int i, int j, int value)
        {
            I = i;
            J = j;
            Value = value;
        }

        public readonly int I;

        public readonly int J;

        public readonly int Value;
    }
}