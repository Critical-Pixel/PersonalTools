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
    public partial class G2 : Form
    {
        /// Connect 4 Multiplayer

        //private

        static int PlayerNum = 1;
        static int Win = 0;
        static int SizeX = 9;
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

        //   ResourceManager rm = Properties.Resources.ResourceManager();
        private void ClickHandler(object sender, EventArgs e)
        {
            if (Win == 0)
            {
                PictureBox USED = (PictureBox)sender;
                GetAddress(sender);

                if (GameArea[x, GetLowestEmpty(x)] == 0)
                {
                    SetLowestImg();
                    if (!GetCheckWin(USED))
                    {

                        if (PlayerNum == 1)
                        { PlayerNum = 2; Wintxt.Text = "X's go"; }
                        else
                        {
                            PlayerNum = 1;
                            Wintxt.Text = "0's go";
                        }

                        TurnSelect.Image = LoadImg();
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
        }

        private void Hover(object sender, System.EventArgs e)
        {
            GetAddress(sender);
            if (GameArea[x, y] == 0 && Win == 0)
            {
                SetLowestImg();
            }
        }
        private void SetLowestImg()
        {
            string BottomAddress = "X" + x + "Y" + GetLowestEmpty(x);
            Control[] List = Controls.Find(BottomAddress, true);
            if (List.Length > 0 && List[0] is PictureBox)
                ((PictureBox)List[0]).Image = LoadImg();
        }

        private void HoverLeave(object sender, System.EventArgs e)
        {
            GetAddress(sender);
            if (GameArea[x, GetLowestEmpty(x)] == 0)
            {
                string BottomAddress = "X" + x + "Y" + GetLowestEmpty(x);
                Control[] List = Controls.Find(BottomAddress, true);
                foreach (Control Ctrl in List)
                {
                    if (Ctrl is PictureBox)
                    { ((PictureBox)Ctrl).Image = null; }
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
        int GetLowestEmpty(int X)
        {
            for (int i = 0; i < SizeY; i++)
            {
                if (GameArea[X, i] != 0)
                    return i - 1;
            }
            return SizeY - 1;
        }

        bool GetCheckWin(PictureBox USED)
        {
            GetAddress(USED);

            int LocalY = GetLowestEmpty(x);
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
            int WinRow = 1;
            for (int i = x +1; i < SizeX; i++)
            {
                if (PlayerNum == GameArea[i, y])
                {
                    WinRow++;
                }
                else
                    break;
            }
            for (int i = x -1; i >= 0; i--)
            {
                if (PlayerNum == GameArea[i, y])
                {
                    WinRow++;
                }
                else
                    break;
            }
            if (WinRow >= WinRowLength)
            { 
                return true; 
            }
            return false;
        }

        int RowAdd(int StartX,int StartY,int IncreaseX,int IncreaseY)
        {
            int WinRow = 0;
            for (int i =  1; i < SizeX; i++)
            {
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
            return new Point((int)((40 * X) * widthRatio), (int)((40 * Y) * heightRatio) + 30);
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

            LoadImg();
            this.X1Y1.Click += new System.EventHandler(ClickHandler);
            CreatePlayArea();
            StartingWidth = Size.Width;
            StartingHeight = Size.Height;
        }

        private void CreatePlayArea()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    PictureBox Pbox = new PictureBox();
                    Pbox.Name = "X" + i + "Y" + j;
                    Pbox.Location = GetLocation(i, j);
                    Pbox.Height = 40;
                    Pbox.Width = 40;
                    Pbox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
                    Pbox.SizeMode = PictureBoxSizeMode.StretchImage;
                    Pbox.MouseEnter += new System.EventHandler(this.Hover);
                    Pbox.MouseLeave += new System.EventHandler(this.HoverLeave);
                    Pbox.Click += new System.EventHandler(this.ClickHandler);
                    //    Pbox.Anchor = AnchorStyles.Bottom |     AnchorStyles.Right |     AnchorStyles.Top |     AnchorStyles.Left;
                    //  Pbox.Dock = DockStyle.Left;
                    //Pbox.Image = LoadImg();
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
                    Ctrl.Width = (int)((40) * widthRatio);
                    Ctrl.Height = (int)((40) * heightRatio);
                }
            }
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
    }
}
