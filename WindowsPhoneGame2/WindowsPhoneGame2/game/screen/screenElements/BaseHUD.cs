using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsPhoneGame2.game.entity;
using XNATweener;
using Microsoft.Xna.Framework;
using WindowsPhoneGame2.game.factory.GUI;

namespace WindowsPhoneGame2.game.screen.screenElements
{
    class BaseHUD : StaticObject
    {
        private Tweener tweener;
        private IncrementalLabel mushroomsLabel;
        private IncrementalLabel comboLabel;
        private IncrementalLabel timeLabel;


        public BaseHUD()
            : base(".\\Sprites\\GUI\\shopScreen\\starContainer")
        {
            mushroomsLabel = new IncrementalLabel(0, ButtonFactory.spriteFont);
            mushroomsLabel.position.X = 70;
            mushroomsLabel.position.Y = 448;

            comboLabel = new IncrementalLabel(0);
            comboLabel.position.X = 380;
            comboLabel.position.Y = 448;

            timeLabel = new IncrementalLabel(0);
            timeLabel.position.X = 220;
            timeLabel.position.Y = 448;
        }
        public override void init(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            base.init(Content);
            mushroomsLabel.init(Content);
            comboLabel.init(Content);
            timeLabel.init(Content);

        }
        internal void updateMushroomLabel(int totMush)
        {
            mushroomsLabel.newValue(totMush);
        }

        internal void start()
        {
            tweener = new Tweener(480f, 402f, 0.5f, Back.EaseOut);
            tweener.Start();

        }
        public override void update(GameTime gameTime)
        {
            base.update(gameTime);
            tweener.Update(gameTime);
            position.Y = tweener.Position;

            mushroomsLabel.position.Y = tweener.Position + 10;
            timeLabel.position.Y = tweener.Position + 4;
            comboLabel.position.Y = tweener.Position + 4;


            comboLabel.update(gameTime);
            timeLabel.update(gameTime);
            mushroomsLabel.update(gameTime);
        }
        public override void draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            base.draw(spriteBatch);
            mushroomsLabel.draw(spriteBatch);
            //comboLabel.draw(spriteBatch);
            //timeLabel.draw(spriteBatch);

            // spriteBatch.Draw(texture, new Rectangle(0, 0, texture.Width, texture.Height),
            //  new Color(255, 255, 255, 0.5f));
        }
    }
}
