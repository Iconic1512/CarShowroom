using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

namespace CarShowRoom
{
    public partial class Customer : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["CarShowroomConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCustomers();
            }
        }

        private void LoadCustomers()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM CUSTOMER", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvCustomers.DataSource = dt;
                gvCustomers.DataBind();
            }
        }

        protected void gvCustomers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "UpdateCustomer")
            {
                int customerId = Convert.ToInt32(e.CommandArgument);
                PopulateCustomerDetails(customerId);
            }
            else if (e.CommandName == "DeleteCustomer")
            {
                int customerId = Convert.ToInt32(e.CommandArgument);
                DeleteCustomer(customerId);
                LoadCustomers(); // Refresh the customer list after deletion
            }
        }

        private void PopulateCustomerDetails(int customerId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM CUSTOMER WHERE CustomerId = @CustomerId", con);
                cmd.Parameters.AddWithValue("@CustomerId", customerId);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    hfCustomerId.Value = reader["CustomerId"].ToString(); // Store Customer ID
                    txtName.Text = reader["Name"].ToString();
                    txtEmail.Text = reader["Email"].ToString();
                    txtPhoneNo.Text = reader["PhoneNo"].ToString();
                    txtAddress.Text = reader["Address"].ToString();
                    txtDOB.Text = Convert.ToDateTime(reader["DOB"]).ToString("yyyy-MM-dd");
                    txtAadharCard.Text = reader["AadharCard"].ToString();
                    txtGender.Text = reader["Gender"].ToString();
                    // You may want to avoid displaying the password
                }
                con.Close();
            }
            pnlCustomers.Visible = false; // Hide the customers list
            pnlCustomerForm.Visible = true; // Show the customer form
            btnAddCustomer.Text = "Update Customer"; // Change button text to indicate update mode
        }

        protected void btnAddCustomer_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string phoneNo = txtPhoneNo.Text.Trim();
            string address = txtAddress.Text.Trim();
            string dob = txtDOB.Text.Trim();
            string aadharCard = txtAadharCard.Text.Trim();
            string gender = txtGender.Text.Trim();
            string password = ""; // Optional field

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                if (!string.IsNullOrEmpty(hfCustomerId.Value)) // If updating
                {
                    SqlCommand cmd = new SqlCommand("UPDATE CUSTOMER SET Name = @Name, Email = @Email, PhoneNo = @PhoneNo, Address = @Address, DOB = @DOB, AadharCard = @AadharCard, Gender = @Gender WHERE CustomerId = @CustomerId", con);
                    cmd.Parameters.AddWithValue("@CustomerId", hfCustomerId.Value);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@PhoneNo", phoneNo);
                    cmd.Parameters.AddWithValue("@Address", address);
                    cmd.Parameters.AddWithValue("@DOB", dob);
                    cmd.Parameters.AddWithValue("@AadharCard", aadharCard);
                    cmd.Parameters.AddWithValue("@Gender", gender);
                    // Do not include password in update query if you want to keep it unchanged

                    con.Open();
                    cmd.ExecuteNonQuery();
                    lblMessage.Text = "Customer updated successfully!";
                }
                else // If adding
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO CUSTOMER (Name, Email, PhoneNo, Address, DOB, AadharCard, Gender, Password) VALUES (@Name, @Email, @PhoneNo, @Address, @DOB, @AadharCard, @Gender, @Password)", con);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@PhoneNo", phoneNo);
                    cmd.Parameters.AddWithValue("@Address", address);
                    cmd.Parameters.AddWithValue("@DOB", dob);
                    cmd.Parameters.AddWithValue("@AadharCard", aadharCard);
                    cmd.Parameters.AddWithValue("@Gender", gender);
                    cmd.Parameters.AddWithValue("@Password", password); // You can choose to generate a random password if needed

                    con.Open();
                    cmd.ExecuteNonQuery();
                    lblMessage.Text = "Customer added successfully!";
                }
                con.Close();
                LoadCustomers();
                ClearForm();
            }
        }

        protected void btnBackToManagement_Click(object sender, EventArgs e)
        {
            pnlCustomers.Visible = true; // Show customer list
            pnlCustomerForm.Visible = false; // Hide customer form
            ClearForm(); // Clear the form fields
        }

        private void ClearForm()
        {
            txtName.Text = "";
            txtEmail.Text = "";
            txtPhoneNo.Text = "";
            txtAddress.Text = "";
            txtDOB.Text = "";
            txtAadharCard.Text = "";
            txtGender.Text = "";
            hfCustomerId.Value = ""; // Clear hidden field
            lblMessage.Text = ""; // Clear message label
            btnAddCustomer.Text = "Add Customer"; // Reset button text
        }

        private void DeleteCustomer(int customerId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM CUSTOMER WHERE CustomerId = @CustomerId", con);
                cmd.Parameters.AddWithValue("@CustomerId", customerId);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                lblMessage.Text = "Customer deleted successfully!";
            }
        }
    }
}
