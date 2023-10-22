using System;
using System.Windows.Forms;

namespace Restudent_Management_System
{
    public partial class Start : Form
    {
        public Start()
        {
            InitializeComponent();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            pnl.Width += 5;
            if (pnl.Width >= 700)
            {
                timer.Stop();
                new Login().Show();
                this.Hide();
                
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
