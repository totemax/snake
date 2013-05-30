using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Snake.Models;
using System.Collections;
namespace Snake.Controllers
{
    class VSAController : GameController, IGameController
    {
        protected static String PLAYER1_WIN = "Player 1 WIN";
        protected static String PLAYER2_WIN = "Player 2 WIN";

        protected SnakeController _snake1;
        protected SnakeController _snake2;
        private static Color SNAKE_1_COLOR = Color.Blue;
        private static Color SNAKE_2_COLOR = Color.Purple;

        public VSAController(int maxX, int maxY, int pixelL)
            : base(maxX, maxY, pixelL, SNAKE_1_COLOR)
        {
            int numPixelsX = maxX / pixelL;
            int numPixelsY = maxY / pixelL;
            this._snake2 = this._snake;
            this._snake1 = new SnakeController(maxX, maxY, pixelL, numPixelsX / 4, numPixelsY / 2, SNAKE_2_COLOR);
            this._tickTimer = 150;
        }

        public new List<Pixel> refresh(Hashtable direction)
        {
            List<Pixel> snakePixels = new List<Pixel>();
            if (direction.ContainsKey(FIRST_PLAYER))
            {
               this._snake1.setDirection((SnakeController.Directions)direction[FIRST_PLAYER]);
            }
            if (direction.ContainsKey(SECOND_PLAYER))
            {
                this._snake2.setDirection((SnakeController.Directions)direction[SECOND_PLAYER]);
            }

            this._snake1.refresh();
            this._snake2.refresh();

            if (this._meat.isEaten(this._snake1.getSnakeBody()))
            {
                this._snake1.eatMeat(this._meat.getEatenValue());
            }
            else if (this._meat.isEaten(this._snake2.getSnakeBody()))
            {
                this._snake2.eatMeat(this._meat.getEatenValue());
            }

            snakePixels.AddRange(this._snake1.getSnakeBody());
            snakePixels.AddRange(this._snake2.getSnakeBody());
            snakePixels.Add(this._meat.refresh(snakePixels, null));

            return snakePixels;
        }

        public new String gameResult()
        {
            if (_snake1.hasCollision())
            {
                return PLAYER2_WIN;
            }
            else if (_snake2.hasCollision())
            {
                return PLAYER1_WIN;
            }
            return null;
        }
    }
}
