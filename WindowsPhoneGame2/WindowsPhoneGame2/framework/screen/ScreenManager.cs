using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using WindowsPhoneGame2.game.utils;

namespace WindowsPhoneGame2.game.screen
{
    class ScreenManager
    {
        private LinkedList<AbstractScreen> screensList;
        public AbstractScreen currentScreen { get; set; }
        public ScreenManager()
        {
            screensList = new LinkedList<AbstractScreen>();
        }
        /**
       * Adiciona uma tela nova
       */
        public void addScreen(AbstractScreen screen)
        {
            screen.screenManager = this;
            screensList.AddLast(screen);
        }
        /**
        * Troca de tela
        */
        public void change(string screenLabel)
        {
           // if (currentScreen != null && screenLabel == currentScreen.screenName)
             //   return;
            for (int i = 0; i < screensList.Count; i++)
            {
                if (screensList.ElementAt(i).screenName == screenLabel)
                {

                    if (screensList.ElementAt(i) == currentScreen)
                    {
                        screensList.ElementAt(i).transitionOut(screensList.ElementAt(i).transitionIn);
                    }
                    else
                   
                    {
                        if (currentScreen != null)
                            currentScreen.transitionOut(screensList.ElementAt(i).transitionIn);
                        else
                            screensList.ElementAt(i).transitionIn();

                    }

                    
                }
            }
        }
        /**
         * Atualiza a tela atual
         */
        public void update(GameTime gameTime)
        {
            if (currentScreen != null)
                currentScreen.update(gameTime);
        }
        /**
        * Desenha a tela atual
        */
        public void draw()
        {
            if (currentScreen != null)
                currentScreen.draw();
        }
    }
}
