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
        private int _eatenValue = 0;
        private int _minValue = 1;
        private int _maxValue = 4;
        private Color _initialColor = Color.Red;
        private Color _meatColor = Color.Black;

        #endregion

        #region [Builders]

        public MeatController(int maxX, int maxY, int lPixel, int minValue, int maxValue)
        {
            this._random = new Random();
            this._maxX = maxX;
            this._maxY = maxY;
            this._lPixel = lPixel;
            this._minValue = minValue;
            this._maxValue = maxValue;
        }

        public MeatController(int maxX, int maxY, int lPixel)
        {
            this._random = new Random();
            this._maxX = maxX;
            this._maxY = maxY;
            this._lPixel = lPixel;
        }

        #endregion

        #region [Getters & Setters]

        /// <summary>
        /// Gets the meat pixel.
        /// </summary>
        /// <returns>Pixel</returns>
        public Pixel getMeatPixel() { return this._meatPixel; }

        /// <summary>
        /// Gets the meat value
        /// </summary>
        /// <returns></returns>
        public int getMeatValue() { return this._meatValue; }

        #endregion

        #region [Functions & Methods]

        /// <summary>
        /// Generates the meat
        /// </summary>
        /// <param name="pixelsOccupied">pixels occupied in the screen</param>
        public void generateMeat(List<Pixel> pixelsOccupied)
        {
            int x = _random.Next((int)(this._maxX / _lPixel));
            int y = _random.Next((int)(this._maxY/ _lPixel));
            while (isPixelOccupied(x * _lPixel, y * _lPixel, pixelsOccupied))
            {
                x = _random.Next((int)(this._maxX / _lPixel));
                y = _random.Next((int)(this._maxY / _lPixel));
            }
            this._meatPixel = new Pixel(x*_lPixel, y*_lPixel, this._initialColor);
            this._meatValue = _random.Next(this._minValue, this._maxValue);
            this._countRefresh = 0;
        }

        /// <summary>
        /// Comprobates if the pixel is occupied in the screen
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="pixelsOccupied"></param>
        /// <returns></returns>
        private bool isPixelOccupied(int x, int y, List<Pixel> pixelsOccupied)
        {
            foreach (Pixel px in pixelsOccupied)
            {
                if (px.getX() == x && px.getY() == y) return true;
            }
            return false;
        }

        /// <summary>
        /// Refresh the meat state
        /// </summary>
        /// <param name="pixelSnake"></param>
        /// <param name="pixelsObstacle"></param>
        /// <returns></returns>
        public Pixel refresh(List<Pixel> pixelSnake, List<Pixel> pixelsObstacle)
        {
            this._isMeatEaten = isEaten(pixelSnake);

            if (this._isMeatEaten || this._countRefresh > 15)
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
            this._countRefresh++;
            return this._meatPixel;
        }

        /// <summary>
        /// Comprobate if the meat is eaten
        /// </summary>
        /// <returns></returns>
        public Boolean isEaten()
        {
            return this._isMeatEaten;
        }

        /// <summary>
        /// Returns the eated meat value
        /// </summary>
        /// <returns></returns>
        public int getEatenValue()
        {
            return this._eatenValue;
        }

        /// <summary>
        /// Comprobate if the meat is eaten
        /// </summary>
        /// <param name="snake"></param>
        /// <returns></returns>
        public bool isEaten(List<Pixel> snake)
        {
            foreach (Pixel snakeBody in snake)
            {
                if (snakeBody.getX() == this._meatPixel.getX() && snakeBody.getY() == this._meatPixel.getY())
                {
                    this._eatenValue = this._meatValue;
                    return true;
                }
            }
            return false;
        }

        #endregion

    }
}
