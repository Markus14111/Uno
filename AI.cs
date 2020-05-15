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
        public int getCard(string[] cards, string top)
        {
            return rand.Next(-1,cards.Length);
        }

        public string chooseColor(string[] cards)
        {
            return util.colors[rand.Next(4)];
        }
    }
}
