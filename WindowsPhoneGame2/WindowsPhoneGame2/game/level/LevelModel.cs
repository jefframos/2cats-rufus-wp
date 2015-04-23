using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace WindowsPhoneGame2.game.level
{
    class LevelModel
    {
        public string cardPath;
        public string bgPath;
        public LinkedList<string> clouds;
        public LinkedList<string> levelLayers;
        public Color color;
        public LevelModel(string _cardPath, string _bgPath)
        {
            cardPath = _cardPath;
            bgPath = _bgPath;
            color = new Color(210, 235, 231);
        }
        public LevelModel(string _cardPath, string _bgPath, LinkedList<string> _clouds)
        {
            color = new Color(210, 235, 231);
            clouds = _clouds;
            cardPath = _cardPath;
            bgPath = _bgPath;
        }

        public LevelModel(string _cardPath, string _bgPath, LinkedList<string> _clouds,LinkedList<string> _levelLayers, Color _color)
        {
            // TODO: Complete member initialization
            levelLayers = _levelLayers;
            clouds = _clouds;
            cardPath = _cardPath;
            bgPath = _bgPath;
            color = _color;
        }
    }
}
