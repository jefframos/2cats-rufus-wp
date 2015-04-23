using Microsoft.Xna.Framework.Graphics;
using WindowsPhoneGame2.framework.primitives;
using Microsoft.Xna.Framework;
using XNATweener;

namespace WindowsPhoneGame2.game.factory.GUI
{
    class IncrementalLabel : Drawable
    {
        private SpriteFont font;
        private int value;
        private int nextValue;
        public Vector2 position;
        public Tweener tweener;
        public float fontSize { get; set; }
        public bool mask { get; set; }
        private string fontPath;
        private int frameCountdow;
        public IncrementalLabel(int _value)
        {
            value = _value;
            fontPath = ".\\Sprites\\GUI\\myriad";
            position = new Vector2();
            fontSize = 1;
            frameCountdow = 0;
            mask = false;

        }
        public IncrementalLabel(int _value, string _fontPath)
        {
            value = _value;
            fontPath = _fontPath;
            position = new Vector2();
            fontSize = 1;
            mask = false;

        }
        public IncrementalLabel(int _value, SpriteFont _font)
        {
            value = _value;
            font = _font;
            position = new Vector2();
            fontSize = 1;
            mask = false;
        }
        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (frameCountdow > 0)
            {
                frameCountdow--;
                if (frameCountdow <= 0)
                    tweener = new Tweener(value, nextValue, 0.5f, Linear.EaseNone);
            }
            if (tweener != null && frameCountdow <= 0)
            {
                tweener.Update(gameTime);
                value = (int)tweener.Position;
            }
        }
        public void newValue(int _newValue)
        {
            if (nextValue != _newValue)
            {
                nextValue = _newValue;
                tweener = new Tweener(value, nextValue, 0.5f, Linear.EaseNone);
            }
        }
        public void newValue(int _newValue, int frameDelay)
        {
            nextValue = _newValue;
            frameCountdow = frameDelay;
        }
        public override void draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            if (mask)
            {
                string maskVaue;

                if (value < 10)
                    maskVaue = "000" + value.ToString();
                else if (value < 100)
                    maskVaue = "00" + value.ToString();
                else if (value < 1000)
                    maskVaue = "0" + value.ToString();
                else
                    maskVaue = value.ToString();

                spriteBatch.DrawString(font, maskVaue, position, Color.White, 0, new Vector2(), fontSize, SpriteEffects.None, 0f);

            }
            else
                spriteBatch.DrawString(font, value.ToString(), position, Color.White, 0, new Vector2(), fontSize, SpriteEffects.None, 0f);
        }
        public override void init(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            if (font == null)
                font = Content.Load<SpriteFont>(fontPath);
        }
    }
}
