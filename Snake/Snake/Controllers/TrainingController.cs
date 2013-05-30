using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snake.Models;
using System.Collections;

namespace Snake.Controllers
{
    class TrainingController : ChallengeController, IGameController
    {
        public TrainingController(int maxX, int maxY, int pixelL, int level): base (maxX, maxY, pixelL)
        {
            this._lvlNum = level;
            this.loadLvl();
        }

        public new List<Pixel> refresh(Hashtable direction)
        {
            List<Pixel> initialPixels = base.refresh(direction);
            if (this._obstacles != null)
            {
                initialPixels.AddRange(this._obstacles);
            }
            return initialPixels;
        }
    }
}
