using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsPhoneGame2.framework.GUI.button;

namespace WindowsPhoneGame2.game.factory.GUI
{
    class HelpButton : SimpleButton
    {

        public HelpButton(Action _callback)
            : base(new ButtonModel(".\\Sprites\\GUI\\infoButton\\infoButton", ".\\Sprites\\GUI\\infoButton\\infoButtonXML", null, new Microsoft.Xna.Framework.Rectangle(-5, -5, -35, 35)), "", new Microsoft.Xna.Framework.Point(), _callback)
        {
            // call = _callback;  
            this.clickCallback = callB;
        }
       
        public void callB()
        {

            Game1.showHelp();

        }
    }
}
