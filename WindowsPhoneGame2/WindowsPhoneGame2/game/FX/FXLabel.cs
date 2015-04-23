using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsPhoneGame2.game.entity;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using WindowsPhoneGame2.game.graphics;
using WindowsPhoneGame2.game.graphics.spritesheet;
using Microsoft.Xna.Framework;
using XNATweener;
using WindowsPhoneGame2.game.factory.GUI;

namespace WindowsPhoneGame2.game.FX
{
    class FXLabel : DefaultFX         
    {
        private string value;
        private Tweener tweener;
        private Color fontColor;
        private Color secondColor;
        public FXLabel(float x, float y, string _value, Color _fontColor)
            : base(x, y)
        {
            fontColor = _fontColor;
            value = _value;
            tweener = new Tweener(.2f, 1f, 0.6f, Cubic.EaseOut);
            tweener.endCallback = kill;
            limitBaseY = 480;
        }
        public FXLabel(float x, float y, string _value, Color _fontColor, Color _secondColor)
            : base(x, y)
        {
            fontColor = _fontColor;
            secondColor = _secondColor;
            value = _value;
            tweener = new Tweener(.2f, 1f, 0.6f, Cubic.EaseOut);
            tweener.endCallback = kill;
            limitBaseY = 480;
        }
        public override void update(GameTime gameTime)
        {
            gravity = 0;
            velocity = new Vector2();
            if (tweener != null)
                tweener.Update(gameTime);
            scale = new Vector2(tweener.Position);
            base.update(gameTime);
        }
        public override void draw(SpriteBatch spriteBatch)
        {
            //base.draw(spriteBatch);
            if(secondColor != null)
                spriteBatch.DrawString(ButtonFactory.spriteFont, value, position, secondColor, 0.3f, new Vector2(7, 7), scale, spriteEffects, 0f);

            spriteBatch.DrawString(ButtonFactory.spriteFont, value, position, fontColor, 0.3f, new Vector2(10, 10), scale, spriteEffects, 0f);
        }
        public override void kill()
        {
            dead = true;
        }       
    }
}
