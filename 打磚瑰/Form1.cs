using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using 打磚瑰.Class;

namespace 打磚瑰
{
    public partial class Form1 : Form
    {
        public static int windowsWidth, windowsHeight;

        Image backgroundImage;

        Ball ball;

        public static Stick stick;

        public static List<Brick> bricks;

        Timer updateTimer;

        public Form1()
        {
            InitializeComponent();

            Size = new Size(600, 800);

            int bias = 15;
            windowsWidth = Width - bias;
            windowsHeight = Height - SystemInformation.ToolWindowCaptionHeight - bias;

            backgroundImage = Image.FromFile("image\\background.jpg");
            backgroundImage = new Bitmap(backgroundImage, new Size(windowsWidth, windowsHeight));

            Setting();

            updateTimer = new Timer();
            updateTimer.Interval = 10;
            updateTimer.Tick += new EventHandler(Run);
            updateTimer.Start();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics G = e.Graphics;

            G.DrawImage(backgroundImage, 0, 0);

            ball.Show(e);

            stick.Show(e);

            foreach (var brick in bricks)
                brick.Show(e);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            stick.x = e.X - stick.w / 2;

            if (stick.x < 0)
                stick.x = 0;

            if (stick.x + stick.w > windowsWidth)
                stick.x = windowsWidth - stick.w;
        }

        void Run(object sender, EventArgs e)
        {
            ball.Move();

            if (IsGameOver() != 0)
                JumpGameOverDialog();

            Invalidate();
        }

        void Setting()
        {
            ball = new Ball();

            stick = new Stick();

            bricks = new List<Brick>();

            ConstructBrick();
        }

        void ConstructBrick()
        {
            int startPosition = windowsWidth / 2 - 175;

            for (int i = 0; i < 7; i++)
                bricks.Add(new Brick(startPosition + i * 50, 180, Color.DodgerBlue));
            for (int i = 0; i < 7; i++)
                bricks.Add(new Brick(startPosition + i * 50, 210, Color.SpringGreen));
            for (int i = 0; i < 7; i++)
                bricks.Add(new Brick(startPosition + i * 50, 240, Color.DodgerBlue));
            for (int i = 0; i < 7; i++)
                bricks.Add(new Brick(startPosition + i * 50, 270, Color.SpringGreen));

            startPosition = windowsWidth / 2 - 125;

            for (int i = 0; i < 5; i++)
                bricks.Add(new Brick(startPosition + i * 50, 360, Color.DodgerBlue));
            for (int i = 0; i < 5; i++)
                bricks.Add(new Brick(startPosition + i * 50, 390, Color.SpringGreen));
            for (int i = 0; i < 5; i++)
                bricks.Add(new Brick(startPosition + i * 50, 420, Color.DodgerBlue));
        }

        int IsGameOver()
        {
            if (bricks.Count == 0)
                return 1;

            if (ball.y > windowsHeight)
                return 2;

            return 0;
        }

        void JumpGameOverDialog()
        {
            Invalidate();

            updateTimer.Stop();

            string text = "你贏了！";

            if (IsGameOver() == 2)
                text = "你輸了！";

            DialogResult dialogResult = MessageBox.Show(text + "\n\n再玩一次？",
                                                        "遊戲結束",
                                                        MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                Setting();

                updateTimer.Start();
            }
            else if (dialogResult == DialogResult.No)
                Environment.Exit(0);
        }
    }
}