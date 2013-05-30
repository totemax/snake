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
        #region [Constants]

            private static String NEW_GAME = "SELECCIONE UN MODO DE JUEGO PARA INICIAR PARTIDA";
            private static String PUSH_START = "Pulse espacio para iniciar la partida";    
        #endregion


        #region [Variables]

        String _messageToRender = null;
        int PIXEL_LENGTH = 13;
        List<Pixel> _pixelsToRender = null;
        SnakeController.Directions _nextDirection = SnakeController.Directions.NO_KEY;
        IGameController _gameMode = null;

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
            this._messageToRender = NEW_GAME;
            this.canvasSnake.Invalidate();
        }

        // Evento encargado de realizar el pintado del canvas
        private void canvasSnake_Paint(object sender, PaintEventArgs e)
        {
            if (this._messageToRender != null)
            {
                this.drawText(this._messageToRender, e);
                this._messageToRender = null;
            }
            if(this._pixelsToRender != null)
            {
                foreach (Pixel px in this._pixelsToRender)
                {
                    Color pxColor = px.getColor();
                    Rectangle rct = new Rectangle(px.getX(), px.getY(), this.PIXEL_LENGTH, this.PIXEL_LENGTH);
                    e.Graphics.FillRectangle(new SolidBrush(pxColor), rct);
                }
            }
        }

        //Evento que se lanza cada vez que hay un tick en el timer.
        private void timer1_Tick(object sender, EventArgs e)
        {
            this._pixelsToRender = this._gameMode.refresh(this._nextDirection);

            this._messageToRender = this._gameMode.gameResult();

            this.timer1.Interval = this._gameMode.getTickerTimer();

            if (this._messageToRender != null)
            {
                this.timer1.Stop();
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

        // Funcion que se encarga de pintar el letrero de "Game Over" cuando perdemos
        private void drawText(String message, PaintEventArgs e)
        {
            e.Graphics.DrawString(message, new Font("Impact", 20), Brushes.Black, new Point(70, 130));
        }        


        //Funcion de control de la puntuacion
        private void incrementScore(int increment)
        {
            int actualScore = int.Parse(score.Text);
            actualScore += increment;
            score.Text = actualScore.ToString();
        }

        #endregion

        private void gameModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.timer1.Stop();
            ToolStripDropDownItem item = (ToolStripDropDownItem)sender;
            this._pixelsToRender = null;

            switch (item.Tag.ToString())
            {
                case "competicion":
                    this._gameMode = new ChallengeController(this.canvasSnake.Width, this.canvasSnake.Height, this.PIXEL_LENGTH);
                    break;
            }

            this._messageToRender = PUSH_START;

            this.canvasSnake.Invalidate();
        }

    }
}
