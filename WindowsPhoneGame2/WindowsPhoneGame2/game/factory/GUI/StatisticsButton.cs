using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsPhoneGame2.framework.GUI.button;

namespace WindowsPhoneGame2.game.factory.GUI
{
    class StatisticsButton : SimpleButton
    {

        public StatisticsButton(Action _callback)
            : base(new ButtonModel(".\\Sprites\\GUI\\statisticButton\\statisctis", ".\\Sprites\\GUI\\statisticButton\\statisctisXML", null, new Microsoft.Xna.Framework.Rectangle(-5,-5,-35,35)), "", new Microsoft.Xna.Framework.Point(), _callback)
        {
           
        }
    }
}
