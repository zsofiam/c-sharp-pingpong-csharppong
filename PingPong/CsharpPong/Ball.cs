using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace CsharpPong
{
    public class Ball : Rectangle
    {
        private DispatcherTimer _timer;
        private Paddle _paddle;

        public Ball(System.Windows.Shapes.Rectangle visualRectangle, Paddle paddle,
            MainWindow mainWindow) : base(mainWindow, visualRectangle)
        {
            _timer = new DispatcherTimer();
            this._paddle = paddle;

        }

        public void StartBall(int level)
        {
            this.level = level;
            SetDirection();
            SetSpeed();
            _timer.Tick += new EventHandler(DispatcherTimer_Tick);
            _timer.Start();
        }

        private void SetSpeed()
        {
            switch (level)
            {
                case 0:
                    _timer.Interval = new TimeSpan(0, 0, 0, 0, 30);
                    break;
                case 1:
                    _timer.Interval = new TimeSpan(0, 0, 0, 0, 90);
                    break;
                case 2:
                    _timer.Interval = new TimeSpan(0, 0, 0, 0, 70);
                    break;
                case 3:
                    _timer.Interval = new TimeSpan(0, 0, 0, 0, 50);
                    break;
            }
            
        }

        public void Halt()
        {
            _timer.Stop();
        }
        public void Restart()
        {
            _timer.Start();
        }
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            this.Move();
        }
        
               
        public void Move()
        {
            ChangeMargin();
            VisualRectangle.Margin = new Thickness(lMargin, tMargin, rMargin, bMargin);
            if(lMargin > (MainWindow.ActualWidth/2)
                || lMargin < -(MainWindow.ActualWidth/2) 
                || tMargin < 0)
            {
                Bounce();
            }
            if (CollisionHappens())
            {
                BounceBackFromPaddle();
            }
            if (falls())
            {
                SetDirection();
            }
        }

        private void Bounce()
        {
            if (tMargin < 0)
            {
                Direction["topMargin"] = -1 * Direction["topMargin"];
            }
            if (lMargin > (MainWindow.ActualWidth / 2 )
                || lMargin < -(MainWindow.ActualWidth / 2 ))
            {
                Direction["leftMargin"] = -1 * Direction["leftMargin"];
            }

        }

        public void SetDirection()
        {
            lMargin = 0;
            tMargin = 0;
            rMargin = 0;
            bMargin = 0;
            Direction["leftMargin"] = 10 * level;
            Direction["topMargin"] = 20 * level;
        }

        //TODO: rename - start with capital letter and verb - variable: noun, css class: noun or melléknév
        internal bool falls()
        {
            return tMargin > MainWindow.ActualHeight;
        }

        private bool CollisionHappens()
        {
            return tMargin >= MainWindow.ActualHeight - 1.5 * _paddle.VisualRectangle.Height - 2 * VisualRectangle.ActualHeight &&
                     _paddle.VisualRectangle.Margin.Left -2* _paddle.VisualRectangle.Width <= VisualRectangle.Margin.Left &&
                     _paddle.VisualRectangle.Margin.Left + _paddle.VisualRectangle.Width >= VisualRectangle.Margin.Left;
        }
        internal void BounceBackFromPaddle()
        {
            Direction["topMargin"] = -1 * Direction["topMargin"];
            MainWindow.IncreaseScore();
        }
    }
}
