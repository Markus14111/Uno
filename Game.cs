using System;
using System.Collections.Generic;
using System.Drawing.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

delegate bool is_Uno();
delegate int get_card();

namespace Uno
{
    struct Player
    {
        public is_Uno Uno;
        public get_card getCard;
    }
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
        Player[] PlayerInterface;
        public Game(Form1 form)
        {
            drawing = form;
        }

        //getter
        public string[] playerHand() { return playerPile[0].read(); }

        public string get_topCard() { return topCard; }

        public int[] get_CPUCards()
        {
            int[] output = new int[Playercount - 1];
            //skip human
            for (int i = 1; i < Playercount; i++)
                output[i - 1] = playerPile[i].read().Length;
            return output;
        }
        //main functions
        private void initPlayers()
        {
            PlayerInterface = new Player[Playercount];
            foreach (Player play in PlayerInterface)
            {
                //Add functions here
            }
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

        //checks for UNO and win
        private bool checkUno(int player)
        {
            //first check for win
            if (playerPile[player].read().Length == 0)
                return false;
            //check for UNO
            if (playerPile[player].read().Length == 1 && PlayerInterface[player].Uno())
                playerPile[player].addCard(drawCard());
            return true;
        }
        public void run()
        {
            bool running = true;
            int i = 0;
            forceddraw = 0;
            blocked = false;
            direction = 1;
            int input = 0;
            string card = "";
            //initialize player hands
            playerPile = new Pile[Playercount];
            for (int j = 0; j < playerPile.Length; j++)
                playerPile[j] = new Pile();
            //initialize drawpile
            drawPile.newDeck();
            //starting hand
            for (int j = 0; j < Playercount; j++)
                for (int k = 0; k < 5; k++)
                    playerPile[j].addCard(drawCard());
            //main game loop
            while (running)
            {
                Console.WriteLine("{0}       {2}        {1}", playerPile[0].read().Length, playerPile[1].read().Length, topCard);
                util.wait(1000);
                //first check if flags for +2/4 and skipturn
                if (blocked) { blocked = false; continue; }
                if (forceddraw > 0)
                {
                    bool draw = true;
                    bool canShift = false;
                    //check if the player CAN play a card
                    foreach (string acard in playerPile[i].read())
                        if ((acard[1] == '+' && isvalid(acard)) || acard[1] == '*')
                            canShift = true;
                    if (canShift)
                    {
                        input = -1;    //-----------------------------   USER INTERACTION HERE   ------------------------------
                        if (input != -1)
                        {
                            card = playerPile[i].draw(i);
                            if ((card[1] == '+' && isvalid(card)) || card[1] == '*')
                            { draw = false; playCard(card); }
                            else
                                playerPile[i].addCard(card);
                        }
                    }
                    if (draw)
                    {
                        //draw number of cards
                        for (int j = 0; j < forceddraw; j++)
                            playerPile[i].addCard(drawCard());
                        forceddraw = 0;
                        continue;
                    }
                }
                //normal move
                bool valid = false;
                while (!valid)
                {
                    //"+" means drawing, add asking for input here
                    input = -1;             //-----------------------------   USER INTERACTION HERE   ------------------------------
                    if (input == -1)
                    {
                        card = drawCard();
                        //can drawn card be placed
                        if (isvalid(card))
                        { playCard(card); }
                        else
                            playerPile[i].addCard(card);   
                        valid = true;
                    }
                    else
                    {
                        card = playerPile[i].draw(input);
                        if (isvalid(card))
                        {
                            playCard(card);
                            running = checkUno(i);
                            valid = true;
                        }
                        else
                            playerPile[i].addCard(card);
                    }
                }
                i = mod(i + direction);
                drawing.Invalidate();
                Application.DoEvents();
            }
        }
    }
}
