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
using System.Media;
using System.IO;
using System.Reflection;

namespace Snake
{
    public partial class Snake : Form
    {
        SnakeController snake;
        MeatController meat;
        int initialX = 260;
        int initialY = 195;
        int pixelLength = 13;
        SnakeController.Directions nextDirection = SnakeController.Directions.NO_KEY;

        public Snake()
        {
            InitializeComponent();
        }

        private void Snake_Load(object sender, EventArgs e)
        {
            SoundPlayer player = new SoundPlayer(@"Science Is Interesting.wav");
            player.PlayLooping();
            timer1.Start();
            newGame();

        }

        private void newGame()
        {
            this.snake = new SnakeController(initialX, initialY, pixelLength, Color.Black);
            this.meat = new MeatController(canvasSnake.Width, canvasSnake.Height, Color.Black, this.pixelLength);
            meat.generateMeat(snake.getSnakeBody());
            canvasSnake.Invalidate();
        }

        private void canvasSnake_Paint(object sender, PaintEventArgs e)
        {
            foreach (Pixel px in snake.getSnakeBody())
            {
                Rectangle rct = new Rectangle(px.getX(), px.getY(), this.pixelLength, this.pixelLength);
                e.Graphics.FillRectangle(new SolidBrush(px.getColor()), rct);
            }

            Pixel brunch = meat.getMeatPixel();
            Rectangle recBrunch = new Rectangle(brunch.getX(), brunch.getY(), this.pixelLength, this.pixelLength);
            e.Graphics.FillRectangle(new SolidBrush(Color.Black), recBrunch);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (snake.hasColision(canvasSnake.Width, canvasSnake.Height))
            {
                timer1.Stop();
            }
            else
            {
                if (!this.nextDirection.Equals(SnakeController.Directions.NO_KEY))
                {
                    snake.setDirection(this.nextDirection);
                    this.nextDirection = SnakeController.Directions.NO_KEY;
                }
                snake.refresh();
                if (snake.eatMeat(meat.getMeatPixel()))
                {
                    meat.generateMeat(snake.getSnakeBody());
                    timer1.Interval -= (timer1.Interval * 5) / 100;
                }
                canvasSnake.Invalidate();
            }
        }



        private void Snake_KeyDown_1(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                    this.nextDirection = SnakeController.Directions.DOWN;
                    break;
                case Keys.Up:
                    this.nextDirection = SnakeController.Directions.UP;
                    break;
                case Keys.Left:
                    this.nextDirection = SnakeController.Directions.LEFT;
                    break;
                case Keys.Right:
                    this.nextDirection = SnakeController.Directions.RIGHT;
                    break;
            }
        }

    }
}
