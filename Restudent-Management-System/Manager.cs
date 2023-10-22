using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restudent_Management_System
{
    public partial class Manager : Form
    {
        string username;
        public Manager()
        {
            InitializeComponent();
        }
        public Manager(string conUsername)
        {
            InitializeComponent();
            username = conUsername;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // search by ID

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string category = CategorycomboBox.Text;
            string Iid = txtItemId.Text;
            string Iname = txtItemName.Text;
            string Iprice = txtItemPrice.Text;

            if ( Iid!= null && Iname != null && Iprice != null && category != null && CategorycomboBox.SelectedItem!=null)
            {

                SqlConnection conn = null;
                try
                {
                    conn = new SqlConnection(@"Data Source=DESKTOP-GECMQDH\MSSQLSERVER03;Initial Catalog=RestudentManagementSystem;Integrated Security=True"); //String Connection Db
                    conn.Open();
                    string query = $"insert into item (Category,ItemId,ItemName,ItemPrice) VALUES ('{category}','{Iid}','{Iname}','{Iprice}');";
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
                itemGridView.Refresh();

            }
            else
            {
                MessageBox.Show(" Fill Up the Information!!");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            SqlConnection conn = null;
            try
            {
                string toDelete = txtItemId.Text;

                conn = new SqlConnection(@"Data Source=DESKTOP-GECMQDH\MSSQLSERVER03;Initial Catalog=RestudentManagementSystem;Integrated Security=True");//String Name DB
                conn.Open();

                string query = $"delete from item where itemId = '{toDelete}';"; //Table_Name,itemId change
                SqlCommand cmd = new SqlCommand(query, conn);
                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                //DataTable dt = ds.Tables[0];
                //string val = dt.Rows[0]["Password"].ToString();
                buttonRefresh_Click(sender, e);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //MessageBox.Show("User doesnot exist");
            }
            finally
            {
                MessageBox.Show("Delete Successfull");
                conn.Close();
            }
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(@"Data Source=DESKTOP-GECMQDH\MSSQLSERVER03;Initial Catalog=RestudentManagementSystem;Integrated Security=True");
                conn.Open();

                string query = "select * from item"; //Table_Name change
                SqlCommand cmd = new SqlCommand(query, conn);
                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                DataTable dt = ds.Tables[0];
                //string val = dt.Rows[0]["X"].ToString();
                itemGridView.DataSource = dt;
                itemGridView.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            string search = txtSearch.Text;

            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(@"Data Source=DESKTOP-GECMQDH\MSSQLSERVER03;Initial Catalog=RestudentManagementSystem;Integrated Security=True"); //SQL CONNECTION STRING ADD
                conn.Open();

                string query2 = $"select * from item where itemid='{search}'"; //Table_Name,item change.
                SqlCommand cmd2 = new SqlCommand(query2, conn);
                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter(cmd2);
                adp.Fill(ds);
                DataTable dt = ds.Tables[0];
                //string val = dt.Rows[0]["X"].ToString();
                itemGridView.DataSource = dt;
                itemGridView.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtItemPrice.Clear();
            txtItemName.Clear();
            txtItemId.Clear();
            txtSearch.Clear();
            CategorycomboBox.Text = "";
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-GECMQDH\MSSQLSERVER03;Initial Catalog=RestudentManagementSystem;Integrated Security=True");
                conn.Open();
                string query = @"UPDATE item SET ItemId ='" + txtItemId.Text+ "',ItemName='" + txtItemName.Text + "',ItemPrice='" + txtItemPrice.Text + "',Category='" + CategorycomboBox.Text + "' where ItemId ='" + txtItemId.Text + "';";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.ExecuteNonQuery();
                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);

                itemGridView.Refresh();
                MessageBox.Show("Edit Successfull");
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void itemGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtItemId.Text = itemGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtItemName.Text = itemGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtItemPrice.Text = itemGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
            CategorycomboBox.Text = itemGridView.Rows[e.RowIndex].Cells[3].Value.ToString();
        }

        private void itemGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void lblLogout_Click(object sender, EventArgs e)
        {
            this.Close();
            new Login().Visible = true;
        }
    }
}
