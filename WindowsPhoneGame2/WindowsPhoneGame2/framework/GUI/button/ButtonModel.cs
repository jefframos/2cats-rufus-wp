using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace WindowsPhoneGame2.framework.GUI.button
{
    class ButtonModel
    {
        public string imagePath { get; set; }
        public string xmlPath { get; set; }
        public SpriteFont font { get; set; }
        public Rectangle rectArea { get; set; }
        public ButtonModel(string _imagePath, string _xmlPath, SpriteFont _font)
        {
            rectArea = new Rectangle(0,0,0,0);
            imagePath = _imagePath;
            xmlPath = _xmlPath;
            font = _font;
        }
        public ButtonModel(string _imagePath, string _xmlPath, SpriteFont _font, Rectangle _rectArea)
        {
            rectArea = _rectArea;
            imagePath = _imagePath;
            xmlPath = _xmlPath;
            font = _font;
        }
    }
}