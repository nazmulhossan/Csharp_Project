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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace LoginRegistrationForm
{

    public partial class incident : Form
    {
        SqlConnection con = new SqlConnection("data source=APURBO\\SQLEXPRESS;database=signup;integrated security=SSPI");
        int id;
        public incident(int id)
        {
            InitializeComponent();
            this.id = id;
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text.Trim()))
            {
                MessageBox.Show("Username is required!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(textBox1.Text.Trim()))
            {
                MessageBox.Show("Place is required!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(richTextBox1.Text.Trim()))
            {
                MessageBox.Show("Incident report is required!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // SQL Command to insert data into the Incident table
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Incident (Username,Place, Incident) VALUES (@Username,@Place, @Incident)", con))
                {
                    cmd.Parameters.AddWithValue("@Username", textBox2.Text);
                    cmd.Parameters.AddWithValue("@Place", textBox1.Text);
                    cmd.Parameters.AddWithValue("@Incident", richTextBox1.Text);

                    // Open the connection
                    con.Open();
                    cmd.ExecuteNonQuery(); // Execute the query
                    con.Close();

                    MessageBox.Show("Incident report submitted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Clear the fields after submission
                    textBox2.Text = ""; // Clear Username field
                    textBox1.Text = ""; // Clear Place field
                    richTextBox1.Text = ""; // Clear Incident field
                    richTextBox1.ReadOnly = false;
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
                using (SqlCommand cmd = new SqlCommand("SELECT Username,Place, Incident FROM Incident WHERE Place = @Place", con))
                {
                    cmd.Parameters.AddWithValue("@Place", textBox1.Text);

                    // Open the connection
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            richTextBox1.Clear(); // Clear existing text
                            richTextBox1.ReadOnly = true;

                            // Loop through the result and display each incident
                            while (reader.Read())
                            {
                                richTextBox1.AppendText("User: " + reader["Username"].ToString() + Environment.NewLine);
                                richTextBox1.AppendText(reader["Incident"].ToString() + Environment.NewLine + Environment.NewLine);
                            }
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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
