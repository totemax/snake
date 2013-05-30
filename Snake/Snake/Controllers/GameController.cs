using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snake.Models;
using System.Collections;
namespace Snake.Controllers
{
    public abstract class GameController :IGameController
    {
        protected SnakeController _snake;
        protected MeatController _meat;
        protected int _tickTimer = 1000;
        protected static string GAME_OVER = "GAME OVER";
        public static string FIRST_PLAYER = "FIRST";
        public static string SECOND_PLAYER = "SECOND";
        protected int _pixelL;
        protected int _xLength;
        protected int _yLength;

        public GameController(int xLength, int yLength, int pixelLength,System.Drawing.Color snakeColor)
        {
            this._pixelL = pixelLength;
            this._xLength = xLength;
            this._yLength = yLength;
            this._snake = new SnakeController(xLength, yLength, pixelLength, snakeColor);
            this._meat = new MeatController(xLength, yLength, pixelLength);
            initMeat();
        }

        protected void initMeat()
        {
            this._meat.generateMeat(this._snake.getSnakeBody());
        }

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

    }
}
