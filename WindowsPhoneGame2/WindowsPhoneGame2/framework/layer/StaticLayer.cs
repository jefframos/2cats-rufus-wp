using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsPhoneGame2.game.entity;
using Microsoft.Xna.Framework;
using WindowsPhoneGame2.game.utils;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsPhoneGame2.game.layer
{
    class StaticLayer : ILayer
    {
        private LinkedList<StaticObject> objList;
        private string _name { get; set; }
        public StaticLayer(string name)
        {
            this._name = name;
            objList = new LinkedList<StaticObject>();
        }
        /**
        * Adiciona uma entidade na camada
        */
        public void add(StaticObject entity)
        {
            objList.AddLast(entity);
        }
        /**
        * Atualiza as entidades e verifica se eles ainda devem ser atualizadas, caso contrario remove eleas
        */
        public void update(GameTime gameTime)
        {
            if ((objList != null) && (objList.Count > 0))
            {
                for (int i = 0; i < objList.Count; i++)
                {
                    if (objList.ElementAt(i).dead)
                    {
                        objList.Remove(objList.ElementAt(i));
                    }
                    else
                    {
                        objList.ElementAt(i).update(gameTime);
                    }
                }
            }

        }       
        /**
        * draw
        */ 
        public void draw(SpriteBatch spriteBatch)
        {
            if ((objList != null) && (objList.Count > 0))
            {
                for (int i = 0; i < objList.Count; i++)
                {
                    if (!objList.ElementAt(i).dead)
                    {
                        objList.ElementAt(i).draw(spriteBatch);
                    }
                }
            }
        }
        /**
       * Inicializa as entidades que ainda nao foram inicializadas
       */
        public void init(ContentManager Content)
        {
            if ((objList != null) && (objList.Count > 0))
            {
                for (int i = 0; i < objList.Count; i++)
                {
                    if (!objList.ElementAt(i).isInitialized)
                    {
                        objList.ElementAt(i).init(Content);
                    }
                }
            }

        }

    }
}
