using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsPhoneGame2.framework.GUI.button;

namespace WindowsPhoneGame2.game.factory.GUI
{
    class HomeButton : SimpleButton
    {

        public HomeButton(Action _callback)
            : base(new ButtonModel(".\\Sprites\\GUI\\homeButton\\homeButton", ".\\Sprites\\GUI\\homeButton\\homeButtonXML", null, new Microsoft.Xna.Framework.Rectangle(-5, -5, -35, 35)), "", new Microsoft.Xna.Framework.Point(), _callback)
        {
           
        }
    }
}
