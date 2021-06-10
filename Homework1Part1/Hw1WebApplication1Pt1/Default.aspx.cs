using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hw1WebApplication1Pt1
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            StringModify1.Service1Client client = new StringModify1.Service1Client();
            string userText = TextBox1.Text;
            TextBox2.Text = (client.vowelCount(userText)).ToString();//sets vowel text box
            TextBox3.Text = (client.capitalCount(userText)).ToString();//sets captial text box
            TextBox4.Text = client.reverseString(userText);
        }
    }
}