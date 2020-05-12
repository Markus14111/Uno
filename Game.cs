using System;
using System.Collections.Generic;
using System.Drawing.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uno
{
    class Game
    {
        private string topCard = "R0";
        private Pile drawPile = new Pile();
        private Pile[] playerPile;
        Form1 drawing;
        public Game(Form1 form)
        {
            drawing = form;
        }

        private bool isvalid(string card)
        {
            //color condition
            if (card[0] == 'U' || card[0] == topCard[0])
                return true;
            //card condition
            if (card[1] == topCard[1])
                return true;
            //no condition
            return false;
        }

        //draws a card for the current player
        private void draw(int player)
        {
            playerPile[player].addCard(drawPile.draw());
        }
        public void run()
        {
            bool running = true;
            int i = 0;
            //initialize player hands
            playerPile = new Pile[2];
            for (int j = 0; j < playerPile.Length; j++)
                playerPile[j] = new Pile();
            //initialize drawpile
            drawPile.newDeck();
            drawPile.shuffle();
            //main game loop
            while (running)
            {
                //draw
                draw(i);
                Console.WriteLine(playerPile[i]);
                if (drawPile.read().Length == 0)
                    running = false;
                util.wait(500);
                i = (i + 1) % 2;
            }
        }
    }
}
