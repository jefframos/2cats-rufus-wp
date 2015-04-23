using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsPhoneGame2.framework.GUI.button;

namespace WindowsPhoneGame2.game.factory.GUI
{
    class InGamePlayButton : SimpleButton
    {

        public InGamePlayButton(Action _callback)
            : base(new ButtonModel(".\\Sprites\\GUI\\playButton\\playGameButton", ".\\Sprites\\GUI\\playButton\\playGameButtonXML", null, new Microsoft.Xna.Framework.Rectangle(-5, -5, -55, 55)), "", new Microsoft.Xna.Framework.Point(), _callback)
        {
           
        }
    }
}
