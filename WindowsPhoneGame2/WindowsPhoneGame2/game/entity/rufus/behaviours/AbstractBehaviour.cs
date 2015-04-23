using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace WindowsPhoneGame2.game.entity.rufus.behaviours
{

    class AbstractBehaviour
    {
        public string tileSheet { get; set; }
        public string tileSheetXML { get; set; }
        public LinkedList<int> idleFrames { get; set; }
        public LinkedList<int> jumpFrames { get; set; }
        public LinkedList<int> fallingFrames { get; set; }
        public LinkedList<int> downFrames { get; set; }
        public LinkedList<int> upFrames { get; set; }
        public LinkedList<int> airdyingFrames { get; set; }
        public LinkedList<int> dieFrames { get; set; }
        public float gravity { get; set; }
        public float force { get; set; }
        public Vector2 centerPosition { get; set; }
        public Vector2 spriteSheetPosition { get; set; }
        public int limitBase { get; set; }
        public int totFrames { get; set; }
        public int range { get; set; }
        public float bounce { get; set; }
        public float frequency { get; set; }
        public float points { get; set; }
        public string type { get; set; }
        public string effect { get; set; }
        public static float DEFAULT_FREQUENCY = 1f;
        public static int DEFAULT_POINTS = 1;
        public static float DEFAULT_GRAVITY = 0.2f;
        public static float DEFAULT_SPEED = 0.03f;
        public static float DEFAULT_BOUNCE = 0.2f;

        public static string MUSHROOM = "mushroom";
        public static string BOLINHA = "bolinha";
        public static string STAR = "star";
    }
}
