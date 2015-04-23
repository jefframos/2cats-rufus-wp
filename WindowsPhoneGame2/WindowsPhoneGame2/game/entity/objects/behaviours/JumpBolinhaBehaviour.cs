using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsPhoneGame2.game.entity.rufus.behaviours;

namespace WindowsPhoneGame2.game.entity.objects.behaviours
{
    class JumpBolinhaBehaviour : AbstractBehaviour
    {
        public JumpBolinhaBehaviour()
        {
            frequency = 0.7f;
            points = 27;
            force = AbstractBehaviour.DEFAULT_SPEED;
            type = AbstractBehaviour.BOLINHA;

        }
    }
}
