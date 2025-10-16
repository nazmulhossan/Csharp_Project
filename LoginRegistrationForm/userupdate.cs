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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LoginRegistrationForm
{
    public partial class userupdate : Form
    {

        private string connectionString = "data source=APURBO\\SQLEXPRESS; database=signup; integrated security=SSPI";
        private int id;
        public userupdate(int id, string username, string password)
        {

            InitializeComponent();

            textBox1.Text = id.ToString();
            textBox3.Text = username;
            textBox2.Text = password;
            textBox1.ReadOnly = true;

        }
        public userupdate(int id)
        {
            InitializeComponent();
            this.id = id;
            LoadDetails();
        }


        private void LoadDetails()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT Id, Username, Password FROM signup WHERE Id = @Id";
                    
                        using (SqlCommand cmd = new SqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@Id", id);
                            connection.Open();

                            using (SqlDataReader dr = cmd.ExecuteReader())
                            {
                                if (dr.Read())
                                {
                                    // Populate the text boxes with the retrieved data
                                    textBox1.Text = dr["Id"].ToString();
                                textBox3.Text = dr["Username"].ToString();
                                textBox2.Text = dr["Password"].ToString();
                                    

                                }
                                else
                                {
                                    MessageBox.Show("No details found for the given ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    this.Close(); // Close the form if no data is found
                                }
                            }
                        }

                    }
                }
            
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void login_close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Confirm deletion
            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete this profile?",
                "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    string query = "DELETE FROM signup WHERE Id = @Id";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            int id = int.Parse(textBox1.Text.Trim());
                            command.Parameters.AddWithValue("@Id", id);

                            connection.Open();
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Profile deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                adminDashboard aform = new adminDashboard(id);
                                aform.Show();
                                this.Hide();
                                // Close the form after successful deletion
                            }
                            else
                            {
                                MessageBox.Show("No profile was found to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string newid = textBox1.Text.Trim();
            string newUsername = textBox3.Text.Trim();
            string newPassword = textBox2.Text.Trim();


            if (string.IsNullOrWhiteSpace(newUsername) || string.IsNullOrWhiteSpace(newPassword))
            {
                MessageBox.Show("All fields must be filled out.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "UPDATE signup SET Username = @Username, Password = @Password WHERE Id = @Id";
                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        command.Parameters.AddWithValue("@Id", newid);
                        command.Parameters.AddWithValue("@Username", newUsername);
                        command.Parameters.AddWithValue("@Password", newPassword);
                        con.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Record updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            adminDashboard aform = new adminDashboard(id);
                            aform.Show();
                            this.Hide();
                           // Optionally close the form after a successful update
                        }
                        else
                        {
                            MessageBox.Show("No record was updated. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            adminDashboard aform = new adminDashboard(id);
            aform.Show();
            this.Hide();
        }
    }
}
    

