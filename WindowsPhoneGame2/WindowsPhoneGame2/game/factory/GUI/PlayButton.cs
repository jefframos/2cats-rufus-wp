using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsPhoneGame2.framework.GUI.button;

namespace WindowsPhoneGame2.game.factory.GUI
{
    class PlayButton : SimpleButton
    {

        public PlayButton(Action _callback)
            : base(new ButtonModel(".\\Sprites\\GUI\\playButton", "", null), "", new Microsoft.Xna.Framework.Point(), _callback)
        {
           
        }
    }
}
