using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Resources;
namespace PersonalTools.GStuff
{
    public enum GravState
    {
        Off = 0,
        Up = 1,
        Down = 2,
        Left = 3,
        Right = 4,
    }
    public partial class G2 : Form
    {
        /// Connect 4 Multiplayer

        //private

        static GravState Gravity = GravState.Down;
        static int PlayerNum = 1;
        static int PlayerTotal = 2;
        static int Win = 0;
        static int SizeX = 20;
        static int SizeY = 9;
        static int WinRowLength = 4;

        public static int[,] GameArea = new int[SizeX, SizeY];
        static int x;
        static int y;
        System.Drawing.Bitmap LoadImg()
        {
            switch (PlayerNum)
            {
                case 1:
                    return Properties.Resources.O;
                case 2:
                    return Properties.Resources.X;
            }
            return null;
        }

        Color Colour()
        {
            switch (PlayerNum)
            {
                case 1:
                    return Color.Red;
                case 2:
                    return Color.Blue;
                case 3:
                    return Color.Green;
                case 4:
                    return Color.Gold;
                case 5:
                    return Color.Aquamarine;
                case 6:
                    return Color.Brown;
                case 7:
                    return Color.Coral;
                case 8:
                    return Color.Cyan;
                case 9:
                    return Color.Cornsilk;
                case 10:
                    return Color.DarkSeaGreen;
            }
            return Color.Transparent;
        }

        //   ResourceManager rm = Properties.Resources.ResourceManager();
        private void ClickHandler(object sender, EventArgs e)
        {
            if (Win == 0)
            {
                PictureBox USED = (PictureBox)sender;
                GetAddress(sender);
                int LowestAddress = GetLowestEmpty(x, y);
                if (LowestAddress >= 0 && GameArea[x, LowestAddress] == 0)
                {

                    SetLowestImgAnimated();
                    if (!GetCheckWin(USED))
                    {

                        if (PlayerNum >= PlayerTotal)
                        {
                            PlayerNum = 1;
                        }
                        else
                        {
                            PlayerNum++;
                            //                Wintxt.Text = "0's go";
                        }
                        Wintxt.Text = "Player " + PlayerNum + "'s go";
                        TurnSelect.BackColor = Colour();
                    }
                    else
                    {
                        if (Win == 3)
                            Wintxt.Text = "Draw";
                        else
                        {
                            Wintxt.Text = "Winner!";
                        }
                        Reset.Visible = true;
                    }

                }
            }
            Hover(sender, e);
        }

        private void Hover(object sender, System.EventArgs e)
        {
            GetAddress(sender);
            if (Win == 0)
            {
                SetLowestImg();
            }
        }
        private void SetLowestImg()
        {
            int Y = GetLowestEmpty(x, y);
            if (Y >= 0)
                SetImg(x, Y, Color.FromArgb(100, Colour()));
            //((PictureBox)List[0]).Image.Palette =  244;
        }

        private void SetLowestImgAnimated()
        {
            if (Gravity != GravState.Off)
            {
                bool twice = false;
                int Y = GetLowestEmpty(x, y);
                if (Y >= 0)
                {
                    for (int i = 0; i <= Y; i++)
                    {
                        if (twice)
                        {
                            System.Threading.Thread.Sleep(50);
                            SetImg(x, i - 1, Color.Transparent);
                        }
                        else
                            SetImg(x, Y, Color.Transparent);
                        SetImg(x, i, Colour());
                        twice = true;
                    }
                }
            }
        }
        private void SetImg(int X, int Y, Color Col)
        {
            string Address = "X" + X + "Y" + Y;
            Control[] List = Controls.Find(Address, true);
            if (List.Length > 0 && List[0] is PictureBox)
                ((PictureBox)List[0]).BackColor = Col;
            ((PictureBox)List[0]).Update();
        }

        private void HoverLeave(object sender, System.EventArgs e)
        {
            GetAddress(sender);
            int LowestAddress = GetLowestEmpty(x, y);
            if (LowestAddress >= 0 && GameArea[x, LowestAddress] == 0)
            {
                string BottomAddress = "X" + x + "Y" + GetLowestEmpty(x, y);
                Control[] List = Controls.Find(BottomAddress, true);
                foreach (Control Ctrl in List)
                {
                    if (Ctrl is PictureBox)
                    { ((PictureBox)Ctrl).BackColor = Color.Transparent; }
                }

            }
        }

        #region Gets
        void GetAddress(object sender)
        {
            if (sender is PictureBox)
            {
                string[] UsedArray = ((PictureBox)sender).Name.Replace("X", "").Split('Y');
                int.TryParse(UsedArray[0], out x);
                int.TryParse(UsedArray[1], out y);
            }
        }
        int GetLowestEmpty(int X, int Y)
        {
            //This is now gravity dependent
            switch (Gravity)
            {
                case GravState.Off:
                    break;
                case GravState.Up:
                    break;
                case GravState.Down:
                    break;
                case GravState.Left:
                    break;
                case GravState.Right:
                    break;
            }
            for (int i = 0; i < SizeY; i++)
            {
                if (GameArea[X, i] != 0)
                {

                    return i - 1;
                }
            }
            return SizeY - 1;
        }

        bool GetCheckWin(PictureBox USED)
        {
            GetAddress(USED);

            int LocalY = GetLowestEmpty(x, y);
            //Check Winrow length for row of Playernum in all 8 directions
            //Stalemate
            // Win = 3;
            GameArea[x, LocalY] = PlayerNum;
            Winrow(x, LocalY);

            return false;
        }
        bool Winrow(int x, int y)
        {
            //Along the X row
            bool WinState = false;
            int WinRow = 1;
            WinRow += RowAdd(x, y, 1, 0);
            WinRow += RowAdd(x, y, -1, 0);

            if (WinRow >= WinRowLength)
            {
                WinState = true; //Horizontal Win
            }
            WinRow = 1;
            WinRow += RowAdd(x, y, 0, 1);
            WinRow += RowAdd(x, y, 0, -1);
            if (WinRow >= WinRowLength)
            {
                WinState = true; //Vertial Win
            }
            WinRow = 1;
            WinRow += RowAdd(x, y, 1, 1);
            WinRow += RowAdd(x, y, -1, -1);
            if (WinRow >= WinRowLength)
            {
                WinState = true; //Diagonal Win (top left  to Bottom right)
            }
            WinRow = 1;
            WinRow += RowAdd(x, y, -1, 1);
            WinRow += RowAdd(x, y, 1, -1);
            if (WinRow >= WinRowLength)
            {
                WinState = true; //Diagonal Win (Bottom left to top right)
            }

            return WinState;
        }

        int RowAdd(int StartX, int StartY, int IncreaseX, int IncreaseY)
        {
            int WinRow = 0;
            for (int i = 1; i < SizeX; i++)
            {
                if ((StartX + (i * IncreaseX) >= SizeX || StartY + (i * IncreaseY) >= SizeY) || (StartX + (i * IncreaseX) < 0 || StartY + (i * IncreaseY) < 0))//Checks doesnt go outside array bounds
                    break;
                if (PlayerNum == GameArea[StartX + (i * IncreaseX), StartY + (i * IncreaseY)])
                {
                    WinRow++;
                }
                else
                    break;
            }
            return WinRow;
        }
        Point GetLocation(int X, int Y)
        {
            //   return new Point((int)((40 * X) * widthRatio), (int)((40 * Y) * heightRatio) + 30);
            return new Point((int)((((this.Size.Width - 150) / SizeX) * X) + 10), (int)((((this.Size.Height - 100) / SizeY) * Y) + 50));
        }
        #endregion

        float StartingWidth;
        float StartingHeight;
        float widthRatio = 1;
        float heightRatio = 1;
        //     float scale = new SizeF(widthRatio, heightRatio);
        public G2()
        {

            InitializeComponent();

            //LoadImg();
            this.Test.Click += new System.EventHandler(ClickHandler);
            // CreatePlayArea();
            StartingWidth = Size.Width;
            StartingHeight = Size.Height;
        }

        private void CreatePlayArea()
        {
            for (int i = 0; i < SizeX; i++)
            {
                for (int j = 0; j < SizeY; j++)
                {

                    PictureBox Pbox = new PictureBox();
                    Pbox.Name = "X" + i + "Y" + j;
                    Pbox.Location = GetLocation(i, j);
                    Pbox.Size = GetWidthHeight();
                    Pbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    Pbox.SizeMode = PictureBoxSizeMode.StretchImage;
                    Pbox.MouseEnter += new System.EventHandler(this.Hover);
                    Pbox.MouseLeave += new System.EventHandler(this.HoverLeave);
                    Pbox.Click += new System.EventHandler(this.ClickHandler);
            //        Console.WriteLine("Box Location :" + Pbox.Location + "Box Size :" + Pbox.Size);
                    Controls.Add(Pbox);
                }
            }
        }

        private void G2_Load(object sender, EventArgs e)
        {
            CreatePlayArea();
        }

        private void SizeControl(object sender, EventArgs e)
        {
            widthRatio = this.Size.Width / StartingWidth;
            heightRatio = this.Size.Height / StartingHeight;
            foreach (Control Ctrl in this.Controls)
            {
                if (Ctrl is PictureBox && ((PictureBox)(Ctrl)).Name.Contains("X"))
                {
                    GetAddress(Ctrl);


                    Ctrl.Location = GetLocation(x, y);
                    //   Ctrl.Size =
                    Ctrl.Size = GetWidthHeight();
                    //  Ctrl.Width = (int)((40) * widthRatio);
                    //  Ctrl.Height = (int)((40) * heightRatio);
                }
            }
        }

        private Size GetWidthHeight()
        {
            return new Size((int)(this.Size.Width - 50) / SizeX, (int)((this.Size.Height - 20) / SizeY));
        }

        private void Reset_Click(object sender, EventArgs e)
        {
            Reset.Visible = false;
            Win = 0;
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    ((PictureBox)x).Image = null;
                }
            }
            GameArea = new int[3, 3];
            PlayerNum = 2;
            // TurnSelect.Image = LoadImg();
            Wintxt.Text = "X's go";
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            G2Options settingsForm = new G2Options(this);
            settingsForm.Show();
            this.Enabled = false;
        }

        public void refresh(G2OptionsCollection Options)
        {
            //Cleans Old Grid
            for (int i = 0; i < SizeX; i++)
            {
                for (int j = 0; j < SizeY; j++)
                {
                    string Address = "X" + i + "Y" + j;
                    Control[] List = Controls.Find(Address, true);
                    foreach (Control Ctrl in List)
                    {
                        if (Ctrl is PictureBox)
                        {
                            this.Controls.Remove(Ctrl);
                        }
                    }
                }
            }
     Gravity = Options.Gravity;
     PlayerNum = Options.PlayerNum;
     PlayerTotal = Options.PlayerTotal;
      Win = 0;
       SizeX = Options.SizeX;
       SizeY =  Options.SizeY;
        WinRowLength = Options.WinRowLength;

        GameArea = new int[SizeX, SizeY];
        CreatePlayArea();
        }
        //CreatePlayArea();
    }
}

