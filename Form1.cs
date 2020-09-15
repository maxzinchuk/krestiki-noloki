using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace krestiki_noloki
{
    public partial class Form1 : Form
    {
        public class Gameobject{
            int x;
            int y;
            Bitmap image;
            public Gameobject(int xpos,int ypos, Bitmap GameImage)
            {
                image = GameImage;
                x = xpos;
                y = ypos;
            }
            public void Draw()
            {
                graph.DrawImage(image, x, y);
            }
        }
        public static Graphics graph;
        Pen pen;
        public List<Gameobject> gameobjects = new List<Gameobject>();
        public Form1()
        {
            InitializeComponent();
            pictureBox1.Image = new Bitmap(800, 800);
            graph = Graphics.FromImage(pictureBox1.Image);
            pen = new Pen(Color.White);
            // graph.FillRectangle(pen.Brush,0,0,800,800);
            gameobjects.Add(new Gameobject(0, 0, Properties.Resources.setka));
            draw();
        }

        byte[,] selected = new byte[3, 3]; 

        public void draw()
        {
            graph.FillRectangle(pen.Brush, 0, 0, 800, 800);
            foreach (Gameobject item in gameobjects)
            {
                item.Draw();
            }
            //graph.DrawImage(Properties.Resources.setka, 0, 0);
            pictureBox1.Refresh();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
           
        }
        bool Player = true;
        int winner = 0;

        public void TestForWin()
        {
            winner = 0;
            for (int j = 1; j < 3; j++)
            {
                for (int i = 0; i < 3; i++)
                {
                    if ((selected[i, 0] == j && selected[i, 1] == j && selected[i, 2] == j) || (selected[2, i] == j && selected[1, i] == j && selected[0, i] == j)) { 
                        winner = j;
                        pen.Color = Color.Black;
                        pen.Width = 15;
                        if((selected[i, 0] == j && selected[i, 1] == j && selected[i, 2] == j))
                        {
                            graph.DrawLine(pen, i * 267 + 133, 0, i * 267 + 133, 800);
                        }
                        else
                        {
                            graph.DrawLine(pen, 0, i * 267 + 133, 800, i * 267 + 133);
                        }
                        pen.Color = Color.White;
                        pen.Width = 15;
                        pictureBox1.Refresh();
                    }
                }
                if ((selected[0, 0] == j && selected[1, 1] == j && selected[2, 2] == j) || (selected[2, 0] == j && selected[1, 1] == j && selected[0, 2] == j)) {
                    winner = j;
                    pen.Color = Color.Black;
                    pen.Width = 15;
                    if (!(selected[0, 0] == j && selected[1, 1] == j && selected[2, 2] == j))
                    {
                        graph.DrawLine(pen, 800, 0, 0, 800);
                    }
                    else
                    {
                        graph.DrawLine(pen, 0, 0, 800, 800);
                    }
                    pen.Color = Color.White;
                    pen.Width = 15;
                    pictureBox1.Refresh();
                }

            }
        }

        private bool Contains(byte[,] masive, byte number)
        {
            foreach (int item in masive)
            {
                if(item == number)
                {
                    return true;
                }
            }
            return false;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (winner == 0 && Contains(selected,0))
            {
                int x, y, X = (e.X / 7) * 8, Y = (e.Y / 7) * 8;
                if (X < 267)
                {
                    x = 0;
                }
                else if (X < 534)
                {
                    x = 1;

                }
                else
                {
                    x = 2;

                }
                if (Y < 267)
                {
                    y = 0;
                }
                else if (Y < 534)
                {
                    y = 1;

                }
                else
                {
                    y = 2;

                }
                if (Player)
                {
                    // gameobjects.Add(new Gameobject((e.X / 7) * 8, (e.Y / 7) * 8, Properties.Resources.Krest));
                    if (selected[x, y] == 0)
                    {
                        selected[x, y] = 1;
                        gameobjects.Add(new Gameobject(x * 267, y * 267, Properties.Resources.Krest));
                        Player = false;
                    }
                }
                else
                {
                    if (selected[x, y] == 0)
                    {
                        selected[x, y] = 2;

                        gameobjects.Add(new Gameobject(x * 267, y * 267, Properties.Resources.krug));
                        Player = true;
                    }
                }

                draw();
                TestForWin();
            }
            else
            {
                winner = 0;
                Player = true;
                selected = new byte[3, 3];
                gameobjects.Clear();
                gameobjects.Add(new Gameobject(0, 0, Properties.Resources.setka));
                draw();


            }
            Text = winner + "";
        }
    }
}
