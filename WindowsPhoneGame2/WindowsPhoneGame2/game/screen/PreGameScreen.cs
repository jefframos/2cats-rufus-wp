using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using WindowsPhoneGame2.game.entity;
using XNATweener;
using Microsoft.Xna.Framework;
using WindowsPhoneGame2.framework.GUI.button;
using WindowsPhoneGame2.game.factory.GUI;
using WindowsPhoneGame2.game.level;
using System;
using System.Collections.Generic;
using System.Linq;
using WindowsPhoneGame2.framework.primitives;
using WindowsPhoneGame2.game.screen.screenElements;
using WindowsPhoneGame2.game.level.behaviours;
using WindowsPhoneGame2.game.entity.rufus;
using WindowsPhoneGame2.game.utils;
using WindowsPhoneGame2.game.screen.shopPages;

namespace WindowsPhoneGame2.game.screen
{
    class PreGameScreen : AbstractScreen
    {
        private BackLevelView background;
        private SimpleButton playButton;
        private SimpleButton locationButton;
        private SimpleButton backRufusBox;
        private StaticObject rufusImage;
        private HomeButton backButton;
        private ArrowButton changeLevel;
        private ArrowButton changeLevelBack;
        private LevelModel currentLevelModel;
        private HelpButton helpButton;
        private SoundButton soundButton;
        private StaticObject loadScreen;
        private bool outt;
        private bool ready;
        public PreGameScreen(string screenName, SpriteBatch _screenBatch, ContentManager _content)
            : base(screenName)
        {
            screenBatch = _screenBatch;
            content = _content;
            loadScreen = new StaticObject(".\\Sprites\\GUI\\loadScreen");
        }

        public override void build()
        {
            base.build();

            loadScreen.init(content);
            //SALVAR O CENARIO ATUAL
            RufusModel temp = GameModel.levelsModels.ElementAt(GameModel.currentLevelID);
            LevelBehaviour temp2 = (LevelBehaviour)(temp.behaviour);
            currentLevelModel = temp2.levelModel;

            background = new BackLevelView(currentLevelModel.bgPath);
            if (currentLevelModel.clouds != null)
                background.addClouds(currentLevelModel.clouds);
            background.setColor(currentLevelModel.color);

            background.init(content);
            addChild(background);
            background.start();
            outt = false;

            ButtonModel playModel = new ButtonModel(".\\Sprites\\GUI\\readyButton\\readyButton", ".\\Sprites\\GUI\\readyButton\\readyButtonXML", null);

            playButton = new SimpleButton(playModel, "", new Point(), gotoGame);
            playButton.init(content);
            playButton.position.Y = 570;
            playButton.position.X = 400 - playButton.texture.Width / 2;
            playButton.tweener = new Tweener(playButton.position.Y, 350f, TimeSpan.FromSeconds(1.0f), Back.EaseOut);
            playButton.tweener.Start();

            locationButton = new SimpleButton(new ButtonModel(".\\Sprites\\GUI\\locationButton\\locationButton", ".\\Sprites\\GUI\\locationButton\\locationButtonXML", null), "", new Point(), changeLevelModel);
            locationButton.init(content);
            locationButton.position.Y = 480;
            locationButton.position.X = 400 - locationButton.texture.Width / 2;
            locationButton.tweener = new Tweener(locationButton.position.Y, 260f, TimeSpan.FromSeconds(1.0f), Back.EaseOut);
            locationButton.tweener.Start();

            backButton = new HomeButton(gotoInit);
            backButton.init(content);
            backButton.position.Y = 40;
            backButton.position.X = 40;
           // backButton.tweener = new Tweener(backButton.position.Y, 40f, TimeSpan.FromSeconds(1.0f), Back.EaseOut);
           // backButton.tweener.Start();

            changeLevel = new ArrowButton(changeRufus);
            changeLevel.init(content);
            changeLevel.position.Y = -40;
            changeLevel.position.X = 490;
            changeLevel.spriteEffects = SpriteEffects.FlipHorizontally;
            changeLevel.scale = new Vector2(.8f);
            changeLevel.tweener = new Tweener(changeLevel.position.Y, 120f, TimeSpan.FromSeconds(1.0f), Cubic.EaseOut);
            changeLevel.tweener.Start();

            changeLevelBack = new ArrowButton(changeRufusBack);
            changeLevelBack.init(content);
            changeLevelBack.position.Y = -40;
            changeLevelBack.position.X = 268;
            changeLevelBack.spriteEffects = SpriteEffects.None;
            changeLevelBack.scale = new Vector2(.8f);
            changeLevelBack.tweener = new Tweener(changeLevel.position.Y, 120f, TimeSpan.FromSeconds(1.0f), Cubic.EaseOut);
            changeLevelBack.tweener.Start();

            backRufusBox = new SimpleButton(new ButtonModel(".\\Sprites\\GUI\\backChoiceRufus", "", null), "", new Point(), openCard);
            backRufusBox.init(content);
            backRufusBox.position.Y = -backRufusBox.texture.Height;
            backRufusBox.position.X = 400 - backRufusBox.texture.Width / 2;
            backRufusBox.tweener = new Tweener(backRufusBox.position.Y, 40f, TimeSpan.FromSeconds(1.0f), Cubic.EaseOut);
            backRufusBox.tweener.Start();


            rufusImage = new StaticObject(GameModel.rufusModels.ElementAt(GameModel.currentRufusID).largePath);
            rufusImage.init(content);
            rufusImage.position.Y = backRufusBox.position.Y + rufusImage.texture.Height / 2 + backRufusBox.texture.Height / 2;
            rufusImage.position.X = 400 - rufusImage.texture.Width / 2;

            soundButton = new SoundButton(null);
            soundButton.init(content);
            soundButton.position.X = 700f;
            soundButton.position.Y = 40f;

            helpButton = new HelpButton(null);
            helpButton.init(content);
            helpButton.position.X = 600f;
            helpButton.position.Y = 40f;

            addChild(backRufusBox);
            addChild(playButton);
            addChild(changeLevel);
            addChild(changeLevelBack);
            addChild(locationButton);
            addChild(backButton);
            addChild(rufusImage);
            addChild(soundButton);
            addChild(helpButton);
        }

        private void openCard()
        {
            Game1.showCard(GameModel.rufusModels.ElementAt(GameModel.currentRufusID), ItemBoxModel.WEARS_ID);
        }
        private void changeRufusBack()
        {
            GameModel.getBackActive();
            reloadRufus();
        }

        private void changeRufus()
        {
            GameModel.getNextActive();
            reloadRufus();
        }
        private void reloadRufus()
        {
            rufusImage.reload(GameModel.rufusModels.ElementAt(GameModel.currentRufusID).largePath);
            rufusImage.init(content);
        }
        private void changeLevelModel()
        {
            string tempPath = currentLevelModel.bgPath;
            RufusModel temp = GameModel.levelsModels.ElementAt(GameModel.getNextLevelActive());
            LevelBehaviour temp2 = (LevelBehaviour)(temp.behaviour);
            currentLevelModel = temp2.levelModel;
            if (currentLevelModel.bgPath != tempPath)
                changeLevelGraphics();
        }

        private void changeLevelGraphics()
        {
            background.reload(currentLevelModel.bgPath);
            if (currentLevelModel.clouds != null)
                background.addClouds(currentLevelModel.clouds);
            background.setColor(currentLevelModel.color);
            background.init(content);
            background.start();
        }
        public override void transitionOut(Action callback)
        {
            outt = true;
            outCallback = callback;

            float timeout = 0.4f;
            playButton.tweener = new Tweener(playButton.position.Y, 570f, TimeSpan.FromSeconds(timeout), Back.EaseIn);
            playButton.tweener.Start();

            backButton.tweener = new Tweener(backButton.position.Y, -40f, TimeSpan.FromSeconds(timeout), Back.EaseIn);
            backButton.tweener.Start();

            locationButton.tweener = new Tweener(locationButton.position.Y, 480f, TimeSpan.FromSeconds(timeout), Back.EaseIn);
            locationButton.tweener.Start();

            changeLevel.tweener = new Tweener(changeLevel.position.Y, -40, TimeSpan.FromSeconds(timeout), Back.EaseIn);
            changeLevel.tweener.Start();

            changeLevelBack.tweener = new Tweener(changeLevelBack.position.Y, -40, TimeSpan.FromSeconds(timeout), Back.EaseIn);
            changeLevelBack.tweener.Start();

            backRufusBox.tweener = new Tweener(backRufusBox.position.Y, -backRufusBox.texture.Height, TimeSpan.FromSeconds(timeout), Back.EaseIn);
            backRufusBox.tweener.Start();

            soundButton.tweener = new Tweener(soundButton.position.Y, -100f, TimeSpan.FromSeconds(timeout), Back.EaseIn);
            soundButton.tweener.Start();

            helpButton.tweener = new Tweener(helpButton.position.Y, -100f, TimeSpan.FromSeconds(timeout), Back.EaseIn);
            helpButton.tweener.Start();
            
        }

        public override void destroy()
        {
            base.dispose();
            screenBatch.Dispose();
        }
        private void gotoInit()
        {
            screenManager.change(ScreenLabels.INIT_SCREEN);
        }
        private void gotoGame()
        {
            ready = true;
            screenManager.change(ScreenLabels.GAME_SCREEN);
        }
        public override void update(GameTime gameTime)
        {
            rufusImage.position.Y = backRufusBox.position.Y - rufusImage.texture.Height / 2 + backRufusBox.texture.Height / 2;
            rufusImage.position.X = 400 - rufusImage.texture.Width / 2;
            base.update(gameTime);

            for (int i = 0; i < childs.Count; i++)
            {
                if (childs.ElementAt(i) is SimpleButton)
                {
                    if ((childs.ElementAt(i) as SimpleButton).tweener != null)
                    {
                        (childs.ElementAt(i) as SimpleButton).tweener.Update(gameTime);
                        (childs.ElementAt(i) as SimpleButton).position.Y = (float)(childs.ElementAt(i) as SimpleButton).tweener.Position;
                    }
                }
            }
            //rufusImage.position.Y = backRufusBox.position.Y + rufusImage.texture.Height / 2 + backRufusBox.texture.Height / 2;
            // rufusImage.position.X = 400 - rufusImage.texture.Width / 2;
            /**
             * condição para trocar de tela, meio gambs isso hein jeff, mas né =P
             */
            if (outt && playButton.position.Y > 450f)
            {
                destroy();
                outCallback();
            }

            updateChilds(gameTime);
        }

        public override void draw()
        {
            screenBatch.Begin();
            if (outt && ready)
                loadScreen.draw(screenBatch);
            else drawChilds(screenBatch);
           
            screenBatch.End();
        }


    }
}
