using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace PingPong
{
    class Gem : Rectangle
    {
        private new static readonly Random Random = new Random();
        private GemType _gemType;
        private bool _gemTypeSet = false;
        private readonly System.Windows.Shapes.Rectangle _paddle;
        private DispatcherTimer _dispatcherTimer = new DispatcherTimer();
        private Ball _ball;
        private Label _gemLabel;

        public Gem(System.Windows.Shapes.Rectangle rectangle, MainWindow mainWindow,
            System.Windows.Shapes.Rectangle paddle, Ball ball, Label gemLabel) : base(rectangle, mainWindow)
        {
            _paddle = paddle;
            _ball = ball;
            this._gemLabel = gemLabel;
            _dispatcherTimer.Tick += DeactivateGem;
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 20);
        }

        public void DecideToDrop(ref double chance, ref bool drop)
        {
            if (Random.NextDouble() <= chance)
            {
                chance = 0;
                drop = true;
                Direction["topMargin"] = Level;
            }
        }

        public void Move(ref bool drop)
        {
            VisualRectangle.Visibility = Visibility.Visible;
            if (!_gemTypeSet) _gemType = GenerateGemType();
            double topMargin = VisualRectangle.Margin.Top;
            if (topMargin >= MainWindow.ActualHeight - 1.5 * _paddle.ActualHeight - 2 * VisualRectangle.ActualHeight &&
                _paddle.Margin.Left - _paddle.ActualWidth <= VisualRectangle.Margin.Left &&
                _paddle.Margin.Left + _paddle.ActualWidth >= VisualRectangle.Margin.Left)
            {
                ActivateGem(ref drop);
                ResetPosition();
                topMargin = 10;
            }

            if (topMargin >= MainWindow.ActualHeight - VisualRectangle.ActualHeight * 1.75)
            {
                drop = false;
                _gemTypeSet = false;
                VisualRectangle.Visibility = Visibility.Hidden;
                ResetPosition();
                topMargin = 10;
            }
            else
            {
                topMargin += Direction["topMargin"];
                VisualRectangle.Margin =
                    new Thickness(VisualRectangle.Margin.Left, topMargin, VisualRectangle.Margin.Right,
                        VisualRectangle.Margin.Bottom);
            }
        }

        private GemType GenerateGemType()
        {
            Array gemTypes = Enum.GetValues(typeof(GemType));
            return (GemType) gemTypes.GetValue(Random.Next(gemTypes.Length));
        }

        private void ActivateGem(ref bool drop)
        {
            MainWindow.GemIsActive = true;
            drop = false;
            _gemTypeSet = false;
            VisualRectangle.Visibility = Visibility.Hidden;
            ResetPosition();
            switch (_gemType)
            {
                case GemType.Extender:
                    _paddle.Width *= 2;
                    _gemLabel.Content = "Extender gem activated";
                    break;
                case GemType.Shortener:
                    _paddle.Width /= 2;
                    _gemLabel.Content = "Extender gem activated";
                    break;
                case GemType.Slower:
                    _ball.SetLevel(_ball.GetLevel() / 2);
                    _gemLabel.Content = "Slower gem activated";
                    break;
                case GemType.Speeder:
                    _ball.SetLevel(_ball.GetLevel() * 2);
                    _gemLabel.Content = "Speeder gem activated";
                    break;
            }

            _gemLabel.Visibility = Visibility.Visible;
            _dispatcherTimer.Start();
            
        }

        private void DeactivateGem(object sender, EventArgs e)
        {
            _gemLabel.Visibility = Visibility.Hidden;
            MainWindow.GemIsActive = false;
            switch (_gemType)
            {
                case GemType.Extender:
                    _paddle.Width /= 2;
                    break;
                case GemType.Shortener:
                    _paddle.Width *= 2;
                    break;
                case GemType.Slower:
                    _ball.SetLevel(_ball.GetLevel() * 2);
                    break;
                case GemType.Speeder:
                    _ball.SetLevel(_ball.GetLevel() / 2);
                    break;
            }
            _dispatcherTimer.Stop();
        }
    }

    enum GemType
    {
        Speeder = 1,
        Slower = 2,
        Extender = 3,
        Shortener = 4
    }
}