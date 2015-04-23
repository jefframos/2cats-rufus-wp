using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using WindowsPhoneGame2.framework.layer;
using WindowsPhoneGame2.game.layer;
using WindowsPhoneGame2.game.entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using WindowsPhoneGame2.game.factory.GUI;
using WindowsPhoneGame2.framework.GUI.button;
using WindowsPhoneGame2.game.utils;
using System;
using WindowsPhoneGame2.game.screen.screenElements;
using System.Collections.Generic;
using System.Linq;
using WindowsPhoneGame2.game.FX;
using WindowsPhoneGame2.game.entity.rufus.behaviours;
using WindowsPhoneGame2.game.level;
using WindowsPhoneGame2.game.level.behaviours;

namespace WindowsPhoneGame2.game.screen
{
    class GameScreen : AbstractScreen
    {
        private LayerManager layerManager;
        private Layer entityLayer;
        private static Layer FXLayer;
        private static Layer BackEntityFXLayer;
        private StaticLayer environment;
        private GameLoop gameLoop;
        private Rufus rufus;
        private PauseButton pauseButton;
        private bool pause;
        private BaseHUD baseHUD;
        private InGameMenu inGameMenu;
        private EndGameMenu endGameMenu;
        private int totMush;
        private LinkedList<Clouds> clouds;
        public Color backColor { get; set; }
        private LevelModel levelModel;
        private ComboBar comboBar;
        //private StaticObject loadScreen;
        private float loadAlpha;
        public GameScreen(string screenName, SpriteBatch _screenBatch, ContentManager _content)
            : base(screenName)
        {
            screenBatch = _screenBatch;
            content = _content;
            //loadScreen = new StaticObject(".\\Sprites\\GUI\\loadScreen");
        }
        public static Layer getFXLayer()
        {
            return FXLayer;
        }
        public static Layer getBackEntityFXLayer()
        {
            return BackEntityFXLayer;
        }
        public override void build()
        {
            base.build();
            layerManager = new LayerManager();
            //loadAlpha = 1;
            levelModel = (GameModel.levelsModels.ElementAt(GameModel.currentLevelID).behaviour as LevelBehaviour).levelModel;
            //loadScreen.init(content);
            backColor = levelModel.color;
            clouds = new LinkedList<Clouds>();
            if (levelModel.clouds != null)
                for (int i = 0; i < levelModel.clouds.Count; i++)
                {
                    Clouds tempCloud = new Clouds();
                    tempCloud.texture = content.Load<Texture2D>(levelModel.clouds.ElementAt(i));
                    Vector2 temp = new Vector2((i + 1) % 2 == 0 ? -100 : 600, (float)new Random().Next(0, 50));
                    tempCloud.position = temp;
                    clouds.AddLast(tempCloud);
                }


            /*Cria as layers*/
            environment = new StaticLayer("ENVIRONMENT");


            for (int i = 0; i < levelModel.levelLayers.Count; i++)
            {
                environment.add(new StaticObject(levelModel.levelLayers.ElementAt(i)));
            }

            FXLayer = new Layer("FX");
            BackEntityFXLayer = new Layer("BACK ENTITY FX");
            entityLayer = new Layer("ENTITY LAYER");
            rufus = new Rufus(400 - 30);
            rufus.position.Y = 200;
            rufus.dieCallback = endGame;
            entityLayer.add(rufus);



            /*Adiciona as layers*/
            layerManager.add(environment);
            layerManager.add(BackEntityFXLayer);
            layerManager.add(entityLayer);
            layerManager.add(FXLayer);
            layerManager.init(content);

            baseHUD = new BaseHUD();
            baseHUD.init(content);
            baseHUD.position.X = 6;
            baseHUD.position.Y = 444;
            baseHUD.start();
            addChild(baseHUD);


            pauseButton = new PauseButton(pauseGame);
            pauseButton.init(content);
            pauseButton.position.X = 732;
            pauseButton.position.Y = 410;
            pauseButton.scale = new Vector2(1f);
            addChild(pauseButton);

            gameLoop = new GameLoop(content, rufus);
            gameLoop.beginGame(entityLayer);

            pause = false;

            comboBar = new ComboBar(rufus.setOnInvencible, rufus.setOffInvencible);
            comboBar.init(content);
            comboBar.updateScale(0);

            addChild(comboBar);

            inGameMenu = new InGameMenu();
            inGameMenu.init(content, menuPlayCallback, backToMenu, reinitGame);
            inGameMenu.hide();
            addChild(inGameMenu);

            endGameMenu = new EndGameMenu();
            endGameMenu.init(content, menuPlayCallback, backToMenu, reinitGame, goShop, goPreGame);
            endGameMenu.hide();
            addChild(endGameMenu);

            totMush = 0;

            rufus.collideCallback = collideCallback;


            //endGame();

        }
        public void goPreGame()
        {
            screenManager.change(ScreenLabels.PRE_GAME_SCREEN);
            destroy();
        }
        public void goShop()
        {
            screenManager.change(ScreenLabels.SHOP_SCREEN);
            destroy();
        }
        public void collideCallback()
        {
            totMush = rufus.levelPoints;
            baseHUD.updateMushroomLabel(totMush);
            //comboBar.updateScale((float)(rufus.mushComboCounter / rufus.maxMushComboCounter));
        }
        public void menuPlayCallback()
        {
            pause = false;
            inGameMenu.hide();
        }
        public void endGame()
        {
            if (!endGameMenu.showed)
            {
                pause = true;// !pause;
                pauseButton.position.X = 800;
                endGameMenu.show(totMush, rufus.mushrooms, rufus.bloobs, rufus.specials, rufus.maxCombo, rufus.maxJumps);
            }
        }
        public void pauseGame()
        {
            if (!inGameMenu.showed)
            {
                pause = true;// !pause;
                inGameMenu.show();
            }
        }
        public void reinitGame()
        {
            screenManager.change(ScreenLabels.GAME_SCREEN);
        }
        public void backToMenu()
        {
            screenManager.change(ScreenLabels.INIT_SCREEN);
            destroy();
        }
        public override void transitionIn()
        {
            base.transitionIn();
        }
        public override void transitionOut(Action callback)
        {
            destroy();
            base.transitionOut(callback);
        }
        public override void destroy()
        {
            base.dispose();
            screenBatch.Dispose();
        }

        public override void update(GameTime gameTime)
        {
            //if (loadAlpha > 0.1)
            //{
            //    loadAlpha -= 0.05f;
            //    loadScreen.alpha = loadAlpha;
            //}
            //else loadScreen.alpha = 0;


            comboBar.updateScale((float)rufus.special / (float)rufus.maxSpecial);
            for (int i = 0; i < clouds.Count; i++)
            {
                clouds.ElementAt(i).position.X += (i + 1) % 2 == 0 ? 0.2f : -0.2f;// = new Vector2(, (float)new Random().Next(0, 100));

                if (clouds.ElementAt(i).position.X < (float)-clouds.ElementAt(i).texture.Width)
                    clouds.ElementAt(i).position.X = 800;

                if (clouds.ElementAt(i).position.X > 800f)
                    clouds.ElementAt(i).position.X = -clouds.ElementAt(i).texture.Width;
            }

            base.update(gameTime);
            updateChilds(gameTime);

            if (!pause)
            {
                rufus.touches = touches;
                // updateChilds(gameTime);
                layerManager.update(gameTime);
                gameLoop.update(gameTime);
                comboBar.update(gameTime, touches);

            }
            else
            {
                // inGameMenu.update(gameTime);
            }
            inGameMenu.update(gameTime, touches);
            endGameMenu.update(gameTime, touches);

        }

        internal void setColor(Color color)
        {
            backColor = color;
        }

        public override void draw()
        {

            screenBatch.Begin();
            screenBatch.GraphicsDevice.Clear(backColor);
            if (clouds != null)
            {
                for (int i = 0; i < clouds.Count; i++)
                {
                    screenBatch.Draw(clouds.ElementAt(i).texture, clouds.ElementAt(i).position, Color.White);
                }
            }
            layerManager.draw(screenBatch);
            drawChilds(screenBatch);
            //if (loadScreen.alpha > 0)
            //{
            //    loadScreen.draw(screenBatch);
            //}
            screenBatch.End();
        }


    }
}
