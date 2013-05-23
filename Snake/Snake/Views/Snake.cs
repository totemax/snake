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
        #region [Variable]

        SnakeController _snake;
        MeatController _meat;
        int _initialX = 130;
        int _initialY = 130;
        String GAME_OVER = "GAME OVER";
        int PIXEL_LENGTH = 10;
        int _defaultMeatVal = 100;
        int _timerReduction = 1;
        SnakeController.Directions _nextDirection = SnakeController.Directions.NO_KEY;
        bool _isGameOver = false;
        bool _isScienceMode = false;
        SoundPlayer _looserPlayer;
        Random _rnd;

        #endregion

        #region [Builders]

        public Snake()
        {
            InitializeComponent();
        }

        #endregion

        public About About
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        #region [Events]

        private void Snake_Load(object sender, EventArgs e)
        {
            _looserPlayer = new SoundPlayer("Media/Sound of a Murloc.wav");
            timer1.Start();
            newGame();
            _rnd = new Random();

        }

        // Evento encargado de realizar el pintado del canvas
        private void canvasSnake_Paint(object sender, PaintEventArgs e)
        {
            if (_isGameOver)
            {
                drawGameOver(e);
            }
            else
            {
                foreach (Pixel px in _snake.getSnakeBody())
                {
                    Color pxColor = px.getColor();
                    if (_isScienceMode)
                    {
                        pxColor = Color.FromArgb(_rnd.Next(255), _rnd.Next(255), _rnd.Next(255));
                    }
                    Rectangle rct = new Rectangle(px.getX(), px.getY(), this.PIXEL_LENGTH, this.PIXEL_LENGTH);
                    e.Graphics.FillRectangle(new SolidBrush(pxColor), rct);
                }

                Pixel brunch = _meat.getMeatPixel();
                Color branchColor = _meat.getColor();
                if (_isScienceMode)
                {
                    branchColor = Color.FromArgb(_rnd.Next(255), _rnd.Next(255), _rnd.Next(255));
                }
                Rectangle recBrunch = new Rectangle(brunch.getX(), brunch.getY(), this.PIXEL_LENGTH, this.PIXEL_LENGTH);
                e.Graphics.FillRectangle(new SolidBrush(branchColor), recBrunch);
            }
        }

        //Evento que se lanza cada vez que hay un tick en el timer.
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_snake.hasColision(canvasSnake.Width, canvasSnake.Height))
            {
                this._isGameOver = true;
                timer1.Stop();
                _looserPlayer.Play();
            }
            else
            {
                if (!this._nextDirection.Equals(SnakeController.Directions.NO_KEY))
                {
                    _snake.setDirection(this._nextDirection);
                    this._nextDirection = SnakeController.Directions.NO_KEY;
                }
                _snake.refresh();
                if (_snake.isEatMeat(_meat.getMeatPixel(), _meat.getMeatValue()))
                {
                    incrementScore(_meat.getActualValue());
                    _meat.generateMeat(_snake.getSnakeBody());
                    this.lblMeat.Text = _meat.getMeatValue().ToString();
                    timer1.Interval -= (timer1.Interval * this._timerReduction) / 100;
                }
                else
                {
                    _meat.decrementMeatScore(1);
                }
            }
            canvasSnake.Invalidate();
        }

        //Evento que se encarga de parsear la entrada por teclado
        private void Snake_KeyDown_1(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                    this._nextDirection = SnakeController.Directions.DOWN;
                    break;
                case Keys.Up:
                    this._nextDirection = SnakeController.Directions.UP;
                    break;
                case Keys.Left:
                    this._nextDirection = SnakeController.Directions.LEFT;
                    break;
                case Keys.Right:
                    this._nextDirection = SnakeController.Directions.RIGHT;
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

        #endregion

        #region [Functions & Methods]

        // Funcion que se encarga de iniciar un nuevo juego
        private void newGame()
        {
            _isScienceMode = false;
            if (dificilToolStripMenuItem.Checked)
            {
                this._meat = new MeatController(canvasSnake.Width, canvasSnake.Height, Color.Black, this.PIXEL_LENGTH, _defaultMeatVal,1);
                this._timerReduction = 7;
                this.timer1.Interval = 140;
            }
            else if (mediaToolStripMenuItem.Checked)
            {
                this._meat = new MeatController(canvasSnake.Width, canvasSnake.Height, Color.Black, this.PIXEL_LENGTH, _defaultMeatVal, 1);
                this._timerReduction = 6;
                this.timer1.Interval = 170;
            }
            else
            {
                this._meat = new MeatController(canvasSnake.Width, canvasSnake.Height, Color.Black, this.PIXEL_LENGTH, _defaultMeatVal, 1);
                this._timerReduction = 5;
                this.timer1.Interval = 200;
            }
            this._snake = new SnakeController(_initialX, _initialY, PIXEL_LENGTH, Color.Black);
         
            _meat.generateMeat(_snake.getSnakeBody());
            this.lblMeat.Text = _meat.getMeatValue().ToString();
            
            this._nextDirection = SnakeController.Directions.NO_KEY;
            this.score.Text = "0";
            this._isGameOver = false;
            canvasSnake.Invalidate();
        }

        // Funcion que se encarga de pintar el letrero de "Game Over" cuando perdemos
        private void drawGameOver(PaintEventArgs e)
        {
            e.Graphics.DrawString(GAME_OVER, new Font("Impact", 20), Brushes.Black, new Point(70, 130));
        }        

        //Funcion de control de la puntuacion
        private void incrementScore(int increment)
        {
            int actualScore = int.Parse(score.Text);
            actualScore += increment;
            score.Text = actualScore.ToString();
        }

        #endregion

    }
}
