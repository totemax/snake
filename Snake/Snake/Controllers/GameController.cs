using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snake.Models;

namespace Snake.Controllers
{
    public abstract class GameController :IGameController
    {
        protected SnakeController _snake;
        protected MeatController _meat;
        protected int _tickTimer = 1000;
        protected static string GAME_OVER = "GAME OVER";

        public GameController(int xLength, int yLength, int pixelLength,System.Drawing.Color snakeColor)
        {
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

        public List<Pixel> refresh(SnakeController.Directions direction)
        {
            if(direction != SnakeController.Directions.NO_KEY){
                this._snake.setDirection(direction);
            }
            this._snake.refresh();
            List<Pixel> snakePixels = this._snake.getSnakeBody();

            snakePixels.Add(this._meat.refresh(snakePixels, null));
            if (this._meat.isEaten())
            {
                this._snake.eatMeat(this._meat.getEatenValue());
            }

            return snakePixels;
        }

        public string gameResult()
        {
            if (this._snake.hasColision())
            {
                return GAME_OVER;
            }
            else return null;
        }

    }
}
