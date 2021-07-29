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
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 200);
            _timer.Tick += new EventHandler(dispatcherTimer_Tick);
           
        }

        public void startBall()
        {
            _timer.Start();
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            this.Move();

            //if (myProgressBar.Value >= 100)
            //{
            //    _timer.Stop();
            //}
        }

        public new void Move()
        {
            ChangeMargin();
            VisualRectangle.Margin = new Thickness(lMargin, tMargin, rMargin, bMargin);
            if (lMargin <= 0 || tMargin <= 0 || rMargin <= 0 || bMargin <= 0)
            {
                Bounce();
            }
        }

        private void Bounce()
        {
            if (bMargin <= 0 || tMargin <= 0)
            {
                Direction["topMargin"] = -1 * Direction["topMargin"];
            }
            if (rMargin <= 0 || lMargin <= 0)
            {
                Direction["leftMargin"] = -1 * Direction["leftMargin"];
            }

        }

        public new void SetDirection()
        {
            Direction["leftMargin"] = 10 * level;
            Direction["topMargin"] = 20 * level;
        }

        public new void ChangeDirection()
        {
            
        }
    }
}
