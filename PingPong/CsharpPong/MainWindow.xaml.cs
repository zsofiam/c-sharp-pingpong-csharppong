using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CsharpPong
{
    // This needs some other info like ball speed and stuff
    struct LevelInfo
    {
        private string _name;
        private int _requiredScore;
        private int _maxTime;

        public LevelInfo(string name, int requiredScore, int maxTime)
        {
            this._name = name;
            this._requiredScore = requiredScore;
            this._maxTime = maxTime;
        }

        public string GetName()
        {
            return _name;
        }

        public int GetRequiredScore()
        {
            return _requiredScore;
        }

        public int GetMaxTime()
        {
            return _maxTime;
        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _debug = false;

        //Classes
        private Paddle _paddle;
        private Ball _ball;

        //Variables
        private LevelInfo[] _levels;
        private bool _inGame = true;
        private bool _isPaused = false;
        private int _level = 1;
        private int _score = 0;

        private DispatcherTimer _playTimer = new DispatcherTimer();
        private int _timeSpent = 0;


        public MainWindow()
        {
            InitializeComponent();
            _paddle = new Paddle(this, PaddleVisual);
            _ball = new Ball(BallVisual, _paddle, this);
            _levels = new LevelInfo[4];
            CreateLevelData();

            InitLevelText();
            InitProgressBars();
            UpdateOnScreenInfo();
        }
        //Window stuff
        //Key presses
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (_inGame && !_isPaused)
            {
                switch (e.Key)
                {
                    case Key.Left:
                    case Key.A:
                        _paddle.Move("left");
                        break;

                    case Key.Right:
                    case Key.D:
                        _paddle.Move("right");
                        break;

                    //Manually update the on-screen-info
                    //Later this HAS TO be moved to the trigger when the ball and the paddle collides!
                    case Key.G:
                        UpdateOnScreenInfo();
                        break;


                    case Key.NumPad0:
                        if (_debug) Play(0);
                        break;
                    case Key.NumPad1:
                        if (_debug) Play(1);
                        break;

                    case Key.NumPad2:
                        if (_debug) Play(2);
                        break;
                    case Key.NumPad3:
                        if (_debug) Play(3);
                        break;

                    case Key.Escape:
                        HandleEscKey();
                        break;

                    case Key.Space:
                        if (_isPaused) Resume();
                        else Pause();
                        break;
                }
            }
            else
            {
                if ((_inGame) && (e.Key == Key.Space))
                {
                    if (_isPaused) Resume();
                    else Pause();
                }
            }
        }

        private void HandleEscKey()
        {
            //should pause game
            PopUpMenu.Visibility = Visibility.Visible;
            _ball.halt();
        }
        
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ScoreProgressVisual.Width = this.ActualWidth;
            TimeProgressVisual.Width = this.ActualWidth;
        }

        internal void IncreaseScore()
        {
            _score++;
        }

        private void PlayTimer_Tick(object sender, EventArgs e)
        {
            _timeSpent++;
            UpdateOnScreenInfo();

            _timeSpent++;
            UpdateProgressBars();

            if (_timeSpent >= _levels[_level].GetMaxTime()) {
                _playTimer.Stop();
                Stop();
                MessageBox.Show("TIME IS OVER!");
            }

            else if (_score >= _levels[_level].GetRequiredScore()){
                _playTimer.Stop();
                Stop();
                MessageBox.Show($"CONGRATS! You scored {_score}!");
            }

        }

        // On screen stuff
        private void InitLevelText()
        {
            LevelVisual.Text = _levels[_level].GetName();
        }

        private void InitProgressBars()
        {
            ScoreProgressVisual.Maximum = _levels[_level].GetRequiredScore();
            TimeProgressVisual.Maximum = _levels[_level].GetMaxTime();
        }

        private void UpdateOnScreenInfo()
        {
            UpdateScore();

            UpdateProgressBars();
        }
        //CALL THIS ONCE SCORES CHANGE
        private void UpdateScore()
        {
            ScoreVisual.Text = _score.ToString();
        }

        private void UpdateProgressBars()
        {
            ScoreProgressVisual.Value = _score;
            TimeProgressVisual.Value = _timeSpent;
        }
        
        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            PopUpMenu.Visibility = Visibility.Hidden;
            _ball.restart();
        }

        //Actual game control
        private void Play(int level)
        {
            this._level = level;
            Restart();

            _ball.SetDirection();
            InitLevelText();
            InitProgressBars();
            UpdateOnScreenInfo();

            //Start timer
            _playTimer.Interval = new TimeSpan(0, 0, 0, 0, 50);
            _playTimer.Tick += PlayTimer_Tick;
            _playTimer.Start();
            _ball.startBall(level);

            _inGame = true;
            _isPaused = false;
        }

        private void Pause()
        {
            _playTimer.Tick -= PlayTimer_Tick;
            _playTimer.Stop();
            _ball.halt();
            PauseVisual.Visibility = Visibility.Visible;

            _isPaused = true;
        }

        private void Stop()
        {
            _playTimer.Tick -= PlayTimer_Tick;
            _playTimer.Stop();
            _ball.halt();
        }

        private void Resume()
        {
            _playTimer.Tick += PlayTimer_Tick;
            _playTimer.Start();
            _ball.restart();
            PauseVisual.Visibility = Visibility.Hidden;

            _isPaused = false;
        }

        private void Restart()
        {
            _playTimer.Tick -= PlayTimer_Tick;

            _timeSpent = 0;
            _score = 0;

            _inGame = false;
            _isPaused = false;
        }

        // Other and things that could get outsourced or have some debuggy purpose
            //This one is dummy/prototype level code, focus on later moving this to like a config file!!!
        private void CreateLevelData()
        {
            LevelInfo easy = new LevelInfo("Basic", 5, 1000);
            LevelInfo intermediate = new LevelInfo("Intermediate", 10, 700);
            LevelInfo expert = new LevelInfo("Expert", 20, 400);
            //This is only here as to serve a placeholder in the 0th place in the array
            //Technically it should be impossible to ever start this! Later we could use it as a "developer hack" for instant win or something
            LevelInfo hacker = new LevelInfo("hacker", 50, 100);

            _levels[0] = hacker;
            _levels[1] = easy;
            _levels[2] = intermediate;
            _levels[3] = expert;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (BasicLevel.IsChecked != null && (bool)BasicLevel.IsChecked)
            {
                _level = 1;
                
            }
            else if (IntermediateLevel.IsChecked != null && (bool)IntermediateLevel.IsChecked)
            {
                _level = 2;
                
            }
            else
            {
                _level = 3;
               
            }

            Play(_level);
            StartMenu.Visibility = Visibility.Hidden;
        }
    }
}
