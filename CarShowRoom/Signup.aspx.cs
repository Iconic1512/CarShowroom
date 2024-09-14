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
            // Validate the input fields
            string errorMessage = "";

            if (string.IsNullOrWhiteSpace(txtName.Text))
                errorMessage += "Name is required.<br/>";
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
                errorMessage += "Email is required.<br/>";
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
                errorMessage += "Password is required.<br/>";
            if (string.IsNullOrWhiteSpace(txtPhoneNo.Text))
                errorMessage += "Phone No is required.<br/>";
            if (string.IsNullOrWhiteSpace(txtAddress.Text))
                errorMessage += "Address is required.<br/>";
            if (string.IsNullOrWhiteSpace(txtDOB.Text))
                errorMessage += "Date of Birth is required.<br/>";
            if (string.IsNullOrWhiteSpace(txtAadharCard.Text))
                errorMessage += "Aadhar Card is required.<br/>";
            if (ddlGender.SelectedValue == "")
                errorMessage += "Gender is required.<br/>";

            if (!string.IsNullOrEmpty(errorMessage))
            {
                // Show an alert if any required fields are missing
                ScriptManager.RegisterStartupScript(this, GetType(), "ShowAlert", $"showModal('{errorMessage}');", true);
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["CarShowroomConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
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
                            ScriptManager.RegisterStartupScript(this, GetType(), "ShowAlert", "showModal('Signup successful. Please login.');", true);
                            // Redirect after a delay to allow the user to see the message
                            Response.AddHeader("REFRESH", "3;URL=Login.aspx");
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "ShowAlert", "showModal('Signup failed. Please try again.');", true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log the error message and show a generic error message
                    // Ideally, use a logging framework or mechanism
                    ScriptManager.RegisterStartupScript(this, GetType(), "ShowAlert", $"showModal('Error: {ex.Message}');", true);
                }
            }
        }
    }
}
