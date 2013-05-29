using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Snake.Models;

namespace Snake.Controllers
{

    class ChallengeController : GameController, IGameController
    {
        private static String LVL_PREFIX = "lvl_";
        private static String WIN = "YOU WIN!";

        private XmlDocument _lvlsDoc = null;

        private int _lvlNum = 1;
        private Boolean _isBoardLoaded = false;
        private int _scoreLvl = 10;
        private List<Pixel> _obstacles = null;
        private Boolean _isWinner = false;

        public ChallengeController(int maxX, int maxY, int pixelL)
            : base(maxX, maxY, pixelL, System.Drawing.Color.Black)
        {
            this.loadLvl();
        }

        private new void initMeat()
        {
            this._meat.generateMeat(this._snake.getSnakeBody().Concat(this._obstacles).ToList());
        }

        public new List<Pixel> refresh(SnakeController.Directions direction)
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
            string previousResult = base.gameResult();
            if (previousResult != null) return previousResult;
            else if(this._isWinner)
            {
                return WIN;
            }
            return null;
        }

        private void loadLvl()
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
                    if (bool.Parse(lvlBoard.Attributes["newBoard"].Value) || !_isBoardLoaded)
                    {
                        if (lvlBoard.HasChildNodes)
                        {
                            this._obstacles = new List<Pixel>();
                            foreach (XmlNode obstacleNode in lvlBoard.ChildNodes)
                            {
                                int x = int.Parse(obstacleNode.Attributes["x"].Value);
                                int y = int.Parse(obstacleNode.Attributes["y"].Value);
                                Pixel pxlObstacle = new Pixel(x, y, System.Drawing.Color.Black);
                                this._obstacles.Add(pxlObstacle);
                            }
                        }
                    }
                }
            }
        }

    }
}
