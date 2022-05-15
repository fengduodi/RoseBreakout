using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace 打磚瑰.Class
{
    class Ball
    {
        int x, size;
        public int y;

        int forward_x, forward_y;

        int rotateDegree;

        Image ballImage;

        public Ball()
        {
            size = 35;

            x = Form1.windowsWidth / 2 - size / 2;
            y = 650;

            rotateDegree = 0;

            ballImage = Image.FromFile("image\\ball.png");
            ballImage = new Bitmap(ballImage, new Size(size, size));

            SetDirection(45);
        }

        public void Move()
        {
            x += forward_x;
            y += forward_y;

            if (x < 0 || x > Form1.windowsWidth - size)
            {
                if (x < 0)
                    x = 0;

                if (x > Form1.windowsWidth - size)
                    x = Form1.windowsWidth - size;

                forward_x *= -1;
            }

            if (y < 0)
            {
                y = 0;

                forward_y *= -1;
            }

            Stick stick = Form1.stick;

            if (x + size / 2 > stick.x && x + size / 2 < stick.x + stick.w &&
                y + size > stick.y && y + size < stick.y + stick.h)
            {
                y = stick.y - size;

                double maxDegree = 150, minDegree = 30;

                double degree = maxDegree - (x + size / 2 - stick.x) * ((maxDegree - minDegree) / (stick.w));

                if (degree > 70 && degree < 88)
                    degree = 70;

                if (degree > 92 && degree < 110)
                    degree = 110;

                SetDirection(degree);
            }

            List<Brick> bricks = Form1.bricks;

            foreach (var brick in bricks.ToArray())
                if (x < brick.x + brick.w && x + size > brick.x &&
                    y < brick.y + brick.h && y + size > brick.y)
                {
                    int dx = Math.Min(Math.Abs(x - (brick.x + brick.w)), Math.Abs(x + size - brick.x));
                    int dy = Math.Min(Math.Abs(y - (brick.y + brick.h)), Math.Abs(y + size - brick.y));

                    if (dx >= dy)
                        forward_y *= -1;

                    if (dx <= dy)
                        forward_x *= -1;

                    bricks.Remove(brick);

                    break;
                }

            rotateDegree = (rotateDegree + 15) % 360;
        }

        public void Show(PaintEventArgs e)
        {
            Graphics G = e.Graphics;

            G.TranslateTransform(x + size / 2, y + size / 2);

            G.RotateTransform(rotateDegree);

            G.DrawImage(ballImage, -size / 2, -size / 2);

            G.ResetTransform();
        }

        void SetDirection(double degree)
        {
            double radian = (Math.PI / 180) * degree;

            int speed = 12;

            forward_x = (int)(Math.Cos(radian) * speed);
            forward_y = -(int)(Math.Sin(radian) * speed);
        }
    }
}