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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Paddle _paddle;
        private Ball _ball;

        public MainWindow()
        {
            InitializeComponent();
            _paddle = new Paddle(this, PaddleVisual);
            _ball = new Ball(this, BallVisual);
            _ball.SetDirection();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 30);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _ball.Move();
        }

        //Key presses
        private void Window_KeyDown(object sender, KeyEventArgs e)
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
            }
        }
    }
}
