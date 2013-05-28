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

        private XmlDocument _lvlsDoc = null;

        private int _lvlNum = 1;
        private Boolean _isBoardLoaded = false;
        private int _scoreLvl = 10;
        private List<Pixel> _obstacles = null;

        public ChallengeController(int startLvl, int initX, int initY, int pixelL) : base(initX, initY, pixelL, System.Drawing.Color.Black)
        {
            this._lvlNum = startLvl;
            this.loadLvl();
        }

        private void loadLvl(){
            if (this._lvlsDoc == null)
            {
                _lvlsDoc = new XmlDocument();
                _lvlsDoc.Load("Resources/Levels.xml");
            }
            XmlElement lvlXml = _lvlsDoc.GetElementById(LVL_PREFIX + this._lvlsDoc);
            
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
                            int x =  int.Parse(obstacleNode.Attributes["x"].Value);
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
