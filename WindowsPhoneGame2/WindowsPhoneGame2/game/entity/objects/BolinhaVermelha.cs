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
using WindowsPhoneGame2.game.entity.objects.behaviours;

namespace WindowsPhoneGame2.game.entity
{
    class BolinhaVermelha : DefaultEntity
    {
        private bool isRunning;
        public BolinhaVermelha(int x)
        {
            //position.X = x;
            isRunning = false;
            behaviour = new GreenBolinhaBehaviour();

        }
        public override void init(ContentManager Content)
        {
            content = Content;
            texture = Content.Load<Texture2D>(".\\Sprites\\mushrooms\\bolinhaVermelha\\bolinhaVermelha");
            spriteSheetPositions = Content.Load<Dictionary<string, Rectangle>>(".\\Sprites\\mushrooms\\bolinhaVermelha\\bolinhaVermelhaXML");
            spriteSheet = new SpriteSheet(texture, spriteSheetPositions);
            spriteSheet.addAnimation(new AnimationStruct("RUN", AnimationStruct.makeSequence(12, 21), 10, null));
            spriteSheet.addAnimation(new AnimationStruct("IDLE1", AnimationStruct.makeSequence(0, 7), 15, null));
            spriteSheet.addAnimation(new AnimationStruct("FALLING", AnimationStruct.makeSequence(8, 11), 12, run));
            spriteSheet.play("IDLE1");


            centerPosition = new Vector2(spriteSheet[spriteSheet.currentAnimation.frames.ElementAt(0)].Width / 2, spriteSheet[spriteSheet.currentAnimation.frames.ElementAt(0)].Height / 2);

            Random rnd = new Random();
            idRandom = rnd.Next(0, 10);
            gravity = .1f;
            range = 30;
            // velocity.Y = 2.8f;
            limitBaseY = 325;
            position = new Vector2();
            position.Y = limitBaseY;
            if (idRandom < 5)
                position.X = -50;
            else
                position.X = 820;

            behaviour.force += 3;
            isInitialized = true;

        }
        private void run()
        {
            isRunning = true;
            spriteSheet.play("RUN");
            if (position.X > 400)
            {
                velocity.X = -behaviour.force;
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            else
            {
                velocity.X = behaviour.force;
            }
        }
        public override DefaultFX getDieEffect()
        {
            return new FXGreenSmoke(position.X, position.Y);
        }
        public override void update(GameTime gameTime)
        {
            base.update(gameTime);
            if (position.Y >= limitBaseY)
            {
                if (!isRunning)
                {
                    if (spriteSheet.currentAnimation.name != "FALLING")
                    {
                        FXFall tempFX = new FXFall(position.X + (spriteEffects == SpriteEffects.FlipHorizontally ? 8 : 0), (float)(limitBaseY + centerPosition.Y + 20));
                        GameScreen.getBackEntityFXLayer().add(tempFX);
                        tempFX.init(content);
                    }
                    spriteSheet.play("FALLING");

                }
            }

            if (position.X > 890 || position.X < -90)
                dead = true;
        }

    }
}
