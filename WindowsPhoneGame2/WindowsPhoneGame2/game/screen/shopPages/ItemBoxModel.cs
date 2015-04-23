using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsPhoneGame2.game.entity;
using WindowsPhoneGame2.game.entity.rufus;

namespace WindowsPhoneGame2.game.screen.shopPages
{
    class ItemBoxModel : DefaultModel
    {
        public static int LEVELS_ID = 2;
        public static int ITENS_ID = 1;
        public static int WEARS_ID = 0;
        public string imagePath { get; set; }
        public int value { get; set; }
        public int type { get; set; }
        public int id { get; set; }
        public RufusModel rufusModel { get; set; }
        public ItemBoxModel(string _imagePath, int _value, bool _isActive, int _type, int _id)
        {
            imagePath = _imagePath;
            value = _value;
            isActive = _isActive;
            type = _type;
            id = _id;
            rufusModel = null;
        }
        public ItemBoxModel(RufusModel _rufusModel, int _type, int _id)
        {
            rufusModel = _rufusModel;
            imagePath = rufusModel.thumbPath;
            value = rufusModel.price;
            isActive = rufusModel.isActive;
            type = _type;
            id = _id;
        }
    }
}
