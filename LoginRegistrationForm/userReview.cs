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
    public partial class userReview : Form
    {
        private readonly int id; // To store the logged-in user's ID
        private string connectionString = "data source=APURBO\\SQLEXPRESS; database=signup; integrated security=SSPI";
        public userReview(int id)
        {
            InitializeComponent();
            this.id = id;
            LoadExistingReview();
        }
        private void LoadExistingReview()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT Review FROM signup WHERE Id = @Id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string existingReview = reader["Review"]?.ToString();
                                if (!string.IsNullOrWhiteSpace(existingReview))
                                {
                                    MessageBox.Show("You have already submitted a review. You can edit it below.", "Review Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    richTextBox1.Text = existingReview; // Populate the RichTextBox with the existing review
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading your review: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string reviewText = richTextBox1.Text.Trim(); // Get the text from the RichTextBox

            if (string.IsNullOrWhiteSpace(reviewText))
            {
                MessageBox.Show("Please write a review before submitting.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (reviewText.Length > 500)
            {
                MessageBox.Show("The review is too long. Please keep it within 500 characters.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Update the review for the existing user
                    string query = "UPDATE signup SET Review = @Review WHERE Id = @Id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        command.Parameters.AddWithValue("@Review", reviewText);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Review saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            userDashboard u = new userDashboard(id);
                            u.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("No existing user found. Please contact support.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            userDashboard u = new userDashboard(id);
            u.Show();
            this.Hide();

        }

        private void login_close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Query to set the Review field to NULL for the logged-in user
                    string query = "UPDATE signup SET Review = NULL WHERE Id = @Id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Review deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            richTextBox1.Clear(); // Clear the RichTextBox after deleting the review
                        }
                        else
                        {
                            MessageBox.Show("No review found to delete. Please check your account.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while deleting the review: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
