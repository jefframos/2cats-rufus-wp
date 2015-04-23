using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsPhoneGame2.game.graphics.spritesheet
{
    public class AnimationStruct
    {
        public string name { get; set; }
        public LinkedList<int> frames { get; set; }
        public float timeFrame { get; set; }
        public int currentID { get; set; }
        public Action callback { get; set; }
        public bool repeat { get; set; }
        public static string IDLE = "IDLE";
        public static string JUMP = "JUMP";
        public static string FALLING = "FALLING";
        public static string DOWN = "DOWN";
        public static string UP = "UP";
        public static string AIRDYING = "AIRDYING";
        public static string DIE = "DIE";

        public AnimationStruct(string _name, LinkedList<int> _frames, int _timeFrame, Action _callback)
        {
            callback = _callback;
            name = _name;
            frames = _frames;
            timeFrame = _timeFrame;
            repeat = true;
        }
        public AnimationStruct(string _name, LinkedList<int> _frames, int _timeFrame, Action _callback, bool _repeat)
        {
            callback = _callback;
            name = _name;
            frames = _frames;
            timeFrame = _timeFrame;
            repeat = _repeat;
        }
        public static LinkedList<int> makeSequence(int begin, int end)
        {
            int i;
            LinkedList<int> temp = new LinkedList<int>();
            if (begin > end)
            {
                for (i = begin; i > end; i--)
                    temp.AddLast(i);
            }
            else
            {
                for (i = begin; i <= end; i++)
                    temp.AddLast(i);
            }
            return temp;
        }
    }
}
