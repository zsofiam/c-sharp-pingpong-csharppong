using System.Windows;

namespace CsharpPong
{
    public class Paddle
    {
        private MainWindow _mainWindow;
        public System.Windows.Shapes.Rectangle VisualRectangle;

        public Paddle(MainWindow mainWindow, System.Windows.Shapes.Rectangle visualRectangle)
        {
            _mainWindow = mainWindow;
            VisualRectangle = visualRectangle;
        }
        public void Move(string direction)
        {
            var lMargin = VisualRectangle.Margin.Left;
            {
                //ToLower so no matter HoW It iS wRitTeN iT wIlL AlWayS wOrK
                switch (direction.ToLower())
                {
                    case "left":
                        if (lMargin >= -_mainWindow.ActualWidth + VisualRectangle.ActualWidth) lMargin -= 10;
                        break;
                    case "right":
                        if (lMargin <= _mainWindow.ActualWidth - VisualRectangle.ActualWidth) lMargin += 10;
                        break;
                }
            }

            VisualRectangle.Margin = new Thickness(lMargin, VisualRectangle.Margin.Top, VisualRectangle.Margin.Right, VisualRectangle.Margin.Bottom);
        }
    }
}
