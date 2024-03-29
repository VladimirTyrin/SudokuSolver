﻿using System;
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
            //var state = new int[Constants.FieldSize, Constants.FieldSize];

            //var state = new[,]
            //{
            //    { 0, 0, 0, 0, 0, 4, 0, 6, 0 },
            //    { 0, 0, 4, 0, 3, 0, 1, 0, 0 },
            //    { 0, 0, 7, 0, 0, 0, 0, 0, 2 },
            //    { 0, 0, 5, 7, 0, 0, 9, 2, 0 },
            //    { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            //    { 0, 8, 0, 0, 6, 0, 0, 0, 0 },
            //    { 7, 0, 0, 0, 0, 0, 0, 8, 0 },
            //    { 6, 0, 0, 9, 0, 5, 0, 3, 0 },
            //    { 0, 0, 0, 0, 4, 0, 2, 1, 0 },

            const int _ = 0;
            var state = new[,]
            {
                { _, _, _, _, 6, 8, _, 3, _ },
                { 1, 9, _, _, _, _, _, _, _ },
                { 8, _, 3, 1, _, _, 2, _, _ },
                { 4, _, _, _, 5, 1, _, 6, _ },
                { 7, _, _, _, 2, _, _, _, 4 },
                { _, _, _, _, 7, 3, 8, _, _ },
                { 3, 1, _, _, _, 5, _, _, 7 },
                { _, _, 4, _, _, _, _, 8, _ },
                { _, 5, _, 7, 3, 4, 1, _, _ },
            };

            //state[0, 0] = 1;
            //state[0, 1] = 2;
            //state[0, 2] = 3;
            //state[0, 3] = 4;
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
