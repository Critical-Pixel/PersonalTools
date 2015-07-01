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
    public partial class G1 : Form
    {
        //private
        static int Turn = 1;
        static int Win = 0;
        public int[,] GameArea = new int[3,3];
        System.Drawing.Bitmap LoadImg()
        {
            switch (Turn)
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
           if (Win ==0) {
                PictureBox USED = (PictureBox)sender;
                string[] UsedArray = ((PictureBox)sender).Name.Replace("X", "").Split('Y');
                int x;
                int y;
                int.TryParse(UsedArray[0], out x);
                int.TryParse(UsedArray[1], out y);
   
               if  (GameArea[x - 1, y - 1] == 0)
                {
                    USED.Image = LoadImg();
                    if (!CheckWin(USED))
                    {

                        if (Turn == 1)
                        { Turn = 2; Wintxt.Text = "X's go"; }
                        else
                        {
                            Turn = 1;
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
            string[] UsedArray = ((PictureBox)sender).Name.Replace("X", "").Split('Y');
            int x;
            int y;
            int.TryParse(UsedArray[0], out x);
            int.TryParse(UsedArray[1], out y);
            if( GameArea[x-1, y-1] == 0 && Win == 0)
            {
                ((PictureBox)sender).Image = LoadImg();
            }
        }
        private void HoverLeave(object sender, System.EventArgs e)
        {
            string[] UsedArray = ((PictureBox)sender).Name.Replace("X", "").Split('Y');
            int x;
            int y;
            int.TryParse(UsedArray[0], out x);
            int.TryParse(UsedArray[1], out y);
            if (GameArea[x-1, y-1] == 0)
            {
                ((PictureBox)sender).Image = null;
            }
        }

        bool CheckWin(PictureBox USED)
        {
            string[] UsedArray = USED.Name.Replace("X", "").Split('Y');
            int x;
            int y;
            int.TryParse(UsedArray[0], out x);
            int.TryParse(UsedArray[1], out y);
            GameArea[x - 1, y - 1] = Turn;
            for (int i = 0 ; i <3 ; i++)
            {
                if (GameArea[i, 0] == GameArea[i, 1] && GameArea[i, 1] == GameArea[i, 2] && GameArea[i, 0] != 0)
                { Win = GameArea[i, 0]; return true; }
                if (GameArea[0, i] == GameArea[1, i] && GameArea[1, i] == GameArea[2, i] && GameArea[0, i] != 0)
                { Win = GameArea[0, i]; return true; }
            }
            if (GameArea[0, 0] == GameArea[1, 1] && GameArea[1,1] == GameArea[2, 2] && GameArea[0, 0] != 0)
            { Win = GameArea[0, 0]; return true; }
            if (GameArea[2, 0] == GameArea[1, 1] && GameArea[1, 1] == GameArea[0, 2] && GameArea[0, 2] != 0)
            { Win = GameArea[0, 2]; return true; }
            for (int i = 0; i <= 2; i++)
            {
                for (int j = 0; j <= 2; j++)
                { 
                    if (GameArea[i, j] == 0)
                        return false;
                }
            }
           //Stalemate
            Win = 3;
            return true;
        }


        public G1()
        {

            InitializeComponent();

            LoadImg();
            this.X1Y1.Click += new System.EventHandler(ClickHandler);
            float widthRatio = Screen.PrimaryScreen.Bounds.Width / 1280;
            float heightRatio = Screen.PrimaryScreen.Bounds.Height / 800f;
            SizeF scale = new SizeF(widthRatio, heightRatio);
        }



        private void G1_Load(object sender, EventArgs e)
        {

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
            Turn = 2;
            TurnSelect.Image = LoadImg();
            Wintxt.Text = "X's go"; 
        }
    }
}
