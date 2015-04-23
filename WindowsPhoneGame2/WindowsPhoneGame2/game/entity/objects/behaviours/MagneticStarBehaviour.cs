using RufusAndTheMagicMushrooms.game.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsPhoneGame2.game.entity.rufus.behaviours;

namespace WindowsPhoneGame2.game.entity.objects.behaviours
{
    class MagneticStarBehaviour : AbstractBehaviour
    {
        public MagneticStarBehaviour()
        {
            tileSheet = ".\\Sprites\\mushrooms\\magnetic\\magnetic";
            tileSheetXML = ".\\Sprites\\mushrooms\\magnetic\\magneticXML";
            totFrames = 1;

            frequency = 0.9f;
            points = 30;
            force = AbstractBehaviour.DEFAULT_SPEED;
            type = AbstractBehaviour.STAR;
            effect = RufusEffects.MAGNETIC;
        }
    }
}
