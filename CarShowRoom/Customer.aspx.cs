using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarShowRoom
{
    public partial class Customer : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["CarShowroomConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pnlCustomerForm.Visible = true;
                pnlCustomers.Visible = false;
                lblMessage.Text = string.Empty;
            }
        }

        protected void btnAddCustomer_Click(object sender, EventArgs e)
        {
            if (ValidateCustomerForm())
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO CUSTOMER (Name, Email, PhoneNo, Address, DOB, AadharCard, Gender) VALUES (@Name, @Email, @PhoneNo, @Address, @DOB, @AadharCard, @Gender)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Name", txtName.Text);
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@PhoneNo", txtPhoneNo.Text);
                        cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                        cmd.Parameters.AddWithValue("@DOB", txtDOB.Text);
                        cmd.Parameters.AddWithValue("@AadharCard", txtAadharCard.Text);
                        cmd.Parameters.AddWithValue("@Gender", txtGender.Text);


                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        ClearForm();
                        ShowMessage("Customer added successfully.", true);
                    }
                }
            }
            else
            {
                ShowMessage("Please fill out all required fields.", false);
            }
        }

        protected void btnUpdateCustomer_Click(object sender, EventArgs e)
        {
            if (ValidateCustomerForm())
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "UPDATE CUSTOMER SET Name = @Name, Email = @Email, PhoneNo = @PhoneNo, Address = @Address, DOB = @DOB, AadharCard = @AadharCard, Gender = @Gender WHERE CustomerId = @CustomerId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@CustomerId", hfCustomerId.Value);
                        cmd.Parameters.AddWithValue("@Name", txtName.Text);
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@PhoneNo", txtPhoneNo.Text);
                        cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                        cmd.Parameters.AddWithValue("@DOB", txtDOB.Text);
                        cmd.Parameters.AddWithValue("@AadharCard", txtAadharCard.Text);
                        cmd.Parameters.AddWithValue("@Gender", txtGender.Text);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        ClearForm();
                        ShowMessage("Customer updated successfully.", true);
                    }
                }
            }
            else
            {
                ShowMessage("Please fill out all required fields.", false);
            }
        }

        protected void btnViewCustomers_Click(object sender, EventArgs e)
        {
            BindCustomerGrid();
            pnlCustomerForm.Visible = false;
            pnlCustomers.Visible = true;
        }

        protected void gvCustomers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "UpdateCustomer")
            {
                int customerId = Convert.ToInt32(e.CommandArgument);
                LoadCustomerDetails(customerId);
            }
            else if (e.CommandName == "DeleteCustomer")
            {
                int customerId = Convert.ToInt32(e.CommandArgument);
                DeleteCustomer(customerId);
            }
        }

        protected void btnBackToManagement_Click(object sender, EventArgs e)
        {
            pnlCustomerForm.Visible = true;
            pnlCustomers.Visible = false;
        }

        private void BindCustomerGrid()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT CustomerId, Name, Email, PhoneNo, Address, DOB, AadharCard, Gender FROM CUSTOMER";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        gvCustomers.DataSource = dt;
                        gvCustomers.DataBind();
                    }
                }
            }
        }

        private void LoadCustomerDetails(int customerId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT CustomerId, Name, Email, PhoneNo, Address, DOB, AadharCard, Gender FROM CUSTOMER WHERE CustomerId = @CustomerId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@CustomerId", customerId);

                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            hfCustomerId.Value = dr["CustomerId"].ToString();
                            txtName.Text = dr["Name"].ToString();
                            txtEmail.Text = dr["Email"].ToString();
                            txtPhoneNo.Text = dr["PhoneNo"].ToString();
                            txtAddress.Text = dr["Address"].ToString();
                            txtDOB.Text = Convert.ToDateTime(dr["DOB"]).ToString("yyyy-MM-dd");
                            txtAadharCard.Text = dr["AadharCard"].ToString();
                            txtGender.Text = dr["Gender"].ToString();

                            btnAddCustomer.Visible = false;
                            btnUpdateCustomer.Visible = true;
                            pnlCustomerForm.Visible = true;
                            pnlCustomers.Visible = false;
                        }
                    }
                }
            }
        }

        private void DeleteCustomer(int customerId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM CUSTOMER WHERE CustomerId = @CustomerId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@CustomerId", customerId);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    BindCustomerGrid();
                    ShowMessage("Customer deleted successfully.", true);
                }
            }
        }

        private void ClearForm()
        {
            hfCustomerId.Value = string.Empty;
            txtName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPhoneNo.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtDOB.Text = string.Empty;
            txtAadharCard.Text = string.Empty;
            txtGender.Text = string.Empty;

            btnAddCustomer.Visible = true;
            btnUpdateCustomer.Visible = false;
        }

        private void ShowMessage(string message, bool isSuccess)
        {
            lblModalMessage.Text = message;
            lblModalMessage.ForeColor = isSuccess ? System.Drawing.Color.Green : System.Drawing.Color.Red;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "messageModal", "$('#messageModal').modal('show');", true);
        }

        private bool ValidateCustomerForm()
        {
            return !string.IsNullOrWhiteSpace(txtName.Text) &&
                   !string.IsNullOrWhiteSpace(txtEmail.Text) &&
                   !string.IsNullOrWhiteSpace(txtPhoneNo.Text) &&
                   !string.IsNullOrWhiteSpace(txtAddress.Text) &&
                   !string.IsNullOrWhiteSpace(txtDOB.Text) &&
                   !string.IsNullOrWhiteSpace(txtAadharCard.Text) &&
                   !string.IsNullOrWhiteSpace(txtGender.Text);
        }
    }
}