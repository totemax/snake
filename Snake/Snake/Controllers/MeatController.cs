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
        private Pixel _meatPixel;
        private Boolean _isMeatEaten = false;
        private int _meatValue;
        private int _countRefresh = 0;
        private int _maxX;
        private int _maxY;
        private int _lPixel;
        private int _meatScore = 100;
        private int _actualScore = 0;
        private int _minValue = 1;
        private Color _initialColor = Color.Red;
        private Color _meatColor = Color.Black;

        #endregion

        #region [Builders]

        public MeatController(int maxX, int maxY, int lPixel, int meatValue, int minValue)
        {
            this._random = new Random();
            this._maxX = maxX;
            this._maxY = maxY;
            this._lPixel = lPixel;
            this._meatScore = meatValue;
            this._minValue = minValue;
        }

        public MeatController(int maxX, int maxY, int lPixel)
        {
            this._random = new Random();
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
        /// Gets the meat value
        /// </summary>
        /// <returns></returns>
        public int getMeatValue() { return this._meatValue; }

        #endregion

        #region [Functions & Methods]

        private void generateMeat(List<Pixel> pixelsOccupied)
        {
            int x = _random.Next(this._maxX);
            int y = _random.Next(this._maxY);
            while (isPixelOccupied(x, y, pixelsOccupied) || x % _lPixel != 0 || y % _lPixel != 0)
            {
                x = _random.Next(this._maxX);
                y = _random.Next(this._maxY);
            }
            this._meatPixel = new Pixel(x, y, this._initialColor);
            this._actualScore = this._meatScore;
            this._meatValue = _random.Next(1, 4);
        }

        private bool isPixelOccupied(int x, int y, List<Pixel> pixelsOccupied)
        {
            foreach (Pixel px in pixelsOccupied)
            {
                if (px.getX() == x && px.getY() == y) return true;
            }
            return false;
        }

        public Pixel refresh(List<Pixel> pixelSnake, List<Pixel> pixelsObstacle)
        {
            foreach (Pixel snakeBody in pixelSnake)
            {
                if (snakeBody.getX() == this._meatPixel.getX() && snakeBody.getY() == this._meatPixel.getY())
                {
                    this._isMeatEaten = true;
                }
            }
            if (this._isMeatEaten || this._countRefresh > 10)
            {
                if (pixelsObstacle != null)
                {
                    this.generateMeat(pixelSnake.Concat(pixelsObstacle).ToList());
                }
                else
                {
                    this.generateMeat(pixelSnake);
                }
            }
            else if(pixelsObstacle != null)
            {
                foreach (Pixel pixelObstacle in pixelsObstacle)
                {
                    if (pixelObstacle.getX() == this._meatPixel.getX() && pixelObstacle.getY() == this._meatPixel.getY())
                    {
                        this.generateMeat(pixelSnake.Concat(pixelsObstacle).ToList());
                        break;
                    }
                }
            }
            if (this._countRefresh > 3)
            {
                this._meatPixel.setColor(this._meatColor);
            }
            return this._meatPixel;
        }

        public Boolean isEaten()
        {
            return this._isMeatEaten;
        }

        #endregion

    }
}
