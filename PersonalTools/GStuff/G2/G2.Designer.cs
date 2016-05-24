namespace PersonalTools.GStuff
{
    partial class G2
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
            this.Test = new System.Windows.Forms.PictureBox();
            this.TurnSelect = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Wintxt = new System.Windows.Forms.Label();
            this.Reset = new System.Windows.Forms.Button();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.newGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Players2 = new System.Windows.Forms.ToolStripMenuItem();
            this.Players3 = new System.Windows.Forms.ToolStripMenuItem();
            this.Players4 = new System.Windows.Forms.ToolStripMenuItem();
            this.Players5 = new System.Windows.Forms.ToolStripMenuItem();
            this.Players6 = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.Test)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TurnSelect)).BeginInit();
            this.menuStrip2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Test
            // 
            this.Test.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Test.BackColor = System.Drawing.SystemColors.Control;
            this.Test.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Test.Location = new System.Drawing.Point(12, 67);
            this.Test.Name = "Test";
            this.Test.Size = new System.Drawing.Size(50, 50);
            this.Test.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.Test.TabIndex = 0;
            this.Test.TabStop = false;
            this.Test.Visible = false;
            this.Test.Click += new System.EventHandler(this.ClickHandler);
            this.Test.MouseEnter += new System.EventHandler(this.Hover);
            this.Test.MouseLeave += new System.EventHandler(this.HoverLeave);
            // 
            // TurnSelect
            // 
            this.TurnSelect.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TurnSelect.Location = new System.Drawing.Point(39, 6);
            this.TurnSelect.Name = "TurnSelect";
            this.TurnSelect.Size = new System.Drawing.Size(52, 52);
            this.TurnSelect.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.TurnSelect.TabIndex = 9;
            this.TurnSelect.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Turn";
            // 
            // Wintxt
            // 
            this.Wintxt.AutoSize = true;
            this.Wintxt.Location = new System.Drawing.Point(41, 61);
            this.Wintxt.Name = "Wintxt";
            this.Wintxt.Size = new System.Drawing.Size(50, 13);
            this.Wintxt.TabIndex = 11;
            this.Wintxt.Text = "Win Text";
            // 
            // Reset
            // 
            this.Reset.Location = new System.Drawing.Point(12, 431);
            this.Reset.Name = "Reset";
            this.Reset.Size = new System.Drawing.Size(371, 53);
            this.Reset.TabIndex = 12;
            this.Reset.Text = "Play again?";
            this.Reset.UseVisualStyleBackColor = true;
            this.Reset.Visible = false;
            this.Reset.Click += new System.EventHandler(this.Reset_Click);
            // 
            // menuStrip2
            // 
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem1,
            this.gameToolStripMenuItem});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(550, 24);
            this.menuStrip2.TabIndex = 15;
            this.menuStrip2.Text = "MainMenuStrip";
            // 
            // fileToolStripMenuItem1
            // 
            this.fileToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGameToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.loadToolStripMenuItem});
            this.fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
            this.fileToolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem1.Text = "File";
            // 
            // newGameToolStripMenuItem
            // 
            this.newGameToolStripMenuItem.Name = "newGameToolStripMenuItem";
            this.newGameToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newGameToolStripMenuItem.Text = "New Game";
            this.newGameToolStripMenuItem.Click += new System.EventHandler(this.newGameToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.loadToolStripMenuItem.Text = "Load";
            // 
            // gameToolStripMenuItem
            // 
            this.gameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playersToolStripMenuItem});
            this.gameToolStripMenuItem.Name = "gameToolStripMenuItem";
            this.gameToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.gameToolStripMenuItem.Text = "Game";
            // 
            // playersToolStripMenuItem
            // 
            this.playersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Players2,
            this.Players3,
            this.Players4,
            this.Players5,
            this.Players6});
            this.playersToolStripMenuItem.Name = "playersToolStripMenuItem";
            this.playersToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.playersToolStripMenuItem.Text = "Players";
            // 
            // Players2
            // 
            this.Players2.Name = "Players2";
            this.Players2.Size = new System.Drawing.Size(80, 22);
            this.Players2.Text = "2";
            this.Players2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // Players3
            // 
            this.Players3.Name = "Players3";
            this.Players3.Size = new System.Drawing.Size(80, 22);
            this.Players3.Text = "3";
            // 
            // Players4
            // 
            this.Players4.Name = "Players4";
            this.Players4.Size = new System.Drawing.Size(80, 22);
            this.Players4.Text = "4";
            // 
            // Players5
            // 
            this.Players5.Name = "Players5";
            this.Players5.Size = new System.Drawing.Size(80, 22);
            this.Players5.Text = "5";
            // 
            // Players6
            // 
            this.Players6.Name = "Players6";
            this.Players6.Size = new System.Drawing.Size(80, 22);
            this.Players6.Text = "6";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.TurnSelect);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.Wintxt);
            this.panel1.Location = new System.Drawing.Point(453, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(97, 102);
            this.panel1.TabIndex = 16;
            // 
            // G2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 496);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip2);
            this.Controls.Add(this.Reset);
            this.Controls.Add(this.Test);
            this.Name = "G2";
            this.Text = "G2";
            this.Load += new System.EventHandler(this.G2_Load);
            this.Resize += new System.EventHandler(this.SizeControl);
            ((System.ComponentModel.ISupportInitialize)(this.Test)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TurnSelect)).EndInit();
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

    

        #endregion

        private System.Windows.Forms.PictureBox Test;
        private System.Windows.Forms.PictureBox TurnSelect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Wintxt;
        private System.Windows.Forms.Button Reset;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem newGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Players2;
        private System.Windows.Forms.ToolStripMenuItem Players3;
        private System.Windows.Forms.ToolStripMenuItem Players4;
        private System.Windows.Forms.ToolStripMenuItem Players5;
        private System.Windows.Forms.ToolStripMenuItem Players6;
        private System.Windows.Forms.Panel panel1;
    }
}