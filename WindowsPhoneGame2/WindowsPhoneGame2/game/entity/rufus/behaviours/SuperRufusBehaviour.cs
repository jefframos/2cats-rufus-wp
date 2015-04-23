using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsPhoneGame2.game.graphics.spritesheet;
using Microsoft.Xna.Framework;

namespace WindowsPhoneGame2.game.entity.rufus.behaviours
{
    class SuperRufusBehaviour : AbstractBehaviour
    {
        public SuperRufusBehaviour()
        {
            tileSheet = ".\\Sprites\\rufus1\\rufusSuperman\\rufusSuperman";
            tileSheetXML = ".\\Sprites\\rufus1\\rufusSuperman\\rufusSupermanXML";
            idleFrames = AnimationStruct.makeSequence(0, 2);
            idleFrames.AddLast(1);
            jumpFrames = AnimationStruct.makeSequence(3, 5);
            fallingFrames = AnimationStruct.makeSequence(9, 11);
            downFrames = AnimationStruct.makeSequence(7, 8);
            upFrames = AnimationStruct.makeSequence(5, 6);
            airdyingFrames = AnimationStruct.makeSequence(12, 13);
            dieFrames = AnimationStruct.makeSequence(14, 16);
            gravity = AbstractBehaviour.DEFAULT_GRAVITY * 0.5f;
            force = AbstractBehaviour.DEFAULT_SPEED * 1.01f;
            centerPosition = new Vector2(25, 25);
            spriteSheetPosition = new Vector2(-25,-20);
            range = 40;
            bounce = 0.5f;
            
        }
    }
}
