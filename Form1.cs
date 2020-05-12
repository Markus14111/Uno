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
        Bitmap Cards;

        public Form1()
        {
            InitializeComponent();

            Cards = new Bitmap("Cards.png");
            
            Pile test = new Pile();
            test.newDeck();
            Console.WriteLine(test);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}
