using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restudent_Management_System
{
    public partial class Staf : Form
    {
        string username;
        static int totalPrice = 0;
        public Staf()
        {
            InitializeComponent();
        }

        public Staf(string conUsername)
        {
            InitializeComponent();
            username= conUsername;
        }
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCustomerId.Clear();
            txtCustomerName.Clear();
            txtItemId.Clear();
            txtItemName.Clear();
            txtItemPrice.Clear();
            txtQuantity.Clear();
            CategorycomboBox.Text = string.Empty;
            //lblTotalPrice.Text = "0.00";
            //totalPrice = 0;
        }

        private void buttonRefreshItem_Click(object sender, EventArgs e)
        {
            try
            {

                SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-GECMQDH\MSSQLSERVER03;Initial Catalog=RestudentManagementSystem;Integrated Security=True");
                conn.Open();

                string query = "select * from item"; //change tablename to manager's table name
                SqlCommand cmd = new SqlCommand(query, conn);
                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                DataTable dt = ds.Tables[0];
                itemGridView.DataSource = dt;
                itemGridView.Refresh();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /*private void buttonRefreshOrder_Click(object sender, EventArgs e)
        {
            try
            {

                SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-GECMQDH\MSSQLSERVER03;Initial Catalog=RestudentManagementSystem;Integrated Security=True");
                conn.Open();

                string query = "select * from OrderC"; //change tablename to manager's table name
                SqlCommand cmd = new SqlCommand(query, conn);
                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                DataTable dt = ds.Tables[0];
                OrderGridView.DataSource = dt;
                OrderGridView.Refresh();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }*/

        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            string itemId =txtItemId.Text;
            string itemName = txtItemName.Text;
           
            int itemPrice = Convert.ToInt32(txtItemPrice.Text);
            string customerName = txtCustomerName.Text;
            string customerId = txtCustomerId.Text;
            int quantity = Convert.ToInt32(txtQuantity.Text);
            int total = itemPrice * quantity;



            if (txtItemId.Text != "" &&
              txtItemName.Text != "" &&
              txtItemPrice.Text != "" &&
              txtCustomerName.Text != "" &&
              txtCustomerId.Text != "" &&
              txtQuantity.Text != "")
            {

                SqlConnection conn = null;
                try
                {
                    conn = new SqlConnection(@"Data Source=DESKTOP-GECMQDH\MSSQLSERVER03;Initial Catalog=RestudentManagementSystem;Integrated Security=True"); //String Connection
                    conn.Open();
                    string query1 = $"insert into OrderC (item_id,item_Name,item_Price,Customer_name,Customer_id,quantity,total) VALUES ('{itemId}','{itemName}','{itemPrice}','{customerName}','{customerId}','{quantity}','{total}');"; //Change table name and column names
                    SqlCommand cmd = new SqlCommand(query1, conn);
                    cmd.ExecuteNonQuery();

                    string query2 = $"select item_id,item_Name,item_Price,quantity,total from OrderC where Customer_id='{customerId}'"; //change table and Column name
                    SqlCommand cmd2 = new SqlCommand(query2, conn);
                    DataSet ds = new DataSet();
                    SqlDataAdapter adp = new SqlDataAdapter(cmd2);
                    adp.Fill(ds);
                    DataTable dt = ds.Tables[0];
                    //string val = dt.Rows[0]["X"].ToString();
                    OrderGridView.DataSource = dt;
                    OrderGridView.Refresh();
                    totalPrice += total;
                    lblTotalPrice.Text = totalPrice.ToString()+".00";

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

        }

        private void lblTotalPrice_Click(object sender, EventArgs e)
        {
           
        }

        private void btnChackOut_Click(object sender, EventArgs e)
        {
            string customerId = txtCustomerId.Text;

            lblTotalPrice.Text = "0.00";
            totalPrice = 0;

            /*SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(@"Data Source=DESKTOP-0GKBBVQ\SQLEXPRESS;Initial Catalog=""Government Management System"";Integrated Security=True");
                conn.Open();
                string query1 = $"select total from Table_Name Where customer_Id='{customerId}'"; //change table name and customer id column namne
                SqlCommand cmd1 = new SqlCommand(query1, conn);
                DataSet ds1 = new DataSet();
                SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                adp1.Fill(ds1);
                DataTable dt1 = ds1.Tables[0];
                //string val = dt.Rows[0]["X"].ToString();
                SqlDataReader da1 = cmd1.ExecuteReader();
                //while (da1.Read())
                {
                    //lblTotalPrice.Text = da1.GetValue(0).ToString(); //total price value update
                    lblTotalPrice.Text = "0.00";
                    totalPrice = 0;
                }
            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }*/ 
        }

        private void lblExit_Click(object sender, EventArgs e)
        {

            totalPrice = 0;
            lblTotalPrice.Text = "0.00";
            Application.Exit();
        }

        private void itemGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void itemGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtItemId.Text = itemGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtItemName.Text = itemGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtItemPrice.Text = itemGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
            CategorycomboBox.Text = itemGridView.Rows[e.RowIndex].Cells[3].Value.ToString();
        }

        private void OrderGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void lblLogout_Click(object sender, EventArgs e)
        {
            this.Close();
            new Login().Visible = true;
        }
    }
}
