using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsPhoneGame2.game.entity.rufus.behaviours;
using Microsoft.Xna.Framework;

namespace WindowsPhoneGame2.game.level.behaviours
{
    class HellBehaviour : LevelBehaviour
    {
        public HellBehaviour()
        {
            //LinkedList<string> tempClouds = new LinkedList<string>();
            //tempClouds.AddLast(".\\Sprites\\elements\\winterNuvem1");
           // tempClouds.AddLast(".\\Sprites\\elements\\winterNuvem2");

            LinkedList<string> levelLayers = new LinkedList<string>();
            levelLayers.AddLast(".\\Sprites\\GUI\\levels\\Level4Graphics\\maskedLayer1");

            levelModel = new LevelModel(".\\Sprites\\GUI\\levels\\level4", ".\\Sprites\\GUI\\levels\\bgPreGame4", null,levelLayers, new Color(80, 30, 15));
            frequency = AbstractBehaviour.DEFAULT_FREQUENCY + 1;
            points = AbstractBehaviour.DEFAULT_POINTS + 1;
            gravity = AbstractBehaviour.DEFAULT_GRAVITY;

            enemy = 4;
        }
    }
}
