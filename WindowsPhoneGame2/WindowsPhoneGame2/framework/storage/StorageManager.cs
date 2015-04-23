using Microsoft.Xna.Framework.Media;
using RufusAndTheMagicMushrooms.game;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using WindowsPhoneGame2.game;
using WindowsPhoneGame2.game.utils;

namespace RufusAndTheMagicMushrooms.framework.storage
{
    class StorageManager
    {
        // public static StorageManager() { }
        public static void save(string path, string[] array)
        {
            try
            {
                using (var store = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    if (store.FileExists("InTheRoot.txt"))
                    {
                        try
                        {
                            using (StreamWriter sw =
                                  new StreamWriter(store.OpenFile("InTheRoot.txt",
                                      FileMode.Open, FileAccess.Write)))
                            {
                                string rufusStr = "";
                                string itensStr = "";
                                string levelStr = "";
                                for (int i = 0; i < GameModel.rufusModels.Count; i++)
                                {
                                    rufusStr += GameModel.rufusModels.ElementAt(i).isActive ? "1" : "0";
                                }

                                for (int i = 0; i < GameModel.itensModels.Count; i++)
                                {
                                    itensStr += GameModel.itensModels.ElementAt(i).isActive ? "1" : "0";
                                }

                                for (int i = 0; i < GameModel.levelsModels.Count; i++)
                                {
                                    levelStr += GameModel.levelsModels.ElementAt(i).isActive ? "1" : "0";
                                }



                                sw.WriteLine(GameModel.currentPoints.ToString() + ",0,1,2," + rufusStr + "," + itensStr + "," + levelStr + ",g,g,g," + MediaPlayer.State + "," + Statistics.toString());
                                //sw.Flush();
                                sw.Close();
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
                Trace.write("store Messag" + ex.Message.ToString());
            }
        }
        // public static  string[2](string path)
        //{
        //string[2] array = new string[2];
        //return array;
        //}
    }
}
