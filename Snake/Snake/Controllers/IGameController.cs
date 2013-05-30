using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snake.Models;
using System.Collections;

namespace Snake.Controllers
{
    public interface IGameController
    {

        String gameResult();

        List<Pixel> refresh(Hashtable direction);

        int getTickerTimer();

    }
}
