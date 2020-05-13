using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Uno
{
    public partial class Form1 : Form
    {
        Bitmap Cards = new Bitmap("Cards.png");
        Game game;

        public Form1()
        {
            InitializeComponent();            
            game = new Game(this);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

            //Size of Card (= width of Card)
            int Size = 50;            
            int offset = 50;
            int spacing = 40;
            int x = 0, y = 0;

            string[] PlayerCards = game.playerHand();
            for (int i = 0; i < PlayerCards.Length; i++)
            {
                //Y Position
                if (PlayerCards[i][0] == 'B') { y = 0; }
                if (PlayerCards[i][0] == 'G') { y = 1; }
                if (PlayerCards[i][0] == 'R') { y = 2; }
                if (PlayerCards[i][0] == 'Y') { y = 3; }

                //X Position
                if (PlayerCards[i][1] > 47 && PlayerCards[i][1] < 58) { x = PlayerCards[i][1] - 48; }
                if (PlayerCards[i][1] == '+') { x = 10; }
                if (PlayerCards[i][1] == 'T') { x = 11; }
                if (PlayerCards[i][1] == 'S') { x = 12; }

                //Universal Colors
                if (PlayerCards[i][0] == 'U')
                {
                    if (PlayerCards[i][1] == 'C') { x = 0; y = 4; }
                    if (PlayerCards[i][1] == '*') { x = 5; y = 4; }
                }
                if (PlayerCards[i][1] == 'C') { x = y + 1; y = 4; }
                if (PlayerCards[i][1] == '*') { x = y + 5; y = 4; }

                e.Graphics.DrawImage(Cards, new Rectangle(offset + (i * spacing), Height - 39 - ((int)(Size * 1.5)), Size, (int)(Size * 1.5)), x * 200, y * 300, 200, 300, GraphicsUnit.Pixel);
            }

            
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            game.run();
        }
    }
}
