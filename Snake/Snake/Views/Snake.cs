using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Snake.Models;
using Snake.Controllers;

namespace Snake
{
    public partial class Snake : Form
    {
        Graphics canvasGraphs;
        SnakeController snake;
        MeatController meat;
        int initialX = 260;
        int initialY = 195;
        int pixelLength = 13;
        public Snake()
        {
            InitializeComponent();
        }

        private void Snake_Load(object sender, EventArgs e)
        {
            canvasGraphs = canvasSnake.CreateGraphics();
            newGame();
        }

        private void newGame()
        {
            this.snake = new SnakeController(initialX, initialY, pixelLength, Color.DarkGray);
            List<Pixel> snakeBody = this.snake.getSnakeBody();
            this.meat = new MeatController(canvasSnake.Width, canvasSnake.Height, Color.DarkGray);
            Pixel brunch = meat.generateMeat(this.snake.getSnakeBody());
            foreach (Pixel pxSnake in snakeBody)
            {
                draw(pxSnake);
            }
            draw(brunch);
            canvasGraphs.Flush();
            canvasSnake.Invalidate();
        }

        private void draw(Pixel pixel)
        {
            Rectangle rct = new Rectangle(pixel.getX(), pixel.getY(), this.pixelLength, this.pixelLength);
            canvasGraphs.FillRectangle(new SolidBrush(pixel.getColor()), rct);
        }

        private void canvasSnake_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}
