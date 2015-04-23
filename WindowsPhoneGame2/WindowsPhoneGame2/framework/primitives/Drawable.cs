using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace WindowsPhoneGame2.framework.primitives
{
    class Drawable
    {
        public virtual void draw(SpriteBatch spriteBatch)
        {
        }
        public virtual void dispose()
        {
        }
        public virtual void update(GameTime gameTime)
        {
        }
        /**
        *Inicializa o objeto
        */
        public virtual void init(ContentManager Content)
        {
           
        }

    }

}
