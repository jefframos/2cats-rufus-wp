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
    class Layer : ILayer
    {
        public LinkedList<DefaultEntity> entityList;
        private string _name { get; set; }
        public bool updateble { get; set; }
        public bool drawable { get; set; }
        public Layer(string name)
        {
            updateble = true;
            drawable = true;
            this._name = name;
            entityList = new LinkedList<DefaultEntity>();
        }
        /**
        * Adiciona uma entidade na camada
        */
        public void add(DefaultEntity entity)
        {
            entity.parentLayer = this;
            entityList.AddLast(entity);
        }
        /**
        * Inicializa as entidades que ainda nao foram inicializadas
        */
        public void init(ContentManager Content)
        {
            if ((entityList != null) && (entityList.Count > 0))
            {
                for (int i = 0; i < entityList.Count; i++)
                {
                    if (!entityList.ElementAt(i).isInitialized)
                    {
                        entityList.ElementAt(i).init(Content);
                    }
                }
            }

        }
        /**
        * Atualiza as entidades e verifica se eles ainda devem ser atualizadas, caso contrario remove eleas
        */
        public void update(GameTime gameTime)
        {
            if (updateble)
            {
                if ((entityList != null) && (entityList.Count > 0))
                {
                    for (int i = 0; i < entityList.Count; i++)
                    {
                        if (entityList.ElementAt(i).dead)
                        {
                            entityList.Remove(entityList.ElementAt(i));
                        }
                        else
                        {
                            entityList.ElementAt(i).update(gameTime);
                        }
                    }
                }
            }

        }
        /**
         * Collide as camadas envia para os objetos dessa camada as entidades que esta colidiu
         */
        public void collide(Layer layerCollide)
        {
            DefaultEntity collider;
            DefaultEntity mycollider;
            LinkedList<DefaultEntity> inCollideList = new LinkedList<DefaultEntity>();

            for (int i = 0; i < entityList.Count; i++)
            {
                mycollider = entityList.ElementAt(i);

                for (int j = 0; j < entityList.Count; j++)
                {
                    if (mycollider != layerCollide.entityList.ElementAt(j) && mycollider.collidable)
                    {
                        collider = layerCollide.entityList.ElementAt(j);

                        if (collider.collidable && collider != mycollider)
                        {
                            if (Vector2.Distance(new Vector2(mycollider.position.X + mycollider.centerPosition.X, mycollider.position.Y + mycollider.centerPosition.Y), new Vector2(collider.position.X + collider.centerPosition.X, collider.position.Y + collider.centerPosition.Y)) < mycollider.range + collider.range)
                            {
                                inCollideList.AddFirst(collider);
                                Trace.write(collider.ToString());
                            }
                        }
                    }

                }

                mycollider.collide(inCollideList);
            }
        }
        /**
         * Collide as camadas envia para os objetos dessa camada as entidades que esta colidiu
         */
        public void collideEntity(DefaultEntity target, Layer layerCollide)
        {
            DefaultEntity collider;
            DefaultEntity mycollider;
            LinkedList<DefaultEntity> inCollideList = new LinkedList<DefaultEntity>();


            mycollider = target;

            for (int j = 0; j < entityList.Count; j++)
            {
                if (mycollider != layerCollide.entityList.ElementAt(j) && mycollider.collidable)
                {
                    collider = layerCollide.entityList.ElementAt(j);

                    if (collider.collidable && collider != mycollider)
                    {
                        if (Vector2.Distance(new Vector2(mycollider.position.X + mycollider.centerPosition.X, mycollider.position.Y + mycollider.centerPosition.Y), new Vector2(collider.position.X + collider.centerPosition.X, collider.position.Y + collider.centerPosition.Y)) < mycollider.range + collider.range)
                        {
                            inCollideList.AddFirst(collider);
                        }
                    }
                }

                mycollider.collide(inCollideList);
            }
        }
        /**
        * draw
        */
        public void draw(SpriteBatch spriteBatch)
        {
            if (drawable)
            {
                if ((entityList != null) && (entityList.Count > 0))
                {
                    for (int i = 0; i < entityList.Count; i++)
                    {
                        if (!entityList.ElementAt(i).dead)
                        {
                            entityList.ElementAt(i).draw(spriteBatch);
                        }
                    }
                }
            }
        }
    }
}
