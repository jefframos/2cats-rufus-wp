using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsPhoneGame2.framework.GUI.button;

namespace WindowsPhoneGame2.game.factory.GUI
{
    class InfoButton : SimpleButton
    {

        public InfoButton(Action _callback)
            : base(new ButtonModel(".\\Sprites\\GUI\\helpButton\\infoButton", ".\\Sprites\\GUI\\helpButton\\infoButtonXML", null, new Microsoft.Xna.Framework.Rectangle(-5, -5, -35, 35)), "", new Microsoft.Xna.Framework.Point(), _callback)
        {
           
        }
    }
}