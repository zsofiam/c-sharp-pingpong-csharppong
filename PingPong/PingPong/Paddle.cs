using System.Windows;

namespace PingPong
{
    public class Paddle : Rectangle
    {
        public Paddle(System.Windows.Shapes.Rectangle rectangle, MainWindow mainWindow) : base(rectangle, mainWindow)
        {
        }

        public void Move(string direction)
        {
            var leftMargin = VisualRectangle.Margin.Left;
            {
                switch (direction)
                {
                    case "Right":
                        if (leftMargin <= MainWindow.ActualWidth - VisualRectangle.ActualWidth) leftMargin += 10;
                        break;
                    case "Left":
                        if (leftMargin >= -MainWindow.ActualWidth + VisualRectangle.ActualWidth) leftMargin -= 10;
                        break;
                }
            }
            VisualRectangle.Margin = new Thickness(leftMargin, VisualRectangle.Margin.Top, VisualRectangle.Margin.Right,
                VisualRectangle.Margin.Bottom);
        }
    }
}