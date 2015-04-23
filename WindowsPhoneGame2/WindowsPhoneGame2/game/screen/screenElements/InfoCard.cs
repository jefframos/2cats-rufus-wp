using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsPhoneGame2.game.entity;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using WindowsPhoneGame2.game.entity.rufus;
using XNATweener;
using WindowsPhoneGame2.game.utils;
using WindowsPhoneGame2.framework.primitives;
using WindowsPhoneGame2.game.factory.GUI;
using Microsoft.Xna.Framework.Input.Touch;
using WindowsPhoneGame2.framework.GUI.button;
using WindowsPhoneGame2.game.screen.shopPages;
using WindowsPhoneGame2.game.entity.rufus.behaviours;
using RufusAndTheMagicMushrooms.framework.storage;

namespace WindowsPhoneGame2.game.screen.screenElements
{
    class InfoCard : StaticObject
    {
        private ContentManager content;
        private StaticObject backShape;
        public bool isActive { get; set; }
        private Tweener tweener;
        private StaticObject mainImage;
        private LinkedList<Drawable> cardImages;
        private ArrowButton backButton;
        private SimpleButton buyButton;
        private string titleLabel;
        private string descLabel;
        private string firstAtt;
        private string secAtt;
        private string thrAtt;

        private StaticObject firstSymbol;
        private StaticObject secSymbol;
        private StaticObject thrSymbol;

        private RufusModel currentModel;

        private StaticObject star;
        private int type;

        public InfoCard()
            : base(".\\Sprites\\GUI\\backCard")
        {
            backShape = new StaticObject(".\\Sprites\\GUI\\backPauseShape");
            cardImages = new LinkedList<Drawable>();
            titleLabel = "RUFUS";
            descLabel = "EU SOU O RUFUS E BLEHHH";
            firstAtt = "BOUNCE";
            secAtt = "GRAVITY";
            thrAtt = "SPEED";

            firstSymbol = new StaticObject(".\\Sprites\\GUI\\symbols\\equalSymbol");
            secSymbol = new StaticObject(".\\Sprites\\GUI\\symbols\\equalSymbol");
            thrSymbol = new StaticObject(".\\Sprites\\GUI\\symbols\\equalSymbol");

            star = new StaticObject(".\\Sprites\\GUI\\shopScreen\\miniStar");
        }
        public override void init(ContentManager Content)
        {
            content = Content;
            base.init(Content);
            backShape.init(content);
            isActive = false;
            position.X = 800 / 2 - texture.Width / 2;
            position.Y = -texture.Height;
            mainImage = new StaticObject(GameModel.rufusModels.ElementAt(0).largePath);
            mainImage.init(content);
            star.init(content);


            firstSymbol.init(content);
            secSymbol.init(content);
            thrSymbol.init(content);

            backButton = new ArrowButton(hide);
            backButton.init(Content);



            ButtonModel defaultButtonModel = new ButtonModel(".\\Sprites\\GUI\\defaultButton\\shapeButton", ".\\Sprites\\GUI\\defaultButton\\shapeButtonXML", ButtonFactory.spriteFont);

            buyButton = new SimpleButton(defaultButtonModel, "BUY", new Point(186, 48), buyItem);
            buyButton.init(content);
            buyButton.fontScale = .9f;
            buyButton.fontMargin = new Vector2(60, 2);

            cardImages.AddLast(mainImage);
            cardImages.AddLast(backButton);
            cardImages.AddLast(buyButton);
            cardImages.AddLast(firstSymbol);
            cardImages.AddLast(secSymbol);
            cardImages.AddLast(thrSymbol);
            cardImages.AddLast(star);


            isActive = false;

        }
        public void show(RufusModel rufusModel, int _type)
        {
            type = _type;
            if (type == ItemBoxModel.WEARS_ID)
            {
                firstAtt = "BOUNCE";
                secAtt = "GRAVITY";
                thrAtt = "SPEED";

                if (rufusModel.behaviour.bounce == AbstractBehaviour.DEFAULT_BOUNCE)
                    firstSymbol.reload(".\\Sprites\\GUI\\symbols\\equalSymbol");
                else if (rufusModel.behaviour.bounce < AbstractBehaviour.DEFAULT_BOUNCE)
                    firstSymbol.reload(".\\Sprites\\GUI\\symbols\\minusSymbol");
                else 
                    firstSymbol.reload(".\\Sprites\\GUI\\symbols\\plusSymbol");
                firstSymbol.init(content);

                if (rufusModel.behaviour.gravity == AbstractBehaviour.DEFAULT_GRAVITY)
                    secSymbol.reload(".\\Sprites\\GUI\\symbols\\equalSymbol");
                else if (rufusModel.behaviour.gravity < AbstractBehaviour.DEFAULT_GRAVITY)
                    secSymbol.reload(".\\Sprites\\GUI\\symbols\\minusSymbol");
                else
                    secSymbol.reload(".\\Sprites\\GUI\\symbols\\plusSymbol");
                secSymbol.init(content);

                if (rufusModel.behaviour.force == AbstractBehaviour.DEFAULT_SPEED)
                    thrSymbol.reload(".\\Sprites\\GUI\\symbols\\equalSymbol");
                else if (rufusModel.behaviour.force < AbstractBehaviour.DEFAULT_SPEED)
                    thrSymbol.reload(".\\Sprites\\GUI\\symbols\\minusSymbol");
                else
                    thrSymbol.reload(".\\Sprites\\GUI\\symbols\\plusSymbol");
                thrSymbol.init(content);
            }
            else if (type == ItemBoxModel.ITENS_ID)
            {
                firstAtt = "FREQUENCY";
                secAtt = "POINTS";
                thrAtt = "SPEED";

                if (rufusModel.behaviour.frequency == AbstractBehaviour.DEFAULT_FREQUENCY)
                    firstSymbol.reload(".\\Sprites\\GUI\\symbols\\equalSymbol");
                else if (rufusModel.behaviour.frequency < AbstractBehaviour.DEFAULT_FREQUENCY)
                    firstSymbol.reload(".\\Sprites\\GUI\\symbols\\minusSymbol");
                else
                    firstSymbol.reload(".\\Sprites\\GUI\\symbols\\plusSymbol");
                firstSymbol.init(content);

                if (rufusModel.behaviour.points == AbstractBehaviour.DEFAULT_POINTS)
                    secSymbol.reload(".\\Sprites\\GUI\\symbols\\equalSymbol");
                else if (rufusModel.behaviour.points < AbstractBehaviour.DEFAULT_POINTS)
                    secSymbol.reload(".\\Sprites\\GUI\\symbols\\minusSymbol");
                else
                    secSymbol.reload(".\\Sprites\\GUI\\symbols\\plusSymbol");
                secSymbol.init(content);

                if (rufusModel.behaviour.force == AbstractBehaviour.DEFAULT_SPEED)
                    thrSymbol.reload(".\\Sprites\\GUI\\symbols\\equalSymbol");
                else if (rufusModel.behaviour.force < AbstractBehaviour.DEFAULT_SPEED)
                    thrSymbol.reload(".\\Sprites\\GUI\\symbols\\minusSymbol");
                else
                    thrSymbol.reload(".\\Sprites\\GUI\\symbols\\plusSymbol");
                thrSymbol.init(content);
            }
            else if (type == ItemBoxModel.LEVELS_ID)
            {
                firstAtt = "GRAVITY";
                secAtt = "POINTS";
                thrAtt = "ENEMY FREQ.";

                if (rufusModel.behaviour.gravity == AbstractBehaviour.DEFAULT_GRAVITY)
                    firstSymbol.reload(".\\Sprites\\GUI\\symbols\\equalSymbol");
                else if (rufusModel.behaviour.gravity < AbstractBehaviour.DEFAULT_GRAVITY)
                    firstSymbol.reload(".\\Sprites\\GUI\\symbols\\minusSymbol");
                else
                    firstSymbol.reload(".\\Sprites\\GUI\\symbols\\plusSymbol");
                firstSymbol.init(content);

                if (rufusModel.behaviour.points == AbstractBehaviour.DEFAULT_POINTS)
                    secSymbol.reload(".\\Sprites\\GUI\\symbols\\equalSymbol");
                else if (rufusModel.behaviour.points < AbstractBehaviour.DEFAULT_POINTS)
                    secSymbol.reload(".\\Sprites\\GUI\\symbols\\minusSymbol");
                else
                    secSymbol.reload(".\\Sprites\\GUI\\symbols\\plusSymbol");
                secSymbol.init(content);

                if (rufusModel.behaviour.frequency == AbstractBehaviour.DEFAULT_FREQUENCY)
                    thrSymbol.reload(".\\Sprites\\GUI\\symbols\\equalSymbol");
                else if (rufusModel.behaviour.frequency < AbstractBehaviour.DEFAULT_FREQUENCY)
                    thrSymbol.reload(".\\Sprites\\GUI\\symbols\\minusSymbol");
                else
                    thrSymbol.reload(".\\Sprites\\GUI\\symbols\\plusSymbol");
                thrSymbol.init(content);
            }
            currentModel = rufusModel;
            titleLabel = currentModel.title;
            descLabel = currentModel.description;

            mainImage.reload(currentModel.largePath);
            mainImage.init(content);
            tweener = new Tweener(-texture.Height, 100f, 0.4f, Back.EaseOut);
            tweener.Start();
            tweener.endCallback = endShow;

        }
        /**
         *Compra o item 
         */
        public void buyItem()
        {
            if (GameModel.currentPoints > currentModel.price)
            {
                currentModel.isActive = true;
                GameModel.currentPoints -= currentModel.price;
                StorageManager.save("",null);

            }
        }
        public void hide()
        {
            isActive = false;
            tweener = new Tweener(position.Y, -texture.Height, 0.4f, Back.EaseOut);
            tweener.Start();
            tweener.endCallback = endHide;
        }
        public void endShow()
        {
            isActive = true;
        }
        public void endHide()
        {
            currentModel = null;
        }
        public void update(GameTime gameTime, TouchCollection touches)
        {
            if (currentModel != null)
            {
                base.update(gameTime);
                if (tweener != null)
                {
                    tweener.Update(gameTime);
                    backShape.alpha = tweener.Position / 100;
                    position.Y = tweener.Position;

                    mainImage.position = position + new Vector2(33 + 75 - mainImage.texture.Width / 2, 82 + 70 - mainImage.texture.Height / 2);
                    backButton.position = position + new Vector2(10 - 20, 10 - 20);
                    buyButton.position = position + new Vector2(192, 174);

                    firstSymbol.position = position + new Vector2(190, 67); ;
                    secSymbol.position = position + new Vector2(190, 97);
                    thrSymbol.position = position + new Vector2(190, 127);

                    star.position = position + new Vector2(35, 45);

                    backButton.updateButton(gameTime, touches);
                    buyButton.updateButton(gameTime, touches);
                }
            }
        }
        public override void draw(SpriteBatch spriteBatch)
        {
            if (currentModel != null)
            {
                backShape.draw(spriteBatch);
                base.draw(spriteBatch);
                if (ButtonFactory.spriteFont != null && position != null)
                {
                    spriteBatch.DrawString(ButtonFactory.spriteFont, firstAtt, position + new Vector2(220, 74), Color.White, 0f, new Vector2(), .7f, SpriteEffects.None, 0f);
                    spriteBatch.DrawString(ButtonFactory.spriteFont, secAtt, position + new Vector2(220, 104), Color.White, 0f, new Vector2(), .7f, SpriteEffects.None, 0f);
                    spriteBatch.DrawString(ButtonFactory.spriteFont, thrAtt, position + new Vector2(220, 134), Color.White, 0f, new Vector2(), .7f, SpriteEffects.None, 0f);
                    spriteBatch.DrawString(ButtonFactory.spriteFont, titleLabel, position + new Vector2(texture.Width/2 - (titleLabel.Length * 12)/2, 6), Color.White, 0f, new Vector2(), .8f, SpriteEffects.None, 0f);
                    
                    spriteBatch.DrawString(ButtonFactory.spriteFont, descLabel, position + new Vector2(44, 247), Color.White, 0f, new Vector2(), .5f, SpriteEffects.None, 0f);

                    if (!currentModel.isActive)
                        spriteBatch.DrawString(ButtonFactory.spriteFont, currentModel.price.ToString(), position + new Vector2(78, 45), color, 0, new Vector2(), 0.8f, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0f);
                }
                for (int i = 0; i < cardImages.Count; i++)
                {
                    if (currentModel.isActive)
                    {
                        mainImage.color = Color.White;
                        if (cardImages.ElementAt(i) != buyButton && cardImages.ElementAt(i) != star)
                        {
                            cardImages.ElementAt(i).draw(spriteBatch);
                        }
                    }
                    else
                    {
                        mainImage.color = new Color(0, 0, 0);
                        cardImages.ElementAt(i).draw(spriteBatch);
                    }
                }
            }
        }

    }
}
