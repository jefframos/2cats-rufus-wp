using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsPhoneGame2.game.entity.rufus;
using WindowsPhoneGame2.game.screen.shopPages;
using WindowsPhoneGame2.game.entity;
using WindowsPhoneGame2.game.utils;
using WindowsPhoneGame2.game.entity.rufus.behaviours;

namespace WindowsPhoneGame2.game
{
    class GameModel
    {
        public static LinkedList<RufusModel> rufusModels;
        public static LinkedList<RufusModel> itensModels;
        public static LinkedList<RufusModel> levelsModels;
        public static int currentRufusID;
        public static int currentLevelID;
        public static int currentPoints;
        public static float deltaTime;

        internal static int getBackActive()
        {
            int tempID = currentRufusID;
            for (int i = currentRufusID - 1; i >= 0; i--)
                if (rufusModels.ElementAt(i).isActive)
                {
                    currentRufusID = i;
                    break;
                }
            if (tempID == currentRufusID)
                for (int i = rufusModels.Count - 1; i >= 0; i--)
                    if (rufusModels.ElementAt(i).isActive)
                    {
                        Trace.write("back " + i.ToString());
                        currentRufusID = i;
                        break;
                    }
            return currentRufusID;
        }

        internal static int getNextActive()
        {
            int tempID = currentRufusID;
            for (int i = currentRufusID + 1; i < rufusModels.Count; i++)
                if (rufusModels.ElementAt(i).isActive)
                {
                    currentRufusID = i;
                    break;
                }
            if (tempID == currentRufusID)
                for (int i = 0; i < rufusModels.Count; i++)
                    if (rufusModels.ElementAt(i).isActive)
                    {
                        Trace.write("next " + i.ToString());
                        currentRufusID = i;
                        break;
                    }
            return currentRufusID;
        }

        internal static int getNextLevelActive()
        {
            int tempID = currentLevelID;
            for (int i = currentLevelID + 1; i < levelsModels.Count; i++)
            {
                if (levelsModels.ElementAt(i).isActive)
                {
                    currentLevelID = i;
                    break;
                }
            }
            if (tempID == currentLevelID)
                for (int i = 0; i < levelsModels.Count; i++)
                {
                    if (levelsModels.ElementAt(i).isActive)
                    {
                        currentLevelID = i;
                        break;
                    }
                }
            return currentLevelID;
        }

        public static DefaultModel getModel(int type, int id)
        {
            if (type == ItemBoxModel.WEARS_ID)
            {
                return rufusModels.ElementAt(id);
            }
            else if (type == ItemBoxModel.ITENS_ID)
            {
                return itensModels.ElementAt(id);
            }
            else if (type == ItemBoxModel.LEVELS_ID)
            {
                return levelsModels.ElementAt(id);
            }
            return null;
        }


        internal static RufusModel getStarByFrequency()
        {
            LinkedList<RufusModel> activeItens = new LinkedList<RufusModel>();
            float freqAcum = 0;
            for (int i = 0; i < itensModels.Count; i++)
            {

                if (itensModels.ElementAt(i).isActive && itensModels.ElementAt(i).behaviour.type == AbstractBehaviour.STAR)
                {
                    activeItens.AddLast(itensModels.ElementAt(i));
                    freqAcum += itensModels.ElementAt(i).behaviour.frequency;
                }
            }

            Random rndn = new Random(DateTime.Now.Millisecond);
            int sortedNumber = rndn.Next(0, (int)(freqAcum * 100));
            float tempAcumFrequency = 0;
            int returnID = -1;

            for (int j = 0; j < activeItens.Count; j++)
            {
                if (sortedNumber < tempAcumFrequency)
                {
                    returnID = j - 1;
                    break;
                }
                tempAcumFrequency += activeItens.ElementAt(j).behaviour.frequency * 100;
            }

            if (returnID <= -1)
                returnID = activeItens.Count - 1;
            if (returnID < 0)
                return null;
            return activeItens.ElementAt(returnID);
        }

        internal static RufusModel getBolinhaByFrequency()
        {
            LinkedList<RufusModel> activeItens = new LinkedList<RufusModel>();
            float freqAcum = 0;
            for (int i = 0; i < itensModels.Count; i++)
            {
                if (itensModels.ElementAt(i).isActive && itensModels.ElementAt(i).behaviour.type == AbstractBehaviour.BOLINHA)
                {
                    activeItens.AddLast(itensModels.ElementAt(i));
                    freqAcum += itensModels.ElementAt(i).behaviour.frequency;
                }
            }

            Random rndn = new Random(DateTime.Now.Millisecond);
            int sortedNumber = rndn.Next(0, (int)(freqAcum * 100));
            float tempAcumFrequency = 0;
            int returnID = -1;

            for (int j = 0; j < activeItens.Count; j++)
            {
                if (sortedNumber < tempAcumFrequency)
                {
                    returnID = j - 1;
                    break;
                }
                tempAcumFrequency += activeItens.ElementAt(j).behaviour.frequency * 100;
            }

            if (returnID <= -1)
                returnID = activeItens.Count - 1;
            if (returnID < 0)
                return null;
            return activeItens.ElementAt(returnID);
        }

        internal static RufusModel getMushroomByFrequency()
        {
            LinkedList<RufusModel> activeItens = new LinkedList<RufusModel>();
            float freqAcum = 0;
            for (int i = 0; i < itensModels.Count; i++)
            {
                if (itensModels.ElementAt(i).isActive && itensModels.ElementAt(i).behaviour.type == AbstractBehaviour.MUSHROOM)
                {
                    activeItens.AddLast(itensModels.ElementAt(i));
                    freqAcum += itensModels.ElementAt(i).behaviour.frequency;
                }
            }

            Random rndn = new Random(DateTime.Now.Millisecond);
            int sortedNumber = rndn.Next(0, (int)(freqAcum * 100));
            float tempAcumFrequency = 0;
            int returnID = -1;

            for (int j = 0; j < activeItens.Count; j++)
            {
                if (sortedNumber < tempAcumFrequency)
                {
                    returnID = j - 1;
                    break;
                }
                tempAcumFrequency += activeItens.ElementAt(j).behaviour.frequency * 100;
            }

            if (returnID <= -1)
                returnID = activeItens.Count - 1;
            return activeItens.ElementAt(returnID);
        }
    }
}
