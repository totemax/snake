using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Snake.Controllers
{
    public class MeatController
    {
        Random random;
        Color color;
        Pixel meatPixel;
        int maxX;
        int maxY;

        public MeatController(int maxX, int maxY, Color color)
        {
            this.random = new Random();
            this.color = color;
            this.maxX = maxX;
            this.maxY = maxY;
        }

        public Pixel getMeatPixel() { return this.meatPixel; }

        public Pixel generateMeat(List<Pixel> pixelsOccupied)
        {
            int x = random.Next(this.maxX);
            int y = random.Next(this.maxY);
            while (isPixelOccupied(x, y, pixelsOccupied))
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
