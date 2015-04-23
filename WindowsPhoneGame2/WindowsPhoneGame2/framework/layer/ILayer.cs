using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace WindowsPhoneGame2.game.layer
{
    interface ILayer
    {
        void draw(SpriteBatch spriteBatch);
        void init(ContentManager Content);
        void update(GameTime gameTime);
    }
}
