using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsPhoneGame2.game.entity.rufus.behaviours;
using Microsoft.Xna.Framework;

namespace WindowsPhoneGame2.game.level.behaviours
{
    class CloudsBehaviour : LevelBehaviour
    {
        public CloudsBehaviour()
        {
           // LinkedList<string> tempClouds = new LinkedList<string>();
           // tempClouds.AddLast(".\\Sprites\\elements\\winterNuvem1");
            //tempClouds.AddLast(".\\Sprites\\elements\\winterNuvem2");

            LinkedList<string> levelLayers = new LinkedList<string>();
            levelLayers.AddLast(".\\Sprites\\GUI\\levels\\Level5Graphics\\level5");

            levelModel = new LevelModel(".\\Sprites\\GUI\\levels\\bgPreGame5", ".\\Sprites\\GUI\\levels\\bgPreGame5", null, levelLayers, new Color(95, 182, 230));
            frequency = AbstractBehaviour.DEFAULT_FREQUENCY + 1;
            points = AbstractBehaviour.DEFAULT_POINTS + 2;
            gravity = AbstractBehaviour.DEFAULT_GRAVITY / 2;

            enemy = 5;
        }
    }
}
