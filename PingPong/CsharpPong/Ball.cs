using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CsharpPong
{
    public class Ball : Rectangle
    {
        private MainWindow MainWindow;
        private System.Windows.Shapes.Rectangle VisualRectangle;

        protected Dictionary<string, double> Direction = new Dictionary<string, double>();
        protected Random Random = new Random();
        private int _level = 1;
        private double lMargin;
        private double tMargin;
        private double rMargin;
        private double bMargin;

        public Ball(MainWindow mainWindow, System.Windows.Shapes.Rectangle visualRectangle) : base(mainWindow, visualRectangle)
        {
            MainWindow = mainWindow;
            VisualRectangle = visualRectangle;
            lMargin = VisualRectangle.Margin.Left;
            tMargin = VisualRectangle.Margin.Top;
            rMargin = VisualRectangle.Margin.Right;
            bMargin = VisualRectangle.Margin.Bottom;
        }
        public void Move()
        {
            lMargin += Direction["leftMargin"];
            rMargin -= Direction["leftMargin"];
            tMargin += Direction["topMargin"];
            bMargin -= Direction["topMargin"];
            VisualRectangle.Margin = new Thickness(lMargin, tMargin, rMargin, bMargin);
            if (lMargin <= 0 || tMargin <= 0 || rMargin <= 0 || bMargin <= 0)
            {
                Bounce();
            }
        }

        private void Bounce()
        {
            if(bMargin <= 0 || tMargin <= 0)
            {
                Direction["topMargin"] = -1 * Direction["topMargin"];
            }
            if (rMargin <= 0 || lMargin <= 0)
            {
                Direction["leftMargin"] = -1 * Direction["leftMargin"];
            }

        }

        public void SetDirection()
        {
            Direction["leftMargin"] = 10 * _level;
            Direction["topMargin"] = 20 * _level;
        }

        public void ChangeDirection()
        {
            
        }
    }
}
