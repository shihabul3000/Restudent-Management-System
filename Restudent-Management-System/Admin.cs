using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restudent_Management_System
{
    public partial class Admin : Form
    {
        string username;
        public Admin()
        {
            InitializeComponent();

        }

        public Admin(string conUname)
        {
            InitializeComponent();
            username = conUname;
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            string uid = txtUserId.Text;
            string uname = txtUserName.Text;
            string usalary = txtUserSalary.Text;
            string upassword = txtPassword.Text;

            if (uid != null && uname != null && usalary != null && upassword != null)
            {

                SqlConnection conn = null;
                try
                {
                    conn = new SqlConnection(@"Data Source=DESKTOP-GECMQDH\MSSQLSERVER03;Initial Catalog=RestudentManagementSystem;Integrated Security=True"); //String Connection Db
                    conn.Open();
                    string query = $"insert into UserDB (userId,userName,userSalary,Password) VALUES ('{uid}','{uname}','{usalary}','{upassword}');"; //Table_Name,userId,userName,userSalary
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully Added");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
                UserGridView.Refresh();



            }
            else
            {
                MessageBox.Show(" Fill Up the Information!!");
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-GECMQDH\MSSQLSERVER03;Initial Catalog=RestudentManagementSystem;Integrated Security=True");
                conn.Open();
                string query = @"UPDATE UserDB SET UserId ='" + txtUserId.Text + "',UserName='" + txtUserName.Text + "',UserSalary='" + txtUserSalary.Text + "',Password='" + txtPassword.Text + "' where UserId ='" + txtUserId.Text + "';";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.ExecuteNonQuery();
                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);

                UserGridView.Refresh();
                MessageBox.Show("Edit Successfull");
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-GECMQDH\MSSQLSERVER03;Initial Catalog=RestudentManagementSystem;Integrated Security=True");//String Name DB
                conn.Open();
                string query = $"delete from UserDB where UserId = '" + txtUserId.Text + "';"; //Table_Name,userid change
                SqlCommand cmd = new SqlCommand(query, conn);
                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                UserGridView.Refresh();
                MessageBox.Show("Delete Successfull");
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtItemName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtItemId_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lblExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lblLogout_Click(object sender, EventArgs e)
        {
            this.Close();
            new Login().Visible = true;

        }

        private void txtItemPrice_TextChanged(object sender, EventArgs e)
        {

        }



        private void btnClear_Click(object sender, EventArgs e)
        {
            txtUserId.Clear();
            txtUserName.Clear();
            txtUserSalary.Clear();
            txtSearch.Clear();
            txtPassword.Clear();
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {

            try
            {

                SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-GECMQDH\MSSQLSERVER03;Initial Catalog=RestudentManagementSystem;Integrated Security=True");
                conn.Open();

                string query = "select * from UserDB";
                SqlCommand cmd = new SqlCommand(query, conn);
                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                DataTable dt = ds.Tables[0];
                UserGridView.DataSource = dt;
                UserGridView.Refresh();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            string search = txtSearch.Text;


            try
            {
                SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-GECMQDH\MSSQLSERVER03;Initial Catalog=RestudentManagementSystem;Integrated Security=True"); //SQL CONNECTION STRING ADD
                conn.Open();

                string query2 = $"select * from UserDB where userid='{search}'"; //Table_Name,userid change.
                SqlCommand cmd2 = new SqlCommand(query2, conn);
                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter(cmd2);
                adp.Fill(ds);
                DataTable dt = ds.Tables[0];
                //string val = dt.Rows[0]["X"].ToString();
                UserGridView.DataSource = dt;
                UserGridView.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void UserGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtUserId.Text = UserGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtUserName.Text = UserGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtUserSalary.Text = UserGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtPassword.Text = UserGridView.Rows[e.RowIndex].Cells[3].Value.ToString();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
