using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hw1WindowsFormPt1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            StringModify.Service1Client client = new StringModify.Service1Client();
            string userText = textBox1.Text;
            textBox2.Text = (client.vowelCount(userText)).ToString();//sets vowel text box
            textBox3.Text = (client.capitalCount(userText)).ToString();//sets captial text box
            textBox4.Text = client.reverseString(userText);
        }
    }
}
