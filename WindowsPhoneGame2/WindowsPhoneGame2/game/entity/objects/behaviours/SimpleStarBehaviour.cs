using RufusAndTheMagicMushrooms.game.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsPhoneGame2.game.entity.rufus.behaviours;

namespace WindowsPhoneGame2.game.entity.objects.behaviours
{
    class SimpleStarBehaviour : AbstractBehaviour
    {
        public SimpleStarBehaviour()
        {
            frequency = 0.9f;
            points = 32;
            force = AbstractBehaviour.DEFAULT_SPEED;
            type = AbstractBehaviour.STAR;
            effect = RufusEffects.INVENCIBLE;

        }
    }
}
