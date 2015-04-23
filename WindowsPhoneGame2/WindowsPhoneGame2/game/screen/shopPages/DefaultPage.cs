using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsPhoneGame2.game.entity;
using XNATweener;
using Microsoft.Xna.Framework;
using WindowsPhoneGame2.game.screen.shopPages.elements;
using Microsoft.Xna.Framework.Input.Touch;
using WindowsPhoneGame2.framework.GUI.button;
using WindowsPhoneGame2.game.utils;

namespace WindowsPhoneGame2.game.screen.shopPages
{
    class DefaultPage : StaticObject, IPage
    {
        protected LinkedList<ItemBox> itemList;
        protected Point initPos;
        protected Tweener tweener;
        protected LinkedList<ItemBoxModel> itemsModel;
        public DefaultPage(string path) : base(path) { }
        public override void init(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
           
            base.init(Content);
            itemList = new LinkedList<ItemBox>();

            int lineAcum = 0;
            int colAcum = 0;
            for (int i = 0; i < itemsModel.Count; i++)
            {
                ItemBox tempItem = new ItemBox(position.X + 40f + (118 * colAcum) + (800f - position.X + 40f + (118 * colAcum)), position.Y + 40f + (128 * lineAcum), itemsModel.ElementAt(i));
                tempItem.initPos = new Vector2(position.X + 40f + (118 * colAcum), position.Y + 40f + (128 * lineAcum));
                itemList.AddLast(tempItem);
                tempItem.init(Content);

                colAcum++;
                if (colAcum >= 4)
                {
                    colAcum = 0;
                    lineAcum++;
                }
            }
            position.X = 800;
        }
        public override void draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            base.draw(spriteBatch);
            foreach (SimpleButton btn in itemList)
                btn.draw(spriteBatch);
        }
        public override void dispose()
        {
            foreach (SimpleButton btn in itemList)
            {
                btn.dispose();
            }
            base.dispose();
        }
        public virtual void show()
        {
            float acum = 0f;
            tweener = new Tweener(position.X, initPos.X, .3f + acum, Cubic.EaseOut);
            tweener.Start();
            foreach (SimpleButton btn in itemList)
            {
                btn.tweener = new Tweener(btn.position.X, (btn as ItemBox).initPos.X, .3f + acum, Cubic.EaseOut);
                acum += 0.005f;
                btn.tweener.Start();
            }
        }

        public virtual void hide(Action endCallback)
        {
            float acum = 0f;
            tweener = new Tweener(position.X, 800, .3f + acum, Cubic.EaseIn);
            tweener.Start();
            foreach (SimpleButton btn in itemList)
            {
                btn.tweener = new Tweener(btn.position.X, (btn as ItemBox).endPos.X, .25f + acum, Cubic.EaseIn);
                acum += 0.01f;
                btn.tweener.Start();
            }
            itemList.ElementAt(itemList.Count - 1).tweener.endCallback = endCallback;
        }
        public virtual void update(Microsoft.Xna.Framework.GameTime gameTime, TouchCollection touches)
        {
            if (tweener != null)
            {
                tweener.Update(gameTime);
                position.X = tweener.Position;
            }
            foreach (SimpleButton btn in itemList)
            {
                btn.update(gameTime);
                if (btn.tweener != null)
                {
                    btn.tweener.Update(gameTime);
                    btn.position.X = btn.tweener.Position;
                    btn.updateButton(gameTime, touches);
                }
            }
        }
    }
}
