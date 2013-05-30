using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snake.Models;
using System.Collections;
namespace Snake.Controllers
{
    /// <summary>
    /// Abstract class that implements the inital functions for Game Controllers
    /// </summary>
    public abstract class GameController :IGameController
    {
        #region [Constants]

        protected static string GAME_OVER = "GAME OVER";
        public static string FIRST_PLAYER = "FIRST";
        public static string SECOND_PLAYER = "SECOND";

        #endregion

        #region [Variables]

        protected SnakeController _snake;
        protected MeatController _meat;
        protected int _tickTimer = 1000;
        protected int _pixelL;
        protected int _xLength;
        protected int _yLength;

        #endregion

        #region [Builders]

        public GameController(int xLength, int yLength, int pixelLength,System.Drawing.Color snakeColor)
        {
            this._pixelL = pixelLength;
            this._xLength = xLength;
            this._yLength = yLength;
            this._snake = new SnakeController(xLength, yLength, pixelLength, snakeColor);
            this._meat = new MeatController(xLength, yLength, pixelLength);
            initMeat();
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Init the meat controller
        /// </summary>
        protected void initMeat()
        {
            this._meat.generateMeat(this._snake.getSnakeBody());
        }

        #endregion

        #region [ Interface implementations ]

        public int getTickerTimer()
        {
            return this._tickTimer;
        }

        public bool isLevelChanged()
        {
            return false;
        }

        public List<Pixel> refresh(Hashtable direction)
        {
            if(direction.ContainsKey(FIRST_PLAYER)){
                this._snake.setDirection((SnakeController.Directions) direction[FIRST_PLAYER]);
            }
            this._snake.refresh();
            List<Pixel> snakePixels = this._snake.getSnakeBody();

            List<Pixel> pixelsToPaint = new List<Pixel>();

            pixelsToPaint.Add(this._meat.refresh(snakePixels, null));
            pixelsToPaint.AddRange(snakePixels);
            if (this._meat.isEaten())
            {
                this._snake.eatMeat(this._meat.getEatenValue());
            }

            return pixelsToPaint;
        }

        public string gameResult()
        {
            if (this._snake.hasCollision())
            {
                return GAME_OVER;
            }
            else return null;
        }

        public string getLvl()
        {
            return null;
        }

        public int getMeatValue()
        {
            return this._meat.getMeatValue();
        }

        #endregion
    }
}
