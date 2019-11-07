using System.Text;
using SudokuSolver.Engine;
using SudokuSolver.Engine.InitialState;

namespace SudokuSolver.Console
{
    internal static class Program
    {
        private static void Main()
        {
            var initialStateProvider = new EmptyInitialStateProvider();

            var game = GameFactory.Create(initialStateProvider);

            game.Step += GameOnStep;

            game.Run();
        }

        private static void GameOnStep(object sender, FieldState e)
        {
            var builder = new StringBuilder(120);
            builder.AppendLine($"{e.AlgorithmStep} - {e.FilledCellsCount}");
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

                builder.AppendLine();
            }

            builder.AppendLine();
            System.Console.WriteLine(builder.ToString());
        }
    }
}
