using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using WindowsPhoneGame2.game.entity;
using WindowsPhoneGame2.game.layer;
using WindowsPhoneGame2.game.screen;
using WindowsPhoneGame2.game.factory.GUI;
using WindowsPhoneGame2.game;
using WindowsPhoneGame2.game.entity.rufus;
using WindowsPhoneGame2.game.entity.rufus.behaviours;
using WindowsPhoneGame2.game.screen.screenElements;
using WindowsPhoneGame2.game.entity.objects.behaviours;
using WindowsPhoneGame2.game.level.behaviours;
using System.Text;
using XNATweener;
using WindowsPhoneGame2.game.utils;
using System.IO.IsolatedStorage;
using System.IO;
using RufusAndTheMagicMushrooms.framework.storage;
using RufusAndTheMagicMushrooms.game;

//using Windows.Storage;

namespace WindowsPhoneGame2
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch _spriteBatch;
        SpriteBatch _spriteBatchIntro;
        SpriteBatch _spriteBatchPreGame;
        SpriteBatch _spriteBatchShop;
        SpriteBatch _spriteBatchCard;
        ScreenManager screenManager;
        private static InfoCard infoCard;
        private static StatisticsCard statisticCard;
        private static HelpCard helpCard;
        private static DevelopInfoCard devInfoCard;
        private static ContentManager content;
        // private SoundEffect ambientSound;
        private static Song ambientSound;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = true;
            graphics.SupportedOrientations = DisplayOrientation.LandscapeRight;
            Content.RootDirectory = "Content";

            Statistics.maxMushrooms = 0;
            Statistics.maxBloobs = 0;
            Statistics.maxSpecials = 0;
            Statistics.maxCombo = 0;
            Statistics.maxJumps = 0;
            Statistics.maxPoints = 0;


            infoCard = new InfoCard();
            statisticCard = new StatisticsCard();
            devInfoCard = new DevelopInfoCard();
            helpCard = new HelpCard();
            // var wbt = new WebBrowserTask();
            // wbt.URL = "http://stackoverflow.com/";
            // wbt.Show();
            // Facebook.HttpMethod
            // Facebook.FacebookClient face = new Facebook.FacebookClient();
            // face.
        }

        private void loadStorage()
        {
            try
            {
                using (var store = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    if (!store.FileExists("InTheRoot.txt"))
                    {
                        IsolatedStorageFileStream rootFile = store.CreateFile("InTheRoot.txt");
                        rootFile.Close();
                        GameModel.currentPoints = 0;
                        StorageManager.save("", null);
                    }
                    else
                    {
                        // StorageManager.save("", null);
                    }
                    if (store.FileExists("InTheRoot.txt"))
                    {
                        try
                        {
                            using (StreamReader srFile =
                               new StreamReader(store.OpenFile("InTheRoot.txt",
                                   FileMode.Open, FileAccess.Read)))
                            {
                                srFile.BaseStream.Seek(0, SeekOrigin.Begin);
                                byte[] bBuffer = new Byte[768];
                                srFile.BaseStream.Read(bBuffer, 0, bBuffer.Length);
                                UTF8Encoding enc = new UTF8Encoding();
                                string teste = enc.GetString(bBuffer, 0, bBuffer.Length);

                                string[] words = teste.Split(',');

                                //SE ISSO AQUI FOR MENOR OU IGUAL A 1, É POR QUE O ARQUIVO NAO EXISTE AINDA
                                if (words.Length > 1)
                                {
                                    GameModel.currentPoints = Convert.ToInt32(words[0]);

                                    if (GameModel.currentPoints > 9999999)
                                        GameModel.currentPoints = 9999999;

                                    if (words[4].Length > GameModel.rufusModels.Count ||
                                        words[5].Length > GameModel.itensModels.Count ||
                                        words[6].Length > GameModel.levelsModels.Count ||
                                        words[10] != null
                                        //words.Length < 15
                                        ) { StorageManager.save("", null); }

                                    for (int i = 0; i < words[4].Length; i++)
                                    {
                                        if (i < GameModel.rufusModels.Count)
                                            GameModel.rufusModels.ElementAt(i).isActive = words[4][i] == '1' ? true : false;
                                    }

                                    for (int i = 0; i < words[5].Length; i++)
                                    {
                                        if (i < GameModel.itensModels.Count)
                                            GameModel.itensModels.ElementAt(i).isActive = words[5][i] == '1' ? true : false;
                                    }

                                    for (int i = 0; i < words[6].Length; i++)
                                    {
                                        if (i < GameModel.levelsModels.Count)
                                            GameModel.levelsModels.ElementAt(i).isActive = words[6][i] == '1' ? true : false;
                                    }

                                    Trace.write(words[10].ToString());
                                    if (words[10].ToString() == MediaState.Paused.ToString())
                                        MediaPlayer.Pause();
                                    else
                                        MediaPlayer.Play(ambientSound);
                                    Statistics.maxMushrooms = Convert.ToInt32(words[11]);
                                    Statistics.maxBloobs = Convert.ToInt32(words[12]);
                                    Statistics.maxSpecials = Convert.ToInt32(words[13]);
                                    Statistics.maxCombo = Convert.ToInt32(words[14]);
                                    Statistics.maxJumps = Convert.ToInt32(words[15]);
                                    Statistics.maxPoints = Convert.ToInt32(words[16]);
                                }
                                else
                                {
                                    GameModel.currentPoints = 0;
                                }
                            }
                        }
                        catch (IsolatedStorageException ex)
                        {
                            Trace.write("store Messag" + ex.Message.ToString());
                        }
                    }

                }
            }
            catch (IsolatedStorageException ex)
            {
                Trace.write("store error" + ex.ToString());
            }
        }
        protected override void Initialize()
        {
            //Trace.write(Statistics.toString());
            ambientSound = Content.Load<Song>(".\\sounds\\FluffingaDuck");

            MediaPlayer.Play(ambientSound);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.5f;
            bool freegame = false;
            if (!freegame)
            {
                GameModel.rufusModels = new LinkedList<RufusModel>();
                GameModel.rufusModels.AddLast(new RufusModel(".\\Sprites\\GUI\\shopScreen\\normalrufus", ".\\Sprites\\GUI\\shopScreen\\rufusLarge", true, new NormalBehaviour(), 0, "RUFUS", "I'M RUFUS, =D"));
                GameModel.rufusModels.AddLast(new RufusModel(".\\Sprites\\GUI\\shopScreen\\fatRufus", ".\\Sprites\\GUI\\shopScreen\\fatRufusLarge", false, new FatBehaviour(), 800, "FAT RUFUS", "I THIK I ATE TOO MUCH"));
                GameModel.rufusModels.AddLast(new RufusModel(".\\Sprites\\GUI\\shopScreen\\superRufus", ".\\Sprites\\GUI\\shopScreen\\superRufusLarge", false, new SuperRufusBehaviour(), 3000, "SUPER RUFUS", "I'M A SUPER HERO"));
                GameModel.rufusModels.AddLast(new RufusModel(".\\Sprites\\GUI\\shopScreen\\rufusMario", ".\\Sprites\\GUI\\shopScreen\\rufusMarioLarge", false, new MarioBehaviour(), 4500, "PIPEMAN", "IT'S ME, RUFUS"));
                GameModel.rufusModels.AddLast(new RufusModel(".\\Sprites\\GUI\\shopScreen\\ryufus", ".\\Sprites\\GUI\\shopScreen\\ryufusLarge", false, new RyufusBehaviour(), 7000, "RYUFUS", "SHOLYUUUUUFUS!"));
                GameModel.rufusModels.AddLast(new RufusModel(".\\Sprites\\GUI\\commingSoon", ".\\Sprites\\GUI\\commingSoon", false, new RyufusBehaviour(), 0, "COMMING", "SHOLYUUUUUFUS!"));
                GameModel.rufusModels.AddLast(new RufusModel(".\\Sprites\\GUI\\commingSoon", ".\\Sprites\\GUI\\commingSoon", false, new RyufusBehaviour(), 0, "COMMING", "SHOLYUUUUUFUS!"));
                GameModel.rufusModels.AddLast(new RufusModel(".\\Sprites\\GUI\\commingSoon", ".\\Sprites\\GUI\\commingSoon", false, new RyufusBehaviour(), 0, "COMMING", "SHOLYUUUUUFUS!"));
                GameModel.rufusModels.AddLast(new RufusModel(".\\Sprites\\GUI\\commingSoon", ".\\Sprites\\GUI\\commingSoon", false, new RyufusBehaviour(), 0, "COMMING", "SHOLYUUUUUFUS!"));
                GameModel.rufusModels.AddLast(new RufusModel(".\\Sprites\\GUI\\commingSoon", ".\\Sprites\\GUI\\commingSoon", false, new RyufusBehaviour(), 0, "COMMING", "SHOLYUUUUUFUS!"));
                GameModel.rufusModels.AddLast(new RufusModel(".\\Sprites\\GUI\\commingSoon", ".\\Sprites\\GUI\\commingSoon", false, new RyufusBehaviour(), 0, "COMMING", "SHOLYUUUUUFUS!"));
                GameModel.rufusModels.AddLast(new RufusModel(".\\Sprites\\GUI\\commingSoon", ".\\Sprites\\GUI\\commingSoon", false, new RyufusBehaviour(), 0, "COMMING", "SHOLYUUUUUFUS!"));

                GameModel.itensModels = new LinkedList<RufusModel>();
                GameModel.itensModels.AddLast(new RufusModel(".\\Sprites\\mushrooms\\cogumelo3", ".\\Sprites\\mushrooms\\cogumelo3", true, new SmallMushroomBehaviour(), 0, "MUSHROOM", "EAT ME"));
                GameModel.itensModels.AddLast(new RufusModel(".\\Sprites\\mushrooms\\cogumelo1", ".\\Sprites\\mushrooms\\cogumelo1", false, new MediumMushroomBehaviour(), 500, "MUSHROOM", "EAT ME, PLEASE"));
                GameModel.itensModels.AddLast(new RufusModel(".\\Sprites\\mushrooms\\cogumelo2", ".\\Sprites\\mushrooms\\cogumelo2", false, new GiantMushroomBehaviour(), 2000, "HYPSTASHOOM", "I'M SUPER COOL"));
                GameModel.itensModels.AddLast(new RufusModel(".\\Sprites\\mushrooms\\cogumelo4", ".\\Sprites\\mushrooms\\cogumelo4", false, new BigodonMushroomBehaviour(), 4000, "MOUSTASHOOM", "MY MOUSTACHE IS AMAZING"));
                GameModel.itensModels.AddLast(new RufusModel(".\\Sprites\\mushrooms\\bolinha0001", ".\\Sprites\\mushrooms\\bolinha0001", false, new GreenBolinhaBehaviour(), 2500, "BLOOB", "YOU CANT TOUCH ME"));
                GameModel.itensModels.AddLast(new RufusModel(".\\Sprites\\mushrooms\\quickBolinha0001", ".\\Sprites\\mushrooms\\quickBolinha0001", false, new QuickBolinhaBehaviour(), 3500, "JUMP BLOOB", "LIKE A JUMPER"));
                GameModel.itensModels.AddLast(new RufusModel(".\\Sprites\\mushrooms\\bolinhaVermelha0002", ".\\Sprites\\mushrooms\\bolinhaVermelha0002", false, new RedBolinhaBehaviour(), 4500, "RED BLOOB", "RUN BLOOB, RUN"));
                GameModel.itensModels.AddLast(new RufusModel(".\\Sprites\\mushrooms\\flyingBolinha0012", ".\\Sprites\\mushrooms\\flyingBolinha0012", false, new JumpBolinhaBehaviour(), 5000, "DONOOTY", "WEE"));
                GameModel.itensModels.AddLast(new RufusModel(".\\Sprites\\mushrooms\\time0001", ".\\Sprites\\mushrooms\\time0001", false, new TimeStarBehaviour(), 7000, "PLUSY", "MORE AND MORE POINTS"));
                GameModel.itensModels.AddLast(new RufusModel(".\\Sprites\\mushrooms\\magnetic0001", ".\\Sprites\\mushrooms\\magnetic0001", false, new MagneticStarBehaviour(), 8000, "MAGNETOOTS", "THE MUSHROOMS GO TO YOU"));
                GameModel.itensModels.AddLast(new RufusModel(".\\Sprites\\mushrooms\\rain20001", ".\\Sprites\\mushrooms\\rain20001", false, new RainStarBehaviour(), 1000, "CLOUDY", "IT'S A RAINING MUSHROOM, ALELUIA"));
                GameModel.itensModels.AddLast(new RufusModel(".\\Sprites\\mushrooms\\star0006", ".\\Sprites\\mushrooms\\star0006", false, new SimpleStarBehaviour(), 12000, "HAPPY STAR", "I LOVE YOU"));

                GameModel.levelsModels = new LinkedList<RufusModel>();
                GameModel.levelsModels.AddLast(new RufusModel(".\\Sprites\\GUI\\shopScreen\\imageLevel1", ".\\Sprites\\GUI\\shopScreen\\imageLevel1", true, new FlorestBehaviour(), 0, "FLOREST", "A SIMPLE FOREST"));
                GameModel.levelsModels.AddLast(new RufusModel(".\\Sprites\\GUI\\shopScreen\\imageLevel2", ".\\Sprites\\GUI\\shopScreen\\imageLevel2", false, new WinterBehaviour(), 1500, "WINTER FOREST", "COLD, JUST COLD!"));
                GameModel.levelsModels.AddLast(new RufusModel(".\\Sprites\\GUI\\shopScreen\\imageLevel3", ".\\Sprites\\GUI\\shopScreen\\imageLevel3", false, new NightBehaviour(), 3000, "NIGHT FOREST", "A SIMPLE FOREST, BUT, IT'S NIGHT"));
                GameModel.levelsModels.AddLast(new RufusModel(".\\Sprites\\GUI\\shopScreen\\imageLevel4", ".\\Sprites\\GUI\\shopScreen\\imageLevel4", false, new HellBehaviour(), 4500, "HOT DUNGEON", "HOT, JUST HOT!"));
                GameModel.levelsModels.AddLast(new RufusModel(".\\Sprites\\GUI\\shopScreen\\imageLevel5", ".\\Sprites\\GUI\\shopScreen\\imageLevel5", false, new CloudsBehaviour(), 6000, "CLOUDS", "WELCOME TO HEAVEN"));
                GameModel.levelsModels.AddLast(new RufusModel(".\\Sprites\\GUI\\shopScreen\\imageLevel6", ".\\Sprites\\GUI\\shopScreen\\imageLevel6", false, new PipemanBehaviour(), 7500, "PIPEMAN LAND", "PIPES AND APPLES AND..."));
                GameModel.levelsModels.AddLast(new RufusModel(".\\Sprites\\GUI\\commingSoon", ".\\Sprites\\GUI\\commingSoon", false, new RyufusBehaviour(), 0, "COMMING", "SHOLYUUUUUFUS!"));
                GameModel.levelsModels.AddLast(new RufusModel(".\\Sprites\\GUI\\commingSoon", ".\\Sprites\\GUI\\commingSoon", false, new RyufusBehaviour(), 0, "COMMING", "SHOLYUUUUUFUS!"));
                GameModel.levelsModels.AddLast(new RufusModel(".\\Sprites\\GUI\\commingSoon", ".\\Sprites\\GUI\\commingSoon", false, new RyufusBehaviour(), 0, "COMMING", "SHOLYUUUUUFUS!"));
                GameModel.levelsModels.AddLast(new RufusModel(".\\Sprites\\GUI\\commingSoon", ".\\Sprites\\GUI\\commingSoon", false, new RyufusBehaviour(), 0, "COMMING", "SHOLYUUUUUFUS!"));
                GameModel.levelsModels.AddLast(new RufusModel(".\\Sprites\\GUI\\commingSoon", ".\\Sprites\\GUI\\commingSoon", false, new RyufusBehaviour(), 0, "COMMING", "SHOLYUUUUUFUS!"));
                GameModel.levelsModels.AddLast(new RufusModel(".\\Sprites\\GUI\\commingSoon", ".\\Sprites\\GUI\\commingSoon", false, new RyufusBehaviour(), 0, "COMMING", "SHOLYUUUUUFUS!"));
            }
            else
            {
                GameModel.rufusModels = new LinkedList<RufusModel>();
                GameModel.rufusModels.AddLast(new RufusModel(".\\Sprites\\GUI\\shopScreen\\normalrufus", ".\\Sprites\\GUI\\shopScreen\\rufusLarge", true, new NormalBehaviour(), 0, "RUFUS", "I'M RUFUS, =D"));
                GameModel.rufusModels.AddLast(new RufusModel(".\\Sprites\\GUI\\shopScreen\\fatRufus", ".\\Sprites\\GUI\\shopScreen\\fatRufusLarge", false, new FatBehaviour(), 800, "FAT RUFUS", "I THIK I ATE TOO MUCH"));
                GameModel.rufusModels.AddLast(new RufusModel(".\\Sprites\\GUI\\shopScreen\\superRufus", ".\\Sprites\\GUI\\shopScreen\\superRufusLarge", false, new SuperRufusBehaviour(), 3000, "SUPER RUFUS", "I'M A SUPER HERO"));
                GameModel.rufusModels.AddLast(new RufusModel(".\\Sprites\\GUI\\proonly", ".\\Sprites\\GUI\\proonly", false, new RyufusBehaviour(), 0, "COMMING", "SHOLYUUUUUFUS!"));
                GameModel.rufusModels.AddLast(new RufusModel(".\\Sprites\\GUI\\proonly", ".\\Sprites\\GUI\\proonly", false, new RyufusBehaviour(), 0, "COMMING", "SHOLYUUUUUFUS!"));
                GameModel.rufusModels.AddLast(new RufusModel(".\\Sprites\\GUI\\proonly", ".\\Sprites\\GUI\\proonly", false, new RyufusBehaviour(), 0, "COMMING", "SHOLYUUUUUFUS!"));
                GameModel.rufusModels.AddLast(new RufusModel(".\\Sprites\\GUI\\proonly", ".\\Sprites\\GUI\\proonly", false, new RyufusBehaviour(), 0, "COMMING", "SHOLYUUUUUFUS!"));
                GameModel.rufusModels.AddLast(new RufusModel(".\\Sprites\\GUI\\proonly", ".\\Sprites\\GUI\\proonly", false, new RyufusBehaviour(), 0, "COMMING", "SHOLYUUUUUFUS!"));
                GameModel.rufusModels.AddLast(new RufusModel(".\\Sprites\\GUI\\proonly", ".\\Sprites\\GUI\\proonly", false, new RyufusBehaviour(), 0, "COMMING", "SHOLYUUUUUFUS!"));
                GameModel.rufusModels.AddLast(new RufusModel(".\\Sprites\\GUI\\proonly", ".\\Sprites\\GUI\\proonly", false, new RyufusBehaviour(), 0, "COMMING", "SHOLYUUUUUFUS!"));
                GameModel.rufusModels.AddLast(new RufusModel(".\\Sprites\\GUI\\proonly", ".\\Sprites\\GUI\\proonly", false, new RyufusBehaviour(), 0, "COMMING", "SHOLYUUUUUFUS!"));
                GameModel.rufusModels.AddLast(new RufusModel(".\\Sprites\\GUI\\proonly", ".\\Sprites\\GUI\\proonly", false, new RyufusBehaviour(), 0, "COMMING", "SHOLYUUUUUFUS!"));

                GameModel.itensModels = new LinkedList<RufusModel>();
                GameModel.itensModels.AddLast(new RufusModel(".\\Sprites\\mushrooms\\cogumelo3", ".\\Sprites\\mushrooms\\cogumelo3", true, new SmallMushroomBehaviour(), 0, "MUSHROOM", "EAT ME"));
                GameModel.itensModels.AddLast(new RufusModel(".\\Sprites\\mushrooms\\cogumelo1", ".\\Sprites\\mushrooms\\cogumelo1", false, new MediumMushroomBehaviour(), 500, "MUSHROOM", "EAT ME, PLEASE"));
                GameModel.itensModels.AddLast(new RufusModel(".\\Sprites\\GUI\\proonly", ".\\Sprites\\GUI\\proonly", false, new RyufusBehaviour(), 0, "COMMING", "SHOLYUUUUUFUS!"));
                GameModel.itensModels.AddLast(new RufusModel(".\\Sprites\\GUI\\proonly", ".\\Sprites\\GUI\\proonly", false, new RyufusBehaviour(), 0, "COMMING", "SHOLYUUUUUFUS!"));
                GameModel.itensModels.AddLast(new RufusModel(".\\Sprites\\mushrooms\\bolinha0001", ".\\Sprites\\mushrooms\\bolinha0001", false, new GreenBolinhaBehaviour(), 2500, "BLOOB", "YOU CANT TOUCH ME"));
                GameModel.itensModels.AddLast(new RufusModel(".\\Sprites\\GUI\\proonly", ".\\Sprites\\GUI\\proonly", false, new RyufusBehaviour(), 0, "COMMING", "SHOLYUUUUUFUS!"));
                GameModel.itensModels.AddLast(new RufusModel(".\\Sprites\\GUI\\proonly", ".\\Sprites\\GUI\\proonly", false, new RyufusBehaviour(), 0, "COMMING", "SHOLYUUUUUFUS!"));
                GameModel.itensModels.AddLast(new RufusModel(".\\Sprites\\GUI\\proonly", ".\\Sprites\\GUI\\proonly", false, new RyufusBehaviour(), 0, "COMMING", "SHOLYUUUUUFUS!"));
                GameModel.itensModels.AddLast(new RufusModel(".\\Sprites\\mushrooms\\time0001", ".\\Sprites\\mushrooms\\time0001", false, new TimeStarBehaviour(), 7000, "PLUSY", "MORE AND MORE POINTS"));
                GameModel.itensModels.AddLast(new RufusModel(".\\Sprites\\GUI\\proonly", ".\\Sprites\\GUI\\proonly", false, new RyufusBehaviour(), 0, "COMMING", "SHOLYUUUUUFUS!"));
                GameModel.itensModels.AddLast(new RufusModel(".\\Sprites\\GUI\\proonly", ".\\Sprites\\GUI\\proonly", false, new RyufusBehaviour(), 0, "COMMING", "SHOLYUUUUUFUS!"));
                GameModel.itensModels.AddLast(new RufusModel(".\\Sprites\\GUI\\proonly", ".\\Sprites\\GUI\\proonly", false, new RyufusBehaviour(), 0, "COMMING", "SHOLYUUUUUFUS!"));

                GameModel.levelsModels = new LinkedList<RufusModel>();
                GameModel.levelsModels.AddLast(new RufusModel(".\\Sprites\\GUI\\shopScreen\\imageLevel1", ".\\Sprites\\GUI\\shopScreen\\imageLevel1", true, new FlorestBehaviour(), 0, "FLOREST", "A SIMPLE FOREST"));
                GameModel.levelsModels.AddLast(new RufusModel(".\\Sprites\\GUI\\shopScreen\\imageLevel2", ".\\Sprites\\GUI\\shopScreen\\imageLevel2", false, new WinterBehaviour(), 1500, "WINTER FOREST", "COLD, JUST COLD!"));
                GameModel.levelsModels.AddLast(new RufusModel(".\\Sprites\\GUI\\shopScreen\\imageLevel3", ".\\Sprites\\GUI\\shopScreen\\imageLevel3", false, new NightBehaviour(), 3000, "NIGHT FOREST", "A SIMPLE FOREST, BUT, IT'S NIGHT"));
                GameModel.levelsModels.AddLast(new RufusModel(".\\Sprites\\GUI\\proonly", ".\\Sprites\\GUI\\proonly", false, new RyufusBehaviour(), 0, "COMMING", "SHOLYUUUUUFUS!"));
                GameModel.levelsModels.AddLast(new RufusModel(".\\Sprites\\GUI\\proonly", ".\\Sprites\\GUI\\proonly", false, new RyufusBehaviour(), 0, "COMMING", "SHOLYUUUUUFUS!"));
                GameModel.levelsModels.AddLast(new RufusModel(".\\Sprites\\GUI\\proonly", ".\\Sprites\\GUI\\proonly", false, new RyufusBehaviour(), 0, "COMMING", "SHOLYUUUUUFUS!"));
                GameModel.levelsModels.AddLast(new RufusModel(".\\Sprites\\GUI\\proonly", ".\\Sprites\\GUI\\proonly", false, new RyufusBehaviour(), 0, "COMMING", "SHOLYUUUUUFUS!"));
                GameModel.levelsModels.AddLast(new RufusModel(".\\Sprites\\GUI\\proonly", ".\\Sprites\\GUI\\proonly", false, new RyufusBehaviour(), 0, "COMMING", "SHOLYUUUUUFUS!"));
                GameModel.levelsModels.AddLast(new RufusModel(".\\Sprites\\GUI\\proonly", ".\\Sprites\\GUI\\proonly", false, new RyufusBehaviour(), 0, "COMMING", "SHOLYUUUUUFUS!"));
                GameModel.levelsModels.AddLast(new RufusModel(".\\Sprites\\GUI\\proonly", ".\\Sprites\\GUI\\proonly", false, new RyufusBehaviour(), 0, "COMMING", "SHOLYUUUUUFUS!"));
                GameModel.levelsModels.AddLast(new RufusModel(".\\Sprites\\GUI\\proonly", ".\\Sprites\\GUI\\proonly", false, new RyufusBehaviour(), 0, "COMMING", "SHOLYUUUUUFUS!"));
                GameModel.levelsModels.AddLast(new RufusModel(".\\Sprites\\GUI\\proonly", ".\\Sprites\\GUI\\proonly", false, new RyufusBehaviour(), 0, "COMMING", "SHOLYUUUUUFUS!"));
            }
            loadStorage();


            GameModel.currentRufusID = 0;
            GameModel.currentLevelID = 0;
            GameModel.deltaTime = 1;
            base.Initialize();
            if (GameModel.currentPoints > 9999999)
                GameModel.currentPoints = 9999999;
            StorageManager.save("", null);

            //GameModel.currentPoints = 999990;
        }

        /**
        * Carrega os assets
        */
        protected override void LoadContent()
        {
            // ambientSound.



            content = Content;

            ButtonFactory.initialize(Content, ".\\Sprites\\GUI\\simpleButton", ".\\Sprites\\GUI\\simpleButtonXML", ".\\Sprites\\font\\Luckiest");
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _spriteBatchIntro = new SpriteBatch(GraphicsDevice);
            _spriteBatchPreGame = new SpriteBatch(GraphicsDevice);
            _spriteBatchShop = new SpriteBatch(GraphicsDevice);
            _spriteBatchCard = new SpriteBatch(GraphicsDevice);
            screenManager = new ScreenManager();
            screenManager.addScreen(new GameScreen(ScreenLabels.GAME_SCREEN, _spriteBatch, Content));
            screenManager.addScreen(new IntroScreen(ScreenLabels.INIT_SCREEN, _spriteBatchIntro, Content));
            screenManager.addScreen(new PreGameScreen(ScreenLabels.PRE_GAME_SCREEN, _spriteBatchPreGame, Content));
            screenManager.addScreen(new ShopScreen(ScreenLabels.SHOP_SCREEN, _spriteBatchShop, Content));
            screenManager.change(ScreenLabels.INIT_SCREEN);

            infoCard.init(Content);
            statisticCard.init(Content);
            devInfoCard.init(Content);
            helpCard.init(Content);
            //SoundEffect soundEngine = Content.Load<SoundEffect>(".\\sounds\\teste");
            //soundEngine.Play();

        }

        /**
         * Loop principal do jogo, atualiza as telas
         */
        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            infoCard.update(gameTime, infoCard.isActive ? TouchPanel.GetState() : new TouchCollection());
            statisticCard.update(gameTime, statisticCard.isActive ? TouchPanel.GetState() : new TouchCollection());
            helpCard.update(gameTime, helpCard.isActive ? TouchPanel.GetState() : new TouchCollection());
            devInfoCard.update(gameTime, devInfoCard.isActive ? TouchPanel.GetState() : new TouchCollection());

            // if(!infoCard.isActive)
            screenManager.update(gameTime);
            base.Update(gameTime);

        }
        /**
         * Desenha as telas
         */
        protected override void Draw(GameTime gameTime)
        {
            screenManager.draw();
            base.Draw(gameTime);

            _spriteBatchCard.Begin();
            infoCard.draw(_spriteBatchCard);
            statisticCard.draw(_spriteBatchCard);
            helpCard.draw(_spriteBatchCard);
            devInfoCard.draw(_spriteBatchCard);
            _spriteBatchCard.End();

        }
        internal static void showHelp()
        {
            helpCard.show();
        }
        internal static void showDevCard()
        {
            devInfoCard.show();
        }
        internal static void showCard(DefaultModel temp, int type)
        {
            infoCard.show(temp as RufusModel, type);
        }

        internal static void showStatistics()
        {
            statisticCard.show();
        }

        public static void playSound(string type)
        {
            if (MediaPlayer.State != MediaState.Paused)
            {
                // Trace.write(this.currentPath);
                SoundEffect fx = content.Load<SoundEffect>(".\\sounds\\" + type);
                fx.Play(0.7f, 1, 1);
            }
        }

        internal static void clickSound()
        {

            if (MediaPlayer.State == MediaState.Paused)
                MediaPlayer.Play(ambientSound);
            else
                MediaPlayer.Pause();
        }
    }
}
