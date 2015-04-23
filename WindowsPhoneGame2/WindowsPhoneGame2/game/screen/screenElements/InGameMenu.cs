using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNATweener;
using WindowsPhoneGame2.game.entity;
using WindowsPhoneGame2.game.utils;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using WindowsPhoneGame2.game.factory.GUI;
using WindowsPhoneGame2.framework.GUI.button;
using Microsoft.Xna.Framework.Input.Touch;

namespace WindowsPhoneGame2.game.screen.screenElements
{
    class InGameMenu : StaticObject
    {
        private Tweener tweener;
        public bool showed;
        private Texture2D sideMenuBack;
        private Vector2 sideMenuPos;
        private Tweener tweenerSideMenu;
        private InGamePlayButton playButton;
        private SoundButton soundButton;
        private HomeButton homeButton;
        private BackButton backButton;
        private LinkedList<SimpleButton> buttonList;
        public InGameMenu()
            : base(".\\Sprites\\GUI\\backPauseShape")
        {
            showed = true;
            sideMenuPos = new Vector2(800, 0);
        }
        public void init(Microsoft.Xna.Framework.Content.ContentManager Content, Action inGameCallback, Action backToMenuCallback, Action reinitCallback)
        {
            base.init(Content);
            sideMenuBack = Content.Load<Texture2D>(".\\Sprites\\GUI\\backMenuShape");


            soundButton = new SoundButton(inGameCallback);
            soundButton.init(Content);

            homeButton = new HomeButton(backToMenuCallback);
            homeButton.init(Content);

            backButton = new BackButton(reinitCallback);
            backButton.init(Content);

            playButton = new InGamePlayButton(inGameCallback);
            playButton.init(Content);

            buttonList = new LinkedList<SimpleButton>();
            buttonList.AddLast(playButton);
            buttonList.AddLast(soundButton);
            buttonList.AddLast(homeButton);
            buttonList.AddLast(backButton);


            playButton.position.Y = 375f;
            playButton.position.X = 800f;

            soundButton.position.Y = 40f;
            soundButton.position.X = 815f;

            homeButton.position.Y = 175f;
            homeButton.position.X = 815f;

            backButton.position.Y = 255f;
            backButton.position.X = 815f;
        }
        public void hide()
        {
            tweener = new Tweener(alpha, 0f, 0.5f, Cubic.EaseOut);
            playButton.tweener = new Tweener(playButton.position.X, 800f, 0.5f, Cubic.EaseOut);
            soundButton.tweener = new Tweener(soundButton.position.X, 815f, 0.5f, Cubic.EaseOut);
            homeButton.tweener = new Tweener(homeButton.position.X, 815f, 0.5f, Cubic.EaseOut);
            backButton.tweener = new Tweener(backButton.position.X, 815f, 0.5f, Cubic.EaseOut);

            tweenerSideMenu = new Tweener(sideMenuPos.X, 800f, 0.5f, Cubic.EaseOut);

            showed = false;
        }
        public void show()
        {

            tweenerSideMenu = new Tweener(sideMenuPos.X, 650f, 0.5f, Cubic.EaseOut);
            tweener = new Tweener(0f, 1f, 0.5f, Cubic.EaseOut);


            playButton.tweener = new Tweener(playButton.position.X, 685f, 0.5f, Cubic.EaseOut);
            soundButton.tweener = new Tweener(soundButton.position.X, 700f, 0.5f, Cubic.EaseOut);
            homeButton.tweener = new Tweener(homeButton.position.X, 700f, 0.5f, Cubic.EaseOut);
            backButton.tweener = new Tweener(backButton.position.X, 700f, 0.5f, Cubic.EaseOut);

            showed = true;
        }
        public void update(Microsoft.Xna.Framework.GameTime gameTime, TouchCollection touches)
        {
            tweener.Update(gameTime);


            if (tweenerSideMenu != null)
            {
                tweenerSideMenu.Update(gameTime);
                sideMenuPos.X = tweenerSideMenu.Position;
            }

            if (buttonList != null)
            {
                for (int i = 0; i < buttonList.Count; i++)
                {
                    buttonList.ElementAt(i).updateButton(gameTime, touches);
                    if (buttonList.ElementAt(i).tweener != null)
                    {
                        buttonList.ElementAt(i).tweener.Update(gameTime);
                        buttonList.ElementAt(i).position.X = buttonList.ElementAt(i).tweener.Position;
                    }
                }
            }
            alpha = tweener.Position;

            base.update(gameTime);
        }
        public override void draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
           // if (alpha <= 0)
            //    showed = false;
            //if (showed)
            {
                base.draw(spriteBatch);
                spriteBatch.Draw(sideMenuBack, sideMenuPos, new Color(255, 255, 255, 255));
                if (buttonList != null)
                {
                    for (int i = 0; i < buttonList.Count; i++)
                    {
                        buttonList.ElementAt(i).draw(spriteBatch);
                       // spriteBatch.Draw(buttonList.ElementAt(i).texture, buttonList.ElementAt(i).position, new Color(255, 255, 255, 255));
                    }
                }
                
            }
        }

       
    }
}
