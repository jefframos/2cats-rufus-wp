using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsPhoneGame2.game.entity.rufus.behaviours;

namespace WindowsPhoneGame2.game.entity.objects.behaviours
{
    class GreenBolinhaBehaviour : AbstractBehaviour
    {
        public GreenBolinhaBehaviour()
        {
            frequency = AbstractBehaviour.DEFAULT_FREQUENCY;
            points = 15;
            force = AbstractBehaviour.DEFAULT_SPEED;
            type = AbstractBehaviour.BOLINHA;

        }
    }
}
