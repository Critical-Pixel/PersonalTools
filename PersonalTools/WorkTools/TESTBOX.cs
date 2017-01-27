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
using PersonalTools;
using PersonalTools.ClassTester;
using System.Data.SqlClient;
using DataAccessLayer;
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
    //          MessageBox.Show("Total Controls: " + c.Count());
    //          richTextBox1.Text = em.SendNotificationEmail();
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
            Child.Maingo();
           // TotalObject TO = new TotalObject();
           // TO.Email.SendNotificationEmail();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime Sept = new DateTime(now.Year, 09, 01);
            DateTime Check = dateTimePicker1.Value;
            if (Check > Sept)
            { DateCheckOutput.Text = "We're after this date"; }
            else if (Check < Sept)
            { DateCheckOutput.Text = "We're before this date"; }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
//              new AdventureEntities().ExecuteStoreCommand(
//        @"    UPDATE Users
//              SET lname = @lname 
//              WHERE Id = @id",
//        new SqlParameter("lname", lname), new SqlParameter("id", id));

//  using (SqlConnection con = new SqlConnection("Persist Security Info=False;Integrated Security=true;Initial Catalog=Remember;server=(local)"))
//  {
//    con.Open();
//    using (SqlCommand cmd = con.CreateCommand())
//    {
//      cmd.CommandText = @"
//          UPDATE Users
//          SET lname = @lname 
//          WHERE Id = @id";
//      cmd.Parameters.AddWithValue("lname", lname);
//      cmd.Parameters.AddWithValue("id", id);
//      cmd.ExecuteNonQuery();
//    }
//  }

//          //  SqlConnection sqlConnection1 = new SteveTestDataEntities();
//            SteveTestDataEntities Entity = new SteveTestDataEntities();
//         // var connection = ((SteveTestDataEntities)context.Connection).StoreConnection;
//            SqlCommand cmd = new SqlCommand();
//            SqlDataReader reader;

//            cmd.CommandText = "SELECT * FROM Customers";
//            cmd.CommandType = CommandType.Text;
//            cmd.Connection = Entity;

//            sqlConnection1.Open();

//            reader = cmd.ExecuteReader();
//            // Data is accessible through the DataReader object here.

//            sqlConnection1.Close();
        }
 
    }
}
