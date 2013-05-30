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
using System.Collections;

namespace Snake.Views
{
    public partial class Snake : Form
    {
        #region [Constants]

            private static String NEW_GAME = "SELECCIONE UN MODO DE JUEGO";
            private static String PUSH_START = "Pulse espacio para iniciar partida";
            private static int PIXEL_LENGTH = 13;
 
        #endregion

        #region [Variables]

        String _messageToRender = null;
        List<Pixel> _pixelsToRender = null;
        Hashtable _nextDirection = new Hashtable();
        IGameController _gameMode = null;

        #endregion

        #region [Builders]

        public Snake()
        {
            InitializeComponent();
        }

        #endregion

        #region [Functions & Methods]

        /// <summary>
        /// Funcion que se encarga de pintar los mensajes por pantalla
        /// </summary>
        /// <param name="message">Mensaje a pintar</param>
        private void drawText(String message)
        {
            this.lblMessages.Visible = true;
            lblMessages.Text = message;
        }        

        #endregion

        #region [ Events ]

        /// <summary>
        /// Evento que se encarga de llamar a la ventana acerca de...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var isTimerStarted = this.timer1.Enabled;
            if (isTimerStarted) timer1.Stop();
            About about = new About();
            about.ShowDialog();
            if (isTimerStarted) timer1.Start();
        }

        /// <summary>
        /// Evento que se encarga de los botones de los modos de juego
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gameModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.timer1.Stop();
            ToolStripDropDownItem item = (ToolStripDropDownItem)sender;
            this._pixelsToRender = null;

            switch (item.Tag.ToString())
            {
                case "competicion":
                    this._gameMode = new ChallengeController(this.canvasSnake.Width, this.canvasSnake.Height, PIXEL_LENGTH);
                    break;
                case "training":
                    this._gameMode = new TrainingController(this.canvasSnake.Width, this.canvasSnake.Height, PIXEL_LENGTH, int.Parse(item.Text));
                    break;
                case "versus_a":
                    this._gameMode = new VSAController(this.canvasSnake.Width, this.canvasSnake.Height, PIXEL_LENGTH);
                    break;
                case "versus_b":
                    this._gameMode = new VSBController(this.canvasSnake.Width, this.canvasSnake.Height, PIXEL_LENGTH);
                    break;
            }

            this._messageToRender = PUSH_START;

            this.canvasSnake.Invalidate();
        }

        /// <summary>
        /// Evento que controla el boton salir
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Evento que se encarga de parsear la entrada por teclado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Snake_KeyDown_1(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                    if (this._nextDirection.ContainsKey(GameController.FIRST_PLAYER))
                    {
                        this._nextDirection[GameController.FIRST_PLAYER] = SnakeController.Directions.DOWN;
                    }
                    else
                    {
                        this._nextDirection.Add(GameController.FIRST_PLAYER, SnakeController.Directions.DOWN);
                    }
                    break;
                case Keys.Up:
                    if (this._nextDirection.ContainsKey(GameController.FIRST_PLAYER))
                    {
                        this._nextDirection[GameController.FIRST_PLAYER] = SnakeController.Directions.UP;
                    }
                    else
                    {
                        this._nextDirection.Add(GameController.FIRST_PLAYER, SnakeController.Directions.UP);
                    }
                    break;
                case Keys.Left:
                    if (this._nextDirection.ContainsKey(GameController.FIRST_PLAYER))
                    {
                        this._nextDirection[GameController.FIRST_PLAYER] = SnakeController.Directions.LEFT;
                    }
                    else
                    {
                        this._nextDirection.Add(GameController.FIRST_PLAYER, SnakeController.Directions.LEFT);
                    }
                    break;
                case Keys.Right:
                    if (this._nextDirection.ContainsKey(GameController.FIRST_PLAYER))
                    {
                        this._nextDirection[GameController.FIRST_PLAYER] = SnakeController.Directions.RIGHT;
                    }
                    else
                    {
                        this._nextDirection.Add(GameController.FIRST_PLAYER, SnakeController.Directions.RIGHT);
                    }
                    break;
                case Keys.W:
                    if (this._nextDirection.ContainsKey(GameController.SECOND_PLAYER))
                    {
                        this._nextDirection[GameController.SECOND_PLAYER] = SnakeController.Directions.UP;
                    }
                    else
                    {
                        this._nextDirection.Add(GameController.SECOND_PLAYER, SnakeController.Directions.UP);
                    }
                    break;
                case Keys.S:
                    if (this._nextDirection.ContainsKey(GameController.SECOND_PLAYER))
                    {
                        this._nextDirection[GameController.SECOND_PLAYER] = SnakeController.Directions.DOWN;
                    }
                    else
                    {
                        this._nextDirection.Add(GameController.SECOND_PLAYER, SnakeController.Directions.DOWN);
                    }
                    break;
                case Keys.A:
                    if (this._nextDirection.ContainsKey(GameController.SECOND_PLAYER))
                    {
                        this._nextDirection[GameController.SECOND_PLAYER] = SnakeController.Directions.LEFT;
                    }
                    else
                    {
                        this._nextDirection.Add(GameController.SECOND_PLAYER, SnakeController.Directions.LEFT);
                    }
                    break;
                case Keys.D:
                    if (this._nextDirection.ContainsKey(GameController.SECOND_PLAYER))
                    {
                        this._nextDirection[GameController.SECOND_PLAYER] = SnakeController.Directions.RIGHT;
                    }
                    else
                    {
                        this._nextDirection.Add(GameController.SECOND_PLAYER, SnakeController.Directions.RIGHT);
                    }
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

        /// <summary>
        /// Evento que se encarga de refrescar todo cuando hay un tick en el timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {

            List<Pixel> pixelsRef = this._gameMode.refresh(this._nextDirection);

            this._messageToRender = this._gameMode.gameResult();

            this.timer1.Interval = this._gameMode.getTickerTimer();

            if (this._messageToRender != null)
            {
                this.timer1.Stop();
            }
            else
            {
                this._pixelsToRender = pixelsRef;
            }

            if (this._gameMode.getLvl() != null)
            {
                this.lvlLbl.Text = this._gameMode.getLvl();
            }
            else
            {
                this.lvlLbl.Text = "-";
            }

            this.lblMeat.Text = this._gameMode.getMeatValue().ToString();

            this._nextDirection.Clear();
            canvasSnake.Invalidate();
        }

        /// <summary>
        /// Evento de inicio del formulario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Snake_Load(object sender, EventArgs e)
        {
            this._messageToRender = NEW_GAME;
            this.canvasSnake.Invalidate();
        }

        /// <summary>
        /// Evento que se encarga de realizar el pintado del canvas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void canvasSnake_Paint(object sender, PaintEventArgs e)
        {
            if (this._messageToRender != null)
            {
                this.drawText(this._messageToRender);
                this._messageToRender = null;
            }
            else
            {
                this.lblMessages.Text = "";
                this.lblMessages.Visible = false;
                if (this._pixelsToRender != null)
                {
                    foreach (Pixel px in this._pixelsToRender)
                    {
                        Color pxColor = px.getColor();
                        Rectangle rct = new Rectangle(px.getX(), px.getY(), PIXEL_LENGTH, PIXEL_LENGTH);
                        e.Graphics.FillRectangle(new SolidBrush(pxColor), rct);
                    }
                }
            }
        }
        #endregion
    }
}
