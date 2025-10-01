using System;
using System.Drawing;
using System.Windows.Forms;

namespace FlappyBirdWinForms
{
    public partial class Form1 : Form
    {
        int pipeSpeed = 8;
        int gravity = 5;
        int score = 0;

        PictureBox flappyBird = new PictureBox();
        PictureBox pipeTop = new PictureBox();
        PictureBox pipeBottom = new PictureBox();
        Label scoreText = new Label();
        Timer gameTimer = new Timer();
        Label startLabel = new Label();

        public Form1()
        {
            InitializeComponent();
            gameTimer.Interval = 20;
            gameTimer.Tick += new EventHandler(GameTimerEvent);
            this.Load += new EventHandler(Form1_Load);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Width = 500;
            this.Height = 600;
            this.Text = "Flappy Bird";

            Panel panel1 = new Panel();
            panel1.Dock = DockStyle.Fill;
            panel1.BackColor = Color.LightBlue;
            this.Controls.Add(panel1);

            Label startLabel = new Label();
            startLabel.Text = "Oyuna başlamak için SPACE tuşuna basınız;
            startLabel.Font = new Font("Arial", 14, FontStyle.Bold);
            startLabel.ForeColor = Color.Black;
            startLabel.BackColor = Color.Transparent;
            startLabel.AutoSize = false;
            startLabel.Size = new Size(400, 60);
            startLabel.TextAlign = ContentAlignment.MiddleCenter;
            startLabel.Location = new Point(50, 250);
            startLabel.Parent = panel1;
            this.Controls.Add(startLabel);

           
            flappyBird.Size = new Size(50, 50);
            flappyBird.Location = new Point(100, 200);
            flappyBird.Image = Properties.Resources.fl;
            flappyBird.SizeMode = PictureBoxSizeMode.StretchImage;
            flappyBird.BackColor = Color.Transparent;
            flappyBird.Parent = panel1;
            this.Controls.Add(flappyBird);

           
            pipeTop.Size = new Size(80, 200);
            pipeTop.Location = new Point(400, 0);
            pipeTop.Image = Properties.Resources.boru;
            pipeTop.SizeMode = PictureBoxSizeMode.StretchImage;
            pipeTop.BackColor = Color.Transparent;
            pipeTop.Parent = panel1;
            this.Controls.Add(pipeTop);

            pipeBottom.Size = new Size(80, 200);
            pipeBottom.Location = new Point(400, 350);
            pipeBottom.Image = Properties.Resources.boru;
            pipeBottom.SizeMode = PictureBoxSizeMode.StretchImage;
            pipeBottom.BackColor = Color.Transparent;
            pipeBottom.Parent = panel1;
            this.Controls.Add(pipeBottom);

      
            scoreText.Text = "Skor: 0";
            scoreText.Font = new Font("Arial", 16, FontStyle.Bold);
            scoreText.Location = new Point(50, 50);
            scoreText.BackColor = Color.Transparent;
            scoreText.Parent = panel1;
            this.Controls.Add(scoreText);


            this.KeyDown += new KeyEventHandler(GameKeyDown);
            this.KeyUp += new KeyEventHandler(GameKeyUp);
        }

        private void GameTimerEvent(object sender, EventArgs e)
        {
            flappyBird.Top += gravity;
            pipeBottom.Left -= pipeSpeed;
            pipeTop.Left -= pipeSpeed;
            scoreText.Text = "Skor: " + score;

            if (pipeBottom.Left < -80)
            {
                pipeBottom.Left = 500;
                score++;
            }
            if (pipeTop.Left < -80)
            {
                pipeTop.Left = 500;
                score++;
            }

            if (flappyBird.Bounds.IntersectsWith(pipeBottom.Bounds) ||
                flappyBird.Bounds.IntersectsWith(pipeTop.Bounds) ||
                flappyBird.Top < 0 ||
                flappyBird.Bounds.IntersectsWith(new Rectangle(0, this.Height - 70, this.Width, 50)))
            {
                EndGame();
            }
        }

        private void GameKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                gravity = -5;

                if (!gameTimer.Enabled)
                {
                    gameTimer.Start();
                    startLabel.Visible = false;
                }
            }
        }

        private void GameKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
                gravity = 5;
        }

        private void EndGame()
        {
            gameTimer.Stop();
            scoreText.Text = "Oyun Bitti! Skor: " + score;
        }
    }
}
