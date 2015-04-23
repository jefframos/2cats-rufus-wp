using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsPhoneGame2.game.entity.rufus.behaviours;
using Microsoft.Xna.Framework;

namespace WindowsPhoneGame2.game.level.behaviours
{
    class FlorestBehaviour : LevelBehaviour
    {
        public FlorestBehaviour()
        {
            LinkedList<string> tempClouds = new LinkedList<string>();
            tempClouds.AddLast(".\\Sprites\\elements\\nuvem1");
            tempClouds.AddLast(".\\Sprites\\elements\\nuvem2");



            LinkedList<string> levelLayers = new LinkedList<string>();
            levelLayers.AddLast(".\\Sprites\\GUI\\levels\\Level1Graphics\\maskedLayer2");
            levelLayers.AddLast(".\\Sprites\\GUI\\levels\\Level1Graphics\\maskedLayer1");
            levelLayers.AddLast(".\\Sprites\\GUI\\levels\\Level1Graphics\\maskedLayer3");
            levelLayers.AddLast(".\\Sprites\\GUI\\levels\\Level1Graphics\\maskedLayer4");

            levelModel = new LevelModel(".\\Sprites\\GUI\\levels\\level1", ".\\Sprites\\GUI\\levels\\bgPreGame", tempClouds,levelLayers, new Color(210, 235, 231));
            
            frequency = AbstractBehaviour.DEFAULT_FREQUENCY;
            points = AbstractBehaviour.DEFAULT_POINTS;
            gravity = 0;//AbstractBehaviour.DEFAULT_GRAVITY;

            enemy = 1;

        }
    }
}
