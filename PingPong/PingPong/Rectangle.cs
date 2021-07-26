using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PingPong
{
    public class Rectangle : Shape
    {

        protected double Level;


        public void SetLevel(double level)
        {
            Level = level;
        }

        public double GetLevel()
        {
            return this.Level;
        }

        protected MainWindow MainWindow;

        protected System.Windows.Shapes.Rectangle VisualRectangle;

        protected Dictionary<string, double> Direction = new Dictionary<string, double>()
            {{"leftMargin", 0}, {"topMargin", 0}};

        protected static readonly Random Random = new Random();

        public Rectangle(System.Windows.Shapes.Rectangle rectangle, MainWindow mainWindow)
        {
            MainWindow = mainWindow;
            VisualRectangle = rectangle;
        }

        protected void ResetPosition()
        {
            var leftMargin = Random.NextDouble() * (MainWindow.ActualWidth - VisualRectangle.ActualWidth * 2) * 2 -
                             (MainWindow.ActualWidth - VisualRectangle.ActualWidth * 2);
            VisualRectangle.Margin = new Thickness(leftMargin, 10, VisualRectangle.Margin.Right, VisualRectangle.Margin.Bottom);
            SetDirection();
        }

        protected void SetDirection()
        {
            while (true)
            {
                Direction["leftMargin"] = Random.Next(-1, 2) * Level;
                Direction["topMargin"] = Random.Next(-1, 2) * Level;
                if ((int)Direction["leftMargin"] == 0 || (int)Direction["topMargin"] == 0) continue;
                break;
            }
        }

        protected void SideBounce()
        {
            Direction["leftMargin"] = -Direction["leftMargin"];
        }

        protected void TopBounce()
        {
            Direction["topMargin"] = -Direction["topMargin"];
        }

        protected override Geometry DefiningGeometry { get; }
    }
}
