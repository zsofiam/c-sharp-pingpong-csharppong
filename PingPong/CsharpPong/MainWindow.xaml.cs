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

        public LevelInfo(string name, int requiredScore)
        {
            this.name = name;
            this.requiredScore = requiredScore;
        }

        public string getName()
        {
            return name;
        }

        public int getRequiredScore()
        {
            return requiredScore;
        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Classes
        private Paddle paddle;

        //Variables
        private LevelInfo[] levels;
        private bool inGame = true;
        private bool isPaused = false;
        private int level;
        private int score;

        private DispatcherTimer playTimer = new DispatcherTimer();


        public MainWindow()
        {
            InitializeComponent();
            paddle = new Paddle(this, PaddleVisual);

            levels = new LevelInfo[4];
            createLevelData();

            level = 1;
            score = 0;
            initLevelText();
            initProgressBar();
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
                }
            }
        }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ProgressVisual.Width = this.ActualWidth;
        }

        // On screen stuff
        private void initLevelText()
        {
            LevelVisual.Text = levels[level].getName();
        }

        private void initProgressBar()
        {
            ProgressVisual.Maximum = levels[level].getRequiredScore();
        }

        private void updateOnScreenInfo()
        {
            updateScore();

            updateProgressBar();
        }

        private void updateScore()
        {
            ScoreVisual.Text = score.ToString();
        }

        private void updateProgressBar()
        {
            ProgressVisual.Value = score;
        }

        //Actual game control
        private void play(int level)
        {
            this.level = level;

            initLevelText();
            initProgressBar();
            updateOnScreenInfo();

            inGame = true;
            isPaused = false;
        }

        private void pause()
        {
            isPaused = true;
        }

        private void resume()
        {
            isPaused = false;
        }

        private void stop()
        {
            inGame = false;
            isPaused = false;
        }

        // Other and things that could get outsourced or have some debuggy purpose
            //This one is dummy/prototype level code, focus on later moving this to like a config file!!!
        private void createLevelData()
        {
            LevelInfo easy = new LevelInfo("easy", 5);
            LevelInfo medium = new LevelInfo("medium", 10);
            LevelInfo intermediate = new LevelInfo("intermediate", 20);
            LevelInfo hacker = new LevelInfo("hacker", 50);

            levels[0] = hacker;
            levels[1] = easy;
            levels[2] = medium;
            levels[3] = intermediate;
        }
    }
}
