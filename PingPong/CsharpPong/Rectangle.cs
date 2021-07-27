using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CsharpPong
{
    class Rectangle : Shape
    {
        protected MainWindow MainWindow;
        protected System.Windows.Shapes.Rectangle VisualRectangle;
        protected Dictionary<string, double> Direction = new Dictionary<string, double>();
        protected Random Random = new Random();
        private int _level = 1;
        protected bool isColliding = false;

        public Rectangle(MainWindow mainWindow, System.Windows.Shapes.Rectangle visualRectangle)
        {
            MainWindow = mainWindow;
            VisualRectangle = visualRectangle;
        }

        public void Move()
        {
            if (isColliding) ChangeDirection();
            var newLeftMargin = VisualRectangle.Margin.Left + Direction["leftMargin"];
            var newTopMargin = VisualRectangle.Margin.Top + Direction["topMargin"];
            VisualRectangle.Margin = new Thickness(newLeftMargin, newTopMargin, VisualRectangle.Margin.Right, VisualRectangle.Margin.Bottom);
           
        }

        public void SetDirection()
        {
            Direction["leftMargin"] = Random.Next(-1, 2) * 10 * _level;
            Direction["topMargin"] = 10 * _level;
        }

        public void ChangeDirection()
        {
            Direction["leftMargin"] = Random.Next(-1, 2) * 10 * _level;
            Direction["topMargin"] = 10 * _level;
        }


        protected override Geometry DefiningGeometry { get; }
    }
}
