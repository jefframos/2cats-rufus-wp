using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsPhoneGame2.game.entity;
using WindowsPhoneGame2.game.entity.rufus.behaviours;

namespace WindowsPhoneGame2.game.level.behaviours
{
    class LevelBehaviour : AbstractBehaviour
    {
        public LevelModel levelModel { get; set; }
        public int enemy { get; set; }
        public int enemyFrequency { get; set; }
        public int pointIncress { get; set; }
        public int gravityIncress { get; set; }
    }
}
