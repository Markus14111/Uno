using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uno
{
    class Pile
    {
        private List<string> cards;

        public Pile()
        {
            cards = new List<string>();
        }

        public void newDeck()
        {
            cards = new List<string>();
            //add color cards first
            foreach (string color in util.colors)
            {
                //add numbers
                for (int i = 0; i <= 9; i++)
                    addCard(color + i.ToString());
                //add non-number color cards
                addCard(color + "+");
                addCard(color + "S");
                addCard(color + "T");
            }
            addCard("UC");
            addCard("U+");
        }
        //returns array of the current pile state
        public string[] read()
        {
            return cards.ToArray();
        }

        public void addCard(string card)
        {
            cards.Add(card);
        }

        //draws card at a certain index (allows negative and index over pilesize)
        public string draw(int index = 0)
        {
            index = index % cards.Count;
            if (Math.Sign(index) == -1)
                index = cards.Count + index;
            string output = cards[index];
            cards.RemoveAt(index);
            return output;
        }

        public void shuffle()
        {
            List<string> shuffled = new List<string>();
            Random rand = new Random();
            while(cards.Count > 0)
            {
                //draw a random card from cards and put it at the end of the new pile
                shuffled.Add(draw(rand.Next(cards.Count)));
            }
            cards = shuffled;
        }

        public override string ToString()
        {
            if (cards.Count == 0)
                return "Pile is empty";
            string output = "";
            foreach (string card in cards)
            {
                output += card + "\n";
            }
            return output;
        }
    }
}
