using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsPhoneGame2.game.entity.rufus.behaviours;

namespace WindowsPhoneGame2.game.entity.objects.behaviours
{
    class GiantMushroomBehaviour : AbstractBehaviour
    {
        public GiantMushroomBehaviour()
        {
            tileSheet = ".\\Sprites\\mushrooms\\cogumelo2";
            frequency = AbstractBehaviour.DEFAULT_FREQUENCY * 0.7f;
            points = 10;
            force = AbstractBehaviour.DEFAULT_SPEED;
            range = 40;
            centerPosition = new Microsoft.Xna.Framework.Vector2(20, 38);
            type = AbstractBehaviour.MUSHROOM;

        }
    }
}
