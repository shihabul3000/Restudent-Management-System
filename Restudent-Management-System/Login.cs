using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restudent_Management_System
{
    public partial class Login : Form
    {
        string username;
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /* if (btnLogin.Enabled && txtUserId.Text == "a001" && txtPassword.Text == "123")
             {
                 new Admin().Show();
                 this.Hide();

             }
             else if (btnLogin.Enabled && txtUserId.Text == "m002" && txtPassword.Text == "123")
             {
                 new Manager().Show();
                 this.Hide();
             }
             else if (btnLogin.Enabled && txtUserId.Text == "s003" && txtPassword.Text == "123")
             {
                 new Staf().Show();
                 this.Hide();
             }
             else
             {
                 MessageBox.Show("Not Valid User", "\tWrong UserID\n\tWrong UserID", MessageBoxButtons.OK, MessageBoxIcon.Error);

             } */
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string username = txtUserId.Text;
            string password = txtPassword.Text;
            string type;

            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(@"Data Source=DESKTOP-GECMQDH\MSSQLSERVER03;Initial Catalog=RestudentManagementSystem;Integrated Security=True"); //Sting connection Datbase
                conn.Open();
                
                string query = "select Password,userid from userdb where Userid = '" + username + "'"; //Password,Type,Table_Name, Username;
                SqlCommand cmd = new SqlCommand(query, conn);
                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                
                DataTable dt = ds.Tables[0];
                string val = dt.Rows[0]["password"].ToString(); //password change
                string val2 = dt.Rows[0]["userid"].ToString();
                
                if (val == password)
                {
                    if (val2[0] == 'a')
                    {
                        MessageBox.Show("Login Successful");
                        Admin admin = new Admin(username);
                        admin.Show();
                    }
                    else if (val2[0] == 'm')
                    {
                        MessageBox.Show("Login Successful");
                        Manager mng = new Manager(username);
                        mng.Show();
                    }
                    else if (val2[0] == 's')
                    {
                        MessageBox.Show("Login Successful");
                        Staf staff = new Staf(username);
                        staff.Show();
                    }

                }
                else
                {
                    MessageBox.Show("Password doesn't match");
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                MessageBox.Show("Username doesn't Match");
            }
            finally
            {
                //MessageBox.Show("Login Successfull");
                conn.Close();
            }


           


        }

        private void lblExit_Click(object sender, EventArgs e)
        {
            Application.Exit(); 
        }
    }
}
