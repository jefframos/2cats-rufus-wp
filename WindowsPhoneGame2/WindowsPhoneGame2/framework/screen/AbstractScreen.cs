using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using WindowsPhoneGame2.framework.primitives;
using Microsoft.Xna.Framework.Input.Touch;
using WindowsPhoneGame2.framework.GUI.button;

namespace WindowsPhoneGame2.game.screen
{
    class AbstractScreen
    {
        public string screenName;
        protected SpriteBatch screenBatch;
        protected ContentManager content;
        public ScreenManager screenManager { get; set; }
        public LinkedList<object> childs;
        protected TouchCollection touches;
        protected Action outCallback;
        public AbstractScreen(string name)
        {
            childs = new LinkedList<object>();
            screenName = name;
        }

        public virtual void build()
        {
            screenManager.currentScreen = this;
            if (screenBatch != null && screenBatch.IsDisposed)
                screenBatch = new SpriteBatch(screenBatch.GraphicsDevice);
        }

        public virtual void addChild(Object child)
        {
            childs.AddLast(child);
        }
        public virtual void transitionIn()
        {
            build();
        }
        public virtual void transitionOut(Action callback)
        {
            outCallback = callback;
            callback();
        }
        public virtual void destroy()
        {
            throw new NotImplementedException();
        }
        public virtual void update(GameTime gameTime)
        {
            touches = TouchPanel.GetState();
        }
        public virtual void dispose()
        {
            while (childs.Count > 0)
                childs.RemoveFirst();
            childs = new LinkedList<object>();
        }
        public virtual void draw()
        {
            throw new NotImplementedException();
        }
        /**
         * atualiza os filhos
         */
        public virtual void updateChilds(GameTime gametime)
        {
            if (childs != null && childs.Count > 0)
            {
                for (int i = 0; i < childs.Count; i++)// object temp in childs)
                {
                    if (childs.ElementAt(i) is Drawable)
                    {
                        if (childs.ElementAt(i) is SimpleButton)
                        {
                            SimpleButton tempBt = (SimpleButton)childs.ElementAt(i);
                            tempBt.updateButton(gametime, touches);
                        }
                        else
                        {
                            Drawable tempDr = (Drawable)childs.ElementAt(i);
                            tempDr.update(gametime);
                        }
                    }
                }
            }
        }
        /**
         * desenha os filhos
         */
        public virtual void drawChilds(SpriteBatch spriteBach)
        {
            if (childs != null)
                foreach (object temp in childs)
                {
                    if (temp is Drawable)
                    {
                        Drawable tempDr = (Drawable)temp;
                        tempDr.draw(spriteBach);
                    }
                }
        }
    }
}
