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
    public partial class adminIncident : Form
    {
        SqlConnection con = new SqlConnection("data source=APURBO\\SQLEXPRESS;database=signup;integrated security=SSPI");
        public adminIncident()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                // SQL Command to update the Incident in the database
                using (SqlCommand cmd = new SqlCommand("UPDATE Incident SET Username = @Username, Incident = @Incident WHERE Place = @Place", con))
                {
                    cmd.Parameters.AddWithValue("@Username", textBox2.Text.Trim());
                    cmd.Parameters.AddWithValue("@Incident", richTextBox1.Text.Trim());
                    cmd.Parameters.AddWithValue("@Place", textBox1.Text.Trim());
                    // Admin provides the username to update the specific incident

                    // Open the connection
                    con.Open();
                    cmd.ExecuteNonQuery(); // Execute the query to update the incident
                    con.Close();

                    MessageBox.Show("Incident report updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Set richTextBox1 back to read-only after updating
                    richTextBox1.ReadOnly = true;
                    textBox2.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Please enter the Place to search for incidents.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // SQL Command to retrieve data from the Incident table based on Place
                using (SqlCommand cmd = new SqlCommand("SELECT Username, Place, Incident FROM Incident WHERE Place = @Place", con))
                {
                    cmd.Parameters.AddWithValue("@Place", textBox1.Text);

                    // Open the connection
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            richTextBox1.Clear(); // Clear existing text

                            // Loop through the result and display each incident
                            while (reader.Read())
                            {
                                textBox2.Text = reader["Username"].ToString();
                                richTextBox1.AppendText(reader["Incident"].ToString() + Environment.NewLine + Environment.NewLine);
                            }

                            // Allow the admin to edit the incident after searching
                            richTextBox1.ReadOnly = false;
                            textBox2.ReadOnly = false;
                        }
                        else
                        {
                            MessageBox.Show("No incident reports found for the specified place.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            adminDashboard a = new adminDashboard();
            a.Show();
            this.Hide();
        }

        private void login_close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Please ensure both Place and Username are filled before attempting to delete.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // SQL query to delete the record from the Incident table
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Incident WHERE Place = @Place AND Username = @Username", con))
                {
                    // Add parameters to prevent SQL injection
                    cmd.Parameters.AddWithValue("@Place", textBox1.Text.Trim());
                    cmd.Parameters.AddWithValue("@Username", textBox2.Text.Trim());

                    // Open the connection
                    con.Open();

                    // Execute the DELETE command
                    int rowsAffected = cmd.ExecuteNonQuery();
                    con.Close();

                    if (rowsAffected > 0)
                    {
                        // Clear the fields after successful deletion
                        textBox1.Text = "";
                        textBox2.Text = "";
                        richTextBox1.Clear();

                        // Optionally reset readonly properties
                        richTextBox1.ReadOnly = true;
                        textBox2.ReadOnly = true;

                        MessageBox.Show("Incident report deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No matching incident found to delete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while deleting the record: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
