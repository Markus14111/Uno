﻿using System;
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
        int Input = -2;
        List<Rectangle> objects;


        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;


            game = new Game(this);

        }

        public int GetInput()
        {
            Input = -2;
            PlayersTurn = true;

            while (Input == -2)
            {
                //wait for mousepress
                Application.DoEvents();
            }

            PlayersTurn = false;
            
            return Input;

        }

        //drawing functions
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            objects = new List<Rectangle>();

            int Size = 100;
            int spacing = 100;
            int offset = 960 - ((game.playerHand().Length / 2) * spacing);

            //Draw Cards on Players Hand
            for (int i = 0; i < game.playerHand().Length; i++)
                DrawCard(e.Graphics, game.playerHand()[i], offset + (i * spacing), 1080 - 39 - ((int)(Size * 1.5)), Size, true);
            
            //draw Draw-Pile
            DrawPile(e.Graphics, 780, 540 - Size, Size);
            //draw TopCard
            DrawCard(e.Graphics, game.get_topCard(), 960, 440, Size, false);

            //Draw Opponent Cards
            for(int i = 0; i < game.get_CPUCards()[0]; i++)
                DrawCard(e.Graphics, "Hidden", offset + (i * 50), 50, Size, false);

        }
        private void DrawCard(Graphics e, string PlayerCard, int pos_x, int pos_y, int Size, bool createobj)
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

            if (PlayerCard[1] == 'C') { x = y + 1; y = 4; }
            if (PlayerCard[1] == '*') { x = y + 6; y = 4; }

            //Universal Colors

            if (PlayerCard[0] == 'U')
            {
                if (PlayerCard[1] == 'C') { x = 0; y = 4; }
                if (PlayerCard[1] == '*') { x = 5; y = 4; }
            }
            if (PlayerCard[1] == 'C') { x = y + 1; y = 4; }
            if (PlayerCard[1] == '*') { x = y + 6; y = 4; }

            if (PlayerCard == "Hidden") { x = 12; y = 4; }

            e.DrawImage(Cards, new Rectangle(pos_x, pos_y, Size, (int)(Size * 1.5)), x * 200, y * 300, 200, 300, GraphicsUnit.Pixel);
            
            if (createobj)
                objects.Add(new Rectangle(pos_x, pos_y, Size, (int)(Size * 1.5)));
            
        }
        private void DrawPile(Graphics e, int pos_x, int pos_y, int Size)
        {            
            //Draw Pile
            int x = 12, y = 4;
            e.DrawImage(Cards, new Rectangle(pos_x, pos_y, Size, (int)(Size * 1.5)), x * 200, y * 300, 200, 300, GraphicsUnit.Pixel);
            e.DrawImage(Cards, new Rectangle(pos_x + 10, pos_y - 5, Size, (int)(Size * 1.5)), x * 200, y * 300, 200, 300, GraphicsUnit.Pixel);
            e.DrawImage(Cards, new Rectangle(pos_x + 20, pos_y - 10, Size, (int)(Size * 1.5)), x * 200, y * 300, 200, 300, GraphicsUnit.Pixel);

            objects.Add(new Rectangle(pos_x, pos_y, Size, (int)(Size * 1.5)));
        }
        private void HighlightInput(Graphics e)
        {
            
        }

        //Get players Input
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (PlayersTurn)
            {
               for(int i = 0; i < objects.Count; i++)
               {
                    if (objects[i].Contains(e.Location))
                    {
                        if (i < objects.Count - 2)
                            Input = i;
                        if (i == objects.Count - 1)
                            Input = -1;
                    }
               }
            }

        }
        //start Main GameLoop
        private void Form1_Shown(object sender, EventArgs e)
        {
            game.run();          
        }
        //clean Exit

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
        //clean Exit
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
