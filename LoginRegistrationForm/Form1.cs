using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace LoginRegistrationForm
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("data source =APURBO\\SQLEXPRESS; database =signup; " +
                                       "integrated security = SSPI");
        


        private void login_registerHere_Click(object sender, EventArgs e)
        {
            Signup signupForm = new Signup();
            signupForm.Show();
            this.Hide();

        }

        private void login_close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void login_showPass_CheckedChanged(object sender, EventArgs e)
        {
            login_password.PasswordChar = login_showPass.Checked ? '\0' : '*';
        }


        private void login_btn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(login_username.Text) || string.IsNullOrEmpty(login_password.Text))
            {
                MessageBox.Show("Both fields are required!");
                return;
            }

            try
            {
                string query = "SELECT * FROM signup WHERE Username = @Username AND Password = @Password";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Username", login_username.Text);
                    cmd.Parameters.AddWithValue("@Password", login_password.Text);

                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read()) // If a record is found
                        {
                            int id = Convert.ToInt32(dr["Id"]);
                            string role = dr["Role"].ToString().ToLower();

                            if (role == "admin")
                            {
                                MessageBox.Show("Admin Login Successful!");
                                adminDashboard adminForm = new adminDashboard(id);
                                adminForm.Show();
                                this.Hide();
                            }
                            else if (role == "user")
                            {
                                MessageBox.Show("User Login Successful!");
                                userDashboard userForm = new userDashboard(id);
                                userForm.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Unknown Role!");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Invalid Username or Password!");
                        }
                    }
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



        





        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void login_username_TextChanged(object sender, EventArgs e)
        {

        }

        private void login_username_KeyPress(object sender, KeyPressEventArgs e)
        {
           
            }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void login_password_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
    }

