using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsPhoneGame2.game.entity;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using WindowsPhoneGame2.game.graphics;
using WindowsPhoneGame2.game.graphics.spritesheet;
using Microsoft.Xna.Framework;

namespace WindowsPhoneGame2.game.FX
{
    class FXFall : DefaultEntity         
    {
        public FXFall(float x, float y)
        {
            position.X = (float)x;
            position.Y = (float)y;
        }
        public override void init(ContentManager Content)
        {
            texture = Content.Load<Texture2D>(".\\Sprites\\effects\\fall\\FXFall");
            spriteSheetPositions = Content.Load<Dictionary<string, Rectangle>>(".\\Sprites\\effects\\fall\\FXFallXML");
            spriteSheet = new SpriteSheet(texture, spriteSheetPositions);
            spriteSheet.addAnimation(new AnimationStruct("INIT", AnimationStruct.makeSequence(0, 9), 6, kill));
            spriteSheet.play("INIT");
            centerPosition = new Vector2(spriteSheet[spriteSheet.currentAnimation.frames.ElementAt(0)].Width / 2, spriteSheet[spriteSheet.currentAnimation.frames.ElementAt(0)].Height / 2);

            idRandom = new Random().Next();
            isInitialized = true;
            collidable = false;

            limitBaseY = 480;
        }
        public override void update(GameTime gameTime)
        {
            velocity = new Vector2();
            gravity = 0f;
            base.update(gameTime);
        }
        public override void kill()
        {
            dead = true;
        }       
    }
}
