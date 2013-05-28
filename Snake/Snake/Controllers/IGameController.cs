using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snake.Models;

namespace Snake.Controllers
{
    public interface IGameController
    {

        String gameResult();

        List<Pixel> refresh(SnakeController.Directions direction);

        Boolean isLevelChanged();

        int getTickerTimer();

    }
}
