namespace EindOpdrachtCsharp
{
    partial class GameGUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Player1:  1000 (3 pogingen)");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameGUI));
            this.drawPanel = new System.Windows.Forms.Panel();
            this.StateLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.highScores = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.widthBox = new System.Windows.Forms.NumericUpDown();
            this.button5 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.colorPanel = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.drawerLabel = new System.Windows.Forms.Label();
            this.playerCount = new System.Windows.Forms.Label();
            this.sessionDetails = new System.Windows.Forms.Label();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.clearPanel = new System.Windows.Forms.Button();
            this.selectItems = new System.Windows.Forms.ListView();
            this.Option = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.waitingPicture = new System.Windows.Forms.PictureBox();
            this.waitingLabel = new System.Windows.Forms.Label();
            this.button6 = new System.Windows.Forms.Button();
            this.hintLabel = new System.Windows.Forms.Label();
            this.drawPanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.widthBox)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.waitingPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // drawPanel
            // 
            this.drawPanel.BackColor = System.Drawing.Color.White;
            this.drawPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.drawPanel.Controls.Add(this.hintLabel);
            this.drawPanel.Controls.Add(this.waitingLabel);
            this.drawPanel.Controls.Add(this.waitingPicture);
            this.drawPanel.Location = new System.Drawing.Point(-12, -9);
            this.drawPanel.Name = "drawPanel";
            this.drawPanel.Size = new System.Drawing.Size(1100, 475);
            this.drawPanel.TabIndex = 0;
            // 
            // StateLabel
            // 
            this.StateLabel.AutoSize = true;
            this.StateLabel.BackColor = System.Drawing.Color.Transparent;
            this.StateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StateLabel.ForeColor = System.Drawing.Color.White;
            this.StateLabel.Location = new System.Drawing.Point(1110, 3);
            this.StateLabel.Name = "StateLabel";
            this.StateLabel.Size = new System.Drawing.Size(183, 26);
            this.StateLabel.TabIndex = 2;
            this.StateLabel.Text = "Drawer/Watcher";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.highScores);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(336, 473);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(252, 157);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Highscores";
            // 
            // highScores
            // 
            this.highScores.AllowColumnReorder = true;
            this.highScores.BackColor = System.Drawing.Color.White;
            this.highScores.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.highScores.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.highScores.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.highScores.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.highScores.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.highScores.Location = new System.Drawing.Point(7, 27);
            this.highScores.MultiSelect = false;
            this.highScores.Name = "highScores";
            this.highScores.Size = new System.Drawing.Size(239, 124);
            this.highScores.TabIndex = 0;
            this.highScores.UseCompatibleStateImageBehavior = false;
            this.highScores.View = System.Windows.Forms.View.Details;
            this.highScores.SelectedIndexChanged += new System.EventHandler(this.highScores_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.widthBox);
            this.groupBox2.Controls.Add(this.button5);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.colorPanel);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(0, 473);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(330, 157);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Brush";
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.White;
            this.button4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button4.Location = new System.Drawing.Point(113, 90);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(95, 59);
            this.button4.TabIndex = 8;
            this.button4.Text = "Brush";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // widthBox
            // 
            this.widthBox.BackColor = System.Drawing.Color.White;
            this.widthBox.Location = new System.Drawing.Point(12, 79);
            this.widthBox.Name = "widthBox";
            this.widthBox.Size = new System.Drawing.Size(82, 26);
            this.widthBox.TabIndex = 7;
            this.widthBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.White;
            this.button5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button5.Location = new System.Drawing.Point(12, 117);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(82, 34);
            this.button5.TabIndex = 6;
            this.button5.Text = "Eraser";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.White;
            this.button2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button2.Location = new System.Drawing.Point(230, 90);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(94, 59);
            this.button2.TabIndex = 0;
            this.button2.Text = "Triangle";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // colorPanel
            // 
            this.colorPanel.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.colorPanel.Location = new System.Drawing.Point(12, 27);
            this.colorPanel.Name = "colorPanel";
            this.colorPanel.Size = new System.Drawing.Size(82, 45);
            this.colorPanel.TabIndex = 5;
            this.colorPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.colorPanel_Paint);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.White;
            this.button3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button3.Location = new System.Drawing.Point(230, 25);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(94, 59);
            this.button3.TabIndex = 6;
            this.button3.Text = "Rectangle";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button1.Location = new System.Drawing.Point(113, 25);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 59);
            this.button1.TabIndex = 7;
            this.button1.Text = "Line";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.Transparent;
            this.groupBox4.Controls.Add(this.drawerLabel);
            this.groupBox4.Controls.Add(this.playerCount);
            this.groupBox4.Controls.Add(this.sessionDetails);
            this.groupBox4.ForeColor = System.Drawing.Color.White;
            this.groupBox4.Location = new System.Drawing.Point(594, 473);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(192, 157);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Lobby Info";
            // 
            // drawerLabel
            // 
            this.drawerLabel.AutoSize = true;
            this.drawerLabel.Location = new System.Drawing.Point(31, 90);
            this.drawerLabel.Name = "drawerLabel";
            this.drawerLabel.Size = new System.Drawing.Size(102, 20);
            this.drawerLabel.TabIndex = 2;
            this.drawerLabel.Text = "{0} is Drawer!";
            this.drawerLabel.Click += new System.EventHandler(this.drawerLabel_Click);
            // 
            // playerCount
            // 
            this.playerCount.AutoSize = true;
            this.playerCount.Location = new System.Drawing.Point(31, 64);
            this.playerCount.Name = "playerCount";
            this.playerCount.Size = new System.Drawing.Size(139, 20);
            this.playerCount.TabIndex = 1;
            this.playerCount.Text = "{0} players in lobby";
            // 
            // sessionDetails
            // 
            this.sessionDetails.AutoSize = true;
            this.sessionDetails.ForeColor = System.Drawing.Color.White;
            this.sessionDetails.Location = new System.Drawing.Point(27, 33);
            this.sessionDetails.Name = "sessionDetails";
            this.sessionDetails.Size = new System.Drawing.Size(80, 20);
            this.sessionDetails.TabIndex = 0;
            this.sessionDetails.Text = "Sessie {0}";
            // 
            // listBox2
            // 
            this.listBox2.BackColor = System.Drawing.Color.White;
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 20;
            this.listBox2.Location = new System.Drawing.Point(9, 25);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(158, 124);
            this.listBox2.TabIndex = 7;
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.Color.Transparent;
            this.groupBox5.Controls.Add(this.listBox2);
            this.groupBox5.ForeColor = System.Drawing.Color.White;
            this.groupBox5.Location = new System.Drawing.Point(792, 473);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(187, 157);
            this.groupBox5.TabIndex = 8;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Connected players";
            // 
            // clearPanel
            // 
            this.clearPanel.BackColor = System.Drawing.Color.White;
            this.clearPanel.Location = new System.Drawing.Point(998, 486);
            this.clearPanel.Name = "clearPanel";
            this.clearPanel.Size = new System.Drawing.Size(90, 59);
            this.clearPanel.TabIndex = 9;
            this.clearPanel.Text = "Clear Panel";
            this.clearPanel.UseVisualStyleBackColor = false;
            this.clearPanel.Click += new System.EventHandler(this.clearPanel_click);
            // 
            // selectItems
            // 
            this.selectItems.Alignment = System.Windows.Forms.ListViewAlignment.Default;
            this.selectItems.AutoArrange = false;
            this.selectItems.BackColor = System.Drawing.Color.White;
            this.selectItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Option});
            this.selectItems.FullRowSelect = true;
            this.selectItems.GridLines = true;
            this.selectItems.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.selectItems.Location = new System.Drawing.Point(1106, 32);
            this.selectItems.MultiSelect = false;
            this.selectItems.Name = "selectItems";
            this.selectItems.Size = new System.Drawing.Size(165, 590);
            this.selectItems.TabIndex = 10;
            this.selectItems.UseCompatibleStateImageBehavior = false;
            this.selectItems.View = System.Windows.Forms.View.Details;
            this.selectItems.SelectedIndexChanged += new System.EventHandler(this.selectItems_SelectedIndexChanged);
            // 
            // Option
            // 
            this.Option.Width = 100;
            // 
            // waitingPicture
            // 
            this.waitingPicture.Image = ((System.Drawing.Image)(resources.GetObject("waitingPicture.Image")));
            this.waitingPicture.Location = new System.Drawing.Point(375, 40);
            this.waitingPicture.Name = "waitingPicture";
            this.waitingPicture.Size = new System.Drawing.Size(299, 293);
            this.waitingPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.waitingPicture.TabIndex = 0;
            this.waitingPicture.TabStop = false;
            this.waitingPicture.Visible = false;
            this.waitingPicture.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // waitingLabel
            // 
            this.waitingLabel.AutoSize = true;
            this.waitingLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.waitingLabel.Location = new System.Drawing.Point(255, 351);
            this.waitingLabel.Name = "waitingLabel";
            this.waitingLabel.Size = new System.Drawing.Size(559, 32);
            this.waitingLabel.TabIndex = 1;
            this.waitingLabel.Text = "You Are Waiting For An Available Lobby";
            this.waitingLabel.Visible = false;
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.White;
            this.button6.Location = new System.Drawing.Point(998, 563);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(90, 59);
            this.button6.TabIndex = 11;
            this.button6.Text = "Hint";
            this.button6.UseVisualStyleBackColor = false;
            // 
            // hintLabel
            // 
            this.hintLabel.AutoSize = true;
            this.hintLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hintLabel.ForeColor = System.Drawing.Color.Red;
            this.hintLabel.Location = new System.Drawing.Point(23, 16);
            this.hintLabel.Name = "hintLabel";
            this.hintLabel.Size = new System.Drawing.Size(75, 29);
            this.hintLabel.TabIndex = 2;
            this.hintLabel.Text = "{hint}";
            // 
            // GameGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1289, 642);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.selectItems);
            this.Controls.Add(this.clearPanel);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.StateLabel);
            this.Controls.Add(this.drawPanel);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Name = "GameGUI";
            this.Text = "GameGUI";
            this.Load += new System.EventHandler(this.GameGUI_Load);
            this.drawPanel.ResumeLayout(false);
            this.drawPanel.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.widthBox)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.waitingPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel drawPanel;
        private System.Windows.Forms.Label StateLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel colorPanel;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label playerCount;
        private System.Windows.Forms.Label sessionDetails;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button clearPanel;
        private System.Windows.Forms.ListView selectItems;
        private System.Windows.Forms.ColumnHeader Option;
        private System.Windows.Forms.ListView highScores;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.NumericUpDown widthBox;
        private System.Windows.Forms.Label drawerLabel;
        private System.Windows.Forms.Label waitingLabel;
        private System.Windows.Forms.PictureBox waitingPicture;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label hintLabel;
    }
}