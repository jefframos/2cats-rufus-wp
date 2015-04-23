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
using WindowsPhoneGame2.game.screen;
using WindowsPhoneGame2.game.entity.rufus.behaviours;
using RufusAndTheMagicMushrooms.game.entity;
using RufusAndTheMagicMushrooms.game.screen.screenElements;

namespace WindowsPhoneGame2.game.entity
{
    class DefaultMooshroom : DefaultEntity
    {
        private Texture2D deadlyMush;
        private bool soonDeadly;
        private int deadlyCounter;
        private int colorBlinkCounter;
        private int dieCounter;
        private bool inRain;
        private Rufus rufus;
        public DefaultMooshroom(int x, AbstractBehaviour _behaviour, bool _inRain, Rufus _rufus)
        {
            rufus = _rufus;
            inRain = _inRain;
            position.X = x;
            behaviour = _behaviour;
            range = 35;

        }
        public override void init(ContentManager Content)
        {
            content = Content;
            idRandom = new Random().Next(0, 100);
            if (idRandom < 10 && behaviour.type == AbstractBehaviour.MUSHROOM)
                soonDeadly = true;

            texture = Content.Load<Texture2D>(behaviour.tileSheet);

            if (behaviour.tileSheetXML != null && behaviour.tileSheetXML != "")
            {
                spriteSheetPositions = Content.Load<Dictionary<string, Rectangle>>(behaviour.tileSheetXML);
                spriteSheet = new SpriteSheet(texture, spriteSheetPositions);
                spriteSheet.addAnimation(new AnimationStruct("IDLE", AnimationStruct.makeSequence(0, behaviour.totFrames), 20, null));
                spriteSheet.play("IDLE");
                centerPosition = new Vector2(spriteSheet[spriteSheet.currentAnimation.frames.ElementAt(0)].Width / 2, spriteSheet[spriteSheet.currentAnimation.frames.ElementAt(0)].Height / 2);
            }
            else
                centerPosition = new Vector2(texture.Width / 2, texture.Height / 2);



            if (soonDeadly && !inRain)
            {
                deadlyMush = Content.Load<Texture2D>(".\\Sprites\\enemies\\deadly\\deadly");
                deadlyCounter = 0;
                colorBlinkCounter = 0;
            }
            if (inRain)
            {
                // gravity = 0.1f;
                //velocity.X = 3f;
                velocity.Y = 3f;
            }
            else
                gravity = 0f;

            range = behaviour.range;
            isInitialized = true;

            DefaultFX tempFX = new FXShow(position.X - texture.Width / 2, position.Y - texture.Height / 2);
            GameScreen.getFXLayer().add(tempFX);
            tempFX.init(Content);

            dieCounter = 2000;
            effect = behaviour.effect;
        }
        public override void kill()
        {
            dead = true;
        }
        public override void update(GameTime gameTime)
        {
            base.update(gameTime);
            parentLayer.collideEntity(this, parentLayer);


            if (rufus.isMagnetic && !(inRain) && behaviour.type == AbstractBehaviour.MUSHROOM)
            {
                float distance = Vector2.Distance(rufus.position, this.position);
                if (distance < 300)
                {
                    double angle = Math.Atan2((double)(rufus.position.Y - this.position.Y), (double)(rufus.position.X - this.position.X)) * (180 / 3.14) + 90;
                    angle = angle / 180 * 3.14;
                    //(float)(angle * 180 / 3.14) + 90)
                    velocity.X = (float)Math.Sin(angle) * (120 / distance) * 1.3f;// *distance * vel;
                    velocity.Y = -(float)Math.Cos(angle) * (120 / distance) * 1.3f;// * distance * vel);
                }
            }
            else if (!(inRain))
            {
                velocity.X = 0;
                velocity.Y = 0;
            }
            if (soonDeadly)
            {
                deadlyCounter++;
                if (colorBlinkCounter > 0)
                    colorBlinkCounter--;
                if (colorBlinkCounter <= 0)
                    color = Color.White;

                if (deadlyCounter % 50 == 0)
                {
                    color = Color.Red;
                    colorBlinkCounter = 6;
                }

                if (deadlyCounter > 300 && deadlyMush != null)
                {
                    deadly = true;
                    texture = deadlyMush;

                    DefaultFX tempFX = new FXShow(position.X - texture.Width / 2, position.Y - texture.Height / 2);
                    GameScreen.getFXLayer().add(tempFX);
                    tempFX.init(content);
                    soonDeadly = false;
                    color = Color.White;
                    dieCounter /= 3;
                }
            }
            dieCounter--;
            if (dieCounter <= 0 && !dead)
            {
                kill();
                DefaultFX tempFX = new FXShow(position.X - texture.Width / 2, position.Y - texture.Height / 2);
                GameScreen.getFXLayer().add(tempFX);
                tempFX.init(content);
            }

            if (position.Y > 500 || position.X > 880 || position.X < -100)
                kill();

        }
        protected override void floorCollide()
        {
            //base.floorCollide();
        }
        public override void collide(LinkedList<DefaultEntity> inCollideList)
        {
            if (!inRain)
                for (int i = 0; i < inCollideList.Count; i++)
                {
                    if ((inCollideList.ElementAt(i) is DefaultMooshroom))
                        kill();
                }
        }
        public override DefaultFX getDieEffect()
        {
            if (behaviour is RainStarBehaviour)
            {
               // EffectImage imgEff = new EffectImage(3);
                //imgEff.init(content);
               // GameScreen.getFXLayer().add(imgEff);
                GameLoop.startRain();
            }
            return new FXGetMush(position.X, position.Y);
        }

    }
}
