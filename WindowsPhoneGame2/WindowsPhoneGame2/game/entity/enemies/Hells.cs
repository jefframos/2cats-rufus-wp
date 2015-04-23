using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using WindowsPhoneGame2.game.graphics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input.Touch;
using WindowsPhoneGame2.game.utils;
using WindowsPhoneGame2.game.graphics.spritesheet;
using WindowsPhoneGame2.game.FX;
using WindowsPhoneGame2.game.screen;

namespace WindowsPhoneGame2.game.entity
{
    class Hells : DefaultEntity
    {
        public Hells(int x, int y)
        {
            position.X = x;
            position.Y = y;
        }
        public override void init(ContentManager Content)
        {
            content = Content;
            texture = Content.Load<Texture2D>(".\\Sprites\\enemies\\hells\\hells");

            //centerPosition = new Vector2(spriteSheet[spriteSheet.currentAnimation.frames.ElementAt(0)].Width / 2, spriteSheet[spriteSheet.currentAnimation.frames.ElementAt(0)].Height / 2);
            centerPosition = new Vector2(texture.Width / 2, texture.Height / 2);


            idRandom = new Random().Next();
            gravity = 0f;
            range =40;
            velocity.Y = 1;
            limitBaseY = 310;

            isInitialized = true;
            deadly = true;
        }
        public override void kill()
        {
            DefaultFX tempFX = new FXImpact(position.X - 40, position.Y - 5);
            GameScreen.getFXLayer().add(tempFX);
            tempFX.init(content);
            dead = true;
        }
        public override void update(GameTime gameTime)
        {
            base.update(gameTime);
            if (position.Y >= limitBaseY)
            {
                kill();
            }
               
        }

    }
}
