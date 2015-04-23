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
    class HelpCard : StaticObject
    {
        private ContentManager content;
        private StaticObject backShape;
        public bool isActive { get; set; }
        private Tweener tweener;
        private StaticObject mainImage;
        private ArrowButton backButton;

        public HelpCard()
            : base(".\\Sprites\\GUI\\helpscreen")
        {
            backShape = new StaticObject(".\\Sprites\\GUI\\backPauseShape");

           

        }
        public override void init(ContentManager Content)
        {
            content = Content;
            base.init(Content);
            backShape.init(content);
            isActive = false;
            position.X = 800 / 2 - texture.Width / 2;
            position.Y = -texture.Height;
            mainImage = new StaticObject(GameModel.rufusModels.ElementAt(0).largePath);
            mainImage.init(content);

            backButton = new ArrowButton(hide);
            backButton.init(Content);

            isActive = false;

        }
        public void show()
        {
          
            isActive = true;
            tweener = new Tweener(-texture.Height, 40f, 0.4f, Back.EaseOut);
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
                //backShape.alpha = tweener.Position / 100;
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

                backButton.draw(spriteBatch);
            }
        }
      
    }
}
