using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.UI;  // Add this namespace to access configuration settings

namespace CarShowRoom
{
    public partial class Service : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["CarShowRoomConnectionString"].ConnectionString; // Use the connection string from web.config

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCars();
                LoadCustomers();
                LoadServiceRecords();
            }
        }

        private void LoadCars()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT CarId, Model FROM Car", conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                ddlCarId.DataSource = reader;
                ddlCarId.DataTextField = "CarId";  // Display the car model in the dropdown
                ddlCarId.DataValueField = "CarId"; // Use CarId as the value
                ddlCarId.DataBind();
                reader.Close();
            }

            // Insert a default item at the top
            ddlCarId.Items.Insert(0, new ListItem("--Select Car--", ""));  // Empty string as the value means null
        }


        private void LoadCustomers()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT CustomerId, Name FROM Customer", conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                ddlCustomerId.DataSource = reader;
                ddlCustomerId.DataTextField = "CustomerId";
                ddlCustomerId.DataValueField = "CustomerId";
                ddlCustomerId.DataBind();
                reader.Close();
            }
            ddlCustomerId.Items.Insert(0, new ListItem("--Select Customer--", ""));
        }

        private void LoadServiceRecords()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Service_Record", conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                gvServiceRecords.DataSource = dt;
                gvServiceRecords.DataBind();
                gvServiceRecords.Visible = dt.Rows.Count > 0;
            }
        }

        protected void btnAddService_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";  // Reset message
            lblMessage.CssClass = "alert alert-danger d-none";  // Reset class to hide

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Service_Record (CarId, CustomerId, Cost, ServiceDate) VALUES (@CarId, @CustomerId, @Cost, @ServiceDate)", conn);
                cmd.Parameters.AddWithValue("@CarId", ddlCarId.SelectedValue);
                cmd.Parameters.AddWithValue("@CustomerId", ddlCustomerId.SelectedValue);
                cmd.Parameters.AddWithValue("@Cost", Convert.ToDecimal(txtCost.Text));
                cmd.Parameters.AddWithValue("@ServiceDate", Convert.ToDateTime(txtServiceDate.Text));

                conn.Open();
                cmd.ExecuteNonQuery();

                lblMessage.Text = "Service record added successfully.";
                lblMessage.CssClass = "alert alert-success";
            }

            ResetFields();
            LoadServiceRecords();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "HideLabel", "hideMessage();", true);
        }

        protected void gvServiceRecords_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            lblMessage.Text = "";  // Reset message
            lblMessage.CssClass = "alert alert-danger d-none";  // Reset class to hide

            int serviceRecordId = Convert.ToInt32(gvServiceRecords.DataKeys[e.RowIndex].Value);
            DeleteServiceRecord(serviceRecordId);
            LoadServiceRecords();
        }



        protected void btnViewService_Click(object sender, EventArgs e)
        {
            LoadServiceRecords();
        }




        private void DeleteServiceRecord(int serviceRecordId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Service_Record WHERE ServiceRecordId = @ServiceRecordId", conn);
                cmd.Parameters.AddWithValue("@ServiceRecordId", serviceRecordId);
                conn.Open();
                cmd.ExecuteNonQuery();
                lblMessage.Text = "Service record deleted successfully.";
                lblMessage.CssClass = "alert alert-success"; // Show the message (removes 'd-none')
                ScriptManager.RegisterStartupScript(this, this.GetType(), "HideLabel", "hideMessage();", true);
            }
        }



        private void ResetFields()
        {
            ddlCarId.SelectedIndex = -1; // Reset dropdown to default
            ddlCustomerId.SelectedIndex = -1; // Reset dropdown to default
            txtCost.Text = "";
            txtServiceDate.Text = "";
            txtModel.Text = "";
            txtYear.Text = "";
            txtColor.Text = "";
            txtPrice.Text = "";
            txtMileage.Text = "";
            txtName.Text = "";
            txtCustomername.Text = "";
        }

        protected void ddlCarId_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMessage.CssClass = "alert alert-danger d-none"; // Show the message (removes 'd-none')
            ScriptManager.RegisterStartupScript(this, this.GetType(), "HideLabel", "hideMessage();", true);

            int carId = Convert.ToInt32(ddlCarId.SelectedValue);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT Model, Mileage, Name, Year, Colour, Price FROM Car WHERE CarId = @CarId", conn);
                cmd.Parameters.AddWithValue("@CarId", carId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    txtMileage.Text = reader["Mileage"].ToString();
                    txtName.Text = reader["Name"].ToString();
                    txtModel.Text = reader["Model"].ToString();
                    txtYear.Text = reader["Year"].ToString();
                    txtColor.Text = reader["Colour"].ToString();
                    txtPrice.Text = reader["Price"].ToString();
                }
                reader.Close();
            }
        }

        protected void ddlCustomerId_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMessage.CssClass = "alert alert-danger d-none"; // Show the message (removes 'd-none')
            ScriptManager.RegisterStartupScript(this, this.GetType(), "HideLabel", "hideMessage();", true);

            int CustomerId = Convert.ToInt32(ddlCustomerId.SelectedValue);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT Name FROM Customer WHERE CustomerId = @CustomerId", conn);
                cmd.Parameters.AddWithValue("@CustomerId", CustomerId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {

                    txtCustomername.Text = reader["Name"].ToString();
                }
                reader.Close();
            }
        }

    }
}
// error is that that after deleting or adding a record messsege does not roperly for 3 sec first time work but after that if we lick to select car id then it shows ahgain