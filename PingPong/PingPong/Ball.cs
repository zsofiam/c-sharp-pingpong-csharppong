using System.Windows;

namespace PingPong
{
    class Ball : Rectangle
    {
        private readonly System.Windows.Shapes.Rectangle _paddle;
        public Ball(System.Windows.Shapes.Rectangle rectangle, MainWindow mainWindow, System.Windows.Shapes.Rectangle paddle) : base(rectangle, mainWindow)
        {
            _paddle = paddle;
        }



        public void IncreaseLevel()
        {
            Level += 1;
            Direction["leftMargin"] *= Level / (Level - 1);
            Direction["topMargin"] *= Level / (Level - 1);
        }

        public void Move()
        {
            if ((int) Direction["leftMargin"] == 0) SetDirection();
            double leftMargin = VisualRectangle.Margin.Left;
            double topMargin = VisualRectangle.Margin.Top;
            if (VisualRectangle.Margin.Left >= MainWindow.ActualWidth - VisualRectangle.ActualWidth ||
                VisualRectangle.Margin.Left <= -(MainWindow.ActualWidth - VisualRectangle.ActualWidth))
            {
                SideBounce();
            }
            else if (topMargin <= 0)
            {
                TopBounce();
            }
            else if (topMargin >= MainWindow.ActualHeight - 1.5 * _paddle.ActualHeight - 2 * VisualRectangle.ActualHeight &&
                     _paddle.Margin.Left - _paddle.ActualWidth <= VisualRectangle.Margin.Left &&
                     _paddle.Margin.Left + _paddle.ActualWidth >= VisualRectangle.Margin.Left)
            {
                PaddleBounce();
                MainWindow.IncreaseScore();
            }

            if (topMargin >= MainWindow.ActualHeight - VisualRectangle.ActualHeight * 1.75)
            {
                ResetPosition();
            }
            else
            {
                leftMargin += Direction["leftMargin"];
                topMargin += Direction["topMargin"];
                VisualRectangle.Margin =
                    new Thickness(leftMargin, topMargin, VisualRectangle.Margin.Right, VisualRectangle.Margin.Bottom);
            }
        }


        private void PaddleBounce()
        {
            Direction["topMargin"] = -Direction["topMargin"];
        }
    }
}