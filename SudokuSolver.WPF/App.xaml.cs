using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;

namespace SudokuSolver.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    [SuppressMessage("ReSharper", "RedundantExtendsListEntry")]
    public partial class App : Application
    {
        public static void RunOnUiThread(Action action) => App.Current.Dispatcher?.InvokeAsync(action);
    }
}
