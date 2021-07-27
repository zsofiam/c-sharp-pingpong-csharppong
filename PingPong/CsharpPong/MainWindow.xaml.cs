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

namespace CsharpPong
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Classes
        private Paddle paddle;

        //Variables
        private int level;
        private int score;



        public MainWindow()
        {
            InitializeComponent();
            paddle = new Paddle(this, PaddleVisual);

            level = 1;
            score = 0;
            updateOnScreenInfo();
        }

        //Window stuff
        //Key presses
        private void Window_KeyDown(object sender, KeyEventArgs e)
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
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ProgressVisual.Width = this.ActualWidth;
        }

        // On screen stuff
        private void updateOnScreenInfo()
        {
            //first update the level
            updateLevel();

            updateScore();
        }

        private void updateLevel()
        {
            switch (level)
            {
                case 0:
                    LevelVisual.Text = "hacker";
                    break;
                case 1:
                    LevelVisual.Text = "easy";
                    break;
                case 2:
                    LevelVisual.Text = "medium";
                    break;
                case 3:
                    LevelVisual.Text = "intermediate";
                    break;
            }
        }

        private void updateScore()
        {
            ScoreVisual.Text = score.ToString();
        }

        
    }
}
