using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Snake.Models;

namespace Snake.Controllers
{
    /// <summary>
    /// Clase que se encarga de controlar el sistema de generación de comidas para la serpiente.
    /// </summary>
    public class MeatController
    {
        #region [Variables]

        private Random _random;
        private Color _color;
        private Pixel _meatPixel;
        private int _meatValue;
        private int _maxX;
        private int _maxY;
        private int _lPixel;
        private int _meatScore = 100;
        private int _actualScore = 0;
        private int _minValue = 1;


        #endregion

        #region [Builders]

        public MeatController(int maxX, int maxY, Color color, int lPixel, int meatValue, int minValue)
        {
            this._random = new Random();
            this._color = color;
            this._maxX = maxX;
            this._maxY = maxY;
            this._lPixel = lPixel;
            this._meatScore = meatValue;
            this._minValue = minValue;
        }

        public MeatController(int maxX, int maxY, Color color, int lPixel)
        {
            this._random = new Random();
            this._color = color;
            this._maxX = maxX;
            this._maxY = maxY;
            this._lPixel = lPixel;
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

        #region [Getters & Setters]

        /// <summary>
        /// Gets the meat pixel.
        /// </summary>
        /// <returns>Pixel</returns>
        public Pixel getMeatPixel() { return this._meatPixel; }

        /// <summary>
        /// Gets the actual meat value
        /// </summary>
        /// <returns>integer</returns>
        public int getActualValue() { return this._actualScore; }

        /// <summary>
        /// Sets the actual meat value
        /// </summary>
        /// <param name="actualValue">integer with the meat value</param>
        public void setActualScore(int actualValue) { this._actualScore = actualValue; }

        /// <summary>
        /// Gets the meat color
        /// </summary>
        /// <returns>Color</returns>
        public Color getColor() { return this._color; }

        /// <summary>
        /// Gets the meat value
        /// </summary>
        /// <returns></returns>
        public int getMeatValue() { return this._meatValue; }

        #endregion

        #region [Functions & Methods]

        public Pixel generateMeat(List<Pixel> pixelsOccupied)
        {
            int x = _random.Next(this._maxX);
            int y = _random.Next(this._maxY);
            while (isPixelOccupied(x, y, pixelsOccupied) || x % _lPixel != 0 || y % _lPixel != 0)
            {
                x = _random.Next(this._maxX);
                y = _random.Next(this._maxY);
            }
            this._meatPixel = new Pixel(x, y, this._color);
            this._actualScore = this._meatScore;
            this._meatValue = _random.Next(1, 4);
            return this._meatPixel;
        }

        private bool isPixelOccupied(int x, int y, List<Pixel> pixelsOccupied)
        {
            foreach (Pixel px in pixelsOccupied)
            {
                if (px.getX() == x && px.getY() == y) return true;
            }
            return false;
        }

        #endregion

    }
}
