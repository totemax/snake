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

namespace Snake.Views
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
        bool scienceMode = false;
        SoundPlayer player;
        Random rnd;
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
            player = new SoundPlayer(@"Media/Science Is Interesting.wav");
            timer1.Start();
            newGame();
            rnd = new Random();

        }

        // Funcion que se encarga de iniciar un nuevo juego
        private void newGame()
        {
            player.Stop();
            scienceMode = false;
            if (dificilToolStripMenuItem.Checked)
            {
                this.meat = new MeatController(canvasSnake.Width, canvasSnake.Height, Color.Black, this.pixelLength, defaultMeatVal,1);
                this.timerReduction = 7;
                this.timer1.Interval = 140;
            }
            else if (mediaToolStripMenuItem.Checked)
            {
                this.meat = new MeatController(canvasSnake.Width, canvasSnake.Height, Color.Black, this.pixelLength, defaultMeatVal, 1);
                this.timerReduction = 6;
                this.timer1.Interval = 170;
            }
            else if (sCIENCEMODEToolStripMenuItem.Checked)
            {
                player.PlayLooping();
                this.meat = new MeatController(canvasSnake.Width, canvasSnake.Height, Color.Black, pixelLength, defaultMeatVal, 1);
                this.timerReduction = 8;
                this.timer1.Interval = 100;
                scienceMode = true;
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

        // Funcion que se encarga de pintar el letrero de "Game Over" cuando perdemos
        private void drawGameOver(PaintEventArgs e)
        {
            for (int i = 0; i < gameOverCoods.GetLength(0); i++)
            {
                Color pxColor = Color.Black;
                if (scienceMode)
                {
                    pxColor = Color.FromArgb(rnd.Next(255), rnd.Next(255), rnd.Next(255));
                }
                Rectangle recPx = new Rectangle(gameOverCoods[i, 1], gameOverCoods[i, 0], this.pixelLength, this.pixelLength);
                e.Graphics.FillRectangle(new SolidBrush(pxColor), recPx);
            }
        }


        // Evento encargado de realizar el pintado del canvas
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
                    Color pxColor = px.getColor();
                    if (scienceMode)
                    {
                        pxColor = Color.FromArgb(rnd.Next(255), rnd.Next(255), rnd.Next(255));
                    }
                    Rectangle rct = new Rectangle(px.getX(), px.getY(), this.pixelLength, this.pixelLength);
                    e.Graphics.FillRectangle(new SolidBrush(pxColor), rct);
                }

                Pixel brunch = meat.getMeatPixel();
                Color branchColor = meat.getColor();
                if (scienceMode)
                {
                    branchColor = Color.FromArgb(rnd.Next(255), rnd.Next(255), rnd.Next(255));
                }
                Rectangle recBrunch = new Rectangle(brunch.getX(), brunch.getY(), this.pixelLength, this.pixelLength);
                e.Graphics.FillRectangle(new SolidBrush(branchColor), recBrunch);
            }
        }

        //Evento que se lanza cada vez que hay un tick en el timer.
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (snake.hasColision(canvasSnake.Width, canvasSnake.Height))
            {
                this.isGameOver = true;
                timer1.Stop();
                player.Stop();
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

        //Funcion de control de la puntuacion
        private void incrementScore(int increment)
        {
            int actualScore = int.Parse(score.Text);
            actualScore += increment;
            score.Text = actualScore.ToString();
        }

        //Evento que se encarga de parsear la entrada por teclado
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
                case Keys.Space:
                    if (timer1.Enabled)
                    {
                        timer1.Stop();
                    }
                    else
                    {
                        timer1.Start();
                    }
                    break;
                case Keys.F2:
                    this.newGame();
                    break;
            }
        }

        //Evento del boton "nuevo"
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Start();
            newGame();
        }

        //Evento que se lanza al pulsar cualquier boton de dificultad para que funcionen como un radiobutton
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

        //Evento que controla el botón salir
        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Evento que lanza la pantalla de "Acerca de..."
        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            About about = new About();
            about.ShowDialog();
            timer1.Start();
        }

    }
}
