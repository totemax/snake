using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Snake.Controllers;

namespace Snake
{
    public partial class Snake : Form
    {
        SnakeController snake;
        int initialX = 260;
        int initialY = 195;
        int pixelLength = 13;
        public Snake()
        {
            InitializeComponent();
        }

        private void Snake_Load(object sender, EventArgs e)
        {
            snake = new SnakeController(initialX, initialY, pixelLength, Color.DarkGray);
        }
    }
}
