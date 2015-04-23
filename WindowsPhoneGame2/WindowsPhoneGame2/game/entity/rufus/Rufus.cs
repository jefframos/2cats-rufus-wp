using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using WindowsPhoneGame2.game.graphics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input.Touch;
using WindowsPhoneGame2.game.utils;
using WindowsPhoneGame2.game.graphics.spritesheet;
using XNATweener;
using WindowsPhoneGame2.game.entity.rufus.behaviours;
using WindowsPhoneGame2.game.layer;
using WindowsPhoneGame2.game.FX;
using WindowsPhoneGame2.game.screen;
using System.Reflection;
using WindowsPhoneGame2.game.factory.GUI;
using RufusAndTheMagicMushrooms.game.entity;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using RufusAndTheMagicMushrooms.game.screen.screenElements;
using RufusAndTheMagicMushrooms.framework.graphics.particles;
using RufusAndTheMagicMushrooms.game;

namespace WindowsPhoneGame2.game.entity
{
    class Rufus : DefaultEntity
    {
        private bool ableToJump;
        private Vector2 initPosition;
        private Vector2 endPosition;
        public TouchCollection touches { get; set; }
        public Action collideCallback;
        public Action dieCallback;
        private Boolean airDying;
        private float velocityFactor;
        private int fallings;
        private float force;
        private Vector2 spriteSheetPosition;
        private int combo;
        public int levelPoints { get; set; }
        private AbstractBehaviour levelBehaviour;
        public float mushComboCounter { get; set; }
        public float maxMushComboCounter { get; set; }
        protected Boolean isInvencible;
        public Boolean isMagnetic { get; set; }
        public int special { get; set; }
        public int maxSpecial { get; set; }
        public int mushrooms { get; set; }
        public int bloobs { get; set; }
        public int specials { get; set; }
        public int maxCombo { get; set; }
        public int maxJumps { get; set; }
        private int pointAcress;
        private ScreenEffect screenFX;
        private Emitter emitter;
        private EffectImage imgEff;
        public Rufus(int x)
        {
            position.X = x;
            position.Y = limitBaseY;
            special = 0;
            maxSpecial = 1000;

            pointAcress = 1;

            screenFX = new ScreenEffect();
            emitter = new Emitter(".\\Sprites\\effects\\particles\\miniStar", position, 3, 3);
        }

        public override void init(ContentManager Content)
        {
            emitter.init(Content);
            content = Content;
            screenFX.init(content);
            behaviour = GameModel.rufusModels.ElementAt(GameModel.currentRufusID).behaviour;
            //  behaviour = new SuperRufusBehaviour();

            spriteSheetPosition = new Vector2();
            texture = Content.Load<Texture2D>(behaviour.tileSheet);
            spriteSheetPositions = Content.Load<Dictionary<string, Rectangle>>(behaviour.tileSheetXML);

            spriteSheet = new SpriteSheet(texture, spriteSheetPositions);
            spriteSheet.addAnimation(new AnimationStruct(AnimationStruct.IDLE, behaviour.idleFrames, 15, null));
            spriteSheet.addAnimation(new AnimationStruct(AnimationStruct.JUMP, behaviour.jumpFrames, 8, endJump));
            spriteSheet.addAnimation(new AnimationStruct(AnimationStruct.FALLING, behaviour.fallingFrames, 10, playIdle));
            spriteSheet.addAnimation(new AnimationStruct(AnimationStruct.DOWN, behaviour.downFrames, 10, playIdle));
            spriteSheet.addAnimation(new AnimationStruct(AnimationStruct.UP, behaviour.upFrames, 10, endJump));
            spriteSheet.addAnimation(new AnimationStruct(AnimationStruct.AIRDYING, behaviour.airdyingFrames, 10, null));
            spriteSheet.addAnimation(new AnimationStruct(AnimationStruct.DIE, behaviour.dieFrames, 8, die, false));
            spriteSheet.play(AnimationStruct.IDLE);

            if (behaviour.centerPosition == null)
                centerPosition = new Vector2(spriteSheet[spriteSheet.currentAnimation.frames.ElementAt(0)].Width / 2, spriteSheet[spriteSheet.currentAnimation.frames.ElementAt(0)].Height / 2);
            else centerPosition = behaviour.centerPosition;

            spriteSheetPosition = behaviour.spriteSheetPosition;
            limitBaseY = 330;
            levelBehaviour = GameModel.levelsModels.ElementAt(GameModel.currentLevelID).behaviour;
            gravity = (behaviour.gravity + levelBehaviour.gravity) / 2;
            range = behaviour.range;
            force = behaviour.force;
            bounce = behaviour.bounce;
            ableToJump = true;
            fallings = -1;
            combo = 0;
            isInitialized = true;
            airDying = false;
            levelPoints = 0;
            maxMushComboCounter = 20f;
            mushComboCounter = 0f;
            isInvencible = false;
            mushrooms = 0;
            bloobs = 0;
            specials = 0;
            maxCombo = 0;
            maxJumps = 0;
        }
        public void setOnInvencible()
        {
            isInvencible = true;
        }
        public void setOffInvencible()
        {
            isInvencible = false;
            mushComboCounter = 0f;
        }
        public void playIdle()
        {
            spriteSheet.play(AnimationStruct.IDLE);
        }
        public void endJump()
        {
            spriteSheet.play(AnimationStruct.UP);
        }
        public void die()
        {
            spriteSheet.setFrame(14);
            if (dieCallback != null)
                dieCallback();
        }
        public override void update(GameTime gameTime)
        {

            if (special > 0)
            {
                special--;
                screenFX.update(gameTime);
                emitter.update(gameTime);
                emitter.position = position + centerPosition;
            }
            else
            {
                isMagnetic = false;
                isInvencible = false;
                pointAcress = 1;
            }

            if (GameModel.deltaTime < 1f)
            {
                GameModel.deltaTime = (maxSpecial - special) / maxSpecial;
                Trace.write(GameModel.deltaTime.ToString());
            }

            base.update(gameTime);
            if (ableToJump && !airDying)
            {
                foreach (TouchLocation touch in touches)
                {
                    if (touch.State == TouchLocationState.Pressed)// && Vector2.Distance(touch.Position, (position + centerPosition)) < range * 3)
                    {
                        velocityFactor = 0;
                        initPosition = touch.Position - centerPosition;

                        DefaultFX tempFX = new FXTouch(touch.Position.X - 20, touch.Position.Y - 20);
                        GameScreen.getFXLayer().add(tempFX);
                        tempFX.init(content);

                        /**
                         * uncomment this pra parar o rufus no touch
                         *   velocity = new Vector2();
                         *   gravity = 0;
                         */
                    }
                    else if ((touch.State == TouchLocationState.Released))// || (Vector2.Distance(new Vector2(initPosition.X, initPosition.Y), touch.Position) > range * 2.5f)) && initPosition.X > 0)
                    {
                        DefaultFX tempFX = new FXTouch(touch.Position.X - 20, touch.Position.Y - 20);
                        GameScreen.getFXLayer().add(tempFX);
                        tempFX.init(content);

                        endPosition = touch.Position - centerPosition;
                        double angle = Math.Atan2((double)(initPosition.Y - endPosition.Y), (double)(initPosition.X - endPosition.X));
                        go((float)(angle * 180 / 3.14) + 90); //Converte radianos para angulos e acresse 90. Acredito que por causa da orientação da tela
                    }
                }
            }
            //velocityFactor++;
            if (velocity.X < 0)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            else if (velocity.X > 0)
            {
                spriteEffects = SpriteEffects.None;
            }
            apllyBoundsCollide();

            if (!airDying && velocity.Y > 0)
                spriteSheet.play(AnimationStruct.DOWN);

            parentLayer.collideEntity(this, parentLayer);
        }

        protected override void floorCollide()
        {
            if (airDying)
            {
                if (spriteSheet.currentAnimation != null && spriteSheet.currentAnimation.name != AnimationStruct.DIE)
                {
                    GameScreen.getBackEntityFXLayer().add(new FXFall(position.X + (spriteEffects == SpriteEffects.FlipHorizontally ? 10 : 0), (float)(limitBaseY + centerPosition.Y + 28)));
                    GameScreen.getBackEntityFXLayer().init(content);
                }
                spriteSheet.play(AnimationStruct.DIE);
            }
            else if (velocity.Y > 0)
            {

                if (combo > maxCombo)
                    maxCombo = combo;
                if (fallings > maxJumps)
                    maxJumps = fallings;
                combo = 0;
                spriteSheet.play(AnimationStruct.FALLING);
                Game1.playSound("argh");
                if (fallings > 0)
                {
                    levelPoints += fallings * 2;

                    DefaultFX comboFX = new FXLabel(40, 40, "+" + (string)(fallings * 2).ToString(), new Color(63, 53, 127), Color.White);
                    GameScreen.getFXLayer().add(comboFX);
                    comboFX.init(content);
                }
                fallings = -1;

                collideCallback();

                GameScreen.getBackEntityFXLayer().add(new FXFall(position.X + (spriteEffects == SpriteEffects.FlipHorizontally ? 10 : 0), (float)(limitBaseY + centerPosition.Y + 28)));
                GameScreen.getBackEntityFXLayer().init(content);
            }

            ableToJump = true;
            base.floorCollide();
            velocity.X = 0;
        }
        private void go(double angle)
        {
            //initPosition = position + centerPosition;
            float distance = Vector2.Distance(new Vector2(initPosition.X, initPosition.Y), new Vector2(endPosition.X, endPosition.Y));
            if (combo > maxCombo)
                maxCombo = combo;
            if (fallings > maxJumps)
                maxJumps = fallings;
            combo = 0;
            if (distance > range / 2.5)// || velocity.Y != 0)
            {
                gravity = (behaviour.gravity + levelBehaviour.gravity) / 2;
                angle = angle / 180 * 3.14;
                float vel = (force + (velocityFactor < 18 ? (float)velocityFactor / 100f : 0f));
                velocity.X = -(float)Math.Sin(angle) * distance * vel;
                velocity.Y = (float)((float)Math.Cos(angle) * distance * vel);
                endPosition = new Vector2(-100, -100);
                initPosition = new Vector2(-100, -100);
                spriteSheet.play(AnimationStruct.JUMP);
                Game1.playSound("jump");
                if (inFloor)
                {
                    FXFall tempFX = new FXFall(position.X + (spriteEffects == SpriteEffects.FlipHorizontally ? 10 : 0), (float)(limitBaseY + centerPosition.Y + 28));
                    GameScreen.getBackEntityFXLayer().add(tempFX);
                    tempFX.init(content);
                }
                inFloor = false;
                fallings++;
            }

        }
        public override void draw(SpriteBatch spriteBatch)
        {

            if (special > 0)
            {
                screenFX.draw(spriteBatch);
                emitter.draw(spriteBatch);
            }
            if (texture != null && !texture.IsDisposed)
            {
                if (spriteSheet != null)
                {
                    if (spriteSheet.currentAnimation != null)
                        spriteBatch.Draw(texture, position + spriteSheetPosition, spriteSheet[spriteSheet.currentAnimation.frames.ElementAt(spriteSheet.currentAnimation.currentID)], Color.White, 0, new Vector2(), scale, spriteEffects, 0f);
                    else
                        spriteBatch.Draw(texture, position + spriteSheetPosition, spriteSheet[spriteSheet.currentFrame], Color.White, 0, new Vector2(), scale, spriteEffects, 0f);
                }
                else
                {
                    spriteBatch.Draw(texture, position, Color.White);
                }
            }

            if (fallings > 0)
            {
                spriteBatch.DrawString(ButtonFactory.spriteFont, fallings.ToString(), new Vector2(22, 7), Color.White, 0.2f, new Vector2(0), 1.1f, SpriteEffects.None, 0f);
                spriteBatch.DrawString(ButtonFactory.spriteFont, fallings.ToString(), new Vector2(20, 5), new Color(63, 53, 127), 0.2f, new Vector2(0), 1.1f, SpriteEffects.None, 0f);
            }

        }
        /**
         * callback acionada quando a entidade colide com outra
         */
        override public void collide(LinkedList<DefaultEntity> inCollideList)
        {
            if (!airDying)
            {
                foreach (DefaultEntity entity in inCollideList)
                {


                    if (!entity.dead)
                    {
                        if (entity.deadly && GameModel.deltaTime == 1f)
                        {
                            Game1.playSound("pshh");

                            if (!isInvencible)
                            {
                                airDying = true;
                                spriteSheet.play(AnimationStruct.AIRDYING);
                                this.velocity.X = -this.velocity.X * 1.2f;
                                this.velocity.Y = -this.velocity.Y * 1.2f;

                                if (combo > maxCombo)
                                    maxCombo = combo;
                                if (fallings > maxJumps)
                                    maxJumps = fallings;

                                if (fallings > 0)
                                {
                                    levelPoints += fallings * 2;

                                    DefaultFX comboFX = new FXLabel(40, 40, "+" + (string)(fallings * 2).ToString(), Color.White, new Color(63, 53, 127));
                                    GameScreen.getFXLayer().add(comboFX);
                                    comboFX.init(content);
                                    collideCallback();
                                }
                                fallings = -1;
                            }
                            entity.kill();

                            DefaultFX tempFX = new FXImpact(entity.position.X, entity.position.Y);
                            GameScreen.getFXLayer().add(tempFX);
                            tempFX.init(content);


                        }
                        else
                        {
                            if (entity.getDieEffect() != null)
                            {
                                Game1.playSound("bup");

                                DefaultFX tempFX = entity.getDieEffect();
                                GameScreen.getFXLayer().add(tempFX);
                                tempFX.init(content);
                                // if (mushComboCounter >= maxMushComboCounter)
                                //   mushComboCounter = 0;
                                //  else

                                mushComboCounter++;
                                combo++;
                                if (entity.behaviour.type == AbstractBehaviour.MUSHROOM)
                                {
                                    mushrooms++;
                                    Statistics.maxMushrooms++;
                                }
                                else if (entity.behaviour.type == AbstractBehaviour.BOLINHA)
                                {
                                    bloobs++;
                                    Statistics.maxBloobs++;
                                }
                                else if (entity.behaviour.type == AbstractBehaviour.STAR)
                                {
                                    specials++;
                                    Statistics.maxSpecials++;

                                }

                                if (entity.effect == RufusEffects.MAGNETIC)
                                {
                                    screenFX.redraw(3);
                                    if (imgEff != null)
                                        imgEff.kill();
                                    imgEff = new EffectImage(2);
                                    imgEff.init(content);
                                    GameScreen.getFXLayer().add(imgEff);

                                    GameModel.deltaTime = 1f;
                                    pointAcress = 1;
                                    isMagnetic = true;
                                    special = maxSpecial;

                                    Game1.playSound("uhul");
                                }
                                else if (entity.effect == RufusEffects.INVENCIBLE)
                                {
                                    screenFX.redraw(1);
                                    if (imgEff != null)
                                        imgEff.kill();
                                    imgEff = new EffectImage(1);
                                    imgEff.init(content);
                                    GameScreen.getFXLayer().add(imgEff);

                                    GameModel.deltaTime = 1f;
                                    pointAcress = 1;
                                    isInvencible = true;
                                    special = maxSpecial;

                                    Game1.playSound("uhul");

                                }
                                else if (entity.effect == RufusEffects.PLUS)
                                {
                                    screenFX.redraw(2);
                                    if (imgEff != null)
                                        imgEff.kill();
                                    imgEff = new EffectImage(4);
                                    imgEff.init(content);
                                    GameScreen.getFXLayer().add(imgEff);

                                    //GameModel.deltaTime = 0f;
                                    pointAcress = 2;
                                    special = maxSpecial;

                                    Game1.playSound("uhul");

                                }
                                if (combo > 1)
                                {
                                    //DefaultFX comboFX = new FXLabel(entity.position.X, entity.position.Y, "X" + combo.ToString(), new Color(162, 142, 250));
                                    DefaultFX comboFX = new FXLabel(entity.position.X, entity.position.Y, "X" + combo.ToString(), Color.White, new Color(63, 53, 127));
                                    GameScreen.getFXLayer().add(comboFX);
                                    comboFX.init(content);
                                    if (combo == 5)
                                    {
                                        GameLoop.mushRain(1);
                                        GameLoop.mushRain(2);
                                       // EffectImage imgEff = new EffectImage(3);
                                       // imgEff.init(content);
                                        //GameScreen.getFXLayer().add(imgEff);
                                    }
                                }

                                int pointss = ((int)(entity.behaviour.points * combo) + (int)levelBehaviour.points) * pointAcress;
                                DefaultFX comboFXP = new FXLabel(240, 420, "+" + (string)(pointss).ToString(), new Color(63, 53, 127), Color.White);
                                GameScreen.getFXLayer().add(comboFXP);
                                comboFXP.init(content);

                                levelPoints += pointss;
                            }
                            entity.dead = true;
                            collideCallback();
                        }
                    }
                }
            }
        }



    }
}
#region
//if (touch.State == TouchLocationState.Released && ableToJump)
//{
//    position.X = touch.Position.X - 30;
//    if (position.X > 380)
//        spriteEffects = SpriteEffects.FlipHorizontally;
//    else
//        spriteEffects = SpriteEffects.None;
//    spriteSheet.play("JUMP");
//    ableToJump = false;
//    jump();
//}



// velocity.Y = -(float)Math.Abs((float)Math.Cos(angle) * distance * 0.03f);





//if (yTweener != null)
//          {
//              yTweener.Update(gameTime);
//              if (yTweener != null)
//              position.Y = yTweener.Position;
//          }
//          if (xTweener != null)
//          {
//              xTweener.Update(gameTime);
//              if (xTweener != null)
//              position.X = xTweener.Position;
//          }
//          if (xTweener == null || yTweener == null)
//          {
//              if (position.X > 800 - centerPosition.X - acceleration)
//              {
//                  spriteEffects = SpriteEffects.FlipHorizontally;
//                  acceleration = -Math.Abs(acceleration * 0.2f);
//                  leftMov = true;
//              }
//              else if (position.X < 0 + centerPosition.X + acceleration)
//              {
//                  spriteEffects = SpriteEffects.None;
//                  acceleration = Math.Abs(acceleration * 0.2f);
//                  leftMov = false;
//              }
//              position.X += acceleration;
//              if (!leftMov)
//              {
//                  if (acceleration > 0)
//                  {
//                      acceleration -= .1f;
//                  }
//                  else acceleration = 0;
//              }
//              else
//              {
//                  if (acceleration < 0)
//                  {
//                      acceleration += .1f;
//                  }
//                  else acceleration = 0;
//              }

//          }
#endregion