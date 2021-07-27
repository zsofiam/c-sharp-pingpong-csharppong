using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CsharpPong
{
    class Paddle : Rectangle
    {
        public Paddle(MainWindow mainWindow, System.Windows.Shapes.Rectangle visualRectangle) : base(mainWindow, visualRectangle)
        {
        }

        public void Move(string direction)
        {
            var lMargin = VisualRectangle.Margin.Left;
            {
                //ToLower so no matter HoW It iS wRitTeN iT wIlL AlWayS wOrK
                switch (direction.ToLower())
                {
                    case "left":
                        if (lMargin >= -MainWindow.ActualWidth + VisualRectangle.ActualWidth) lMargin -= 10;
                        break;
                    case "right":
                        if (lMargin <= MainWindow.ActualWidth - VisualRectangle.ActualWidth) lMargin += 10;
                        break;
                }
            }

            VisualRectangle.Margin = new Thickness(lMargin, VisualRectangle.Margin.Top, VisualRectangle.Margin.Right, VisualRectangle.Margin.Bottom);
        }
    }
}
