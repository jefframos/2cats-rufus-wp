using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WindowsPhoneGame2.game.graphics;
using Microsoft.Xna.Framework.Content;
using WindowsPhoneGame2.game.utils;
using WindowsPhoneGame2.game.layer;
using WindowsPhoneGame2.framework.primitives;
using WindowsPhoneGame2.game.FX;
using WindowsPhoneGame2.game.entity.rufus.behaviours;
using RufusAndTheMagicMushrooms.game.entity;

namespace WindowsPhoneGame2.game.entity
{

    class DefaultEntity : Drawable
    {
        public Vector2 position;
        public Vector2 velocity;
        public float gravity;
        public double idRandom { get; set; }
        public int range { get; set; }
        protected int limitBaseY;
        private float jumpForce;
        public Texture2D texture { get; set; }
        public Dictionary<string, Rectangle> spriteSheetPositions { get; set; }
        public SpriteSheet spriteSheet { get; set; }
        public Boolean dead { get; set; }
        public Boolean isInitialized { get; set; }
        public Layer parentLayer { get; set; }
        public Vector2 scale { get; set; }
        public SpriteEffects spriteEffects { get; set; }
        public Vector2 centerPosition;
        public Boolean collidable { get; set; }
        public Boolean deadly { get; set; }
        public string effect { get; set; }
        protected ContentManager content;
        protected bool inFloor;
        protected float bounce;
        protected Color color;
        public AbstractBehaviour behaviour { get; set; }
        public DefaultEntity()
        {
            isInitialized = false;
            dead = false;
            gravity = 0f;
            limitBaseY = 320;
            jumpForce = 10;
            position = new Vector2(10, 10);
            velocity = new Vector2(0, gravity);
            scale = new Vector2(1, 1);
            spriteEffects = SpriteEffects.None;
            centerPosition = new Vector2();
            collidable = true;
            deadly = false;
            bounce = 0.2f;
            color = Color.White;
            effect = RufusEffects.NONE;
        }
        /**
         * Avisa a entidade em quais objetos ela está colidindo
         */
        public virtual void collide(LinkedList<DefaultEntity> inCollideList)
        {
        }
        public virtual DefaultFX getDieEffect()
        {
            return null;
        }
        override public void update(GameTime gameTime)
        {
            if (spriteSheet != null)
                spriteSheet.update();

            if (position.Y - velocity.Y >= limitBaseY && velocity.Y >= 0)
            {
                floorCollide();
            }
            else
                velocity.Y += gravity;

            position.Y += velocity.Y;
            position.X += velocity.X;
        }
        protected virtual void floorCollide()
        {
            inFloor = true;
            position.Y = limitBaseY;
            velocity.Y = 0;
        }
        public virtual void kill()
        {
            dead = true;
        }
        public void jump()
        {
            velocity.Y -= jumpForce;
        }

        public override void init(ContentManager Content)
        {
            content = Content;
            isInitialized = true;
        }
        override public void dispose()
        {
            texture.Dispose();
        }
        /**
         * Verifica se a entidade está nos bounds da tela e já altera as velocidades para não sair
         */
        protected bool apllyBoundsCollide()
        {
            bool bCollide = false;
            if (velocity.X > 0 && position.X > 790 - centerPosition.X - velocity.X)
            {
                velocity.X = -Math.Abs(velocity.X * bounce);
                bCollide = true;
            }
            else if (velocity.X < 0 && position.X < 10 + velocity.X)
            {
                velocity.X = Math.Abs(velocity.X * bounce);
                bCollide = true;
            }
            if (velocity.Y > 0 && position.Y > 470 - centerPosition.Y - velocity.Y)
            {
                velocity.Y = -Math.Abs(velocity.Y * bounce);
                bCollide = true;
            }
            else if (velocity.Y < 0 && position.Y < 10 + velocity.Y)
            {
                velocity.Y = Math.Abs(velocity.Y * bounce);
                bCollide = true;
            }
            return bCollide;

        }
        override public void draw(SpriteBatch spriteBatch)
        {
            base.draw(spriteBatch);
            if (texture != null && !texture.IsDisposed)
            {
                if (spriteSheet != null)
                {
                    if (spriteSheet.currentAnimation != null)
                        spriteBatch.Draw(texture, position, spriteSheet[spriteSheet.currentAnimation.frames.ElementAt(spriteSheet.currentAnimation.currentID)], color, 0, new Vector2(), scale, spriteEffects, 0f);
                    else
                        spriteBatch.Draw(texture, position, spriteSheet[spriteSheet.currentFrame], color, 0, new Vector2(), scale, spriteEffects, 0f);
                }
                else
                {
                    spriteBatch.Draw(texture, position, color);
                }
            }

        }
    }
}
