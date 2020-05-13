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
        private bool blocked;
        private int forceddraw;
        private const int Playercount = 2;
        int direction;
        Form1 drawing;
        public Game(Form1 form)
        {
            drawing = form;
        }

        //getter
        public string[] playerHand() { return playerPile[0].read(); }

        public string get_topCard() { return topCard; }

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
        private void playCard(string card)
        {
            //U lets you choose a color
            if (card[0] == 'U')
                card = "R" + card[1];     //----------------------------    USER INTERACTION HERE     ---------------------
            //apply special effects of card
            switch (card[1])
            {
                case '+':
                    forceddraw += 2;
                    break;
                case '*':
                    forceddraw += 4;
                    break;
                case 'S':
                    blocked = true;
                    break;
                case 'T':
                    direction = -direction;
                    break;
            }
            topCard = card;
        }

        private string drawCard()
        {
            if (drawPile.read().Length == 0)
                drawPile.newDeck();
            return drawPile.draw();
        }

        // -2%3 = 1  
        private int mod(int number)
        {
            return (number + Playercount) % Playercount;
        }
        public void run()
        {
            bool running = true;
            int i = 0;
            forceddraw = 0;
            blocked = false;
            direction = 1;
            //initialize player hands
            playerPile = new Pile[2];
            for (int j = 0; j < playerPile.Length; j++)
                playerPile[j] = new Pile();
            //initialize drawpile
            drawPile.newDeck();
            //main game loop
            while (running)
            {
                bool valid = false;
                while (!valid)
                {
                    //first check if flags for +2/4 and skipturn
                    if (blocked) { blocked = false; continue; }
                    if (forceddraw > 0)
                    {
                        //TODO ------------------ shift draw to next player (by playing another +2) ----------------------------
                        //draw number of cards
                        for (int j = 0; j < forceddraw; j++)
                            playerPile[i].addCard(drawCard());
                        forceddraw = 0;
                        continue;
                    }
                    //"+" means drawing, add asking for input here
                    string input = "+";             //-----------------------------   USER INTERACTION HERE   ------------------------------
                    if (input == "+")
                    {
                        string card = drawPile.draw();
                        //can drawn card be placed
                        if (isvalid(card))
                            playCard(card);
                        else
                            playerPile[i].addCard(card);
                        //restock drawpile if needed
                        if (drawPile.read().Length == 0)
                            running = false;    
                        valid = true;
                    }
                    else
                    {
                        if(isvalid(input))
                        {
                            playCard(input);
                            valid = true;
                        }
                        else
                            playerPile[i].addCard(input);
                    }
                    //Console.WriteLine("{0}               {1}",topCard,drawPile.read().Length);
                    util.wait(500);
                }
                i = mod(i + direction);
            }
            //Console.WriteLine("Player1: {0}\nPlayer2: {1}", playerPile[0].read().Length, playerPile[1].read().Length);
        }
    }
}
