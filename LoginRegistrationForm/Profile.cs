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
using static System.Net.Mime.MediaTypeNames;
using System.IO;


namespace LoginRegistrationForm
{
    public partial class Profile : Form
    {
        private string connectionString = "data source=APURBO\\SQLEXPRESS; database=signup; integrated security=SSPI";
        private int id;
        private string imageLocation = string.Empty;
        public Profile(int id, string username, string password)
        {
            InitializeComponent();
            textBox1.Text = id.ToString();
            textBox2.Text = password;
            textBox3.Text = username;

            SetTextBoxesReadOnly();
        }
        public Profile(int id)
        {
            InitializeComponent();
            this.id = id;
            LoadUserInfo();
            SetTextBoxesReadOnly();
        }
        private void LoadUserInfo()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "SELECT Id, Username, Password,Photo FROM signup WHERE Id = @Id";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        con.Open();

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                textBox1.Text = dr["Id"].ToString();
                                textBox3.Text = dr["Username"].ToString();
                                textBox2.Text = dr["Password"].ToString();

                                if (dr["Photo"] != DBNull.Value)
                                {
                                    byte[] imageBytes = (byte[])dr["Photo"];
                                    using (MemoryStream ms = new MemoryStream(imageBytes))
                                    {
                                        image1.Image = System.Drawing.Image.FromStream(ms);
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("User information not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.Close();
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

        private void Profile_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int id = int.Parse(textBox1.Text);
            string username = textBox3.Text;
            string password = textBox2.Text;

            myinfo m = new myinfo(id, username, password);
            m.Show();
            this.Hide();

        }
        private void SetTextBoxesReadOnly()
        {
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox3.ReadOnly = true;

            // Change the background color to indicate the text boxes are not editable
            textBox1.BackColor = Color.White;
            textBox2.BackColor = Color.White;
            textBox3.BackColor = Color.White;

            // Remove the cursor focus from the text boxes
            textBox1.TabStop = false;
            textBox2.TabStop = false;
            textBox3.TabStop = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    OpenFileDialog dialog = new OpenFileDialog();
                    dialog.Filter = "JPG files (*.jpg)|*.jpg|PNG files (*.png)|*.png|All Files (*.*)|*.*";

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        imageLocation = dialog.FileName;
                        image1.ImageLocation = imageLocation;

                        byte[] images;
                        using (FileStream stream = new FileStream(imageLocation, FileMode.Open, FileAccess.Read))
                        {
                            using (BinaryReader brs = new BinaryReader(stream))
                            {
                                images = brs.ReadBytes((int)stream.Length);
                            }
                        }

                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            string query = "UPDATE signup SET Photo = @Photo WHERE Id = @Id";

                            using (SqlCommand cmd = new SqlCommand(query, connection))
                            {
                                cmd.Parameters.AddWithValue("@Photo", images);
                                cmd.Parameters.AddWithValue("@Id", id);

                                int rowsAffected = cmd.ExecuteNonQuery();
                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Image uploaded and saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("Failed to save the image. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            

                // Navigate to userDashboard after saving the image
                userDashboard user = new userDashboard(id);
                user.Show();
                this.Hide();
            }
            
        

        private void login_close_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void image1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "SELECT Photo FROM signup WHERE Id = @Id";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        con.Open();

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read() && dr["Photo"] != DBNull.Value)
                            {
                                byte[] imageBytes = (byte[])dr["Photo"];
                                using (MemoryStream ms = new MemoryStream(imageBytes))
                                {
                                    image1.Image = System.Drawing.Image.FromStream(ms);
                                }
                            }
                            else
                            {
                                MessageBox.Show("No image found for this user.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}
