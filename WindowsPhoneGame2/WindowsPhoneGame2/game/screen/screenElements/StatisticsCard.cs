using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsPhoneGame2.game.entity;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using WindowsPhoneGame2.game.entity.rufus;
using XNATweener;
using WindowsPhoneGame2.game.utils;
using WindowsPhoneGame2.framework.primitives;
using WindowsPhoneGame2.game.factory.GUI;
using Microsoft.Xna.Framework.Input.Touch;
using WindowsPhoneGame2.framework.GUI.button;
using WindowsPhoneGame2.game.screen.shopPages;
using WindowsPhoneGame2.game.entity.rufus.behaviours;
using RufusAndTheMagicMushrooms.framework.storage;
using RufusAndTheMagicMushrooms.game;

namespace WindowsPhoneGame2.game.screen.screenElements
{
    class StatisticsCard : StaticObject
    {
        private ContentManager content;
        private StaticObject backShape;
        public bool isActive { get; set; }
        private Tweener tweener;
        private StaticObject mainImage;
        private ArrowButton backButton;
        private TwitterButton twButton;
        private FBButton fbButton;
        private string mushrooms;
        private string bloobs;
        private string specials;
        private string maxcombo;
        private string maxjumps;
        private string maxpoints;

        public StatisticsCard()
            : base(".\\Sprites\\GUI\\statisticsCard")
        {
            backShape = new StaticObject(".\\Sprites\\GUI\\backPauseShape");
          

            twButton = new TwitterButton(null);
            fbButton = new FBButton(null);

        }
        public override void init(ContentManager Content)
        {
            content = Content;
            base.init(Content);
            backShape.init(content);
            twButton.init(content);
            fbButton.init(content);
            isActive = false;
            position.X = 800 / 2 - texture.Width / 2;
            position.Y = -texture.Height;
            mainImage = new StaticObject(GameModel.rufusModels.ElementAt(0).largePath);
            mainImage.init(content);

            backButton = new ArrowButton(hide);
            backButton.init(Content);

            ButtonModel defaultButtonModel = new ButtonModel(".\\Sprites\\GUI\\defaultButton\\shapeButton", ".\\Sprites\\GUI\\defaultButton\\shapeButtonXML", ButtonFactory.spriteFont);



            isActive = false;

        }
        public void show()
        {
            mushrooms = stringnizer(Statistics.maxMushrooms);
            bloobs = stringnizer(Statistics.maxBloobs);
            specials = stringnizer(Statistics.maxSpecials);
            maxcombo = stringnizer(Statistics.maxCombo);
            maxjumps = stringnizer(Statistics.maxJumps);
            maxpoints = stringnizer(Statistics.maxPoints);

            isActive = true;
            tweener = new Tweener(-texture.Height, 95f, 0.4f, Back.EaseOut);
            tweener.Start();
            tweener.endCallback = endShow;

        }
        /**
         *Compra o item 
         */

        public void hide()
        {
            isActive = false;
            tweener = new Tweener(position.Y, -texture.Height, 0.4f, Back.EaseOut);
            tweener.Start();
            tweener.endCallback = endHide;
        }
        public void endShow()
        {
            
        }
        public void endHide()
        {

        }
        public void update(GameTime gameTime, TouchCollection touches)
        {

            base.update(gameTime);
            if (tweener != null)
            {
                tweener.Update(gameTime);
                backShape.alpha = tweener.Position / 100;
                position.Y = tweener.Position;

                mainImage.position = position + new Vector2(33 + 75 - mainImage.texture.Width / 2, 82 + 70 - mainImage.texture.Height / 2);
                backButton.position = position + new Vector2(10 - 20, 10 - 20);

                backButton.updateButton(gameTime, touches);

                //twButton.position = position + new Vector2(80 + 40, 260);
               // fbButton.position = position + new Vector2(180 + 40, 260);

            }
        }
        public override void draw(SpriteBatch spriteBatch)
        {
            if (isActive)
            {
                backShape.draw(spriteBatch);
                base.draw(spriteBatch);
                if (ButtonFactory.spriteFont != null && position != null)
                {
                    //mushrooms = "00000";
                    //bloobs = "0000";
                    //specials = "0000";
                    //maxcombo = "0000";
                    //maxjumps = "000";
                    //maxpoints = "000";
                    int gambs = -8;
                    spriteBatch.DrawString(ButtonFactory.spriteFont, mushrooms, position + new Vector2(220, 70 + gambs), Color.White, 0f, new Vector2(), .7f, SpriteEffects.None, 0f);
                    spriteBatch.DrawString(ButtonFactory.spriteFont, bloobs, position + new Vector2(220, 100 + gambs), Color.White, 0f, new Vector2(), .7f, SpriteEffects.None, 0f);
                    spriteBatch.DrawString(ButtonFactory.spriteFont, specials, position + new Vector2(220, 130 + gambs), Color.White, 0f, new Vector2(), .7f, SpriteEffects.None, 0f);
                    spriteBatch.DrawString(ButtonFactory.spriteFont, maxcombo, position + new Vector2(220, 160 + gambs), Color.White, 0f, new Vector2(), .7f, SpriteEffects.None, 0f);
                    spriteBatch.DrawString(ButtonFactory.spriteFont, maxjumps, position + new Vector2(220, 190 + gambs), Color.White, 0f, new Vector2(), .7f, SpriteEffects.None, 0f);
                    spriteBatch.DrawString(ButtonFactory.spriteFont, maxpoints, position + new Vector2(220, 220 + gambs), Color.White, 0f, new Vector2(), .7f, SpriteEffects.None, 0f);

                    backButton.draw(spriteBatch);

                }
                //twButton.draw(spriteBatch);
               // fbButton.draw(spriteBatch);
            }
        }
        private static string stringnizer(int value)
        {
            string rt;
            if (value < 10)
                rt = "00000" + value.ToString();
            else if (value < 100)
                rt = "0000" + value.ToString();
            else if (value < 1000)
                rt = "000" + value.ToString();
            else if (value < 10000)
                rt = "00" + value.ToString();
            else if (value < 100000)
                rt = "0" + value.ToString();
            else rt =  value.ToString();

            rt = value.ToString();
            return rt;
        }

    }
}
