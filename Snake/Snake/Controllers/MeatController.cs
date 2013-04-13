using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Snake.Models;

namespace Snake.Controllers
{
    public class MeatController
    {
        Random random;
        Color color;
        Pixel meatPixel;
        int maxX;
        int maxY;
        int lPixel;

        public MeatController(int maxX, int maxY, Color color, int lPixel)
        {
            this.random = new Random();
            this.color = color;
            this.maxX = maxX;
            this.maxY = maxY;
            this.lPixel = lPixel;
        }

        public Pixel getMeatPixel() { return this.meatPixel; }

        public Pixel generateMeat(List<Pixel> pixelsOccupied)
        {
            int x = random.Next(this.maxX);
            int y = random.Next(this.maxY);
            while (isPixelOccupied(x, y, pixelsOccupied) || x % lPixel != 0 || y % lPixel != 0)
            {
                x = random.Next(this.maxX);
                y = random.Next(this.maxY);
            }
            this.meatPixel = new Pixel(x, y, this.color);
            return this.meatPixel;
        }

        private bool isPixelOccupied(int x, int y, List<Pixel> pixelsOccupied)
        {
            foreach (Pixel px in pixelsOccupied)
            {
                if (px.getX() == x && px.getY() == y) return true;
            }
            return false;
        }

    }
}
