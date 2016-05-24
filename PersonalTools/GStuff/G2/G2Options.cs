using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersonalTools.GStuff
{
    public partial class G2Options : Form
    {
        G2 Parent;
        public G2Options(G2 Parent_)
        {
            Parent = Parent_;
            InitializeComponent();
        }

        private void G2Options_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Parent.Enabled = true;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Parent.Enabled = true;
            G2OptionsCollection Options = new G2OptionsCollection();
            Options.PlayerTotal = (int)Players.Value;
            Options.SizeX = (int)Width.Value;
            Options.SizeY = (int)Height.Value;
            Options.WinRowLength = (int)WinLength.Value;
            ((G2)Parent).refresh(Options);
        
            this.Close();
        }
     
    }
}
