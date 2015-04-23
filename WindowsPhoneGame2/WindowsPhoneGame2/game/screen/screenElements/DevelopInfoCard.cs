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
    class DevelopInfoCard : StaticObject
    {
        private ContentManager content;
        private StaticObject backShape;
        public bool isActive { get; set; }
        private Tweener tweener;
        private ArrowButton backButton;
        private ArrowButton cheatButton;
        private string info;
        private int cheatCounter;
        public DevelopInfoCard()
            : base(".\\Sprites\\GUI\\backScreen")
        {
            backShape = new StaticObject(".\\Sprites\\GUI\\backPauseShape");
            info = "DEVELOPED BY JEFF RAMOS\nWWW.JEFFRAMOS.ME\nMUSIC BY KEVIN MACLEOD\n\nREGARDS TO:\nRICARDO RASERA\nENDEL DREYER\nDANIEL DIAS\nDIEGO DIAS";
            this.scale = new Vector2(1, 0.8f);

        }
        public override void init(ContentManager Content)
        {
            content = Content;
            base.init(Content);
            backShape.init(content);
            isActive = false;
            position.X = 800 / 2 - texture.Width / 2;
            position.Y = -texture.Height;

            backButton = new ArrowButton(hide);
            backButton.init(Content);
           
            cheatButton = new ArrowButton(cheat);
            cheatButton.init(Content);
            cheatButton.position.X = 750;
            cheatButton.position.Y = 430;

            ButtonModel defaultButtonModel = new ButtonModel(".\\Sprites\\GUI\\defaultButton\\shapeButton", ".\\Sprites\\GUI\\defaultButton\\shapeButtonXML", ButtonFactory.spriteFont);

            cheatCounter = 0;

            isActive = false;

        }
        public void cheat()
        {
            cheatCounter++;
            if (cheatCounter > 15)
            {
                cheatCounter = 0;
                GameModel.currentPoints += 5000;
            }
        }
        public void show()
        {

            isActive = true;
            tweener = new Tweener(-texture.Height, 480 / 2 - (texture.Height / 2 * scale.Y), 0.4f, Back.EaseOut);
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

                backButton.position = position + new Vector2(10 - 20, 10 - 20);
                backButton.updateButton(gameTime, touches);
                cheatButton.updateButton(gameTime, touches);

            }
        }
        public override void draw(SpriteBatch spriteBatch)
        {
            if (isActive)
            {
                backShape.draw(spriteBatch);
                base.draw(spriteBatch);
                spriteBatch.DrawString(ButtonFactory.spriteFont, info, position + new Vector2(42, 42), new Color(63, 53, 127), 0, new Vector2(), 0.45f, SpriteEffects.None, 0f);
                spriteBatch.DrawString(ButtonFactory.spriteFont, info, position + new Vector2(40, 40), Color.White, 0, new Vector2(), 0.45f, SpriteEffects.None, 0f);
                if (ButtonFactory.spriteFont != null && position != null)
                {
                    backButton.draw(spriteBatch);
                    //cheatButton.draw(spriteBatch);

                }
            }
        }

    }
}
