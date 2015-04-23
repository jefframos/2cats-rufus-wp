using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using WindowsPhoneGame2.game.entity;
using Microsoft.Xna.Framework.Input.Touch;
using XNATweener;
using WindowsPhoneGame2.game.utils;
using Microsoft.Xna.Framework;
using WindowsPhoneGame2.framework.GUI.button;
using WindowsPhoneGame2.game.factory.GUI;
using WindowsPhoneGame2.game.screen.shopPages;
using System.Collections.Generic;
using System.Linq;

namespace WindowsPhoneGame2.game.screen
{
    class ShopScreen : AbstractScreen
    {
        private HomeButton backButton;
        private bool outt;
        private WearPage wearPage;
        private ItemPage itemPage;
        private SimpleButton wearButton;
        private SimpleButton itemButton;
        private SimpleButton levelButton;
        private DefaultPage currentPage;
        private LevelPage levelPage;
        private StaticObject coins;
        private IncrementalLabel starsLabel;
        private LinkedList<DefaultPage> pages;

        public ShopScreen(string screenName, SpriteBatch _screenBatch, ContentManager _content)
            : base(screenName)
        {
            screenBatch = _screenBatch;
            content = _content;
        }

        public override void build()
        {
            base.build();

            outt = false;

            wearPage = new WearPage();
            addChild(wearPage);
            wearPage.init(content);
           

            itemPage = new ItemPage();
            addChild(itemPage);
            itemPage.init(content);

            levelPage = new LevelPage();
            addChild(levelPage);
            levelPage.init(content);

            pages = new LinkedList<DefaultPage>();
            pages.AddLast(wearPage);
            pages.AddLast(itemPage);
            pages.AddLast(levelPage);

            currentPage = wearPage;
            currentPage.show();

            ButtonModel defaultButtonModel = new ButtonModel(".\\Sprites\\GUI\\defaultButton\\shapeButton", ".\\Sprites\\GUI\\defaultButton\\shapeButtonXML", ButtonFactory.spriteFont);
            backButton = new HomeButton(gotoInit);
            backButton.init(content);
            backButton.position.Y = 20;
            backButton.position.X = -backButton.texture.Width - 5;
            backButton.tweener = new Tweener(-backButton.texture.Width - 5, 20f, TimeSpan.FromSeconds(.5f), Back.EaseOut);
            backButton.tweener.Start();

            wearButton = new SimpleButton(defaultButtonModel, "WEAR", new Point(205, 56), setWearPage);
            wearButton.init(content);
            wearButton.fontScale = .9f;
           // wearButton.fontColor = new Color(63, 53, 127);
            wearButton.fontMargin = new Vector2(55, 6);
            wearButton.position.Y = 85;
            wearButton.position.X = -wearButton.texture.Width - 5;
            wearButton.tweener = new Tweener(-wearButton.texture.Width - 5, 15f, TimeSpan.FromSeconds(.7f), Back.EaseOut);
            wearButton.tweener.Start();
            wearButton.isActive = true;
          
           // itemButton = new SimpleButton(new ButtonModel(".\\Sprites\\GUI\\defaultButton\\itemsButton", "", null), "", new Point(), setItemPage);
            itemButton = new SimpleButton(defaultButtonModel, "ITEM", new Point(205, 56), setItemPage);
            itemButton.init(content);
            itemButton.fontScale = .9f;
            //itemButton.fontColor = new Color(63,53,127);
            itemButton.fontMargin = new Vector2(62, 6);
            itemButton.position.Y = 150;
            itemButton.position.X = -itemButton.texture.Width - 5;
            itemButton.tweener = new Tweener(-itemButton.texture.Width - 5, 15f, TimeSpan.FromSeconds(.8f), Back.EaseOut);
            itemButton.tweener.Start();

            levelButton = new SimpleButton(defaultButtonModel, "LEVELS", new Point(205, 56), setLevelPage);
            levelButton.init(content);
            levelButton.fontScale = .9f;
            //levelButton.fontColor = new Color(63, 53, 127);
            levelButton.fontMargin = new Vector2(50, 6);
            levelButton.position.Y = 215;
            levelButton.position.X = -levelButton.texture.Width - 5;
            levelButton.tweener = new Tweener(-levelButton.texture.Width - 5, 15f, TimeSpan.FromSeconds(.9f), Back.EaseOut);
            levelButton.tweener.Start();

            coins = new StaticObject(".\\Sprites\\GUI\\shopScreen\\starContainer");
            coins.init(content);
            coins.position.Y = 405;
            coins.position.X = levelButton.position.X - 10;

            starsLabel = new IncrementalLabel(0, ButtonFactory.spriteFont);
            starsLabel.init(content);
            starsLabel.newValue(GameModel.currentPoints);
            starsLabel.position.Y = 415;
            starsLabel.position.X = coins.position.X + 60;

            addChild(wearButton);

            addChild(itemButton);
            addChild(levelButton);
            addChild(backButton);
            addChild(coins);
            addChild(starsLabel);
        }
        private void setLevelPage()
        {
            wearButton.isActive = false;
            itemButton.isActive = false;
            levelButton.isActive = true;
            if (currentPage is LevelPage)
                return;
            currentPage.hide(levelPage.show);
            currentPage = levelPage;
        }
        private void setWearPage()
        {
            wearButton.isActive = true;
            itemButton.isActive = false;
            levelButton.isActive = false;
            if (currentPage is WearPage)
                return;
            currentPage.hide(wearPage.show);
            currentPage = wearPage;
        }
        private void setItemPage()
        {
            wearButton.isActive = false;
            itemButton.isActive = true;
            levelButton.isActive = false;
            if (currentPage is ItemPage)
                return;
            currentPage.hide(itemPage.show);
            currentPage = itemPage;
        }
        private void gotoInit()
        {
            screenManager.change(ScreenLabels.INIT_SCREEN);
        }
        public override void transitionOut(Action callback)
        {
            outt = true;
            outCallback = callback;

            backButton.tweener = new Tweener(backButton.position.X, - backButton.texture.Width - 5,  TimeSpan.FromSeconds(.5f), Back.EaseOut);
            backButton.tweener.Start();

            wearButton.tweener = new Tweener(wearButton.position.X, -wearButton.texture.Width - 5,  TimeSpan.FromSeconds(.7f), Back.EaseOut);
            wearButton.tweener.Start();

            itemButton.tweener = new Tweener(itemButton.position.X, -itemButton.texture.Width - 5,  TimeSpan.FromSeconds(.8f), Back.EaseOut);
            itemButton.tweener.Start();

            levelButton.tweener = new Tweener(levelButton.position.X, -levelButton.texture.Width - 5,  TimeSpan.FromSeconds(.9f), Back.EaseOut);
            levelButton.tweener.Start();
        }

        public override void destroy()
        {
            base.dispose();
            screenBatch.Dispose();
        }
        private void gotoGame()
        {
            screenManager.change(ScreenLabels.GAME_SCREEN);
        }
        public override void update(GameTime gameTime)
        {
            base.update(gameTime);

            wearButton.tweener.Update(gameTime);
            wearButton.position.X = wearButton.tweener.Position;
            backButton.tweener.Update(gameTime);
            backButton.position.X = backButton.tweener.Position;
            itemButton.tweener.Update(gameTime);
            itemButton.position.X = itemButton.tweener.Position;
            levelButton.tweener.Update(gameTime);
            levelButton.position.X = levelButton.tweener.Position;
            coins.position.X = levelButton.tweener.Position - 10;
            starsLabel.position.X = coins.position.X + 60;
            starsLabel.newValue(GameModel.currentPoints);

            if (outt)
            {
                destroy();
                outCallback();
            }

            for (int i = 0; i < childs.Count; i++)
            {
                if (childs.ElementAt(i) == currentPage)
                {
                    childs.Remove(currentPage);
                    childs.AddLast(currentPage);
                }
            }

            for (int i = 0; i < pages.Count; i++)
            {
                pages.ElementAt(i).update(gameTime, touches);
            }
            updateChilds(gameTime);
        }

        public override void draw()
        {            
            screenBatch.Begin();
            screenBatch.GraphicsDevice.Clear(new Color(100,78,188));
            drawChilds(screenBatch);
            screenBatch.End();
        }
    }
}
