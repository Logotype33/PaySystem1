using PaySystemBL;
using PaySystemBL.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaySystem1
{
    public partial class Form1 : Form
    {
        readonly IAuthentication authentication = new Authentication();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            authentication.Login(textBox1.Text, textBox2.Text);
            if (User.IsAuthorized)
            {
                this.Close();
            }
                
            else
                MessageBox.Show("Wrong login or password");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DbConnection.Close();
        }
        private void button3_Click_1(object sender, EventArgs e)
        {
            authentication.Register(textBox1.Text, textBox2.Text);
            if (authentication.IsRegistred())
            {
                MessageBox.Show("Registred");
            }
            else
            {
                MessageBox.Show("Not Registred");
            }

        }
    }
}
