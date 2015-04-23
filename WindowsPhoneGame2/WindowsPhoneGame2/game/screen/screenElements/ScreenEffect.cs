using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsPhoneGame2.game.entity;
using WindowsPhoneGame2.game.graphics;
using WindowsPhoneGame2.game.graphics.spritesheet;

namespace RufusAndTheMagicMushrooms.game.screen.screenElements
{
    class ScreenEffect:DefaultEntity
    {
        private int type;
        public ScreenEffect()
        {
        }
        public void redraw(int type)
        {
            
            if (type == 1)
            {
                texture = content.Load<Texture2D>(".\\Sprites\\effects\\screenFX\\screeneffect");
                spriteSheetPositions = content.Load<Dictionary<string, Rectangle>>(".\\Sprites\\effects\\screenFX\\screeneffectXML");
            }
            else if (type == 2)
            {
                texture = content.Load<Texture2D>(".\\Sprites\\effects\\screenFX\\screeneffectPlus");
                spriteSheetPositions = content.Load<Dictionary<string, Rectangle>>(".\\Sprites\\effects\\screenFX\\screeneffectPlusXML");
            }
            else if (type == 3)
            {
                texture = content.Load<Texture2D>(".\\Sprites\\effects\\screenFX\\screeneffectMag");
                spriteSheetPositions = content.Load<Dictionary<string, Rectangle>>(".\\Sprites\\effects\\screenFX\\screeneffectMagXML");
            }
            spriteSheet = new SpriteSheet(texture, spriteSheetPositions);
            spriteSheet.addAnimation(new AnimationStruct("IDLE1", AnimationStruct.makeSequence(0, 1), 2, null));
            spriteSheet.play("IDLE1");
            position = new Vector2();
        }
        public override void init(ContentManager Content)
        {
            content = Content;
            if (type == 1)
            {
                texture = Content.Load<Texture2D>(".\\Sprites\\effects\\screenFX\\screeneffect");
                spriteSheetPositions = Content.Load<Dictionary<string, Rectangle>>(".\\Sprites\\effects\\screenFX\\screeneffectXML");
            }
            else if (type == 2)
            {
                texture = Content.Load<Texture2D>(".\\Sprites\\effects\\screenFX\\screeneffectPlus");
                spriteSheetPositions = Content.Load<Dictionary<string, Rectangle>>(".\\Sprites\\effects\\screenFX\\screeneffectPlusXML");
            }
            else if (type == 3)
            {
                texture = Content.Load<Texture2D>(".\\Sprites\\effects\\screenFX\\screeneffectMag");
                spriteSheetPositions = Content.Load<Dictionary<string, Rectangle>>(".\\Sprites\\effects\\screenFX\\screeneffectMagXML");
            }
            spriteSheet = new SpriteSheet(texture, spriteSheetPositions);
            spriteSheet.addAnimation(new AnimationStruct("IDLE1", AnimationStruct.makeSequence(0, 1), 2, null));
            spriteSheet.play("IDLE1");
            position = new Vector2();
        }       
    }
}
