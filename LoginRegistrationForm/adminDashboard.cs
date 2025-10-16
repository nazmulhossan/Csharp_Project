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

namespace LoginRegistrationForm
{
    public partial class adminDashboard : Form
    {
        private readonly int id;
        public adminDashboard(int id)
        {
            InitializeComponent();

        }
        public adminDashboard()
        {
            InitializeComponent();

        }
        SqlConnection con = new SqlConnection("data source =APURBO\\SQLEXPRESS; database =signup; " +
                                       "integrated security = SSPI");

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Form1 lForm = new Form1();
            lForm.Show();
            this.Hide();
        }

        private void login_close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) ||
               string.IsNullOrEmpty(textBox2.Text) ||
               string.IsNullOrEmpty(textBox3.Text) ||
               string.IsNullOrEmpty(textBox4.Text))

            {
                MessageBox.Show("All fields are required.");
                return;
            }
            if (!int.TryParse(textBox4.Text, out int userId))
            {
                MessageBox.Show("Fare must be a valid number.");
                return;
            }
            using (SqlCommand cmd = new SqlCommand("INSERT INTO Bus(Origin, Destination, Bus,Fare) VALUES (@Origin, @Destination, @Bus, @Fare)", con))
            {
                cmd.Parameters.AddWithValue("@Origin", textBox1.Text);
                cmd.Parameters.AddWithValue("@Destination", textBox2.Text);
                cmd.Parameters.AddWithValue("@Bus", textBox3.Text);
                cmd.Parameters.AddWithValue("@Fare", textBox4.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Information inserted successfully!");

                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";

            }
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            userinfo userForm = new userinfo(id);
            userForm.Show();
            this.Hide();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            adminDashboard a = new adminDashboard(id);
            a.Show();
            this.Hide();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
               "Are you sure you want to sign out?",
               "Log Out",
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Question
           );

            if (result == DialogResult.Yes)
            {
                Form1 form = new Form1();
                form.Show();
                this.Hide();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            adminIncident a = new adminIncident();
            a.Show();
            this.Hide();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            adminReview a = new adminReview();
            a.Show();
            this.Hide();
        }
    }
}
