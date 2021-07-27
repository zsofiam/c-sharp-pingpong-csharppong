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

        public Rectangle(MainWindow mainWindow, System.Windows.Shapes.Rectangle visualRectangle)
        {
            MainWindow = mainWindow;
            VisualRectangle = visualRectangle;
        }


        protected override Geometry DefiningGeometry { get; }
    }
}
