using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;


namespace PingPong
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private bool _isInGame = true;
        private DispatcherTimer _timer = new DispatcherTimer();
        private DispatcherTimer _playTimer = new DispatcherTimer();

        private int _score;
        private const int MaxScore = 20;
        private int _timeSpent;
        private const int MaxTime = 180;
        private int _level;
        private double _chanceToDropGem;
        private bool _gemDrop;
        private Ball _ball;
        private Gem _gem;
        private Paddle _paddle;
        public static bool GemIsActive;

        public MainWindow()
        {
            InitializeComponent();
            _ball = new Ball(BallVisual, this, PaddleVisual);
            _gem  = new Gem(GemVisual,this,PaddleVisual, _ball, GemDescription);
            _paddle = new Paddle(PaddleVisual, this);
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            _ball.Move();
            if (_gemDrop) _gem.Move(ref _gemDrop);
        }


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (_isInGame)
            {
                switch (e.Key)
                {
                    case Key.Escape:
                        HandleEscapeKey();
                        break;
                    case Key.Right:
                        _paddle.Move("Right");
                        break;
                    case Key.Left:
                        _paddle.Move("Left");
                        break;
                    case Key.Space:
                        Pause();
                        break;
                }
            }
            else if (e.Key == Key.Space)
            {
                Resume();
            }
        }


        private void HandleEscapeKey()
        {
            Pause();
            PopUpMenu.Visibility = Visibility.Visible;
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            PopUpMenu.Visibility = Visibility.Hidden;
            Resume();
        }

        public void IncreaseScore()
        {
            _score++;
            if (_score == MaxScore)
            {
                Pause();
                CongratulationPopUp();
            }

            ScoreVisual.Content = $"Score: {_score}";
        }

        private void PlayTimer_Tick(object sender, EventArgs e)
        {
            _timeSpent++;
            if (!_gemDrop && !GemIsActive) _chanceToDropGem += 0.01;
            _gem.DecideToDrop(ref _chanceToDropGem, ref _gemDrop);
            if (_level == 3 && _timeSpent % 30 == 0) _ball.IncreaseLevel();
            if ((double) _timeSpent / MaxTime > (double) _score / MaxScore)
            {
                ProgressBar.Value = _timeSpent;
            }
            else
            {
                ProgressBar.Value = (double) _score / MaxScore * MaxTime;
            }

            ProgressPercentage.Content = $"{(int) ((ProgressBar.Value / ProgressBar.Maximum) * 100)} %";
            if (!(ProgressBar.Value >= 180)) return;
            _playTimer.Stop();
            Pause();
            CongratulationPopUp();
        }

        private void Resume()
        {
            _playTimer.Tick += PlayTimer_Tick;
            _timer.Tick += dispatcherTimer_Tick;
            _isInGame = true;
        }

        private void Pause()
        {
            _playTimer.Tick -= PlayTimer_Tick;
            _timer.Tick -= dispatcherTimer_Tick;
            _isInGame = false;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (BasicLevel.IsChecked != null && (bool) BasicLevel.IsChecked)
            {
                _level = 1;
                LevelDescription.Content = "Basic";
            }
            else if (IntermediateLevel.IsChecked != null && (bool) IntermediateLevel.IsChecked)
            {
                _level = 2;
                LevelDescription.Content = "Intermediate";
            }
            else
            {
                _level = 3;
                LevelDescription.Content = "Expert";
            }

            _ball.SetLevel(_level);
            _gem.SetLevel(_level);
            StartMenu.Visibility = Visibility.Hidden;
            _timer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 0, 0, 10)
            };
            _timer.Tick += dispatcherTimer_Tick;
            _timer.Start();
            _playTimer.Interval = new TimeSpan(0, 0, 1);
            _playTimer.Tick += PlayTimer_Tick;
            _playTimer.Start();
        }

        private void CongratulationPopUp()
        {
            PopUpCongratText.Content =
                $"Congratulations! You have reached {_score} points in only {_timeSpent} seconds!!";
            PopUpCongrat.Visibility = Visibility.Visible;
        }
    }
}