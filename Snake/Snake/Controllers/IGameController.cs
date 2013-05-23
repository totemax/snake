using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snake.Models;

namespace Snake.Controllers
{
    public interface IGameController
    {
        public String gameResult();

        public Pixel[] refresh(SnakeController.Directions direction);

        public Boolean isLevelChanged();

        public int getTickerTimer();

    }
}
