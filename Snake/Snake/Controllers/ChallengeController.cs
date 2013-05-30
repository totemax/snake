using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Snake.Models;
using System.Collections;
namespace Snake.Controllers
{

    /// <summary>
    /// Controller for the CHALLENGE mode
    /// </summary>
    class ChallengeController : GameController, IGameController
    {
        #region [ Constants ]

        private static String LVL_PREFIX = "lvl_";
        private static String WIN = "YOU WIN!";

        #endregion

        #region [ Variables ]

        private XmlDocument _lvlsDoc = null;
        protected int _lvlNum = 1;
        private int _scoreLvl = 10;
        protected List<Pixel> _obstacles = null;
        private Boolean _isWinner = false;

        #endregion

        #region [ Builders ]

        public ChallengeController(int maxX, int maxY, int pixelL)
            : base(maxX, maxY, pixelL, System.Drawing.Color.Black)
        {
            this.loadLvl();
        }

        #endregion

        #region [Abstract reimplimentations]

        private new void initMeat()
        {
             this._meat.generateMeat(this._snake.getSnakeBody().Concat(this._obstacles).ToList());
        }

        #endregion

        #region [Interfaz implementations]

        public new List<Pixel> refresh(Hashtable direction)
        {
            List<Pixel> initialPixels = base.refresh(direction);
            if (this._snake.getLength() > this._scoreLvl)
            {
                this._lvlNum++;
                loadLvl();
            }
            if (this._obstacles != null)
            {
                initialPixels.AddRange(this._obstacles);
            }
            return initialPixels;
        }

        public new string gameResult(){
            if (this._snake.hasCollision(this._obstacles)) return GAME_OVER;
            else if (this._isWinner)
            {
                return WIN;
            }
            return null;
        }

        public new string getLvl()
        {
            return this._lvlNum.ToString();
        }

        protected void loadLvl()
        {
            if (this._lvlsDoc == null)
            {
                _lvlsDoc = new XmlDocument();
                _lvlsDoc.Load("Resources/Levels.xml");
            }
            XmlElement lvlXml = _lvlsDoc.GetElementById(LVL_PREFIX + this._lvlNum);

            if (lvlXml == null)
            {
                this._isWinner = true;
            }
            else
            {
                //loading lvl attributes...
                XmlNodeList tickTimerTags = lvlXml.GetElementsByTagName("tickTimer");
                if (tickTimerTags.Count > 0)
                {
                    XmlNode tickTimer = tickTimerTags[0];
                    this._tickTimer = int.Parse(tickTimer.InnerText);
                }

                XmlNodeList scoreLvlTags = lvlXml.GetElementsByTagName("scoreLvl");
                if (scoreLvlTags.Count > 0)
                {
                    XmlNode scoreLvl = scoreLvlTags[0];
                    this._scoreLvl = int.Parse(scoreLvl.InnerText);
                }

                XmlNodeList levelBoardTags = lvlXml.GetElementsByTagName("board");
                if (levelBoardTags.Count > 0)
                {
                    XmlNode lvlBoard = levelBoardTags[0];
                    if (bool.Parse(lvlBoard.Attributes["newBoard"].Value))
                    {
                        if (lvlBoard.HasChildNodes)
                        {
                            this._obstacles = new List<Pixel>();
                            foreach (XmlNode obstacleNode in lvlBoard.ChildNodes)
                            {
                                int xPixl = int.Parse(obstacleNode.Attributes["x"].Value);
                                int yPixl = int.Parse(obstacleNode.Attributes["y"].Value);
                                if (xPixl * this._pixelL <= this._xLength && xPixl * this._pixelL >= 0 && yPixl * this._pixelL <= this._yLength && yPixl * this._pixelL >= 0)
                                {
                                    System.Drawing.Color pxlColor = System.Drawing.Color.FromArgb(int.Parse(obstacleNode.Attributes["r"].Value), int.Parse(obstacleNode.Attributes["g"].Value), int.Parse(obstacleNode.Attributes["b"].Value));
                                    Pixel pxlObstacle = new Pixel(xPixl * this._pixelL, yPixl * this._pixelL, pxlColor);
                                    this._obstacles.Add(pxlObstacle);
                                }

                            }
                        }
                        else
                        {
                            this._obstacles = null;
                        }
                    }
                }
            }
        }

        #endregion

    }
}
