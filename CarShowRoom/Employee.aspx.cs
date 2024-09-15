using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class Employee : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string name = txtName.Text;
        string email = txtEmail.Text;
        string phoneNo = txtPhoneNo.Text;
        string address = txtAddress.Text;
        DateTime hireDate;
        decimal salary;
        string designation = txtDesignation.Text;

        if (!DateTime.TryParse(txtHireDate.Text, out hireDate))
        {
            lblMessage.Text = "Invalid Hire Date.";
            lblMessage.CssClass = "alert alert-danger";
            lblMessage.Visible = true;
            return;
        }

        if (!decimal.TryParse(txtSalary.Text, out salary))
        {
            lblMessage.Text = "Invalid Salary.";
            lblMessage.CssClass = "alert alert-danger";
            lblMessage.Visible = true;
            return;
        }

        string connectionString = ConfigurationManager.ConnectionStrings["CarShowroomConnectionString"].ConnectionString;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO Employee (Name, Email, PhoneNo, Address, HireDate, Salary, Designation) " +
                           "VALUES (@Name, @Email, @PhoneNo, @Address, @HireDate, @Salary, @Designation)";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@PhoneNo", phoneNo);
                command.Parameters.AddWithValue("@Address", address);
                command.Parameters.AddWithValue("@HireDate", hireDate);
                command.Parameters.AddWithValue("@Salary", salary);
                command.Parameters.AddWithValue("@Designation", designation);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    lblMessage.Text = "Employee added successfully.";
                    lblMessage.CssClass = "alert alert-success";
                    lblMessage.Visible = true;
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Error: " + ex.Message;
                    lblMessage.CssClass = "alert alert-danger";
                    lblMessage.Visible = true;
                }
            }
        }
    }

    protected void btnViewEmployees_Click(object sender, EventArgs e)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["CarShowroomConnectionString"].ConnectionString;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT EmployeeId, Name, Email, PhoneNo, Address, HireDate, Salary, Designation FROM Employee";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    gvEmployees.DataSource = dt;
                    gvEmployees.DataBind();
                    gvEmployees.Visible = true;
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Error: " + ex.Message;
                    lblMessage.CssClass = "alert alert-danger";
                    lblMessage.Visible = true;
                }
            }
        }
    }
}
