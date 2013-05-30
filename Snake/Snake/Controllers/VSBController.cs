using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake.Controllers
{
    /// <summary>
    /// Controller for VERSUS B mode
    /// </summary>
    class VSBController: VSAController, IGameController
    {
        #region [Builders]

        public VSBController(int maxX, int maxY, int pixelL)
            : base(maxX, maxY, pixelL)
        {

        }

        #endregion

        #region [Interface implementations]

        public new String gameResult()
        {
            if (_snake1.hasCollision(this._snake2.getSnakeBody()))
            {
                return PLAYER1_WIN;
            }
            else if (_snake2.hasCollision(this._snake1.getSnakeBody()))
            {
                return PLAYER2_WIN;
            }
            return null;
        }

        #endregion
    }
}
