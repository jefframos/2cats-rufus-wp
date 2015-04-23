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
using WindowsPhoneGame2.game.entity.objects.behaviours;

namespace WindowsPhoneGame2.game.entity
{
    class Star : DefaultEntity
    {
        private int acumDie;
        private int life;
        public Star(int _life)
        {
             life = _life;
            behaviour = new SimpleStarBehaviour();

        }
        public override void init(ContentManager Content)
        {
            acumDie = 0;
           
            texture = Content.Load<Texture2D>(".\\Sprites\\mushrooms\\star\\star");
            spriteSheetPositions = Content.Load<Dictionary<string, Rectangle>>(".\\Sprites\\mushrooms\\star\\starXML");
            spriteSheet = new SpriteSheet(texture, spriteSheetPositions);
            spriteSheet.addAnimation(new AnimationStruct("INIT", AnimationStruct.makeSequence(0, 5), 10, endInit));
            spriteSheet.addAnimation(new AnimationStruct("IDLE", AnimationStruct.makeSequence(5, 8), 12, endIdle));
            spriteSheet.addAnimation(new AnimationStruct("GOAWAY", AnimationStruct.makeSequence(5, 0), 8, kill));
            spriteSheet.play("INIT");


            centerPosition = new Vector2(spriteSheet[spriteSheet.currentAnimation.frames.ElementAt(0)].Width / 2, spriteSheet[spriteSheet.currentAnimation.frames.ElementAt(0)].Height / 2);


            idRandom = new Random().Next();
            gravity = 0f;
            range = 35;
            //velocity.Y = 1;
            limitBaseY = 325;
            effect = behaviour.effect;

            isInitialized = true;

        }
        private void endIdle()
        {
            acumDie++;
            if (acumDie >= life)
                spriteSheet.play("GOAWAY");
        }
        private void endInit()
        {
            spriteSheet.play("IDLE");
        }
        public override void kill()
        {
            dead = true;
        }
        public override void update(GameTime gameTime)
        {
            gravity = 0f;
            velocity = new Vector2();

            base.update(gameTime);

            if (position.Y >= limitBaseY)
            {
                if (collidable)
                    spriteSheet.play("DEAD");
                collidable = false;
            }

        }
        public override DefaultFX getDieEffect()
        {
            return new FXGetStar(position.X, position.Y);
        }

    }
}
