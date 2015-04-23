using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsPhoneGame2.game.graphics.spritesheet;
using Microsoft.Xna.Framework;

namespace WindowsPhoneGame2.game.entity.rufus.behaviours
{
    class NormalBehaviour : AbstractBehaviour
    {
        public NormalBehaviour()
        {
            tileSheet = ".\\Sprites\\rufus1\\normalRufus\\normalRufus";
            tileSheetXML = ".\\Sprites\\rufus1\\normalRufus\\normalRufusXML";
            idleFrames = AnimationStruct.makeSequence(0, 3);
            jumpFrames = AnimationStruct.makeSequence(4, 6);
            fallingFrames = AnimationStruct.makeSequence(8, 10);
            downFrames = AnimationStruct.makeSequence(7, 7);
            upFrames = AnimationStruct.makeSequence(6, 6);
            airdyingFrames = AnimationStruct.makeSequence(11, 11);
            dieFrames = AnimationStruct.makeSequence(12, 14);
            dieFrames.AddLast(14);
            dieFrames.AddLast(14);
            dieFrames.AddLast(14);
            gravity = AbstractBehaviour.DEFAULT_GRAVITY;
            force = AbstractBehaviour.DEFAULT_SPEED;
            spriteSheetPosition = new Vector2();
            centerPosition = new Vector2(30, 30);
            range = 30;
            bounce = 0.2f;
        }
    }
}
