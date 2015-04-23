using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using WindowsPhoneGame2.framework.GUI.button;

namespace WindowsPhoneGame2.game.factory.GUI
{
    class ButtonFactory
    {
        public static SpriteFont spriteFont;
        public static ButtonModel buttonModel { get; set; }
        public static void initialize(ContentManager Content, string _imagePath, string _xmlPath, string _fontPath)
        {
            spriteFont = Content.Load<SpriteFont>(_fontPath);

            buttonModel = new ButtonModel(_imagePath, _xmlPath, spriteFont);
        }
    }
}
