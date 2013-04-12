using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Snake.Controllers
{
    /*
     * Controller for game pixels 
     * Authors: jjorge & arodrigo
     */
    public class Pixel
    {
        #region [ Variables ]

        //Position variables
        private int x;
        private int y;

        //Color variables
        private Color color;

        //Position counter
        private int count = 0;

        #endregion

        #region [ Builders ]

        public Pixel(int x, int y, int count, Color color)
        {
            this.x = x;
            this.y = y;
            this.count = count;
            this.color = color;
        }

        public Pixel(int x, int y, Color color)
        {
            this.x = x;
            this.y = y;
            this.color = color;
        }

        #endregion

        #region [ Getters & setters ]

        public int getX() { return this.x; }

        public void setX(int x) { this.x = x; }

        public int getY() { return this.y; }

        public void setY(int y) { this.y = y; }

        public Color getColor() { return this.color; }

        public void setColor(Color color) { this.color = color; }

        public int getCount() { return this.count; }

        #endregion

        #region [ Methods ]

        public void refresh()
        {
            this.count++;
        }

        #endregion 

    }
}
