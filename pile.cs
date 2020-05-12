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
    }
}
