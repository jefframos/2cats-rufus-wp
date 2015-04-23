using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace WindowsPhoneGame2.game.utils
{
    class PointUtil
    {
        public static double distance(Point point1, Point point2)
        {
            return Math.Sqrt( (point1.X -point2.X)*(point1.X -point2.X) +  (point1.Y -point2.Y)*(point1.Y -point2.Y));
            
        }
    }
    
}
