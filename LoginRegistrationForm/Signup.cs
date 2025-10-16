using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Net;

namespace LoginRegistrationForm
{
    public partial class Signup : Form
    {

        public Signup()
        {
            InitializeComponent();
            
        }
        SqlConnection con = new SqlConnection("data source =APURBO\\SQLEXPRESS; database =signup; " +
                                       "integrated security = SSPI");
      
        private void signup_loginHere_Click(object sender, EventArgs e)
        {
            Form1 A = new Form1();
            A.Show();
            this.Hide();
        }

        private void signup_close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void signup_btn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(signup_id.Text) ||
                string.IsNullOrEmpty(signup_username.Text) ||
                string.IsNullOrEmpty(signup_password.Text) ||
                string.IsNullOrEmpty(signup_Cpass.Text))
                 
            {
                MessageBox.Show("All fields are required.");
                return;
            }
            if (!int.TryParse(signup_id.Text, out int userId))
            {
                MessageBox.Show("ID must be a valid number.");
                return;
            }

            if (signup_password.Text != signup_Cpass.Text)
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }

            try
            {
                // Check if the user already exists
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM signup WHERE ID = @Id", con))
                {
                    cmd.Parameters.AddWithValue("@Id", signup_id.Text);

                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            MessageBox.Show("This User has already registered.");
                            return;
                        }
                    }
                    con.Close();
                }

                // Insert new user into the database
                using (SqlCommand cmd = new SqlCommand("INSERT INTO signup(Id, Username, Password,Role) VALUES (@Id, @Username, @Password, 'User')", con))
                {
                    cmd.Parameters.AddWithValue("@Id", signup_id.Text);
                    cmd.Parameters.AddWithValue("@Username", signup_username.Text);
                    cmd.Parameters.AddWithValue("@Password", signup_password.Text);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Registered successfully!");

                    // Clear input fields
                    Form1 form1 = new Form1();
                    form1.Show();
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }




        private void signup_showPass_CheckedChanged(object sender, EventArgs e)
        {
            // Toggle password visibility
            signup_password.PasswordChar = signup_showPass.Checked ? '\0' : '*';
            signup_Cpass.PasswordChar = signup_showPass.Checked ? '\0' : '*';
        }

        

        private void signup_username_TextChanged(object sender, EventArgs e)
        {

        }

        private void signup_username_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (signup_username.Text == "")
                {
                    MessageBox.Show("Blank Not Allowed");
                }
                else if (signup_username.Text.Length > 6)
                {
                    MessageBox.Show("Password should 6 Character");
                }
                else
                {
                    signup_password.Enabled = true;
                    signup_password.Focus();
                }

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void signup_Cpass_KeyPress(object sender, KeyPressEventArgs e)
        {

      }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void signup_id_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
    
    

