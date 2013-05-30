using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snake.Models;
using System.Collections;

namespace Snake.Controllers
{
    /// <summary>
    /// Game controller for TRAINING mode
    /// </summary>
    class TrainingController : ChallengeController, IGameController
    {
        #region [Builders]

        public TrainingController(int maxX, int maxY, int pixelL, int level): base (maxX, maxY, pixelL)
        {
            this._lvlNum = level;
            this.loadLvl();
        }

        #endregion

        #region [Interface implementations]

        public new List<Pixel> refresh(Hashtable direction)
        {
            List<Pixel> initialPixels = base.refresh(direction);
            if (this._obstacles != null)
            {
                initialPixels.AddRange(this._obstacles);
            }
            return initialPixels;
        }

        #endregion
    }
}
