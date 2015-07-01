using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PersonalTools.GStuff;

namespace PersonalTools
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Console.WriteLine("Starting Testbox");
            Application.Run(new G1());
            //Application.Run(new TESTBOX());
        }
    }
}
