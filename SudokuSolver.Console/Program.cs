using System;
using System.Text;
using SudokuSolver.Engine;
using SudokuSolver.Engine.InitialState;

namespace SudokuSolver.Console
{
    internal static class Program
    {
        private const int PrintEverySteps = 10_000;

        private static void Main()
        {
            var initialStateProvider = GetInitialStateProvider();

            var game = GameFactory.Create(initialStateProvider);

            game.Step += GameOnStep;
            game.GameCompleted += GameOnGameCompleted;

            game.Run();
        }

        private static IInitialStateProvider GetInitialStateProvider()
        {
            var state = new int[Constants.FieldSize, Constants.FieldSize];

            state[0, 0] = 1;
            state[0, 1] = 2;
            state[0, 2] = 3;
            state[0, 3] = 4;
            //state[0, 4] = 5;
            //state[0, 5] = 6;
            // state[0, 6] = 7;
            //state[0, 7] = 8;
            //state[0, 8] = 9;

            return new FixedInitialStateProvider(state);
        }

        private static void GameOnStep(object sender, FieldState e)
        {
            if (e.AlgorithmStep % PrintEverySteps != 0)
                return;
            DrawState(e);
        }

        private static void GameOnGameCompleted(object sender, FieldState e)
        {
            var prevColor = System.Console.ForegroundColor;
            System.Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine($"Completed in {e.AlgorithmStep} steps");
            DrawState(e);
            System.Console.ForegroundColor = prevColor;
        }

        private static void DrawState(FieldState e)
        {
            var builder = new StringBuilder(120);
            builder.AppendLine($"Step {e.AlgorithmStep} - filled {e.FilledCellsCount}");
            for (var i = 0; i < Constants.FieldSize; i++)
            {
                for (var j = 0; j < Constants.FieldSize; j++)
                {
                    if (e[i, j] == 0)
                    {
                        builder.Append(' ');
                    }
                    else
                    {
                        builder.Append(e[i, j]);
                    }
                }

                builder.Append("|");
                builder.AppendLine();
            }

            for (var i = 0; i < Constants.FieldSize; i++)
            {
                builder.Append('-');
            }
            builder.AppendLine();

            builder.AppendLine();
            System.Console.WriteLine(builder.ToString());
        }
    }
}
