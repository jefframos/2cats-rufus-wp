using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsPhoneGame2.game.layer;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace WindowsPhoneGame2.framework.layer
{
    class LayerManager
    {
        public LinkedList<ILayer> layerList;
        public LayerManager()
        {
            layerList = new LinkedList<ILayer>();
        }
        /**
         * adiciona uma nova camada
         */
        public void add(ILayer layer)
        {
            layerList.AddLast(layer);
        }
        /**
        * inicializa todas as camadas
        */
        public void init(ContentManager Content)
        {
            foreach (ILayer layer in layerList)
                layer.init(Content);
        }
        /**
        * desenha todas as camadas
        */
        public void draw(SpriteBatch spriteBatch)
        {
            foreach (ILayer layer in layerList)
            {
                layer.draw(spriteBatch);
            }
        }
        /**
        * atualiza todas as camadas
        */
        public void update(GameTime gameTime)
        {
            foreach (ILayer layer in layerList)
                layer.update(gameTime);
        }
    }
}
