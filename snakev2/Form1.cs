using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace snakev2
{
    public partial class Form1 : Form
    {
        private List<SnakePart> Snake = new List<SnakePart>();
        private SnakePart food = new SnakePart();

    
        public Form1()
        {
            InitializeComponent();
            pb1.Image = Image.FromFile("image/background.jpg");
            new Settings();
            timer1.Interval = 1000 / Settings.Speed;
            timer1.Tick += updateScreen;
        }

        private void updateScreen (object sender, EventArgs e)
        {
            if (Settings.GamerOver==true)
            {
                if (Input.KeyPress(Keys.Enter))
                {
                    startGame();
                }
            }
            else
            {
                if (Input.KeyPress(Keys.Right) && Settings.direction != Directions.Left)
                {
                    Settings.direction = Directions.Right;
                }
                else if (Input.KeyPress(Keys.Left) && Settings.direction != Directions.Right)
                {
                    Settings.direction = Directions.Left;
                }
                else if (Input.KeyPress(Keys.Up) && Settings.direction != Directions.Down)
                {
                    Settings.direction = Directions.Up;
                }
                else if (Input.KeyPress(Keys.Down) && Settings.direction != Directions.Up)
                {
                    Settings.direction = Directions.Down;
                }

                movePlayer();
            }
            pb1.Invalidate();
        }

        private void movePlayer()
        {
            for (int i = Snake.Count - 1; i >= 0;i--)
            {
                if (i==0)
                {
                    switch (Settings.direction)
                    {

                        case Directions.Right:
                            Snake[i].X++;
                            break;
                        case Directions.Left:
                            Snake[i].X--;
                            break;
                        case Directions.Up:
                            Snake[i].Y--;
                            break;
                        case Directions.Down:
                            Snake[i].Y++;
                            break;
                    }
                    int maxXpos = pb1.Size.Width / Settings.Width;
                    int maxYpos = pb1.Size.Height / Settings.Height;
                    if (Snake[i].X<0 || Snake[i].Y<0 || Snake[i].X>maxXpos || Snake[i].Y>maxYpos)
                    {
                        die();
                    }

                    for (int j = 1; j < Snake.Count; j++)
                    {
                        if (Snake[i].X == Snake[j].X && Snake[i].Y == Snake[j].Y)
                        {
                            die();
                        }
                    }

                    if (Snake[0].X == food.X && Snake[0].Y == food.Y)
                    {
                        eat();
                    }
                }
                else
                {
                    Snake[i].X = Snake[i - 1].X;
                    Snake[i].Y = Snake[i - 1].Y;
                }
            }
        }
        private void keyisdown(object sender, KeyEventArgs e)
        {
            Input.changeState(e.KeyCode, true);
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            Input.changeState(e.KeyCode, false);
        }

        private void updateGraphics(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;
            if (Settings.GamerOver==false)
            {
                Brush snakeColour;
                for (int i = 0; i < Snake.Count; i++)
                {
                    if (i == 0)
                    {
                        snakeColour = Brushes.Black;
                    }
                    else
                    {
                        snakeColour = Brushes.Green;
                    }

                    canvas.FillEllipse(snakeColour, new Rectangle(Snake[i].X * Settings.Width,
                    Snake[i].Y * Settings.Height,
                    Settings.Width, Settings.Height));

                    canvas.FillEllipse(Brushes.Red, new Rectangle(food.X * Settings.Width,
                        food.Y * Settings.Height,
                        Settings.Width, Settings.Height));
                }
            }
            else
            {
                string gameOver =  "Game Over";
                label3.Text = gameOver;
                label3.Visible = true;
            }
        }

        private void startGame()
        {
            label3.Visible = false;
            new Settings();
            Snake.Clear();
            SnakePart head = new SnakePart { X = 10, Y = 5 };
            Snake.Add(head);
            for (int i = 0; i < 5; i++)
            {
                SnakePart body = new SnakePart { X = 9 - i, Y = 5 };
                Snake.Add(body);
            }
            label2.Text = Settings.Score.ToString();
            generateFood();
        }

        private void generateFood()
        {
            int maxXpos = pb1.Size.Width / Settings.Width;
            int maxYpos = pb1.Size.Height / Settings.Height;
            Random rnd = new Random();
            food = new SnakePart { X = rnd.Next(0, maxXpos), Y = rnd.Next(0, maxYpos) };
        }

        private void eat()
        {
            SnakePart body = new SnakePart
            {
                X = Snake[Snake.Count - 1].X,
                Y = Snake[Snake.Count - 1].Y
            };
            Snake.Add(body);
            Settings.Score += Settings.Points;
            label2.Text = Settings.Score.ToString(); //id ekéne lekérni majd a pontot
            generateFood();
        }

        private void die()
        {
            Settings.GamerOver = true;
            btnStart.Enabled = true;
            btnLeaderB.Enabled = true;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            btnLeaderB.Enabled = false;
            timer1.Start();
            startGame();
        }
    }
}
