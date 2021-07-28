using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CsharpPong
{
    class Ball : Rectangle
    {
        protected Dictionary<string, double> Direction = new Dictionary<string, double>();
        protected Random Random = new Random();
        private int _level = 1;
        protected bool isColliding = false;

        public Ball(MainWindow mainWindow, System.Windows.Shapes.Rectangle visualRectangle) : base(mainWindow, visualRectangle)
        {
        }
        public void Move()
        {
            if (isColliding) ChangeDirection();
            var lMargin = VisualRectangle.Margin.Left;
            var tMargin = VisualRectangle.Margin.Top;
            var rMargin = VisualRectangle.Margin.Right;
            var bMargin = VisualRectangle.Margin.Bottom;
            lMargin += Direction["leftMargin"];
            rMargin -= Direction["leftMargin"];
            tMargin += Direction["topMargin"];
            bMargin -= Direction["topMargin"];
            VisualRectangle.Margin = new Thickness(lMargin, tMargin, rMargin, bMargin);
        }

        public void SetDirection()
        {
            Direction["leftMargin"] = (Random.NextDouble() + 0.2) * 2 * _level;
            Direction["topMargin"] = (Random.NextDouble() + 0.2) * 2 * _level;
        }

        public void ChangeDirection()
        {
            Direction["leftMargin"] = Random.NextDouble() + 0.1 - 1 * Direction["leftMargin"];
            Direction["topMargin"] = (Random.NextDouble() + 0.1) * 10 * _level;
        }
    }
}
