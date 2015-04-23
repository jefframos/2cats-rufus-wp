using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsPhoneGame2.framework.GUI.button;

namespace WindowsPhoneGame2.game.factory.GUI
{
    class TwitterButton : SimpleButton
    {

        public TwitterButton(Action _callback)
            : base(new ButtonModel(".\\Sprites\\GUI\\twitterbutton\\twitterbutton", ".\\Sprites\\GUI\\twitterbutton\\twitterbuttonXML", null, new Microsoft.Xna.Framework.Rectangle(-5, -5, -35, 35)), "", new Microsoft.Xna.Framework.Point(), _callback)
        {
           
        }
    }
}
