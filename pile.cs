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

        //returns array of the current pile state
        public string[] read()
        {
            return cards.ToArray();
        }

        public void addCard(string card)
        {
            cards.Append(card);
        }

        //draws card at a certain index (stadart is top card)
        public string draw(int index = 0)
        {
            string output = cards[index % cards.Count];
            cards.RemoveAt(index % cards.Count);
            return output;
        }

        public void shuffle()
        {
            List<string> shuffled = new List<string>();
            Random rand = new Random();
            while(cards.Count > 0)
            {
                //draw a random card from cards and put it at the end of the new pile
                shuffled.Append(draw(rand.Next(cards.Count)));
            }
            cards = shuffled;
        }

        public override string ToString()
        {
            string output = "";
            foreach (string card in cards)
            {
                output += card + "\n";
            }
            return output;
        }
    }
}
