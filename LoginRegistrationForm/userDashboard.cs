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
    public partial class userDashboard : Form
    {
        string connectionString = "data source=APURBO\\SQLEXPRESS; database=signup; integrated security=SSPI";
        
        private readonly int id;
        private readonly String Username;



        public userDashboard(int id)
        {
            InitializeComponent();
            this.id = id;
            textBox5.ReadOnly = true;
            textBox6.ReadOnly = true;

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
           
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void login_close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

       

        private void pictureBox3_Click_1(object sender, EventArgs e)
        {
            aboutUs a = new aboutUs(id);
            a.Show();
            this.Hide();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Origin = textBox3.Text.Trim();
            string Destination = textBox4.Text.Trim();

            if (string.IsNullOrWhiteSpace(Origin) || string.IsNullOrWhiteSpace(Destination))
            {
                MessageBox.Show("Please enter both Origin and Destination.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            string query = @"SELECT Bus, Fare 
                             FROM Bus 
                             WHERE Origin = @Origin AND Destination = @Destination";


            try
            {
                // Connect to the SQL Server
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@Origin", Origin);
                        command.Parameters.AddWithValue("@Destination", Destination);

                        // Open the connection
                        connection.Open();

                        // Execute the query and read the result
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            // Display the BusName and Fare in textBox5 and textBox6
                            textBox5.Text = reader["Bus"].ToString();
                            textBox6.Text = reader["Fare"].ToString();
                        }
                        else
                        {
                            // No matching result
                            MessageBox.Show("No bus found for the given route.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            textBox5.Text = "";
                            textBox6.Text = "";
                        }

                        // Close the reader
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any errors
                MessageBox.Show("An error occurred during the search: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox5.Text = "";
            textBox6.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                Profile uForm = new Profile(id);
                uForm.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while opening the profile: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            aboutUs a = new aboutUs(id);
             a.Show();
             this.Hide();
            
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            incident a = new incident(id);
            a.Show();
            this.Hide();
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

        private void button6_Click(object sender, EventArgs e)
        {
            userDashboard u = new userDashboard(id);
            u.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            userReview u = new userReview(id);
            u.Show();
            this.Hide();
        }
    }
    }

