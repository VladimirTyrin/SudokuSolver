using System;

namespace SudokuSolver.Engine.Exceptions
{
    public class GameConfigurationException : Exception
    {
        public GameConfigurationException(string message) : base(message)
        {

        }
    }
}