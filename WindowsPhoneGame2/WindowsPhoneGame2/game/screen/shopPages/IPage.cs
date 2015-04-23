using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsPhoneGame2.game.screen.shopPages
{
    interface IPage
    {
        void show();
        void hide(Action endCallback);
    }
}
