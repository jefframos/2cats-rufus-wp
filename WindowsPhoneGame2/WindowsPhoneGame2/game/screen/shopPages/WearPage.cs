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
using WindowsPhoneGame2.game.entity.rufus;

namespace WindowsPhoneGame2.game.screen.shopPages
{
    class WearPage : DefaultPage
    {

        public WearPage()
            : base(".\\Sprites\\GUI\\shopScreen\\backScreen")
        {
            position.X = 240;
            position.Y = 20;
            initPos = new Point((int)position.X, (int)position.Y);

            itemsModel = new LinkedList<ItemBoxModel>();
            for (int i = 0; i < GameModel.rufusModels.Count; i++)
            {
                itemsModel.AddLast(new ItemBoxModel(GameModel.rufusModels.ElementAt(i), ItemBoxModel.WEARS_ID, i));
            }
        }
    }
}
