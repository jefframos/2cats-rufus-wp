using RufusAndTheMagicMushrooms.game.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsPhoneGame2.game.entity.rufus.behaviours;

namespace WindowsPhoneGame2.game.entity.objects.behaviours
{
    class SmallMushroomBehaviour : AbstractBehaviour
    {
        public SmallMushroomBehaviour()
        {
            //tileSheet = ".\\Sprites\\mushrooms\\star\\star";
            //tileSheetXML = ".\\Sprites\\mushrooms\\star\\starXML";
            //totFrames = 5;

            tileSheet = ".\\Sprites\\mushrooms\\cogumelo3";
            frequency = AbstractBehaviour.DEFAULT_FREQUENCY;
            points = AbstractBehaviour.DEFAULT_POINTS;
            force = AbstractBehaviour.DEFAULT_SPEED;
            range = 30;
            type = AbstractBehaviour.MUSHROOM;
           // effect = RufusEffects.INVENCIBLE;
        }
    }
}
