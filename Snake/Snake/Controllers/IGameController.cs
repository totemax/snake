using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snake.Models;
using System.Collections;

namespace Snake.Controllers
{
    /// <summary>
    /// Game mode principal interface
    /// </summary>
    public interface IGameController
    {
        /// <summary>
        /// Result of the game mode
        /// </summary>
        /// <returns>String </returns>
        String gameResult();

        /// <summary>
        /// Refresh the game mode and update the components
        /// 
        /// </summary>
        /// <param name="direction">The next direction</param>
        /// <returns>List of the pixels to draw</returns>
        List<Pixel> refresh(Hashtable direction);

        /// <summary>
        /// Returns the timer interval (difficult levels)
        /// </summary>
        /// <returns></returns>
        int getTickerTimer();

        /// <summary>
        /// Return the level (if exists)
        /// </summary>
        /// <returns></returns>
        String getLvl();

        /// <summary>
        /// Returns the meal value
        /// </summary>
        /// <returns></returns>
        int getMeatValue();

    }
}
