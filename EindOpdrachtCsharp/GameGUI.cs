using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace EindOpdrachtCsharp
{
    public partial class GameGUI : Form
    {
        private double x = -1;
        private double y = -1;

        private int paintWidth = 1;
        private Color color = Color.Black;
        private GameClient client;

        public GameGUI(GameClient client)
        {
            InitializeComponent();
            panel1.MouseMove += mouseEvent;
            this.client = client;
            client.drawNotifier += DrawPoint;
        }



        private void GameGUI_Load(object sender, EventArgs e)
        {
        }

        private void mouseEvent(object sender, EventArgs args)
        {
            if (args is MouseEventArgs)
            {
                var args2 = (MouseEventArgs) args;
                var currentx = args2.X/(double) panel1.Width*100;
                var currenty = args2.Y/(double) panel1.Height*100;
                if ((args2.Button & MouseButtons.Left) != 0)
                {
                    if ((currentx != x) || (currenty != y))
                        if ((currentx >= 0) && (currenty >= 0) && (currenty <= 100.0) && (currentx <= 100.0))
                        {
                            //DrawPoint(currentx, currenty);
                            var point = new DrawPoint(currentx, currenty, x, y);
                            client.sendData(point);
                            DrawPoint(point);
                            x = currentx;
                            y = currenty;
                        }
                }
                else
                {
                    x = -1;
                    y = -1;
                }
            }
        }

        public void DrawPoint(DrawPoint drawpoint)
        {
            
            var g = panel1.CreateGraphics();
            var pen = new Pen(color, paintWidth);
            
            var totalx = (int) (drawpoint.x/100.0*panel1.Width);
            var totaly = (int) (drawpoint.y/100.0*panel1.Height);
            var prevx = totalx;
            var prevy = totaly;

            if (drawpoint.prevx != -1)
            {
                prevx = (int) (drawpoint.prevx/100.0*panel1.Width);
                prevy = (int) ((drawpoint.prevy / 100.0)*panel1.Height);
            }
            //g.FillEllipse(new SolidBrush(color),x,y,10,10);
            g.DrawLine(pen,totalx,totaly,prevx,prevy);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}