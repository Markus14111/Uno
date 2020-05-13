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
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;


            Inputposition = Tuple.Create(-1, -1);

            game = new Game(this);

        }

        public int GetInput()
        {
            PlayersTurn = true;

            while(!checkValid(Inputposition))
            {

            }
            
            //+ draw card -> -1
            // or Name of card -> index card

            PlayersTurn = false;
            return 0;

        }


        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            int Size = 100;
            int spacing = 30;
            int offset = Width / 2 - (game.playerHand().Length / 2 * spacing) - ((Size - spacing) / 2);

            //Draw Cards on Players Hand
            for (int i = 0; i < game.playerHand().Length; i++)
                DrawCard(e.Graphics, game.playerHand()[i], offset + (i * spacing), Height - 39 - ((int)(Size * 1.5)), Size);
            
            //draw Draw-Pile
            DrawPile(e.Graphics, (Width / 2) - 180, (Height / 2) - Size, 100);
            //draw TopCard
            DrawCard(e.Graphics, game.get_topCard(), Width / 2, Height / 2 - 100, Size);
            
        }

        private void DrawCard(Graphics e, string PlayerCard, int pos_x, int pos_y, int Size)
        {           
            int x = 0, y = 0;
            
            //Y Position
            if (PlayerCard[0] == 'B') { y = 0; }
            if (PlayerCard[0] == 'G') { y = 1; }
            if (PlayerCard[0] == 'R') { y = 2; }
            if (PlayerCard[0] == 'Y') { y = 3; }

            //X Position
            if (PlayerCard[1] > 47 && PlayerCard[1] < 58) { x = PlayerCard[1] - 48; }
            if (PlayerCard[1] == '+') { x = 10; }
            if (PlayerCard[1] == 'T') { x = 11; }
            if (PlayerCard[1] == 'S') { x = 12; }

            //Universal Colors
            if (PlayerCard[0] == 'U')
            {
                if (PlayerCard[1] == 'C') { x = 0; y = 4; }
                if (PlayerCard[1] == '*') { x = 5; y = 4; }
            }
            if (PlayerCard[1] == 'C') { x = y + 1; y = 4; }
            if (PlayerCard[1] == '*') { x = y + 5; y = 4; }

            e.DrawImage(Cards, new Rectangle(pos_x, pos_y, Size, (int)(Size * 1.5)), x * 200, y * 300, 200, 300, GraphicsUnit.Pixel);
            
        }
        private void DrawPile(Graphics e, int pos_x, int pos_y, int Size)
        {            
            //Draw Pile
            int x = 12, y = 4;
            e.DrawImage(Cards, new Rectangle(pos_x, pos_y, Size, (int)(Size * 1.5)), x * 200, y * 300, 200, 300, GraphicsUnit.Pixel);
            e.DrawImage(Cards, new Rectangle(pos_x + 10, pos_y - 5, Size, (int)(Size * 1.5)), x * 200, y * 300, 200, 300, GraphicsUnit.Pixel);
            e.DrawImage(Cards, new Rectangle(pos_x + 20, pos_y - 10, Size, (int)(Size * 1.5)), x * 200, y * 300, 200, 300, GraphicsUnit.Pixel);
        }

        private void HighlightInput(Graphics e)
        {
            
        }


        private bool checkValid(Tuple<int, int> Input)
        {
            if (true)
                return true;

            return false;

        }

        //Get players Input
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (PlayersTurn)
                Inputposition = Tuple.Create(e.X, e.Y);

            Refresh();
        }
        //start Main GameLoop
        private void Form1_Shown(object sender, EventArgs e)
        {
            game.run();
        }
        //Toggle Fullscreen
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //Toggle Fullscreen
            if (e.KeyCode == Keys.F11)
            {
                if (WindowState == FormWindowState.Normal)
                {
                    FormBorderStyle = FormBorderStyle.None;
                    WindowState = FormWindowState.Maximized;
                }
                else
                {
                    FormBorderStyle = FormBorderStyle.FixedSingle;
                    WindowState = FormWindowState.Normal;
                }
            }
            if (e.KeyCode == Keys.Escape)
            {
                FormBorderStyle = FormBorderStyle.FixedSingle;
                WindowState = FormWindowState.Normal;
            }
        }


    }
}
