using System.Drawing;
using System.Windows.Forms;

namespace 打磚瑰.Class
{
    public class Brick
    {
        public int x, y, w, h;

        Color color;

        public Brick(int x, int y, Color color)
        {
            this.x = x;
            this.y = y;

            w = 50;
            h = 30;

            this.color = color;
        }

        public void Show(PaintEventArgs e)
        {
            Graphics G = e.Graphics;

            G.FillRectangle(new SolidBrush(color), x, y, w, h);
            G.DrawRectangle(new Pen(Color.Black, 2), x + 1, y + 1, w - 2, h - 2);
        }
    }
}