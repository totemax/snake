using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Snake.Controllers
{
    public class SnakeController
    {
        public enum Directions
        {
            UP,
            DOWN,
            LEFT,
            RIGHT
        }

        #region [ Variables ]

        private List<Pixel> snakeBody;
        private int pixelL;
        private int length = 2;
        private Directions direction = Directions.LEFT;
        private Color color;

        #endregion

        #region [ Builders ]


        public SnakeController(int initX, int initY, int pixelL, Color snakeColor)
        {
            this.pixelL = pixelL;
            this.color = snakeColor;
            this.snakeBody = new List<Pixel>();
            this.snakeBody.Add(new Pixel(initX, initY, this.color));
            this.snakeBody.Add(new Pixel(initX - this.pixelL, initY, this.color));
        }

        #endregion

        #region [ Getters & Setters ]

        public List<Pixel> getSnakeBody() { return this.snakeBody; }
        public int getLength() { return this.length; }

        public void setDirection(Directions direction)
        {
            if (!isOpositteDirection(direction)) this.direction = direction;
        }

        #endregion 

        #region [ Methods ]

        private bool isOpositteDirection(Directions direction)
        {
            if (this.direction.Equals(Directions.UP) && direction.Equals(Directions.DOWN)) return true;
            if (this.direction.Equals(Directions.DOWN) && direction.Equals(Directions.UP)) return true;
            if (this.direction.Equals(Directions.LEFT) && direction.Equals(Directions.RIGHT)) return true;
            if (this.direction.Equals(Directions.RIGHT) && direction.Equals(Directions.LEFT)) return true;
            return false;
        }

        public void refresh()
        {
            for (int i = 0; i < snakeBody.Count; i++)
            {
                Pixel px = snakeBody[i];
                px.refresh();
                if (px.getCount() > this.length)
                    snakeBody.Remove(px);
            }
            Pixel snakeHead = this.snakeBody[snakeBody.Count() - 1];
            int newX = snakeHead.getX();
            int newY = snakeHead.getY();
            switch (this.direction)
            {
                case Directions.DOWN:
                    newY -= this.pixelL;
                    break;
                case Directions.LEFT:
                    newX -= this.pixelL;
                    break;
                case Directions.RIGHT:
                    newX += this.pixelL;
                    break;
                case Directions.UP:
                    newY += this.pixelL;
                    break;
            }
            snakeBody.Add(new Pixel(newX, newY, this.color));
        }

        #endregion
    }
}
