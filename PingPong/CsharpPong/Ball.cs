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

        public Ball(MainWindow mainWindow, System.Windows.Shapes.Rectangle visualRectangle) : base(mainWindow, visualRectangle)
        {
            _timer = new DispatcherTimer();

        }

        public void startBall(int level)
        {
            this.level = level;
            SetDirection();
            setSpeed();
            _timer.Tick += new EventHandler(dispatcherTimer_Tick);
            _timer.Start();
        }

        private void setSpeed()
        {
            switch (level)
            {
                case 0:
                    _timer.Interval = new TimeSpan(0, 0, 0, 0, 30);
                    break;
                case 1:
                    _timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
                    break;
                case 2:
                    _timer.Interval = new TimeSpan(0, 0, 0, 0, 70);
                    break;
                case 3:
                    _timer.Interval = new TimeSpan(0, 0, 0, 0, 50);
                    break;
            }
            
        }

        public void halt()
        {
            _timer.Stop();
        }
        public void restart()
        {
            _timer.Start();
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            this.Move();
        }
        
               
public new void Move()
        {
            ChangeMargin();
            VisualRectangle.Margin = new Thickness(lMargin, tMargin, rMargin, bMargin);
            if(lMargin > (MainWindow.ActualWidth/2)
                || lMargin < -(MainWindow.ActualWidth/2) 
                || tMargin < 0)
            {
                Bounce();
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

        public new void SetDirection()
        {
            lMargin = 0;
            tMargin = 0;
            rMargin = 0;
            bMargin = 0;
            Direction["leftMargin"] = 10 * level;
            Direction["topMargin"] = 20 * level;
        }

        public new void ChangeDirection()
        {
       
        }

        internal bool falls()
        {
            return tMargin > MainWindow.ActualHeight;
        }
    }
}
