using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SudokuSolver.Engine;
using SudokuSolver.Engine.InitialState;

namespace SudokuSolver.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly TextBox[,] _textBoxes = new TextBox[Constants.FieldSize, Constants.FieldSize];
        private readonly Label[,] _borders = new Label[Constants.SquareSize, Constants.SquareSize];
        private readonly ConcurrentStack<Action> _uiUpdates = new ConcurrentStack<Action>();
        
        public MainWindow()
        {
            InitializeComponent();

            
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            for (var i = 0; i < Constants.FieldSize; ++i)
            {
                CellsGrid.ColumnDefinitions.Add(new ColumnDefinition());
                CellsGrid.RowDefinitions.Add(new RowDefinition());
            }

            for (var i = 0; i < Constants.SquareSize; ++i)
            {
                for (var j = 0; j < Constants.SquareSize; ++j)
                {
                    _borders[i, j] = new Label();
                    var border = _borders[i, j];
                    border.SetValue(Grid.RowProperty, i * Constants.SquareSize);
                    border.SetValue(Grid.ColumnProperty, j * Constants.SquareSize);
                    border.SetValue(Grid.RowSpanProperty, Constants.SquareSize);
                    border.SetValue(Grid.ColumnSpanProperty, Constants.SquareSize);
                    border.BorderThickness = new Thickness(0.5);
                    border.BorderBrush = Brushes.Black;
                    border.HorizontalAlignment = HorizontalAlignment.Stretch;
                    border.VerticalAlignment = VerticalAlignment.Stretch;
                    CellsGrid.Children.Add(border);
                }
            }

            for (var i = 0; i < Constants.FieldSize; i++)
            {
                for (var j = 0; j < Constants.FieldSize; j++)
                {
                    _textBoxes[i, j] = new TextBox(); 
                    var textBox = _textBoxes[i, j];
                    textBox.HorizontalAlignment = HorizontalAlignment.Center;
                    textBox.VerticalAlignment = VerticalAlignment.Center;
                    textBox.Margin = new Thickness(5);
                    textBox.Height = 32;
                    textBox.Width = 32;
                    textBox.FontSize = 24;
                    textBox.VerticalContentAlignment = VerticalAlignment.Center;
                    textBox.HorizontalContentAlignment = HorizontalAlignment.Center;
                    CellsGrid.Children.Add(_textBoxes[i, j]);
                    textBox.SetValue(Grid.RowProperty, i);
                    textBox.SetValue(Grid.ColumnProperty, j);
                    textBox.Text = "";
                }
            }

            
        }

        private void StartButton_OnClick(object sender, RoutedEventArgs e)
        {
            var provider = new FuncInitialStateProvider(() =>
            {
                var state = new int[Constants.FieldSize, Constants.FieldSize];
                for (var i = 0; i < Constants.FieldSize; ++i)
                {
                    for (var j = 0; j < Constants.FieldSize; ++j)
                    {
                        if (string.IsNullOrWhiteSpace(_textBoxes[i, j].Text))
                        {
                            state[i, j] = 0;
                        }
                        else
                        {
                            state[i, j] = int.Parse(_textBoxes[i, j].Text);
                        }
                        
                    }
                }

                return new InitialFieldState(state);
            });

            var game = GameFactory.Create(provider);

            game.Step += GameOnStep;
            game.GameCompleted += GameOnGameCompleted;
            Task.Run(() => game.Run());
        }

        private void GameOnGameCompleted(object sender, FieldState e)
        {
            App.RunOnUiThread(() => DrawState(e));
        }

        private void GameOnStep(object sender, FieldState e)
        {
            Action action = () =>
            {
                DrawState(e);
            };
            Thread.Sleep(100);
            _uiUpdates.Push(action);
            App.RunOnUiThread(() =>
            {
                if (_uiUpdates.TryPop(out var update))
                {
                    update.Invoke();
                }

                while (_uiUpdates.TryPop(out _))
                {
                    
                }
            });
        }

        private void DrawState(FieldState e)
        {
            for (var i = 0; i < Constants.FieldSize; ++i)
            {
                for (var j = 0; j < Constants.FieldSize; ++j)
                {
                    if (e[i, j] == 0)
                    {
                        _textBoxes[i, j].Clear();
                    }
                    else
                    {
                        _textBoxes[i, j].Text = e[i, j].ToString();
                    }
                }
            }
        }

        private void Clear_OnClick(object sender, RoutedEventArgs e)
        {
            for (var i = 0; i < Constants.FieldSize; ++i)
            {
                for (var j = 0; j < Constants.FieldSize; ++j)
                {
                    _textBoxes[i, j].Text = string.Empty;
                }
            }
        }
    }
}
