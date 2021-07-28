using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CsharpPong
{
    public class Rectangle : Shape
    {
        protected MainWindow MainWindow;
        protected System.Windows.Shapes.Rectangle VisualRectangle;
        protected Dictionary<string, double> Direction = new Dictionary<string, double>();
        protected Random Random = new Random();
        protected int level = 1;
        protected double lMargin;
        protected double tMargin;
        protected double rMargin;
        protected double bMargin;

        public Rectangle(MainWindow mainWindow, System.Windows.Shapes.Rectangle visualRectangle)
        {
            MainWindow = mainWindow;
            VisualRectangle = visualRectangle;
            lMargin = VisualRectangle.Margin.Left;
            tMargin = VisualRectangle.Margin.Top;
            rMargin = VisualRectangle.Margin.Right;
            bMargin = VisualRectangle.Margin.Bottom;
        }

        protected void ChangeMargin()
        {
            lMargin += Direction["leftMargin"];
            rMargin -= Direction["leftMargin"];
            tMargin += Direction["topMargin"];
            bMargin -= Direction["topMargin"];
            
        }

        public void Move()
        {
            ChangeMargin();
            VisualRectangle.Margin = new Thickness(lMargin, tMargin, rMargin, bMargin);
        }

        public void SetDirection()
        {
            Direction["leftMargin"] = Random.Next(-1, 2) * 10 * level;
            Direction["topMargin"] = 10 * level;
        }

        public void ChangeDirection()
        {
            Direction["leftMargin"] = Random.Next(-1, 2) * 10 * level;
            Direction["topMargin"] = 10 * level;
        }


        protected override Geometry DefiningGeometry { get; }
    }
}
