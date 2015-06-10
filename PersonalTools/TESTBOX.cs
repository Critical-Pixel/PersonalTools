using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Reflection;
using PersonalTools.Models;

namespace PersonalTools
{

    public partial class TESTBOX : Form
    {
        EmailModel em = new EmailModel();
        EmailData ED = new EmailData();
        public TESTBOX()
        {
            InitializeComponent();
        }
        public IEnumerable<Control> GetAll(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();

            return controls.SelectMany(ctrl => GetAll(ctrl, type))
                                      .Concat(controls)
                                      .Where(c => c.GetType() == type);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ED.First_name = "this";
            ConnectStringObject(ED);

    //        var c = GetAll(this, typeof(TextBox));
  
    //        foreach (Control Text in c)
   //         {
    //            Text.Text = Text.Name;
   //         }
    //        MessageBox.Show("Total Controls: " + c.Count());
          // richTextBox1.Text = em.SendNotificationEmail();
        }

        private void TESTBOX_Load(object sender, EventArgs e)
        {
        }
        private void ConnectStringObject(object StringOb)
        {
            IEnumerable<Control> l = GetAll(this, typeof(Label));
        //    Type myType = StringOb.GetType();
         //   IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
            int i = 1;
            try
            {
                foreach (var prop in StringOb.GetType().GetProperties()) // Adds all strings into email values
                {
                    var this_item = l.Where(x => x.Name == "label" + i).SingleOrDefault();

                    this_item.Text = prop.Name;
                    //     var aValue = StringOb.GetType().GetProperty(prop.Name).GetValue(EmailOb, null);
                    //     if (prop.PropertyType.Name.Equals("String"))
                    //    {
                    //          body.Add(prop.Name, (string)aValue);
                    //      }
                    i++;
                }
            }
            catch
            { 
            }
           
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            TotalObject TO = new TotalObject();
            TO.Email.SendNotificationEmail();
        }
 
    }
}
