using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TryItPagePart3
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            if (sender.Equals(Submit))
            {
                string latitude = LatitudeBox.Text;
                string longitude = LongitudeBox.Text;

                string url = "http://localhost:55037/Service1.svc/getForcast/" + latitude + "/" + longitude;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                WebResponse response = request.GetResponse();

                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);

                string result = reader.ReadToEnd();//get result
                AnswerBox.Text = result;
                response.Close();
            }
            else if(sender.Equals(Submit1))
            {
                string latitude = LatitudeBox1.Text;
                string longitude = LongitudeBox1.Text;

                string url = "http://localhost:55037/Service1.svc/getSolar/" + latitude + "/" + longitude;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                WebResponse response = request.GetResponse();

                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);

                string result = reader.ReadToEnd();//get result
                AnswerBox1.Text = result;
                response.Close();
            }
        }
    }
}