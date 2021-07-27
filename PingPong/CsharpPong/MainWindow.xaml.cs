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
        private string name;
        private int requiredScore;
        private int maxTime;

        public LevelInfo(string name, int requiredScore, int maxTime)
        {
            this.name = name;
            this.requiredScore = requiredScore;
            this.maxTime = maxTime;
        }

        public string getName()
        {
            return name;
        }

        public int getRequiredScore()
        {
            return requiredScore;
        }

        public int getMaxTime()
        {
            return maxTime;
        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool DEBUG = true;


        //Classes
        private Paddle paddle;

        //Variables
        private LevelInfo[] levels;
        private bool inGame = true;
        private bool isPaused = false;
        private int level = 1;
        private int score = 0;

        private DispatcherTimer playTimer = new DispatcherTimer();
        private int timeSpent = 0;


        public MainWindow()
        {
            InitializeComponent();
            paddle = new Paddle(this, PaddleVisual);

            levels = new LevelInfo[4];
            createLevelData();

            initLevelText();
            initProgressBars();
            updateOnScreenInfo();
        }

        //Window stuff
        //Key presses
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (inGame && !isPaused)
            {
                switch (e.Key)
                {
                    case Key.Left:
                    case Key.A:
                        paddle.Move("left");
                        break;

                    case Key.Right:
                    case Key.D:
                        paddle.Move("right");
                        break;


                    case Key.NumPad0:
                        if (DEBUG) play(0);
                        break;
                    case Key.NumPad1:
                        if (DEBUG) play(1);
                        break;

                    case Key.NumPad2:
                        if (DEBUG) play(2);
                        break;
                    case Key.NumPad3:
                        if (DEBUG) play(3);
                        break;
                }
            }
        }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ScoreProgressVisual.Width = this.ActualWidth;
            TimeProgressVisual.Width = this.ActualWidth;
        }
        private void PlayTimer_Tick(object sender, EventArgs e)
        {
            timeSpent++;
            updateProgressBars();

            if (!(timeSpent >= levels[level].getMaxTime())) return;

            playTimer.Stop();
            pause();

            if (DEBUG) MessageBox.Show("DEBUG: secs over");
        }

        // On screen stuff
        private void initLevelText()
        {
            LevelVisual.Text = levels[level].getName();
        }

        private void initProgressBars()
        {
            ScoreProgressVisual.Maximum = levels[level].getRequiredScore();
            TimeProgressVisual.Maximum = levels[level].getMaxTime();
        }

        private void updateOnScreenInfo()
        {
            updateScore();

            updateProgressBars();
        }

        private void updateScore()
        {
            ScoreVisual.Text = score.ToString();
        }

        private void updateProgressBars()
        {
            ScoreProgressVisual.Value = score;
            TimeProgressVisual.Value = timeSpent;
        }

        //Actual game control
        private void play(int level)
        {
            stop();

            this.level = level;

            initLevelText();
            initProgressBars();
            updateOnScreenInfo();

            //Start timer
            playTimer.Interval = new TimeSpan(0, 0, 1);
            playTimer.Tick += PlayTimer_Tick;
            playTimer.Start();

            inGame = true;
            isPaused = false;
        }

        private void pause()
        {
            playTimer.Tick -= PlayTimer_Tick;
            isPaused = true;
        }

        private void resume()
        {
            playTimer.Tick += PlayTimer_Tick;
            isPaused = false;
        }

        private void stop()
        {
            playTimer.Tick -= PlayTimer_Tick;
            inGame = false;
            isPaused = false;
        }

        // Other and things that could get outsourced or have some debuggy purpose
            //This one is dummy/prototype level code, focus on later moving this to like a config file!!!
        private void createLevelData()
        {
            LevelInfo easy = new LevelInfo("easy", 5, 180);
            LevelInfo medium = new LevelInfo("medium", 10, 150);
            LevelInfo intermediate = new LevelInfo("intermediate", 20, 130);
            LevelInfo hacker = new LevelInfo("hacker", 50, 100);

            levels[0] = hacker;
            levels[1] = easy;
            levels[2] = medium;
            levels[3] = intermediate;
        }
    }
}
