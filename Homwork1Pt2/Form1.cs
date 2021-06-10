using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Homwork1Pt2
{
    public partial class Form1 : Form
    {
        private string verifierString;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)//navigates to the web page
        {
            webBrowser1.Navigate(textBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            webBrowser1.Refresh();
        }

        private void button3_Click(object sender, EventArgs e)//encript the text in the textbox
        {
            EncryptDecrypt.ServiceClient encrypt = new EncryptDecrypt.ServiceClient();
            textBox2.Text = encrypt.Encrypt(textBox2.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            EncryptDecrypt.ServiceClient decrypt = new EncryptDecrypt.ServiceClient();
            textBox2.Text = decrypt.Decrypt(textBox2.Text);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)//generate Image
        {
            VerifierString.ServiceClient image = new VerifierString.ServiceClient();
            verifierString = image.GetVerifierString(textBox3.Text);
            System.IO.Stream stream = image.GetImage(verifierString);
            Image i = Image.FromStream(stream);
            pictureBox1.Image = i;
        }

        private void button5_Click(object sender, EventArgs e)//check if the string is correct
        {
            string userInput = textBox4.Text;
            if(userInput.Equals(verifierString))
            {
                label2.Text = "Correct!";
            }
            else
            {
                label2.Text = "Incorrect";
            }
        }
    }
}
