using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsPhoneGame2.game.entity;
using WindowsPhoneGame2.game.screen.shopPages.elements;
using WindowsPhoneGame2.framework.GUI.button;
using XNATweener;
using WindowsPhoneGame2.game.utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;

namespace WindowsPhoneGame2.game.screen.shopPages
{
    class LevelPage : DefaultPage
    {

        public LevelPage()
            : base(".\\Sprites\\GUI\\shopScreen\\backScreen")
        {
            position.X = 240;
            position.Y = 20;
            initPos = new Point((int)position.X, (int)position.Y);

            itemsModel = new LinkedList<ItemBoxModel>();
            for (int i = 0; i < GameModel.levelsModels.Count; i++)
            {
                itemsModel.AddLast(new ItemBoxModel(GameModel.levelsModels.ElementAt(i), ItemBoxModel.LEVELS_ID, i));
            }
        }
    }
}
