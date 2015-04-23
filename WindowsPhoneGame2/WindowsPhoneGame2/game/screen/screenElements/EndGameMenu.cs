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
using System.IO;
using System.IO.IsolatedStorage;
using RufusAndTheMagicMushrooms.framework.storage;
using RufusAndTheMagicMushrooms.game;

namespace WindowsPhoneGame2.game.screen.screenElements
{
    class EndGameMenu : StaticObject
    {
        private Tweener tweener;
        public bool showed;
        private Texture2D sideMenuBack;
        private Vector2 sideMenuPos;
        private Tweener tweenerSideMenu;
        private SoundButton soundButton;
        private HomeButton homeButton;
        private InGamePlayButton playButton;
        private ArrowButton backButton;
        private SimpleButton shopButton;
        private LinkedList<SimpleButton> buttonList;
        private IncrementalLabel levelScore;
        private IncrementalLabel totalScore;
        private IncrementalLabel maxMush;
        private IncrementalLabel maxBoobs;
        private IncrementalLabel maxSpecial;
        private IncrementalLabel maxCombo;
        private IncrementalLabel maxJump;

        public EndGameMenu()
            : base(".\\Sprites\\GUI\\backPauseShape")
        {
            showed = true;
            sideMenuPos = new Vector2(225, 0);
        }
        public void init(Microsoft.Xna.Framework.Content.ContentManager Content, Action inGameCallback, Action backToMenuCallback, Action reinitCallback, Action shopCallback, Action backToPreGameCallback)
        {
            base.init(Content);
            sideMenuBack = Content.Load<Texture2D>(".\\Sprites\\GUI\\backEndGame");


            soundButton = new SoundButton(inGameCallback);
            soundButton.init(Content);

            homeButton = new HomeButton(backToMenuCallback);
            homeButton.init(Content);

            backButton = new ArrowButton(backToPreGameCallback);
            backButton.init(Content);

            playButton = new InGamePlayButton(reinitCallback);
            playButton.init(Content);

            shopButton = new SimpleButton(new ButtonModel(".\\Sprites\\GUI\\shopButton\\shopButton", ".\\Sprites\\GUI\\shopButton\\shopButtonXML", null), "", new Point(), shopCallback);
            shopButton.init(Content);
            shopButton.position.Y = 480;
            shopButton.position.X = 400 - shopButton.texture.Width / 2;
            shopButton.tweener = new Tweener(shopButton.position.Y, 270f, TimeSpan.FromSeconds(1.0f), Back.EaseOut);
            shopButton.tweener.Start();


            buttonList = new LinkedList<SimpleButton>();
            buttonList.AddLast(soundButton);
            buttonList.AddLast(homeButton);
            buttonList.AddLast(backButton);
            buttonList.AddLast(playButton);
            buttonList.AddLast(shopButton);

            levelScore = new IncrementalLabel(0, ButtonFactory.spriteFont);
            levelScore.init(Content);
            levelScore.fontSize = 1.2f;
            levelScore.mask = true;

            totalScore = new IncrementalLabel(GameModel.currentPoints, ButtonFactory.spriteFont);
            totalScore.init(Content);
            totalScore.fontSize = 0.8f;

            maxMush = new IncrementalLabel(0, ButtonFactory.spriteFont);
            maxMush.init(Content);
            maxMush.fontSize = 0.6f;
            //maxMush.mask = true;

            maxSpecial = new IncrementalLabel(0, ButtonFactory.spriteFont);
            maxSpecial.init(Content);
            maxSpecial.fontSize = 0.6f;
            // maxSpecial.mask = true;

            maxBoobs = new IncrementalLabel(0, ButtonFactory.spriteFont);
            maxBoobs.init(Content);
            maxBoobs.fontSize = 0.6f;
            //maxBoobs.mask = true;

            maxCombo = new IncrementalLabel(0, ButtonFactory.spriteFont);
            maxCombo.init(Content);
            maxCombo.fontSize = 0.6f;
            //maxCombo.mask = true;

            maxJump = new IncrementalLabel(0, ButtonFactory.spriteFont);
            maxJump.init(Content);
            maxJump.fontSize = 0.6f;
            //maxJump.mask = true;


            soundButton.position.X = 700f;

            homeButton.position.X = 515f;
           

            playButton.position.X = sideMenuPos.X + sideMenuBack.Width / 2 - playButton.texture.Width / 4;
            playButton.position.Y = -100;

            backButton.position.X = sideMenuPos.X + 10f;

            sideMenuPos.Y = -480f;
            soundButton.position.Y = -140f;

        }
        public void hide()
        {
            tweener = new Tweener(alpha, 0f, 0.5f, Cubic.EaseOut);
            soundButton.tweener = new Tweener(soundButton.position.Y, -140f, 0.5f, Back.EaseIn);
            showed = false;
        }
        public void show(int levelPoints, int _totMush, int _totBoobs, int _totSpecials, int _maxCombo, int _maxJump)
        {
            if(_maxCombo > Statistics.maxCombo)
                Statistics.maxCombo = _maxCombo;
             if(_maxJump > Statistics.maxJumps)
                Statistics.maxJumps = _maxJump;
             if(levelPoints > Statistics.maxPoints)
                Statistics.maxPoints = levelPoints;

            GameModel.currentPoints += levelPoints;

            if (GameModel.currentPoints > 9999999)
                GameModel.currentPoints = 9999999;
            //using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            //{
            //    if (!store.FileExists("InTheRoot.txt"))
            //    {
            //        IsolatedStorageFileStream rootFile = store.CreateFile("InTheRoot.txt");
            //        rootFile.Close();
            //    }
            //    using (StreamWriter sw =
            //                       new StreamWriter(store.OpenFile("InTheRoot.txt",
            //                           FileMode.Open, FileAccess.Write)))
            //    {
            //        sw.WriteLine(GameModel.currentPoints.ToString() + ",22222");
            //        sw.Flush();
            //        sw.Close();
            //    }
            //}

            if (_maxCombo <= 1)
                _maxCombo = 0;
            if (_maxJump <= 1)
                _maxJump = 0;
            levelScore.newValue(levelPoints, 20);
           
            totalScore.newValue(GameModel.currentPoints, 50);
            maxMush.newValue(_totMush, 30);
            maxBoobs.newValue(_totBoobs, 40);
            maxSpecial.newValue(_totSpecials, 50);
            Trace.write(_maxCombo.ToString());

            maxCombo.newValue(_maxCombo, 60);
            maxJump.newValue(_maxJump, 70);
            tweenerSideMenu = new Tweener(sideMenuPos.Y, 25f, 1f, Back.EaseOut);
            soundButton.tweener = new Tweener(soundButton.position.Y, 40f, 0.5f, Back.EaseOut);
            tweener = new Tweener(0f, 1f, 0.5f, Cubic.EaseOut);
            showed = true;

            StorageManager.save("PATH", null);
        }
        public void update(Microsoft.Xna.Framework.GameTime gameTime, TouchCollection touches)
        {
            tweener.Update(gameTime);
            levelScore.update(gameTime);
            totalScore.update(gameTime);
            maxMush.update(gameTime);
            maxBoobs.update(gameTime);
            maxSpecial.update(gameTime);
            maxCombo.update(gameTime);
            maxJump.update(gameTime);

            if (tweenerSideMenu != null)
            {
                tweenerSideMenu.Update(gameTime);
                sideMenuPos.Y = (int)tweenerSideMenu.Position;
            }
            if (buttonList != null)
            {
                for (int i = 0; i < buttonList.Count; i++)
                {
                    buttonList.ElementAt(i).updateButton(gameTime, touches);
                    if (buttonList.ElementAt(i).tweener != null)
                    {
                        buttonList.ElementAt(i).tweener.Update(gameTime);
                        buttonList.ElementAt(i).position.Y = buttonList.ElementAt(i).tweener.Position;
                    }
                }
            }

            homeButton.position.Y = sideMenuPos.Y + 10;
            backButton.position.Y = sideMenuPos.Y + 10;

            playButton.position.Y = sideMenuPos.Y + sideMenuBack.Height - playButton.texture.Height / 2;
            playButton.position.X = sideMenuPos.X + sideMenuBack.Width / 2 - playButton.texture.Width / 4;

            shopButton.position.Y = sideMenuPos.Y + 275;
            levelScore.position.X = sideMenuPos.X + 120;
            levelScore.position.Y = sideMenuPos.Y + 60;

            totalScore.position.X = shopButton.position.X + 67;
            totalScore.position.Y = shopButton.position.Y + 25;

            maxMush.position.X = 460;
            maxMush.position.Y = sideMenuPos.Y + 118;

            maxBoobs.position.X = 460;
            maxBoobs.position.Y = maxMush.position.Y + 30;

            maxSpecial.position.X = 460;
            maxSpecial.position.Y = maxBoobs.position.Y + 30;

            maxCombo.position.X = 460;
            maxCombo.position.Y = maxSpecial.position.Y + 30;

            maxJump.position.X = 460;
            maxJump.position.Y = maxCombo.position.Y + 30;

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
                        //spriteBatch.Draw(buttonList.ElementAt(i).texture, buttonList.ElementAt(i).position, new Color(255, 255, 255, 255));
                    }
                }
                levelScore.draw(spriteBatch);
                totalScore.draw(spriteBatch);
                maxMush.draw(spriteBatch);
                maxBoobs.draw(spriteBatch);
                maxSpecial.draw(spriteBatch);
                maxCombo.draw(spriteBatch);
                maxJump.draw(spriteBatch);
            }
        }


    }
}
