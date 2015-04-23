using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsPhoneGame2.game.entity.rufus.behaviours;

namespace WindowsPhoneGame2.game.entity.rufus
{
    class RufusModel : DefaultModel
    {

        public AbstractBehaviour behaviour { get; set; }
        public string thumbPath { get; set; }
        public string largePath { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int price { get; set; }
        public RufusModel(string _thumbPath, string _largePath, bool _isActive, AbstractBehaviour _behaviour, int _price, string _title, string _description)
        {
            thumbPath = _thumbPath;
            largePath = _largePath;
            behaviour = _behaviour;
            isActive = _isActive;
            price = _price;
            title = _title;
            description = _description;
        }

    }
}
