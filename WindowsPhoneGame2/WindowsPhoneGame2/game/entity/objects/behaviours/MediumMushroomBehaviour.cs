using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsPhoneGame2.game.entity.rufus.behaviours;

namespace WindowsPhoneGame2.game.entity.objects.behaviours
{
    class MediumMushroomBehaviour : AbstractBehaviour
    {
        public MediumMushroomBehaviour()
        {
            tileSheet = ".\\Sprites\\mushrooms\\cogumelo1";
            frequency = AbstractBehaviour.DEFAULT_FREQUENCY * 0.8f;
            points = 3;
            force = AbstractBehaviour.DEFAULT_SPEED;
            range = 30;
            type = AbstractBehaviour.MUSHROOM;

        }
    }
}
