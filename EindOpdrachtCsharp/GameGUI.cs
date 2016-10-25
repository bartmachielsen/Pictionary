using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using EindOpdrachtCsharp.ConnectionManagers;

namespace EindOpdrachtCsharp
{
    public partial class GameGUI : Form
    {
        private double x = -1;
        private double y = -1;
        

        private List<DrawPoint> alreadySelected = new List<DrawPoint>();

        private enum mode { DRAW, RECT, TRIANGLE, LINE, GUM };
        mode currentmode = mode.DRAW;
        
        private Color color = Color.Black;
        private GameClient client;

        private bool canGues = false;

        public GameGUI(GameClient client)
        {
            InitializeComponent();
            drawPanel.MouseMove += mouseEvent;
            drawPanel.MouseClick += mouseClick;
            colorPanel.MouseClick += selectColor;
            this.client = client;
            client.notifyOnData += parseData;
            client.sendData(CommandsToSend.CONNECT);
        }

        public void selectColor(object sender, EventArgs args)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                Color color = colorDialog.Color;
                this.color = color;
                colorPanel.BackColor = color;
            }
        }

        public void parseData(object data, object sender)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() => parseData(data, sender)));

            }
            else
            {
                if (data is CommandsToSend)
                {
                    switch ((CommandsToSend) data)
                    {
                        case CommandsToSend.CLEARPANEL:
                            drawPanel.CreateGraphics().Clear(Color.White);
                            break;
                        case CommandsToSend.BLOCKEDFROMGUESSING:
                            /// SHOW THAT GUESSLIMIT HAS REACHED
                            canGues = false;
                            break;

                        case CommandsToSend.STARTGUESSING:
                            canGues = true;
                            break;


                    }
                }

                if (data is message)
                {
                    message messag = (message) data;
                    switch (messag.command)
                    {
                        case CommandsToSend.CORRECTANSWER:
                            answerResponse(messag.data.ToString(),true);
                            break;

                        case CommandsToSend.WRONGANSWER:
                            answerResponse(messag.data.ToString(), false);
                            break;
                    }
                
                }

                if (data is SessionScore)
                {
                    updateScore((SessionScore) data);
                    canGues = false;                                //  BLOCK GUESSING FROM ANSWERS
                    drawPanel.CreateGraphics().Clear(Color.White);  //  CLEARING PANEL
                }
                if (data is DrawPoint)
                    DrawPoint((DrawPoint) data);
                if (data is SessionDetails)
                    LoadSessionDetails((SessionDetails) data);
                

            }
        }

        public void updateScore(SessionScore score)
        {
            highScores.Items.Clear();
            highScores.Columns[0].Width = highScores.Width;
            foreach (var playerscore in score.playerScore())
            {
                highScores.Items.Add(playerscore.ToString());
            }
        }

        public void answerResponse(string givenAnswer, bool correct)
        {
            Color color = Color.Red;
            if (correct)
                color = Color.Green;
            foreach (ListViewItem item in selectItems.Items)
                if (item.Text == givenAnswer)
                {
                    item.ForeColor = color;
                    return;
                }
        }

        public void LoadSessionDetails(SessionDetails sessionDetails)
        {
            client.answer = null;
            selectItems.Items.Clear();
            drawPanel.CreateGraphics().Clear(Color.White);  //  CLEARING PANEL
            foreach (var value in sessionDetails.options)
            {
                selectItems.Items.Add(value);
            }

            this.sessionDetails.Text = $"sessie {sessionDetails.sessionid}";
            this.playerCount.Text = $"{sessionDetails.participants.Count} deelnemers";
            this.drawerLabel.Text = $"{sessionDetails.drawer} is drawing!";
            this.Text = sessionDetails.name;
            this.connected.Items.Clear();
            this.connected.Items.AddRange(sessionDetails.participants.ToArray());
            if (sessionDetails.isDrawer)
            {
                StateLabel.Text = "Drawer";
                drawPanel.Enabled = false;
            }
            else
                StateLabel.Text = "Watcher";


        }

        private void GameGUI_Load(object sender, EventArgs e)
        {
            selectItems.Columns[0].Width = selectItems.Width-30;
            selectItems.GridLines = false;
        }

        private void mouseClick(object sender, EventArgs args)
        {
            var args2 = (MouseEventArgs)args;
            var currentx = getPoints(args2)[0];
            var currenty = getPoints(args2)[1];
            alreadySelected.Add(new DrawPoint(currentx, currenty));
            switch (currentmode)
            {
                case mode.LINE:
                    if (alreadySelected.Count > 1)
                    {

                        DrawPoint a = alreadySelected.ElementAt(0);
                        DrawPoint b = alreadySelected.ElementAt(1);
                        DrawPoint c = new DrawPoint(b.x,b.y, a.x, a.y, color, (int)widthBox.Value);
                        DrawPoint(c);
                        if (client.drawer) client.sendData(c);
                        

                        x = currentx;
                        y = currenty;

                        alreadySelected.Clear();
                    }
                    break;

                case mode.TRIANGLE:
                    if (alreadySelected.Count > 1)
                    {
                        DrawPoint a = alreadySelected.ElementAt(0);
                        DrawPoint b = alreadySelected.ElementAt(1);


                        DrawPoint line1 = new DrawPoint(a.x, a.y, b.x, a.y, color, (int)widthBox.Value);
                        DrawPoint line2 = new DrawPoint(b.x, a.y, b.x, b.y, color, (int)widthBox.Value);
                        DrawPoint line3 = new DrawPoint(b.x, b.y, a.x, a.y, color, (int)widthBox.Value);

                        DrawPoint(line1);
                        DrawPoint(line2);
                        DrawPoint(line3);

                        client.sendData(line1);
                        client.sendData(line2);
                        client.sendData(line3);

                        alreadySelected.Clear();
                    }

                    break;

            


                case mode.RECT:
                    if (alreadySelected.Count > 1)
                    {
                        DrawPoint a = alreadySelected.ElementAt(0);
                        DrawPoint b = alreadySelected.ElementAt(1);



                        DrawPoint line1 = new DrawPoint(a.x, a.y, a.x, b.y, color, (int)widthBox.Value);
                        DrawPoint line2 = new DrawPoint(a.x, b.y, b.x, b.y, color, (int)widthBox.Value);
                        DrawPoint line3 = new DrawPoint(b.x, b.y, b.x, a.y, color, (int)widthBox.Value);
                        DrawPoint line4 = new DrawPoint(b.x, a.y, a.x, a.y, color, (int)widthBox.Value);

                        DrawPoint(line1);
                        DrawPoint(line2);
                        DrawPoint(line3);
                        DrawPoint(line4);

                        client.sendData(line1);
                        client.sendData(line2);
                        client.sendData(line3);
                        client.sendData(line4);

                        alreadySelected.Clear();
                    }

                    break;
            }

        }

        private double[] getPoints(MouseEventArgs args)
        {
            var args2 = (MouseEventArgs)args;
            var currentx = args2.X / (double)drawPanel.Width * 100;
            var currenty = args2.Y / (double)drawPanel.Height * 100;
            return new[] {currentx, currenty};
        }


        private void mouseEvent(object sender, EventArgs args)
        {
            if (args is MouseEventArgs)
            {
                var args2 = (MouseEventArgs) args;
                var currentx = getPoints(args2)[0];
                var currenty = getPoints(args2)[1];
                if ((args2.Button & MouseButtons.Left) != 0)
                {
                    if ((currentx != x) || (currenty != y))
                        if ((currentx >= 0) && (currenty >= 0) && (currenty <= 100.0) && (currentx <= 100.0))
                        {
                            switch (currentmode)
                            {
                                case mode.DRAW:
                                    var point = new DrawPoint(currentx, currenty, x, y, color,(int)widthBox.Value);
                                    if (client.drawer) client.sendData(point);
                                    DrawPoint(point);
                                    x = currentx;
                                    y = currenty;
                                    break;

                                case mode.GUM:
                                    var point2 = new DrawPoint(currentx, currenty, x, y, Color.White, 30);
                                    if (client.drawer) client.sendData(point2);
                                    DrawPoint(point2);
                                    x = currentx;
                                    y = currenty;

                                    break;

                            }
                            
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
                var g = drawPanel.CreateGraphics();
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                var pen = new Pen(drawpoint.color, drawpoint.width);
                pen.SetLineCap(LineCap.Round, LineCap.Round, DashCap.Round);
                var totalx = (int) (drawpoint.x/100.0*drawPanel.Width);
                var totaly = (int) (drawpoint.y/100.0*drawPanel.Height);
                var prevx = totalx;
                var prevy = totaly;

                if (drawpoint.prevx != -1)
                {
                    prevx = (int) (drawpoint.prevx/100.0*drawPanel.Width);
                    prevy = (int) ((drawpoint.prevy/100.0)*drawPanel.Height);
                }
                g.DrawLine(pen, totalx, totaly, prevx, prevy);
                
            }
        }

        
        private void clearPanel_click(object sender, EventArgs e)
        {
            drawPanel.CreateGraphics().Clear(Color.White);
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

        private void setAsAnswer(string answer)
        {
            if (client.answer == null)
            {
                client.sendMessage(CommandsToSend.ANSWER, answer);
                StateLabel.Text += $":{answer}";
                drawPanel.Enabled = true;
            }
        }

      

        private void selectItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectItems.SelectedItems.Count <= 0) return;
            var selected = selectItems.SelectedItems[0];
            if (client.drawer)
            {
                setAsAnswer(selected.Text);
                if(client.answer != null)
                    selected.ForeColor = Color.Green;
            }
            else
            {
                if (canGues)
                    client.sendMessage(CommandsToSend.GUESS, selected.Text);
            }
        }

        private void highScores_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            currentmode = mode.DRAW;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            currentmode = mode.LINE;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            currentmode = mode.RECT;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            currentmode = mode.TRIANGLE;
        }

        private void colorPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            currentmode = mode.GUM;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}