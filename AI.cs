using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Uno
{
    class AI
    {
        Random rand;
        public AI()
        {
            rand = new Random();
        }

        //-------- public functions --------------
        public Tuple<int, bool> getCard(string[] cards, string top)
        {
            return Tuple.Create(rand.Next(-1,cards.Length), true);
        }

        public string chooseColor(string[] cards)
        {
            return util.colors[rand.Next(4)];
        }
    }
}
