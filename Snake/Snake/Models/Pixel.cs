using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Snake.Models
{
    /// <summary>
    /// Model for pixels
    /// </summary>
    public class Pixel
    {
        #region [ Variables ]

        //Position variables
        private int _x;
        private int _y;

        //Color variables
        private Color _color;

        //Position counter
        private int _count = 0;

        #endregion

        #region [ Builders ]

        public Pixel(int x, int y, int count, Color color)
        {
            this._x = x;
            this._y = y;
            this._count = count;
            this._color = color;
        }

        public Pixel(int x, int y, Color color)
        {
            this._x = x;
            this._y = y;
            this._color = color;
        }

        #endregion

        #region [ Getters & setters ]

        public int getX() { return this._x; }

        public void setX(int x) { this._x = x; }

        public int getY() { return this._y; }

        public void setY(int y) { this._y = y; }

        public Color getColor() { return this._color; }

        public void setColor(Color color) { this._color = color; }

        public int getCount() { return this._count; }

        #endregion

        #region [ Methods ]

        public void refresh()
        {
            this._count++;
        }

        #endregion 

    }
}
