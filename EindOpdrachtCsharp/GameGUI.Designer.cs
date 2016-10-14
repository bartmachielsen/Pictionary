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
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Player1:  1000 (3 pogingen)");
            this.drawPanel = new System.Windows.Forms.Panel();
            this.StateLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.colorPanel = new System.Windows.Forms.Panel();
            this.colorPicker = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.playerCount = new System.Windows.Forms.Label();
            this.sessionDetails = new System.Windows.Forms.Label();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.clearPanel = new System.Windows.Forms.Button();
            this.selectItems = new System.Windows.Forms.ListView();
            this.Option = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.highScores = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // drawPanel
            // 
            this.drawPanel.BackColor = System.Drawing.Color.White;
            this.drawPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.drawPanel.Location = new System.Drawing.Point(0, 3);
            this.drawPanel.Name = "drawPanel";
            this.drawPanel.Size = new System.Drawing.Size(1088, 463);
            this.drawPanel.TabIndex = 0;
            // 
            // StateLabel
            // 
            this.StateLabel.AutoSize = true;
            this.StateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StateLabel.Location = new System.Drawing.Point(1116, 3);
            this.StateLabel.Name = "StateLabel";
            this.StateLabel.Size = new System.Drawing.Size(183, 26);
            this.StateLabel.TabIndex = 2;
            this.StateLabel.Text = "Drawer/Watcher";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.highScores);
            this.groupBox1.Location = new System.Drawing.Point(201, 473);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(252, 157);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Highscores";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.colorPanel);
            this.groupBox2.Controls.Add(this.colorPicker);
            this.groupBox2.Location = new System.Drawing.Point(0, 473);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(83, 157);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Colors";
            // 
            // colorPanel
            // 
            this.colorPanel.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.colorPanel.Location = new System.Drawing.Point(6, 50);
            this.colorPanel.Name = "colorPanel";
            this.colorPanel.Size = new System.Drawing.Size(57, 28);
            this.colorPanel.TabIndex = 5;
            // 
            // colorPicker
            // 
            this.colorPicker.Location = new System.Drawing.Point(6, 84);
            this.colorPicker.Name = "colorPicker";
            this.colorPicker.Size = new System.Drawing.Size(57, 26);
            this.colorPicker.TabIndex = 0;
            this.colorPicker.Text = "pick";
            this.colorPicker.UseVisualStyleBackColor = true;
            this.colorPicker.Click += new System.EventHandler(this.colorpicker_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button3);
            this.groupBox3.Controls.Add(this.button2);
            this.groupBox3.Location = new System.Drawing.Point(89, 473);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(106, 157);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Shapes";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(6, 27);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(94, 26);
            this.button3.TabIndex = 6;
            this.button3.Text = "rechth";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(6, 59);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(94, 26);
            this.button2.TabIndex = 0;
            this.button2.Text = "cirkel";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.playerCount);
            this.groupBox4.Controls.Add(this.sessionDetails);
            this.groupBox4.Location = new System.Drawing.Point(459, 473);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(192, 157);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Lobby Info";
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
            this.sessionDetails.Location = new System.Drawing.Point(27, 33);
            this.sessionDetails.Name = "sessionDetails";
            this.sessionDetails.Size = new System.Drawing.Size(80, 20);
            this.sessionDetails.TabIndex = 0;
            this.sessionDetails.Text = "Sessie {0}";
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 20;
            this.listBox2.Location = new System.Drawing.Point(27, 26);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(158, 124);
            this.listBox2.TabIndex = 7;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.listBox2);
            this.groupBox5.Location = new System.Drawing.Point(669, 473);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(214, 157);
            this.groupBox5.TabIndex = 8;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Connected players";
            // 
            // clearPanel
            // 
            this.clearPanel.Location = new System.Drawing.Point(919, 485);
            this.clearPanel.Name = "clearPanel";
            this.clearPanel.Size = new System.Drawing.Size(133, 55);
            this.clearPanel.TabIndex = 9;
            this.clearPanel.Text = "Clear Panel";
            this.clearPanel.UseVisualStyleBackColor = true;
            this.clearPanel.Click += new System.EventHandler(this.clearPanel_click);
            // 
            // selectItems
            // 
            this.selectItems.Alignment = System.Windows.Forms.ListViewAlignment.Default;
            this.selectItems.AutoArrange = false;
            this.selectItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Option});
            this.selectItems.FullRowSelect = true;
            this.selectItems.GridLines = true;
            this.selectItems.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.selectItems.Location = new System.Drawing.Point(1094, 32);
            this.selectItems.MultiSelect = false;
            this.selectItems.Name = "selectItems";
            this.selectItems.Size = new System.Drawing.Size(249, 612);
            this.selectItems.TabIndex = 10;
            this.selectItems.UseCompatibleStateImageBehavior = false;
            this.selectItems.View = System.Windows.Forms.View.Details;
            this.selectItems.SelectedIndexChanged += new System.EventHandler(this.selectItems_SelectedIndexChanged);
            // 
            // Option
            // 
            this.Option.Width = 100;
            // 
            // highScores
            // 
            this.highScores.AllowColumnReorder = true;
            this.highScores.BackColor = System.Drawing.SystemColors.Control;
            this.highScores.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.highScores.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.highScores.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.highScores.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem2});
            this.highScores.Location = new System.Drawing.Point(7, 27);
            this.highScores.MultiSelect = false;
            this.highScores.Name = "highScores";
            this.highScores.Size = new System.Drawing.Size(239, 124);
            this.highScores.TabIndex = 0;
            this.highScores.UseCompatibleStateImageBehavior = false;
            this.highScores.View = System.Windows.Forms.View.Details;
            this.highScores.SelectedIndexChanged += new System.EventHandler(this.highScores_SelectedIndexChanged);
            // 
            // GameGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1355, 642);
            this.Controls.Add(this.selectItems);
            this.Controls.Add(this.clearPanel);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.StateLabel);
            this.Controls.Add(this.drawPanel);
            this.Name = "GameGUI";
            this.Text = "GameGUI";
            this.Load += new System.EventHandler(this.GameGUI_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel drawPanel;
        private System.Windows.Forms.Label StateLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel colorPanel;
        private System.Windows.Forms.Button colorPicker;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.GroupBox groupBox3;
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
    }
}