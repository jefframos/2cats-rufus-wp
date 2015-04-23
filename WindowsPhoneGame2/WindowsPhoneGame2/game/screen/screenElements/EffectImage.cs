using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsPhoneGame2.framework.primitives;
using WindowsPhoneGame2.game.entity;
using WindowsPhoneGame2.game.utils;
using XNATweener;

namespace RufusAndTheMagicMushrooms.game.screen.screenElements
{
    class EffectImage : DefaultEntity
    {
        private StaticObject image;
        //private ContentManager content;
        private Tweener tweener;
        private int hideCounter;
        private bool goaway;
        public EffectImage(int type)
        {

            hideCounter = 1000;
            if (type == 1)
                image = new StaticObject(".\\Sprites\\GUI\\invencible");
            else if (type == 2)
                image = new StaticObject(".\\Sprites\\GUI\\magnetic");
            if (type == 3)
            {
                image = new StaticObject(".\\Sprites\\GUI\\mushrain");
                hideCounter = 200;
            }
            if (type == 4)
                image = new StaticObject(".\\Sprites\\GUI\\stop");

           
            goaway = false;
        }
        public override void init(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            content = Content;
            image.init(Content);
            image.position.X = 800 / 2 - image.texture.Width / 2;
            image.position.Y = -image.texture.Height;

            tweener = new Tweener(image.position.Y, 40, 1f, Back.EaseOut);
           // tweener.endCallback = goBack;
        }
        private void goBack()
        {
            goaway = true;
            tweener = new Tweener(image.position.Y, -image.texture.Height, 1f, Cubic.EaseOut);
            tweener.endCallback = kill;
        }
        public override void update(GameTime gameTime)
        {

            if (hideCounter <= 0 && !goaway)
                goBack();
            else hideCounter--;
            Trace.write(hideCounter.ToString());
            if (tweener != null)
            {
                tweener.Update(gameTime);
                image.position.Y = tweener.Position;
            }

        }
        public override void draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            if (image != null && image.texture != null)
                image.draw(spriteBatch);
        }
    }
}
