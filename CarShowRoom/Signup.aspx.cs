using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace CarShowRoom
{
    public partial class Signup : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSignup_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["CarShowroomConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Customer (Name, Email, PhoneNo, Address, DOB, AadharCard, Gender, Password) " +
                               "VALUES (@Name, @Email, @PhoneNo, @Address, @DOB, @AadharCard, @Gender, @Password)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@PhoneNo", txtPhoneNo.Text);
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@DOB", txtDOB.Text);
                    cmd.Parameters.AddWithValue("@AadharCard", txtAadharCard.Text);
                    cmd.Parameters.AddWithValue("@Gender", ddlGender.SelectedValue);
                    cmd.Parameters.AddWithValue("@Password", txtPassword.Text);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        lblMessage.Text = "Signup successful. Please login.";
                        Response.Redirect("Login.aspx");
                    }
                    else
                    {
                        lblMessage.Text = "Signup failed. Please try again.";
                    }
                }
            }
        }
    }
}
