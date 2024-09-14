using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarShowRoom
{
    public partial class Customer : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pnlCustomers.Visible = false;
                pnlCustomerForm.Visible = true;
            }
        }

        protected void btnAddCustomer_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) ||
                string.IsNullOrEmpty(txtEmail.Text) ||
                string.IsNullOrEmpty(txtPhoneNo.Text) ||
                string.IsNullOrEmpty(txtAddress.Text) ||
                string.IsNullOrEmpty(txtDOB.Text) ||
                string.IsNullOrEmpty(txtAadharCard.Text) ||
                string.IsNullOrEmpty(txtGender.Text) ||
                string.IsNullOrEmpty(txtPassword.Text))
            {
                ShowModal("All fields are required.");
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["CarShowroomConnectionString"].ConnectionString;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Customer (Name, Email, PhoneNo, Address, DOB, AadharCard, Gender, Password) VALUES (@Name, @Email, @PhoneNo, @Address, @DOB, @AadharCard, @Gender, @Password)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", txtName.Text);
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@PhoneNo", txtPhoneNo.Text);
                        cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                        cmd.Parameters.AddWithValue("@DOB", txtDOB.Text);
                        cmd.Parameters.AddWithValue("@AadharCard", txtAadharCard.Text);
                        cmd.Parameters.AddWithValue("@Gender", txtGender.Text);
                        cmd.Parameters.AddWithValue("@Password", txtPassword.Text);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                ShowModal("Customer added successfully.");
                ClearForm();
            }
            catch (Exception ex)
            {
                ShowModal("An error occurred while adding the customer: " + ex.Message);
            }
        }

        protected void btnUpdateCustomer_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCustomerId.Text))
            {
                ShowModal("Customer ID is required for update.");
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["CarShowroomConnectionString"].ConnectionString;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Customer SET Name=@Name, Email=@Email, PhoneNo=@PhoneNo, Address=@Address, DOB=@DOB, AadharCard=@AadharCard, Gender=@Gender, Password=@Password WHERE CustomerId=@CustomerId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CustomerId", txtCustomerId.Text);
                        cmd.Parameters.AddWithValue("@Name", txtName.Text);
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@PhoneNo", txtPhoneNo.Text);
                        cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                        cmd.Parameters.AddWithValue("@DOB", txtDOB.Text);
                        cmd.Parameters.AddWithValue("@AadharCard", txtAadharCard.Text);
                        cmd.Parameters.AddWithValue("@Gender", txtGender.Text);
                        cmd.Parameters.AddWithValue("@Password", txtPassword.Text);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                ShowModal("Customer updated successfully.");
                ClearForm();
            }
            catch (Exception ex)
            {
                ShowModal("An error occurred while updating the customer: " + ex.Message);
            }
        }

        protected void btnViewCustomers_Click(object sender, EventArgs e)
        {
            BindGrid();
            pnlCustomers.Visible = true;
            pnlCustomerForm.Visible = false;
        }

        protected void btnBackToManagement_Click(object sender, EventArgs e)
        {
            pnlCustomers.Visible = false;
            pnlCustomerForm.Visible = true;
        }

        protected void gvCustomers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteCustomer")
            {
                int customerId = Convert.ToInt32(e.CommandArgument);
                DeleteCustomer(customerId);
                BindGrid();
            }
            else if (e.CommandName == "UpdateCustomer")
            {
                int customerId = Convert.ToInt32(e.CommandArgument);
                LoadCustomerData(customerId);
            }
        }

        private void BindGrid()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["CarShowroomConnectionString"].ConnectionString;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Customer";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        gvCustomers.DataSource = dt;
                        gvCustomers.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                ShowModal("An error occurred while loading customers: " + ex.Message);
            }
        }

        private void DeleteCustomer(int customerId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["CarShowroomConnectionString"].ConnectionString;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Customer WHERE CustomerId=@CustomerId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CustomerId", customerId);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                ShowModal("Customer deleted successfully.");
            }
            catch (Exception ex)
            {
                ShowModal("An error occurred while deleting the customer: " + ex.Message);
            }
        }

        private void LoadCustomerData(int customerId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["CarShowroomConnectionString"].ConnectionString;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Customer WHERE CustomerId=@CustomerId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CustomerId", customerId);
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            txtCustomerId.Text = reader["CustomerId"].ToString();
                            txtName.Text = reader["Name"].ToString();
                            txtEmail.Text = reader["Email"].ToString();
                            txtPhoneNo.Text = reader["PhoneNo"].ToString();
                            txtAddress.Text = reader["Address"].ToString();
                            txtDOB.Text = reader["DOB"].ToString();
                            txtAadharCard.Text = reader["AadharCard"].ToString();
                            txtGender.Text = reader["Gender"].ToString();
                            txtPassword.Text = reader["Password"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowModal("An error occurred while loading customer data: " + ex.Message);
            }
        }

        private void ClearForm()
        {
            txtCustomerId.Text = string.Empty;
            txtName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPhoneNo.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtDOB.Text = string.Empty;
            txtAadharCard.Text = string.Empty;
            txtGender.Text = string.Empty;
            txtPassword.Text = string.Empty;
        }

        private void ShowModal(string message)
        {
            lblModalMessage.Text = message;
            ClientScript.RegisterStartupScript(this.GetType(), "showModal", "showModal();", true);
        }
    }
}
