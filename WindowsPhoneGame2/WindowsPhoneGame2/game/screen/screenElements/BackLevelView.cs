using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsPhoneGame2.game.entity;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using WindowsPhoneGame2.game.utils;
using XNATweener;

namespace WindowsPhoneGame2.game.screen.screenElements
{
    class BackLevelView : StaticObject
    {
        //private Vector2 positionCloud;
        private LinkedList<Clouds> clouds;
        private LinkedList<string> cloudsPath;
        private Color backColor;
        private Tweener tweener;
        public BackLevelView(string path)
            : base(path)
        {

        }

        public override void reload(string newPath)
        {
            base.reload(newPath);
        }
        public void addClouds(LinkedList<string> clouds)
        {
            cloudsPath = clouds;
        }
        public override void init(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            base.init(Content);
            if (clouds != null)
                while (clouds.Count > 0)
                    clouds.RemoveFirst();

            // Trace.write(cloudsPath.Count.ToString());
            if (cloudsPath != null)
            {
                clouds = new LinkedList<Clouds>();
                for (int i = 0; i < cloudsPath.Count; i++)
                {
                    Clouds tempCloud = new Clouds();
                    Vector2 temp = new Vector2((i + 1) % 2 == 0 ? -100 : 600, (float)new Random().Next(0, 50));
                    tempCloud.texture = Content.Load<Texture2D>(cloudsPath.ElementAt(i));
                    tempCloud.position = temp;
                    clouds.AddLast(tempCloud);
                }
            }
        }
        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            // positionCloud.X+= 0.1f;

            if (clouds != null)
                for (int i = 0; i < clouds.Count; i++)
                {
                    clouds.ElementAt(i).position.X += (i + 1) % 2 == 0 ? 0.1f : -0.1f;// = new Vector2(, (float)new Random().Next(0, 100));

                    if (clouds.ElementAt(i).position.X < (float)-clouds.ElementAt(i).texture.Width)
                        clouds.ElementAt(i).position.X = 800;

                    if (clouds.ElementAt(i).position.X > 800f)
                        clouds.ElementAt(i).position.X = -clouds.ElementAt(i).texture.Width;
                }

            //for (int j = 0; j < cloudsPosition.Count / 2; j++)
            //{
            //    cloudsPosition.Remove(cloudsPosition.ElementAt(j));
            //}

            base.update(gameTime);

            tweener.Update(gameTime);
            position.Y = tweener.Position;
            //if (position.Y > 0)
            //    position.Y--;
            //else
            //    position.Y = 0;
        }
        public override void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.GraphicsDevice.Clear(backColor);

            if (clouds != null)
            {
                for (int i = 0; i < clouds.Count; i++)
                {
                    //clouds.AddLast(Content.Load<Texture2D>(".\\Sprites\\elements\\giantCloudNight"));
                    spriteBatch.Draw(clouds.ElementAt(i).texture, clouds.ElementAt(i).position, Color.White);
                }
            }

            base.draw(spriteBatch);
        }
        internal void start()
        {
            position.Y = 20;

            tweener = new Tweener(position.Y, 0, .5f, Back.EaseOut);
        }

        internal void setColor(Color color)
        {
            backColor = color;
        }
    }
}
