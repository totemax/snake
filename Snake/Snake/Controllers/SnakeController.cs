using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake.Controllers
{
    public class SnakeController
    {
        #region [ Variables ]

        private List<Pixel> snakeBody;
        private int pixelL;
        private int longSnake = 2;

        #endregion

        #region [ Builders ]


        public SnakeController(int initX, int initY, int pixelL)
        {
            this.pixelL = pixelL;
            this.snakeBody = new List<Pixel>();
            this.snakeBody.Add(new Pixel(initX, initY));
            this.snakeBody.Add(new Pixel(initX + this.pixelL, initY));
        }

        #endregion

        #region [ Getters & Setters ]

        public List<Pixel> getSnakeBody() { return this.snakeBody; }

        #endregion 

        #region [ Methods ]

        public void refresh()
        {
            for (int i = 0; i<snakeBody.Count; i++)
            {
                Pixel px = snakeBody[i];
                px.refresh();
                if (px.getCount() > this.longSnake)
                    snakeBody.Remove(px);
            }
        }

        #endregion
    }
}
