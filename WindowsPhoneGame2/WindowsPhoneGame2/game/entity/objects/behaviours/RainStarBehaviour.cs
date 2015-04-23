using RufusAndTheMagicMushrooms.game.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsPhoneGame2.game.entity.rufus.behaviours;

namespace WindowsPhoneGame2.game.entity.objects.behaviours
{
    class RainStarBehaviour : AbstractBehaviour
    {
        public RainStarBehaviour()
        {
            tileSheet = ".\\Sprites\\mushrooms\\rain2\\rain2";
            tileSheetXML = ".\\Sprites\\mushrooms\\rain2\\rain2XML";
            totFrames = 1;

            frequency = 0.8f;
            points = 35;
            force = AbstractBehaviour.DEFAULT_SPEED;
            type = AbstractBehaviour.STAR;
        }
    }
}
