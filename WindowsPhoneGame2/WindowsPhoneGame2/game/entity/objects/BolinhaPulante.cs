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
    class BolinhaPulante : DefaultEntity
    {
        private bool isRunning;
        public BolinhaPulante(int x)
        {
            position.X = x;
            isRunning = false;
            behaviour = new GreenBolinhaBehaviour();

        }
        public override void init(ContentManager Content)
        {
            content = Content;
            texture = Content.Load<Texture2D>(".\\Sprites\\mushrooms\\bolinhaVoadora\\flyingBolinha");
            spriteSheetPositions = Content.Load<Dictionary<string, Rectangle>>(".\\Sprites\\mushrooms\\bolinhaVoadora\\flyingBolinhaXML");
            spriteSheet = new SpriteSheet(texture, spriteSheetPositions);
            spriteSheet.addAnimation(new AnimationStruct("RUN", AnimationStruct.makeSequence(11,12), 10, null));
            spriteSheet.addAnimation(new AnimationStruct("IDLE1", AnimationStruct.makeSequence(0, 10), 15, endIdle));   
            spriteSheet.play("IDLE1");


            centerPosition = new Vector2(spriteSheet[spriteSheet.currentAnimation.frames.ElementAt(0)].Width / 2, spriteSheet[spriteSheet.currentAnimation.frames.ElementAt(0)].Height / 2);


            idRandom = new Random().Next();
            gravity = .1f;
            range = 30;
           // velocity.Y = 2.8f;
            limitBaseY = 325;

            isInitialized = true;

            position.Y = limitBaseY;
           // velocity.Y = -2;
            
        }
        private void endIdle()
        {
            velocity.Y = -6;
            spriteSheet.play("RUN");
        }
        public override DefaultFX getDieEffect()
        {
            //GameLoop.startRain();
            return new FXGreenSmoke(position.X, position.Y);
        }
        public override void update(GameTime gameTime)
        {
            base.update(gameTime);
            gravity = 0;

            if (position.X > 820 || position.X < -50)
                dead = true;

            if (position.Y > 520 || position.Y < -80)
                dead = true;
        }

    }
}
