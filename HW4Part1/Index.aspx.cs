using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.XPath;

namespace HW4Part1
{
    public partial class Index : System.Web.UI.Page
    {
        protected XmlDocument doc = new XmlDocument();
        protected XmlNode node;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string file = TextBox1.Text;
            XPathDocument dx = new XPathDocument(file);
            XPathNavigator nav = dx.CreateNavigator();
            XPathNodeIterator iterator = nav.Select("/Restaurants/Restaurant");//goes through each restaurant
            while(iterator.MoveNext())
            {
                XPathNodeIterator it = iterator.Current.Select("Name");
                it.MoveNext();
                TheDiv.InnerHtml = TheDiv.InnerHtml + "<br /> <br />Restaurant Name: " + it.Current.Value;//sets name
                it = iterator.Current.Select("Contact/Phone");
                it.MoveNext();
                TheDiv.InnerHtml = TheDiv.InnerHtml + "<br />Contact Information:<br />&nbsp;&nbsp;Phone: " + it.Current.Value;//sets phone
                it = iterator.Current.Select("Contact/Email");
                it.MoveNext();
                TheDiv.InnerHtml = TheDiv.InnerHtml + "<br />&nbsp;&nbsp;Email: " + it.Current.Value;//sets Email
                it = iterator.Current.Select("@Delivery");
                it.MoveNext();
                TheDiv.InnerHtml = TheDiv.InnerHtml + "<br />Delivery: " + it.Current.Value;//sets Email
                it = iterator.Current.Select("Website");
                it.MoveNext();
                TheDiv.InnerHtml = TheDiv.InnerHtml + 
                    "<br />Website: <a href='" + it.Current.Value + "'>" + it.Current.Value+"</a>";//sets websites
            
                if (it.Current.HasAttributes)
                {
                    it = iterator.Current.Select("Website/@facebook");
                    it.MoveNext();
                    TheDiv.InnerHtml = TheDiv.InnerHtml + "<br />Facebook: " + it.Current.Value;//sets Email
                }
            }

        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}