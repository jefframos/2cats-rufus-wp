using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsPhoneGame2.game.entity;
using WindowsPhoneGame2.framework.primitives;
using Microsoft.Xna.Framework;
using WindowsPhoneGame2.game.utils;
using WindowsPhoneGame2.framework.GUI.button;
using Microsoft.Xna.Framework.Input.Touch;
using XNATweener;
using Microsoft.Xna.Framework.Content;
using WindowsPhoneGame2.game.FX;

namespace WindowsPhoneGame2.game.screen.screenElements
{
    class ComboBar : Drawable
    {
        private StaticObject backBar;
        private StaticObject colorBar;
        private SimpleButton invencibleButton;
        //private Boolean ableInvencible;
        private Boolean initDown;
        private Tweener barTween;
        private Action invelnciblecallback;
        private Action endInvelnciblecallback;
        private ContentManager content;
        public ComboBar(Action _invelnciblecallback, Action _endInvelnciblecallback)
        {
            invelnciblecallback = _invelnciblecallback;
            endInvelnciblecallback = _endInvelnciblecallback;
            invencibleButton = new SimpleButton(new ButtonModel(".\\Sprites\\GUI\\frontBar", "", null, new Rectangle(-20, -20, 20, 20)), "", new Point(), initInvencible);
            backBar = new StaticObject(".\\Sprites\\GUI\\backColorBar");

            backBar.position = new Vector2(10, 10);
            colorBar = new StaticObject(".\\Sprites\\GUI\\colorBar");
            colorBar.position = new Vector2(10, 10);
            invencibleButton.position = new Vector2(20, 80);
            colorBar.spriteEffects = Microsoft.Xna.Framework.Graphics.SpriteEffects.FlipVertically;

        }
        public override void init(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            content = Content;
            backBar.init(Content);
            colorBar.init(Content);
            invencibleButton.init(Content);
            colorBar.origin = new Vector2(0, colorBar.position.Y + colorBar.texture.Height - 5);
            //ableInvencible = false;
            initDown = false;
            colorBar.scale.Y = 0f;
        }
        public void initInvencible()
        {
            invencibleButton.reload(".\\Sprites\\GUI\\frontBarInvencible");
            invencibleButton.init(content);
            backBar.reload(".\\Sprites\\GUI\\backColorBarInvencible");
            backBar.init(content);
            colorBar.reload(".\\Sprites\\GUI\\colorBarInvencible");
            colorBar.init(content);

            initDown = true;

            barTween = new Tweener(1f, 0f, 10f, Linear.EaseNone);
            barTween.endCallback = endDownTween;
            invelnciblecallback();
        }

        public void updateScale(float newScale)
        {
            colorBar.scale.Y = newScale;
            //if (!initDown)
            //{
                //if (newScale >= 1f)
                //{
                //    newScale = 1f;
                //    if (!ableInvencible)
                //    {
                //        invencibleButton.reload(".\\Sprites\\GUI\\frontBarTap");
                //        invencibleButton.init(content);

                //        DefaultFX tempFX = new FXShow(invencibleButton.position.X + invencibleButton.texture.Width / 2 - 25, invencibleButton.position.Y + invencibleButton.texture.Height / 2 - 25);
                //        GameScreen.getFXLayer().add(tempFX);
                //        tempFX.init(content);
                //    }
                //    ableInvencible = true;
                //}
                //barTween = new Tweener(colorBar.scale.Y, newScale, .2f, Cubic.EaseOut);
                //barTween.endCallback = null;
            //}

            //barTween = new Tweener(colorBar.scale.Y, newScale, .2f, Cubic.EaseOut);
            //barTween.endCallback = null;
        }
        public void endDownTween()
        {
            endInvelnciblecallback();
            initDown = false;
            //ableInvencible = false;

            invencibleButton.reload(".\\Sprites\\GUI\\frontBar");
            invencibleButton.init(content);
            backBar.reload(".\\Sprites\\GUI\\backColorBar");
            backBar.init(content);
            colorBar.reload(".\\Sprites\\GUI\\colorBar");
            colorBar.init(content);
        }
        public void update(GameTime gameTime, TouchCollection touches)
        {
            //if (colorBar.scale.Y > 0)
            //{
            //    if (ableInvencible)
            //        invencibleButton.updateButton(gameTime, touches);
            //    if (barTween != null)
            //    {
            //        barTween.Update(gameTime);
            //        colorBar.scale = new Vector2(1, barTween.Position);
            //    }
            //}
        }
        public override void draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            if (colorBar.scale.Y > 0)
            {
                spriteBatch.Draw(backBar.texture, backBar.position + invencibleButton.position, Color.White);
                colorBar.position = new Vector2(invencibleButton.position.X + 10, invencibleButton.position.Y + 165);
                colorBar.draw(spriteBatch);
                invencibleButton.draw(spriteBatch);
            }
        }
    }
}
