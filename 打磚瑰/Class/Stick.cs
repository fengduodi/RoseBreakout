using System.Drawing;
using System.Windows.Forms;

namespace 打磚瑰.Class
{
    public class Stick
    {
        public int x, y, w, h;

        public Stick()
        {
            w = 100;
            h = 20;

            x = Form1.windowsWidth / 2 - w / 2;
            y = Form1.windowsHeight - 80;
        }

        public void Show(PaintEventArgs e)
        {
            Graphics G = e.Graphics;

            G.FillRectangle(new SolidBrush(Color.Black), x, y, w, h);
            G.DrawRectangle(new Pen(Color.White, 2), x + 1, y + 1, w - 2, h - 2);
        }
    }
}