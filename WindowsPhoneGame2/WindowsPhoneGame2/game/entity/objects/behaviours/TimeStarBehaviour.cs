using RufusAndTheMagicMushrooms.game.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsPhoneGame2.game.entity.rufus.behaviours;

namespace WindowsPhoneGame2.game.entity.objects.behaviours
{
    class TimeStarBehaviour : AbstractBehaviour
    {
        public TimeStarBehaviour()
        {
            tileSheet = ".\\Sprites\\mushrooms\\time\\time";
            tileSheetXML = ".\\Sprites\\mushrooms\\time\\timeXML";
            totFrames = 1;

            frequency = AbstractBehaviour.DEFAULT_FREQUENCY;
            points = 30;
            force = AbstractBehaviour.DEFAULT_SPEED;
            type = AbstractBehaviour.STAR;
            effect = RufusEffects.PLUS;
        }
    }
}
