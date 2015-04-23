using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsPhoneGame2.framework.primitives;
using WindowsPhoneGame2.game.entity;

namespace RufusAndTheMagicMushrooms.framework.graphics.particles
{
    class Emitter : Drawable
    {
        public Vector2 position{get;set;}
        private int timerCount;
        private int quantity;
        private string particlePath;
        private ContentManager content;
        private int acum;
        private LinkedList<StaticObject> vecParticles;
        private Random rnd;
        public Emitter(string particlePath, Vector2 position, int timerCount, int quantity)
        {
            this.particlePath = particlePath;
            this.position = position;
            this.timerCount = timerCount;
            this.quantity = quantity;
            rnd = new Random();
            acum = 0;
            vecParticles = new LinkedList<StaticObject>();
        }
        public override void update(GameTime gameTime)
        {
            acum++;
            if(acum > timerCount)
            {
                for (int i = 0; i <= quantity; i++)
                {
                  
                    StaticObject temp = new StaticObject(particlePath);
                    temp.init(content);

                    temp.velocity = new Vector2((float)(rnd.NextDouble() - 0.5d), (float)rnd.NextDouble());
                    temp.position = position;
                    temp.timeToDestruct = 40;
                    vecParticles.AddLast(temp);
                }
                acum = 0;
            }
            for (int i = 0; i < vecParticles.Count; i++)
            {
                vecParticles.ElementAt(i).update(gameTime);
                if (vecParticles.ElementAt(i).dead)
                    vecParticles.Remove(vecParticles.ElementAt(i));
            }
            base.update(gameTime);
        }
        public override void init(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            content = Content;
            base.init(Content);
        }
        public override void draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            for (int i = 0; i < vecParticles.Count; i++)
            {
                vecParticles.ElementAt(i).draw(spriteBatch);
            }
            base.draw(spriteBatch);
        }
    }
}
