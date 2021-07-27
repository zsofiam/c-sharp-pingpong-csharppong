﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CsharpPong
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Paddle paddle;

        public MainWindow()
        {
            InitializeComponent();
            paddle = new Paddle(this, PaddleVisual);
        }

        //Key presses
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                case Key.A:
                    paddle.Move("left");
                    break;

                case Key.Right:
                case Key.D:
                    paddle.Move("right");
                    break;
                case Key.Escape:
                    Application.Current.Shutdown();
                    break;
            }
        }
    }
}
