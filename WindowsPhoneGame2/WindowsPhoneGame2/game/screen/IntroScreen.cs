using System;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using WindowsPhoneGame2.game.entity;
using Microsoft.Xna.Framework.Input.Touch;
using XNATweener;
using WindowsPhoneGame2.game.utils;
using Microsoft.Xna.Framework;
using WindowsPhoneGame2.framework.GUI.button;
using WindowsPhoneGame2.game.factory.GUI;
using WindowsPhoneGame2.game.screen.screenElements;
using WindowsPhoneGame2.game.entity.rufus;
using WindowsPhoneGame2.game.entity.rufus.behaviours;

namespace WindowsPhoneGame2.game.screen
{
    class IntroScreen : AbstractScreen
    {
        private StaticObject background;
        private SimpleButton playButton;
        private StatisticsButton statisticsButton;
        private SimpleButton shopButton;
        private SimpleButton logoButton;
        private SoundButton soundButton;
        private InfoButton infoButton;
        private bool outt;
        private IncrementalLabel starsLabel;
        public IntroScreen(string screenName, SpriteBatch _screenBatch, ContentManager _content)
            : base(screenName)
        {
            screenBatch = _screenBatch;
            content = _content;
        }

        public override void build()
        {
            base.build();
            background = new StaticObject(".\\Sprites\\rufusConcept");
            background.init(content);
            addChild(background);

            outt = false;

            ButtonModel playModel = new ButtonModel(".\\Sprites\\GUI\\playButtonNew\\playButtonNew", ".\\Sprites\\GUI\\playButtonNew\\playButtonNewXML", null);


            soundButton = new SoundButton(null);
            soundButton.init(content);
            soundButton.position.X = 700f;
            soundButton.position.Y = 40f;

            infoButton = new InfoButton(Game1.showDevCard);
            infoButton.init(content);
            infoButton.position.X = 40f;
            infoButton.position.Y = 40f;

            playButton = new SimpleButton(playModel, "", new Point(), gotoGame);
            playButton.fontMargin = new Vector2(55, 15);
            playButton.fontScale = 1;
            playButton.init(content);
            playButton.position.Y = 480;
            playButton.position.X = 500;
            playButton.tweener = new Tweener(playButton.position.Y, 350f, TimeSpan.FromSeconds(1.0f), Back.EaseOut);
            playButton.tweener.Start();

            ButtonModel shopModel = new ButtonModel(".\\Sprites\\GUI\\shopButton\\shopButton", ".\\Sprites\\GUI\\shopButton\\shopButtonXML", null);
            shopButton = new SimpleButton(shopModel, "", new Point(), gotoShop);
            shopButton.fontMargin = new Vector2(55, 15);
            shopButton.fontScale = 1;
            shopButton.init(content);
            shopButton.position.Y = 480;
            shopButton.position.X = 148;
            shopButton.tweener = new Tweener(shopButton.position.Y, 350f, TimeSpan.FromSeconds(1.0f), Back.EaseOut);
            shopButton.tweener.Start();

            statisticsButton = new StatisticsButton(openStatistic);
            statisticsButton.init(content);
            statisticsButton.position.Y = 480;
            statisticsButton.position.X = 40;
            statisticsButton.tweener = new Tweener(statisticsButton.position.Y, 350f, TimeSpan.FromSeconds(1.0f), Back.EaseOut);
            statisticsButton.tweener.Start();

            ButtonModel logoModel = new ButtonModel(".\\Sprites\\GUI\\rufusLogo", "", null);
            logoButton = new SimpleButton(logoModel, "", new Point(), null);
            logoButton.init(content);
            logoButton.position.Y = -100;
            logoButton.position.X = 140;
            logoButton.tweener = new Tweener(logoButton.position.Y, 20f, TimeSpan.FromSeconds(1.0f), Back.EaseOut);
            logoButton.tweener.Start();

            starsLabel = new IncrementalLabel(0, ButtonFactory.spriteFont);
            starsLabel.init(content);
            starsLabel.newValue(GameModel.currentPoints);
            starsLabel.position.Y = shopButton.position.Y + 25;
            starsLabel.position.X = shopButton.position.X + 72;
            starsLabel.fontSize = 0.8f;

            addChild(playButton);
            addChild(shopButton);
            addChild(statisticsButton);
            addChild(logoButton);
            addChild(starsLabel);
            addChild(soundButton);
            addChild(infoButton);

        }

        private void openStatistic()
        {
            Game1.showStatistics();
        }
        public override void transitionOut(Action callback)
        {
            outt = true;
            outCallback = callback;

            float timeout = 0.4f;
            playButton.tweener = new Tweener(playButton.position.Y, 480f, TimeSpan.FromSeconds(timeout), Back.EaseIn);
            playButton.tweener.Start();

            shopButton.tweener = new Tweener(shopButton.position.Y, 480f, TimeSpan.FromSeconds(timeout), Back.EaseIn);
            shopButton.tweener.Start();

            statisticsButton.tweener = new Tweener(statisticsButton.position.Y, 480f, TimeSpan.FromSeconds(timeout), Back.EaseIn);
            statisticsButton.tweener.Start();

            
            logoButton.tweener = new Tweener(logoButton.position.Y, -100f, TimeSpan.FromSeconds(timeout), Back.EaseIn);
            logoButton.tweener.Start();

            soundButton.tweener = new Tweener(soundButton.position.Y, -100f, TimeSpan.FromSeconds(timeout), Back.EaseIn);
            soundButton.tweener.Start();

            infoButton.tweener = new Tweener(soundButton.position.Y, -100f, TimeSpan.FromSeconds(timeout), Back.EaseIn);
            infoButton.tweener.Start();
        }

        public override void destroy()
        {
            base.dispose();
            screenBatch.Dispose();
        }
        private void gotoShop()
        {
            screenManager.change(ScreenLabels.SHOP_SCREEN);
        }
        private void gotoGame()
        {
            screenManager.change(ScreenLabels.PRE_GAME_SCREEN);
        }
        public override void update(GameTime gameTime)
        {
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
            starsLabel.position.Y = shopButton.position.Y + 25;
            if (outt && playButton.position.Y > 450f)
                outCallback();

            updateChilds(gameTime);
        }

        public override void draw()
        {
            screenBatch.Begin();
            drawChilds(screenBatch);
            screenBatch.End();
        }
    }
}
