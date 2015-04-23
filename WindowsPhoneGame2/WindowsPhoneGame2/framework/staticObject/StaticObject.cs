using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using WindowsPhoneGame2.framework.primitives;

namespace WindowsPhoneGame2.game.entity
{

    class StaticObject : Drawable
    {
        public Vector2 position; //{ get; set; }
        public Vector2 velocity { get; set; }
        public Texture2D texture { get; set; }
        public Boolean isInitialized { get; set; }
        public Boolean dead { get; set; }
        public float alpha { get; set; }
        private string path;
        public Color color;
        public Vector2 scale;
        public Vector2 origin;
        public SpriteEffects spriteEffects { get; set; }
        public StaticObject(string _path)
        {
            path = _path;
            alpha = 1f;
            position = new Vector2(0, 0);
            velocity = new Vector2(0, 0);
            scale = new Vector2(1, 1);
            origin = new Vector2(0,0);
            color = new Color(255, 255, 255, alpha);
            spriteEffects = SpriteEffects.None;
            timeToDestruct = -1;
        }
        /**
         * Avisa a entidade em quais objetos ela está colidindo
         */
        public virtual void collide(LinkedList<DefaultEntity> inCollideList)
        {
        }
        override public void update(GameTime gameTime)
        {
            if (timeToDestruct > 0)
            {
                timeToDestruct--;
                if (timeToDestruct == 0)
                    dead = true;
            }
            position.X += velocity.X;
            position.Y += velocity.Y;
        }
        override public void dispose()
        {
            texture.Dispose();
            isInitialized = false;
        }
        public virtual void reload(string newPath)
        {
            path = newPath;
        }
        /**
         *Inicializa o objeto
         */
        public override void init(ContentManager Content)
        {
            texture = Content.Load<Texture2D>(path);
            isInitialized = true;
        }
        override public void draw(SpriteBatch spriteBatch)
        {
            if (texture != null && !texture.IsDisposed)
                spriteBatch.Draw(texture, position, new Rectangle(0, 0, texture.Width, texture.Height), new Color(color.R, color.G, color.B, alpha), 0, origin, scale, spriteEffects, 0f);
        }

        public int timeToDestruct { get; set; }
    }
}
