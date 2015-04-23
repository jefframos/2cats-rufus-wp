using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsPhoneGame2.game.layer;
using Microsoft.Xna.Framework;
using WindowsPhoneGame2.game.utils;
using WindowsPhoneGame2.game.entity;
using Microsoft.Xna.Framework.Content;
using System.Collections;
using WindowsPhoneGame2.game.FX;
using WindowsPhoneGame2.game.screen;
using WindowsPhoneGame2.game.entity.objects.behaviours;
using WindowsPhoneGame2.game.entity.rufus.behaviours;
using WindowsPhoneGame2.game.entity.rufus;
using WindowsPhoneGame2.game.level;
using WindowsPhoneGame2.game.level.behaviours;


namespace WindowsPhoneGame2.game
{
    class GameLoop
    {
        private static Layer entityLayer;
        private float timer;
        private float timecounter;
        private static ContentManager content;
        private static List<int> lineID;
        private static List<int> colID;
        private static Rufus rufus;
        private static int acumSpecial;
        public GameLoop(ContentManager _content, Rufus _rufus)
        {
            rufus = _rufus;
            List<int> teste = new List<int>();

            content = _content;
            lineID = new List<int>();
            colID = new List<int>();
            acumSpecial = 40;
            for (int i = 1; i <= 9; i++)
            {
                if (i < 6)
                    lineID.Add(i);
                colID.Add(i);
            }
        }
        internal void beginGame(Layer _entityLayer)
        {
            entityLayer = _entityLayer;
            timecounter = 4f;

        }

        private static void Shuffle(IList<int> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                int value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        public static void mushRain(int level)
        {
            DefaultEntity temp;

            Shuffle(lineID);
            Shuffle(colID);



            for (int i = 0; i < entityLayer.entityList.Count; i++)
            {
                if (entityLayer.entityList.ElementAt(i).deadly)
                {
                    entityLayer.entityList.ElementAt(i).kill();
                }
            }

            int acum = 0;
            for (int i = 0; i < 4; i++)
            {

                temp = new DefaultMooshroom(0, GameModel.getMushroomByFrequency().behaviour, true, rufus);

                temp.position.X = colID[acum] * 80;
                temp.position.Y = lineID[acum] * 60 - (480 * level);

                entityLayer.add(temp);
                entityLayer.init(content);

                acum++;
            }
        }
        public static void startRain()
        {
            mushRain(1);
            mushRain(2);
            mushRain(3);
        }
        internal void update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;


            if (timer >= timecounter)
            {
                Shuffle(lineID);
                Shuffle(colID);

                DefaultEntity temp;

                Random rndn = new Random(DateTime.Now.Millisecond);
                int acum = 0;
                AbstractBehaviour levelBehaviour = GameModel.levelsModels.ElementAt(GameModel.currentLevelID).behaviour;
                for (int i = 0; i < 5; i++)
                {

                    float rnd = rndn.Next(0, 35);
                    acumSpecial--;
                    if (acumSpecial <= 0)
                    {
                        temp = null;
                        RufusModel tempModel = GameModel.getStarByFrequency();
                        if (tempModel != null)
                        {
                            if (tempModel.behaviour is SimpleStarBehaviour)
                            {
                                temp = new Star(10);
                                temp.position.X = colID[acum] * 80;
                                temp.position.Y = lineID[acum] * 60;
                            }
                            else if (tempModel.behaviour is MagneticStarBehaviour || tempModel.behaviour is RainStarBehaviour || tempModel.behaviour is TimeStarBehaviour)
                            {
                                temp = new DefaultMooshroom(0, tempModel.behaviour, false, rufus);
                                temp.position.X = colID[acum] * 80;
                                temp.position.Y = lineID[acum] * 60;
                            }
                        }
                        acumSpecial = (int)(10 + rndn.NextDouble() * 40);
                    }
                    else if (rnd < 4 + levelBehaviour.frequency)
                    {

                        LevelBehaviour tempBehaviour = levelBehaviour as LevelBehaviour;

                        if (tempBehaviour.enemy == 1)
                            temp = new Bomb1(colID[acum] * 80, (int)((float)rndn.NextDouble()*30 - 30f));
                        else if (tempBehaviour.enemy == 2)
                            temp = new BerryBomb(colID[acum] * 80, (int)((float)rndn.NextDouble() * 30 - 30f));
                        else if (tempBehaviour.enemy == 3)
                            temp = new JackO(colID[acum] * 80, (int)((float)rndn.NextDouble() * 30 - 30f));
                        else if (tempBehaviour.enemy == 4)
                            temp = new Hells(colID[acum] * 80, (int)((float)rndn.NextDouble() * 30 - 30f));
                        else if (tempBehaviour.enemy == 5)
                            temp = new CloudEye(colID[acum] * 80, (int)((float)rndn.NextDouble() * 30 - 30f));
                        else if (tempBehaviour.enemy == 6)
                            temp = new Goomba(colID[acum] * 80, (int)((float)rndn.NextDouble() * 30 - 30f));
                        else temp = new Bomb1(colID[acum] * 80, (int)((float)rndn.NextDouble() * 30 - 30f));

                        temp.position.Y = -60;
                    }
                    else if (rnd < 7 + levelBehaviour.frequency)
                    {
                        RufusModel tempModel = GameModel.getBolinhaByFrequency();
                        if (tempModel != null)
                        {
                            if (tempModel.behaviour is GreenBolinhaBehaviour)
                            {
                                temp = new Bolinha(colID[acum] * 80);
                                temp.position.Y = -50;
                            }
                            else if (tempModel.behaviour is RedBolinhaBehaviour)
                                temp = new BolinhaVermelha(colID[acum] * 80);
                            else if (tempModel.behaviour is JumpBolinhaBehaviour)
                                temp = new BolinhaPulante(colID[acum] * 80);
                            else
                            {
                                temp = new BolinhaQuicante(colID[acum] * 80);
                                temp.position.Y = -50;
                            }
                        }
                        else temp = null;

                    }
                    //else if (rnd < 8 + levelBehaviour.frequency)
                    //{
                    //    temp = null;
                    //    RufusModel tempModel = GameModel.getStarByFrequency();
                    //    if (tempModel != null)
                    //    {
                    //        if (tempModel.behaviour is SimpleStarBehaviour)
                    //        {
                    //            temp = new Star(10);
                    //            temp.position.X = colID[acum] * 80;
                    //            temp.position.Y = lineID[acum] * 60;
                    //        }
                    //        else if (tempModel.behaviour is MagneticStarBehaviour || tempModel.behaviour is RainStarBehaviour || tempModel.behaviour is TimeStarBehaviour)
                    //        {
                    //            temp = new DefaultMooshroom(0, tempModel.behaviour, false, rufus);
                    //            temp.position.X = colID[acum] * 80;
                    //            temp.position.Y = lineID[acum] * 60;
                    //        }
                    //    }
                    //}
                    else
                    {

                        // temp = new DefaultMooshroom(0, GameModel.itensModels.ElementAt(1).behaviour);
                        temp = new DefaultMooshroom(0, GameModel.getMushroomByFrequency().behaviour, false, rufus);

                        temp.position.X = colID[acum] * 80;
                        temp.position.Y = lineID[acum] * 60;


                    }
                   

                    if (temp != null)
                    {
                        entityLayer.add(temp);
                        entityLayer.init(content);
                    }

                    timer = 0f;

                    acum++;
                }
            }
        }
    }
}
