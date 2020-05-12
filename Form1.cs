using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Uno
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Pile test = new Pile();
            Console.WriteLine(test);
            test.addCard("B2");
            test.addCard("UC");
            test.addCard("U4");
            test.addCard("R+");
            test.addCard("B4");
            Console.WriteLine(test);
            test.shuffle();
            Console.WriteLine(test);
            Console.WriteLine("drawn: {0}",test.draw());
            Console.WriteLine("drawn: {0}", test.draw(-1));
            Console.WriteLine(test);
        }
    }
}
