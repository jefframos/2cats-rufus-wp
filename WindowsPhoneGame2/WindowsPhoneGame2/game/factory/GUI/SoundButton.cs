using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsPhoneGame2.framework.GUI.button;

namespace WindowsPhoneGame2.game.factory.GUI
{
    class SoundButton : SimpleButton
    {
       // private Action call;
        public SoundButton(Action _callback)
            : base(new ButtonModel(".\\Sprites\\GUI\\soundButton\\soundButton", ".\\Sprites\\GUI\\soundButton\\soundButtonXML", null, new Microsoft.Xna.Framework.Rectangle(-5, -5, -35, 35)), "", new Microsoft.Xna.Framework.Point(), _callback)
        {
           // call = _callback;  
            this.clickCallback = callB;
        }
        public override void init(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            base.init(Content);
            if (MediaPlayer.State == MediaState.Playing)
                isActive = false;
            else
                isActive = true;
        }
        public void callB()
        {

            Game1.clickSound();

            if (MediaPlayer.State == MediaState.Playing)
                isActive = false;
            else
                isActive = true;
           
        }
    }
}
