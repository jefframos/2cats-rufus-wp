using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsPhoneGame2.game.entity.rufus.behaviours;
using Microsoft.Xna.Framework;

namespace WindowsPhoneGame2.game.level.behaviours
{
    class PipemanBehaviour : LevelBehaviour
    {
        public PipemanBehaviour()
        {
            LinkedList<string> tempClouds = new LinkedList<string>();
            tempClouds.AddLast(".\\Sprites\\elements\\cloudsMario");
            tempClouds.AddLast(".\\Sprites\\elements\\cloudsMario2");

            LinkedList<string> levelLayers = new LinkedList<string>();
            levelLayers.AddLast(".\\Sprites\\GUI\\levels\\Level6Graphics\\level6");

            levelModel = new LevelModel(".\\Sprites\\GUI\\levels\\bgPreGame6", ".\\Sprites\\GUI\\levels\\bgPreGame6", tempClouds, levelLayers, new Color(0, 95, 182));
            frequency = AbstractBehaviour.DEFAULT_FREQUENCY + 1;
            points = AbstractBehaviour.DEFAULT_POINTS + 2;
            gravity = AbstractBehaviour.DEFAULT_GRAVITY / 2;

            enemy = 6;
        }
    }
}
