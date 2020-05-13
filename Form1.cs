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
        bool PlayersTurn = false;
        Tuple<int, int> Inputposition;

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            Inputposition = Tuple.Create(-1, -1);

            game = new Game(this);

        }

        public int GetInput()
        {
            PlayersTurn = true;
            
            while(true)
            {

            }
            //+ draw card -> -1
            // or Name of card -> index card

            PlayersTurn = false;
            return 0;

        }


        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawCards(e.Graphics);
            DrawPiles(e.Graphics);
            HighlightInput(e.Graphics);
            
        }

        private void DrawCards(Graphics e)
        {
            //Size of Card (= width of Card)
            int Size = 100;
            int offset = 50;
            int spacing = 30;
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

                e.DrawImage(Cards, new Rectangle(offset + (i * spacing), Height - 39 - ((int)(Size * 1.5)), Size, (int)(Size * 1.5)), x * 200, y * 300, 200, 300, GraphicsUnit.Pixel);
            }
        }
        private void DrawPiles(Graphics e)
        {
            Console.WriteLine("asd");
            int Size = 100;
            
            //Draw Pile
            int x = 12, y = 4;
            e.DrawImage(Cards, new Rectangle(250, 350, Size, (int)(Size * 1.5)), x * 200, y * 300, 200, 300, GraphicsUnit.Pixel);



        }
        private void HighlightInput(Graphics e)
        {
            //if ()
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (PlayersTurn)
                Inputposition = Tuple.Create(e.X, e.Y);
            Refresh();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            game.run();
        }
    }
}
