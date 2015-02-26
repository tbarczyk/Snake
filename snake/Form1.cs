using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace snake
{
    public partial class Form1 : Form
    {
        bool walls;
        bool start;
        List<SnakePart> snake = new List<SnakePart>();
        int a;
        int b;
        const int part_height = 10;
        const int part_width = 10;
        bool empty;
        bool gameover = false;
        //ArrangeStartingPosition=false;
        int score = 0;
        int direction = 0; //down0 up1 left2 right3 

        SnakePart food = new SnakePart();

        Timer gameloop = new Timer();
        Timer foodloop = new Timer();



        public Form1()
        {
            start = false;
            InitializeComponent();


            gameloop.Interval = 1000 / (int)trackBar1.Value;
            gameloop.Tick += new EventHandler(Game);
            gameloop.Start();
                    
                
               
            
            /*while (true)
                if (start == true) break;
            pictureBox1.Enabled = true;*/
        }

        private void StartGame()
        {
            gameloop.Interval = 1000 / (int)trackBar1.Value;
            snake.Clear();
            snake.Add(new SnakePart(10, 3));
            gameover = false;
            score = 0;
            direction = 0;

            
        }

        private void Generate()
        { 
            Random los = new Random();
            //SnakePart piece = new SnakePart();
            food.X = los.Next(0, pictureBox1.Width  / part_width);
            food.Y = los.Next(0, pictureBox1.Height / part_height);
            empty = false;
        }

        private void ChangeDirection()
        {
            //CHANGING DIRECTION
            if (Input.Pressed(Keys.Left) && direction != 3)
            {
                direction = 2;
            }

            if (Input.Pressed(Keys.Right) && direction != 2)
            {
                direction = 3;
            }

            if (Input.Pressed(Keys.Up) && direction != 0)
            {
                direction = 1;
            }

            if (Input.Pressed(Keys.Down) && direction != 1)
            {
                direction = 0;
            }
        }

        private void Move()
        {

            for (int i = snake.Count - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    if (direction == 0) snake[0].Y++;
                    if (direction == 1) snake[0].Y--;
                    if (direction == 2) snake[0].X--;
                    if (direction == 3) snake[0].X++;
                    
                    
                }
                else
                {
                    
                    if (i > 1 && snake[i].X == snake[0].X && snake[i].Y == snake[0].Y)
                        gameover = true;
         
                    
                    snake[i].X = snake[i - 1].X;
                    snake[i].Y = snake[i - 1].Y;
                }

                if (checkBox1.Checked)
                    if (snake[0].X >= pictureBox1.Width / part_width || snake[0].Y >= pictureBox1.Height / part_height || snake[0].Y <= 0 || snake[0].X <= 0)
                        gameover = true;


              

            }

            if (!checkBox1.Checked)
            {
                if (snake[0].X >= pictureBox1.Width / part_width) snake[0].X = 0;
                if (snake[0].X < 0) snake[0].X = pictureBox1.Width / part_width;
                if (snake[0].Y >= pictureBox1.Height / part_height) snake[0].Y = 0;
                if (snake[0].Y < 0) snake[0].Y = pictureBox1.Height / part_height;
            }
        }

        private void Eat()
        {
            if (snake[0].X == food.X && snake[0].Y == food.Y)
            {
                snake.Add(new SnakePart(snake[snake.Count - 1].X, snake[snake.Count - 1].Y));
                score++;
                Generate();
            }
        }

        private void Game(object sender, EventArgs e)
        {
            if (start)
            {
               
                //Changing direction by arrows
                ChangeDirection();
                //Permament move + free walls
                Move();
                //Eating and score incrementation
                Eat();
                pictureBox1.Refresh();
            }
        }



        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, true);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, false);
        }

        //DRAWING
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (start)
            {
                Graphics grafika = e.Graphics;
                if (gameover)
                {
                    button1.Enabled = true;
                    trackBar1.Enabled = true;
                    checkBox1.Enabled = true;
                    start = false;
                    grafika.DrawString("Gameover", SystemFonts.CaptionFont, Brushes.White, 30, 30);
                    grafika.DrawString("Press Start to play again.", SystemFonts.DialogFont, Brushes.White, 30, 70);        
                }
                else
                {
                    String score_string = "  " + score.ToString();
                    for (int i = 1; i < snake.Count(); i++)
                        grafika.FillRectangle(Brushes.Black, new Rectangle(snake[i].X * part_width, snake[i].Y * part_height, part_width, part_height));
                    grafika.FillRectangle(Brushes.Red, new Rectangle(snake[0].X * part_width, snake[0].Y * part_height, part_width, part_height));
                    grafika.FillRectangle(Brushes.Blue, new Rectangle(food.X * part_width, food.Y * part_height, part_width, part_height));
                    richTextBox1.Text = score_string;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StartGame();
            gameover = false;
            start = true;
            button1.Enabled = false;
            trackBar1.Enabled = false;
            checkBox1.Enabled = false;

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Snake v.1.0 \n\n\n"
            + "TODO: \n\n"
            + "Stop BUTTON\n"
            + "Chosing blocks\n\n\n\n\n\n"
            + "2013                                                     Autor: Tomek"
            + "\n                            Monia <3"
            );


        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gameover = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

    }
}
