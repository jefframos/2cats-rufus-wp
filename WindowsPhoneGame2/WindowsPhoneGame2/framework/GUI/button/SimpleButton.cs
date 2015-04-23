using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsPhoneGame2.framework.primitives;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using WindowsPhoneGame2.game.graphics;
using Microsoft.Xna.Framework.Input.Touch;
using WindowsPhoneGame2.game.utils;
using XNATweener;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace WindowsPhoneGame2.framework.GUI.button
{
    class SimpleButton : Drawable
    {
        public Vector2 position;
        public Texture2D texture { get; set; }
        public Dictionary<string, Rectangle> spriteSheetPositions { get; set; }
        public SpriteSheet spriteSheet { get; set; }
        public Boolean isInitialized { get; set; }
        private string label;
        private Point size;
        protected Action clickCallback;
        private bool onPressed;
        public Vector2 scale;
        public ButtonModel buttonModel { get; set; }
        public float fontScale { get; set; }
        public Vector2 fontMargin { get; set; }
        public Color fontColor { get; set; }
        public Tweener tweener { get; set; }
        public float rotation { get; set; }
        public float alpha { get; set; }
        public Color color;
        public SpriteEffects spriteEffects { get; set; }
        public bool isActive { get; set; }
        private Rectangle rectArea;
        private string currentPath;
        private ContentManager content;
        public SimpleButton(ButtonModel _buttonModel, string _label, Point _size, Action _clickCallback)
        {
            position = new Vector2();
            fontMargin = new Vector2();
            fontColor = Color.White;
            buttonModel = _buttonModel;
            currentPath = _buttonModel.imagePath;
            scale = new Vector2();
            rectArea = _buttonModel.rectArea;
            label = _label;
            size = _size;
            clickCallback = _clickCallback;
            onPressed = false;
            fontScale = 1;
            rotation = 0;
            spriteEffects = SpriteEffects.None;
            alpha = 1f;
            color = new Color(255, 255, 255, alpha);
            isActive = false;
        }
        public virtual void reload(string newPath)
        {
            currentPath = newPath;
        }
        public virtual void updateButton(GameTime gameTime, TouchCollection touches)
        {
            Boolean inTouch = false;

            foreach (TouchLocation touch in touches)
            {
                inTouch = touchOnBounds(touch.Position);
                if (touch.State == TouchLocationState.Pressed)
                {
                    if (inTouch)
                    {
                        onPressed = true;
                    }
                }
                else if (touch.State == TouchLocationState.Released && onPressed)
                {
                    onPressed = false;
                    if (inTouch && clickCallback != null)
                        clickCallback();
                }
            }
            if (spriteSheet != null)
            {
                if ((onPressed && inTouch) || isActive)
                {
                    if (spriteSheet.currentFrame != 1)
                    {
                        spriteSheet.setFrame(1);
                        if (MediaPlayer.State != MediaState.Paused && (onPressed && inTouch))
                        {
                           // Trace.write(this.currentPath);
                            SoundEffect fx = content.Load<SoundEffect>(".\\sounds\\bup");
                            fx.Play(1f, 1, 1);
                        }
                    }
                }
                else
                {
                    spriteSheet.setFrame(0);
                }
            }

        }
        private bool touchOnBounds(Vector2 touchPosition)
        {
            if (spriteSheetPositions != null)
            {
                if (touchPosition.X > position.X + rectArea.X && touchPosition.X < position.X + scale.X * texture.Width + rectArea.Width &&
                      touchPosition.Y > position.Y + rectArea.Y && touchPosition.Y < position.Y + scale.Y * (texture.Height / spriteSheetPositions.Count) + rectArea.Height)
                    return true;
            }
            else
            {
                if (touchPosition.X > position.X + rectArea.X && touchPosition.X < position.X + scale.X * (texture.Width + rectArea.Width) &&
                       touchPosition.Y > position.Y + rectArea.Y && touchPosition.Y < position.Y + scale.Y * (texture.Height + rectArea.Height))
                    return true;
            }
            return false;
        }
        /**
         *Inicializa o objeto
         */
        public override void init(ContentManager Content)
        {
            content = Content;
            texture = Content.Load<Texture2D>(currentPath);

            if (buttonModel.xmlPath != "")
            {
                spriteSheetPositions = Content.Load<Dictionary<string, Rectangle>>(buttonModel.xmlPath);

                spriteSheet = new SpriteSheet(texture, spriteSheetPositions);
                spriteSheet.setFrame(0);
            }

            if (size.X <= 0 || size.Y <= 0)
                scale = new Vector2(1, 1);
            else
            {
                scale.X = (float)size.X / texture.Width;
                if (spriteSheetPositions != null)
                    scale.Y = (float)size.Y / (texture.Height / spriteSheetPositions.Count);
                else
                    scale.Y = (float)size.Y / texture.Height;
            }
            isInitialized = true;
            color = new Color(255, 255, 255, alpha);
        }
        override public void dispose()
        {
            texture.Dispose();
        }
        override public void draw(SpriteBatch spriteBatch)
        {
            base.draw(spriteBatch);
            if (texture != null && !texture.IsDisposed)
            {
                if (spriteSheet != null)
                {
                    if (spriteSheet.currentAnimation != null)
                        spriteBatch.Draw(texture, position, spriteSheet[spriteSheet.currentAnimation.frames.ElementAt(spriteSheet.currentAnimation.currentID)], new Color((float)color.R, (float)color.G, (float)color.B, alpha), rotation, new Vector2(), scale, spriteEffects, 0f);
                    else
                    {
                        spriteBatch.Draw(texture, position, spriteSheet[spriteSheet.currentFrame], new Color((float)color.R, (float)color.G, (float)color.B, alpha), rotation, new Vector2(), scale, spriteEffects, 0f);
                    }
                }
                else
                {
                    color.A = (byte)(alpha * 255);
                    spriteBatch.Draw(texture, position, new Rectangle((int)0, (int)0, (int)texture.Width, (int)texture.Height), color, rotation, new Vector2(), scale, spriteEffects, 0f);
                    //Trace.write(alpha.ToString());
                }
            }

            if (buttonModel.font != null)
                spriteBatch.DrawString(buttonModel.font, label, new Vector2(position.X + fontMargin.X, position.Y + fontMargin.Y), fontColor, rotation, new Vector2(), fontScale, SpriteEffects.None, 0f);
        }
    }

}
