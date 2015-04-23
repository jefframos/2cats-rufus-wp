using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsPhoneGame2.game.entity.rufus.behaviours;

namespace WindowsPhoneGame2.game.entity.objects.behaviours
{
    class BigodonMushroomBehaviour : AbstractBehaviour
    {
        public BigodonMushroomBehaviour()
        {
            tileSheet = ".\\Sprites\\mushrooms\\cogumelo4";
            frequency = AbstractBehaviour.DEFAULT_FREQUENCY * 0.6f;
            points = 12;
            force = AbstractBehaviour.DEFAULT_SPEED;
            range = 35;
            type = AbstractBehaviour.MUSHROOM;

        }
    }
}
