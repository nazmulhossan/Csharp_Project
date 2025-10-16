using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginRegistrationForm
{
    public partial class aboutUs : Form
    {
        int id;
        public aboutUs(int id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            userDashboard u = new userDashboard(id);
            u.Show();
            this.Hide();

        }
    }
}
