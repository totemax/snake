using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Snake.Models;

namespace Snake.Controllers
{
    /// <summary>
    /// Clase que controla el avance y parametros de la serpiente.
    /// </summary>
    public class SnakeController
    {
        public enum Directions
        {
            UP,
            DOWN,
            LEFT,
            RIGHT,
            NO_KEY
        }

        #region [ Variables ]

        private List<Pixel> _snakeBody;
        private int _xLength;
        private int _yLength;
        private int _pixelL;
        private int _length = 1;
        private Directions _direction = Directions.LEFT;
        private Color _color;

        #endregion

        #region [ Builders ]


        public SnakeController(int xLength, int yLength, int pixelL, Color snakeColor)
        {
            this._xLength = xLength;
            this._yLength = yLength;

            int numPixelsX = xLength % pixelL;
            int numPixelsY = yLength % pixelL;

            this._pixelL = pixelL;
            this._color = snakeColor;
            this._snakeBody = new List<Pixel>();
            this._snakeBody.Add(new Pixel((numPixelsX % 2) * pixelL, (numPixelsY % 2) * pixelL, 0, this._color));
        }

        #endregion

        public Snake.Models.Pixel Pixel
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        #region [ Getters & Setters ]

        /// <summary>
        /// Get the snake's body
        /// </summary>
        /// <returns> A list of pixel. </returns>
        public List<Pixel> getSnakeBody() { return this._snakeBody; }

        /// <summary>
        /// Get the snake's lenght
        /// </summary>
        /// <returns>Integer</returns>
        public int getLength() { return this._length; }

        /// <summary>
        /// Set the snake direction
        /// </summary>
        /// <param name="direction">The direction input</param>
        public void setDirection(Directions direction)
        {
            if (!isOpositteDirection(direction) && !direction.Equals(Directions.NO_KEY)) this._direction = direction;
        }

        #endregion 

        #region [ Methods ]

        /// <summary>
        /// Validate the direction 
        /// </summary>
        /// <param name="direction">The input direction</param>
        /// <returns>Boolean</returns>
        private bool isOpositteDirection(Directions direction)
        {
            if (this._direction.Equals(Directions.UP) && direction.Equals(Directions.DOWN)) return true;
            if (this._direction.Equals(Directions.DOWN) && direction.Equals(Directions.UP)) return true;
            if (this._direction.Equals(Directions.LEFT) && direction.Equals(Directions.RIGHT)) return true;
            if (this._direction.Equals(Directions.RIGHT) && direction.Equals(Directions.LEFT)) return true;
            return false;
        }

        /// <summary>
        /// Method whom refresh the snake body positions
        /// </summary>
        public void refresh()
        {
            Pixel snakeHead = this._snakeBody[_snakeBody.Count() - 1];
            int newX = snakeHead.getX();
            int newY = snakeHead.getY();
            switch (this._direction)
            {
                case Directions.DOWN:
                    newY += this._pixelL;
                    break;
                case Directions.LEFT:
                    newX -= this._pixelL;
                    break;
                case Directions.RIGHT:
                    newX += this._pixelL;
                    break;
                case Directions.UP:
                    newY -= this._pixelL;
                    break;
            }
            _snakeBody.Add(new Pixel(newX, newY, this._color));
            for (int i = 0; i < _snakeBody.Count; i++)
            {
                Pixel px = _snakeBody[i];
                px.refresh();
                if (px.getCount() >= this._length)
                {
                    _snakeBody.Remove(px);
                }
            }
            
        }


        /// <summary>
        /// Detects the snake's collision
        /// </summary>
        /// <returns>Boolean</returns>
        public bool hasColision()
        {
            Pixel snakeHead = this._snakeBody[_snakeBody.Count() - 1];
            if (snakeHead.getX() >= this._xLength || snakeHead.getX() < 0 || snakeHead.getY() >= this._yLength || snakeHead.getY() < 0){
                return true;
            }
            foreach (Pixel px in this._snakeBody)
            {
                if (px.getX() == snakeHead.getX() && px.getY() == snakeHead.getY() && px.getCount() != snakeHead.getCount()) return true;
            }
            return false;
        }

        public bool hasCollision(List<Pixel> obstacles)
        {
            if (this.hasColision())
            {
                return true;
            }
            else
            {
                Pixel snakeHead = this._snakeBody[_snakeBody.Count() - 1];
                foreach (Pixel px in obstacles)
                {
                    if (px.getX() == snakeHead.getX() && px.getY() == snakeHead.getY() && px.getCount() != snakeHead.getCount()) return true;
                }
                return false;
            }
        }

        public void eatMeat(int meatValue)
        {
            this._length += meatValue;
        }

        #endregion
    }
}
