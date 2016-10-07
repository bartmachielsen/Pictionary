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
            client.notifyOnData += parseData;
            client.sendData(CommandsToSend.CONNECT);
        }

        public void parseData(object data,object sender)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() => parseData(data,sender)));
            
            }
            else
            {
                if (data is CommandsToSend)
                {
                    switch ((CommandsToSend) data)
                    {
                        case CommandsToSend.CLEARPANEL:
                            Console.WriteLine("CLEARING PANEL");
                            panel1.CreateGraphics().Clear(Color.White);
                            break;
                    }
                }
                if (data is DrawPoint)
                    DrawPoint((DrawPoint)data);
                if (data is SessionDetails)
                    LoadSessionDetails((SessionDetails)data);
            }
        }


        public void LoadSessionDetails(SessionDetails sessionDetails)
        {
            listBox1.Items.Clear();
            listBox1.Items.AddRange(sessionDetails.options);
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
                            var point = new DrawPoint(currentx, currenty, x, y,color);
                            if(client.drawer)client.sendData(point);
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
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() => DrawPoint(drawpoint)));
            }
            else
            {

                var g = panel1.CreateGraphics();
                var pen = new Pen(drawpoint.color, paintWidth);

                var totalx = (int) (drawpoint.x/100.0*panel1.Width);
                var totaly = (int) (drawpoint.y/100.0*panel1.Height);
                var prevx = totalx;
                var prevy = totaly;

                if (drawpoint.prevx != -1)
                {
                    prevx = (int) (drawpoint.prevx/100.0*panel1.Width);
                    prevy = (int) ((drawpoint.prevy/100.0)*panel1.Height);
                }
                g.DrawLine(pen, totalx, totaly, prevx, prevy);
            }
        }

        private void clearPanel_click(object sender, EventArgs e)
        {
            panel1.CreateGraphics().Clear(Color.White);
            if(client.drawer)   client.sendData(CommandsToSend.CLEARPANEL);
        }

        private void colorpicker_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                Color color = colorDialog.Color;
                this.color = color;
                colorPanel.BackColor = color;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(client.drawer)client.sendMessage(CommandsToSend.ANSWER,listBox1.SelectedItem);
            else
            {
                client.sendMessage(CommandsToSend.GUESS, listBox1.SelectedItem);
            }
        }
    }
}