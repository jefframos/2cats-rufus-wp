using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsPhoneGame2.game.entity;
using WindowsPhoneGame2.framework.GUI.button;
using WindowsPhoneGame2.game.factory.GUI;
using Microsoft.Xna.Framework;
using WindowsPhoneGame2.game.utils;
using WindowsPhoneGame2.game.entity.rufus;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace WindowsPhoneGame2.game.screen.shopPages.elements
{
    class ItemBox : SimpleButton
    {
        private StaticObject image;
        private StaticObject star;
        public Vector2 initPos;
        public Vector2 endPos;
        private ItemBoxModel itemModel;
        private ContentManager content;
        private float scalee;
        private Vector2 labelPos;
        public ItemBox(float x, float y, ItemBoxModel _itemModel)
            : base(new ButtonModel(".\\Sprites\\GUI\\shopScreen\\box", "", null), "", new Microsoft.Xna.Framework.Point(), null)
        {
            position.X = x;
            position.Y = y;
            itemModel = _itemModel;
            endPos = new Vector2(x, y);
            image = new StaticObject(itemModel.imagePath);
            star = new StaticObject(".\\Sprites\\GUI\\shopScreen\\miniStar");
            clickCallback = itemClick;
            scalee = itemModel.value.ToString().Length > 4 ? 0.4f : 0.5f;
            labelPos = itemModel.value.ToString().Length < 5 ? new Vector2(44, 84) : new Vector2(45, 88);

        }
        private void itemClick()
        {
            if (itemModel.rufusModel.title != "COMMING")
            {
                if (MediaPlayer.State != MediaState.Paused)
                {
                    SoundEffect fx = content.Load<SoundEffect>(".\\sounds\\bup");
                    fx.Play(0.5f, 1, 1);
                }

                DefaultModel temp = GameModel.getModel(itemModel.type, itemModel.id);
                Game1.showCard(temp, itemModel.type);
            }

        }
        public override void updateButton(GameTime gameTime, Microsoft.Xna.Framework.Input.Touch.TouchCollection touches)
        {
            base.updateButton(gameTime, touches);
        }
        public override void init(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            content = Content;
            base.init(Content);
            image.init(Content);
            if (itemModel.rufusModel.title != "COMMING")
                star.init(Content);
        }
        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.update(gameTime);
            if (itemModel.rufusModel != null)
            {

                if (!itemModel.rufusModel.isActive)
                {
                    if (itemModel.rufusModel.title != "COMMING")
                    {
                        image.color = new Color(0, 0, 0);
                    }
                }
                else
                {
                    image.color = Color.White;
                }
            }
            else
            {
                if (!itemModel.isActive)
                {
                    if (itemModel.rufusModel.title != "COMMING")
                    {
                        image.color = new Color(0, 0, 0);
                    }
                }
                else
                {
                    image.color = Color.White;
                }
            }

            image.position.X = position.X + texture.Width / 2 - image.texture.Width / 2;
            image.position.Y = position.Y + texture.Height / 2 - image.texture.Height / 2;

            star.position.X = position.X + 5;
            star.position.Y = position.Y + 75;
        }
        public override void draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            base.draw(spriteBatch);
            image.draw(spriteBatch);
            if (itemModel.rufusModel != null)
            {
                if (itemModel.rufusModel.title != "COMMING")
                {
                    if (!itemModel.rufusModel.isActive)
                    {
                        star.draw(spriteBatch);
                        spriteBatch.DrawString(ButtonFactory.spriteFont, itemModel.value.ToString(), position + labelPos, color, 0, new Vector2(), scalee, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0f);
                    }
                }
            }
            else if (!itemModel.isActive)
            {
                if (itemModel.rufusModel.title != "COMMING")
                {
                    star.draw(spriteBatch);
                    spriteBatch.DrawString(ButtonFactory.spriteFont, itemModel.value.ToString(), position + labelPos, color, 0, new Vector2(), scalee, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0f);
                }
            }
        }

    }
}
