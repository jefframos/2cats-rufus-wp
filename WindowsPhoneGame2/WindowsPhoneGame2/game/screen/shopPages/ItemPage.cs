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
    class ItemPage : DefaultPage, IPage
    {

        public ItemPage()
            : base(".\\Sprites\\GUI\\shopScreen\\backScreen")
        {
            position.X = 240;
            position.Y = 20;
            initPos = new Point((int)position.X, (int)position.Y);

            itemsModel = new LinkedList<ItemBoxModel>();
            for (int i = 0; i < GameModel.itensModels.Count; i++)
            {
                itemsModel.AddLast(new ItemBoxModel(GameModel.itensModels.ElementAt(i), ItemBoxModel.ITENS_ID, i));
            }

            //itemsModel = new LinkedList<ItemBoxModel>();
            //itemsModel.AddLast(new ItemBoxModel(".\\Sprites\\GUI\\shopScreen\\cogumelo1", 1000, true, ItemBoxModel.ITENS_ID, itemsModel.Count));
            //itemsModel.AddLast(new ItemBoxModel(".\\Sprites\\GUI\\shopScreen\\cogumelo2", 2000, true, ItemBoxModel.ITENS_ID, itemsModel.Count));
            //itemsModel.AddLast(new ItemBoxModel(".\\Sprites\\GUI\\shopScreen\\cogumelo3", 3000, true, ItemBoxModel.ITENS_ID, itemsModel.Count));
            //itemsModel.AddLast(new ItemBoxModel(".\\Sprites\\GUI\\shopScreen\\cogumelo3", 4000, false, ItemBoxModel.ITENS_ID, itemsModel.Count));
            //itemsModel.AddLast(new ItemBoxModel(".\\Sprites\\GUI\\shopScreen\\bolinha", 4000, true, ItemBoxModel.ITENS_ID, itemsModel.Count));
            //itemsModel.AddLast(new ItemBoxModel(".\\Sprites\\GUI\\shopScreen\\bolinha", 4000, false, ItemBoxModel.ITENS_ID, itemsModel.Count));
            //itemsModel.AddLast(new ItemBoxModel(".\\Sprites\\GUI\\shopScreen\\bolinha", 4000, false, ItemBoxModel.ITENS_ID, itemsModel.Count));
            //itemsModel.AddLast(new ItemBoxModel(".\\Sprites\\GUI\\shopScreen\\bolinha", 4000, false, ItemBoxModel.ITENS_ID, itemsModel.Count));
            //itemsModel.AddLast(new ItemBoxModel(".\\Sprites\\GUI\\shopScreen\\cogumelo3", 4000, false, ItemBoxModel.ITENS_ID, itemsModel.Count));
            //itemsModel.AddLast(new ItemBoxModel(".\\Sprites\\GUI\\shopScreen\\cogumelo3", 4000, false, ItemBoxModel.ITENS_ID, itemsModel.Count));
            //itemsModel.AddLast(new ItemBoxModel(".\\Sprites\\GUI\\shopScreen\\cogumelo2", 4000, false, ItemBoxModel.ITENS_ID, itemsModel.Count));
            //itemsModel.AddLast(new ItemBoxModel(".\\Sprites\\GUI\\shopScreen\\cogumelo1", 9999, false, ItemBoxModel.ITENS_ID, itemsModel.Count));
        }
    }
}
