// Home.aspx.cs
using System;
using System.Web.UI;

namespace CarShowRoom
{
    public partial class Home : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CustomerId"] == null)
            {
                Response.Redirect("Login.aspx");
            }
        }
    }
}
