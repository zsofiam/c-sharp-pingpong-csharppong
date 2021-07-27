using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpPong
{
    class Ball : Rectangle
    {
        public Ball(MainWindow mainWindow, System.Windows.Shapes.Rectangle visualRectangle) : base(mainWindow, visualRectangle)
        {
        }
    }
}
