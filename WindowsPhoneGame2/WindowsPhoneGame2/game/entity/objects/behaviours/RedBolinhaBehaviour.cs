using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsPhoneGame2.game.entity.rufus.behaviours;

namespace WindowsPhoneGame2.game.entity.objects.behaviours
{
    class RedBolinhaBehaviour : AbstractBehaviour
    {
        public RedBolinhaBehaviour()
        {
            frequency = 0.8f;
            points = 20;
            force = AbstractBehaviour.DEFAULT_SPEED * 1.5f;
            type = AbstractBehaviour.BOLINHA;

        }
    }
}
