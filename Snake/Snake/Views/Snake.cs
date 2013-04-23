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
        int defaultMeatVal = 100;
        int timerReduction = 1;
        SnakeController.Directions nextDirection = SnakeController.Directions.NO_KEY;
        bool isGameOver = false;
        int[,] gameOverCoods = new int[,] {
        //Coordenadas de la G
        {104, 91},
        {104, 104},
        {117, 78},
        {130, 78},
        {130, 104},
        {130, 117},
        {143, 78},
        {143, 117},
        {156, 91},
        {156, 104},
        //Coordenadas de la A
        {104, 156},
        {104, 169},
        {130, 156},
        {130, 169},
        {143, 143},
        {143, 182},
        {156, 143},
        {156, 182},
        //Coordenadas de la M
        {104, 208},
        {104, 260},
        {117, 208}, 
        {117, 221},
        {117, 247},
        {117, 260},
        {130, 208}, 
        {130, 234},
        {130, 260},
        {143, 208}, 
        {143, 260},
        {156, 208}, 
        {156, 260},
        //Coordenadas de la E
        {104, 286}, 
        {104, 299}, 
        {104, 312},
        {104, 325}, 
        {130, 286}, 
        {130, 299},
        {130, 312},
        {156, 286}, 
        {156, 299}, 
        {156, 312},
        {156, 325},
        //Coordenadas de la O
        {234, 156},
        {234, 169},
        {247, 143},
        {247, 182},
        {260, 143},
        {260, 182},
        {273, 143},
        {273, 182},
        {286, 156},
        {286, 169},
        //Coordenadas de la V
        {234, 208},
        {234, 260},
        {247, 208},
        {247, 260},
        {260, 221},
        {260, 247},
        {273, 221},
        {273, 247},
        {286, 234},
        //Coordenadas de la E
        {234, 286}, 
        {234, 299}, 
        {234, 312},
        {234, 325},
        {260, 286}, 
        {260, 299},
        {260, 312},
        {286, 286}, 
        {286, 299}, 
        {286, 312},
        {286, 325},
        //Coordenadas de la R
        {234, 351},
        {234, 364},
        {234, 377},
        {247, 351},
        {247, 377},
        {260, 351},
        {260, 364},
        {260, 377},
        {273, 351},
        {273, 390},
        {286, 351},
        {286, 390}
        };


        public Snake()
        {
            InitializeComponent();
        }

        private void Snake_Load(object sender, EventArgs e)
        {
           // SoundPlayer player = new SoundPlayer(@"Science Is Interesting.wav");
            //player.PlayLooping();
            timer1.Start();
            newGame();

        }

        private void newGame()
        {
            if (dificilToolStripMenuItem.Checked)
            {
                this.meat = new MeatController(canvasSnake.Width, canvasSnake.Height, Color.Black, this.pixelLength, defaultMeatVal,3);
                this.timerReduction = 7;
                this.timer1.Interval = 140;
            }
            else if (mediaToolStripMenuItem.Checked)
            {
                this.meat = new MeatController(canvasSnake.Width, canvasSnake.Height, Color.Black, this.pixelLength, defaultMeatVal, 2);
                this.timerReduction = 6;
                this.timer1.Interval = 170;
            }
            else
            {
                this.meat = new MeatController(canvasSnake.Width, canvasSnake.Height, Color.Black, this.pixelLength, defaultMeatVal, 1);
                this.timerReduction = 5;
                this.timer1.Interval = 200;
            }
            this.snake = new SnakeController(initialX, initialY, pixelLength, Color.Black);
         
            meat.generateMeat(snake.getSnakeBody());
            this.nextDirection = SnakeController.Directions.NO_KEY;
            this.score.Text = "0";
            this.isGameOver = false;
            canvasSnake.Invalidate();
        }

        private void drawGameOver(PaintEventArgs e)
        {
            for (int i = 0; i < gameOverCoods.GetLength(0); i++)
            {
                Rectangle recPx = new Rectangle(gameOverCoods[i, 1], gameOverCoods[i, 0], this.pixelLength, this.pixelLength);
                e.Graphics.FillRectangle(new SolidBrush(Color.Black), recPx);
            }
        }



        private void canvasSnake_Paint(object sender, PaintEventArgs e)
        {
            if (isGameOver)
            {
                drawGameOver(e);
            }
            else
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
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (snake.hasColision(canvasSnake.Width, canvasSnake.Height))
            {
                this.isGameOver = true;
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
                    incrementScore(meat.getActualValue());
                    meat.generateMeat(snake.getSnakeBody());
                    timer1.Interval -= (timer1.Interval * this.timerReduction) / 100;
                }
                else
                {
                    meat.decrementMeatScore(1);
                }
            }
            canvasSnake.Invalidate();
        }

        private void incrementScore(int increment)
        {
            int actualScore = int.Parse(score.Text);
            actualScore += increment;
            score.Text = actualScore.ToString();
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

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Start();
            newGame();
        }

        private void difficultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            if (!item.Checked) item.Checked = true;
            foreach (ToolStripMenuItem itemRB in dificultadToolStripMenuItem.DropDownItems)
            {
                if (itemRB != item)
                {
                    itemRB.Checked = false;
                }
            }
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
