using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarShowRoom
{
    public partial class Car : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
                BindManufacturers(); // Bind dropdown for ManufacturerId
            }
        }

        protected void btnAddCar_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["CarShowroomConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Car (ManufacturerId, Model, Year, Colour, Price, Mileage, EngineType, Transmission, Description) " +
                               "VALUES (@ManufacturerId, @Model, @Year, @Colour, @Price, @Mileage, @EngineType, @Transmission, @Description)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ManufacturerId", ddlManufacturer.SelectedValue);
                    cmd.Parameters.AddWithValue("@Model", txtModel.Text.Trim());
                    cmd.Parameters.AddWithValue("@Year", txtYear.Text.Trim());
                    cmd.Parameters.AddWithValue("@Colour", txtColour.Text.Trim());
                    cmd.Parameters.AddWithValue("@Price", txtPrice.Text.Trim());
                    cmd.Parameters.AddWithValue("@Mileage", txtMileage.Text.Trim());
                    cmd.Parameters.AddWithValue("@EngineType", txtEngineType.Text.Trim());
                    cmd.Parameters.AddWithValue("@Transmission", txtTransmission.Text.Trim());
                    cmd.Parameters.AddWithValue("@Description", txtDescription.Text.Trim());

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            BindGrid();
            ClearFields();
            lblMessage.Text = "Car added successfully.";
            lblMessage.ForeColor = System.Drawing.Color.Green;
        }

        protected void btnUpdateCar_Click(object sender, EventArgs e)
        {
            if (selectedCarId != -1)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["CarShowroomConnectionString"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Car SET ManufacturerId=@ManufacturerId, Model=@Model, Year=@Year, Colour=@Colour, Price=@Price, " +
                                   "Mileage=@Mileage, EngineType=@EngineType, Transmission=@Transmission, Description=@Description " +
                                   "WHERE CarId=@CarId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CarId", selectedCarId);
                        cmd.Parameters.AddWithValue("@ManufacturerId", ddlManufacturer.SelectedValue);
                        cmd.Parameters.AddWithValue("@Model", txtModel.Text.Trim());
                        cmd.Parameters.AddWithValue("@Year", txtYear.Text.Trim());
                        cmd.Parameters.AddWithValue("@Colour", txtColour.Text.Trim());
                        cmd.Parameters.AddWithValue("@Price", txtPrice.Text.Trim());
                        cmd.Parameters.AddWithValue("@Mileage", txtMileage.Text.Trim());
                        cmd.Parameters.AddWithValue("@EngineType", txtEngineType.Text.Trim());
                        cmd.Parameters.AddWithValue("@Transmission", txtTransmission.Text.Trim());
                        cmd.Parameters.AddWithValue("@Description", txtDescription.Text.Trim());

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                BindGrid();
                ClearFields();
                lblMessage.Text = "Car updated successfully.";
                lblMessage.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblMessage.Text = "Please select a car to update.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnDeleteCar_Click(object sender, EventArgs e)
        {
            if (selectedCarId != -1)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["CarShowroomConnectionString"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Car WHERE CarId=@CarId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CarId", selectedCarId);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                BindGrid();
                ClearFields();
                lblMessage.Text = "Car deleted successfully.";
                lblMessage.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblMessage.Text = "Please select a car to delete.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void gvCars_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvCars.Rows[index];
                selectedCarId = Convert.ToInt32(gvCars.DataKeys[row.RowIndex].Value);
                ddlManufacturer.SelectedValue = row.Cells[1].Text;
                txtModel.Text = row.Cells[2].Text;
                txtYear.Text = row.Cells[3].Text;
                txtColour.Text = row.Cells[4].Text;
                txtPrice.Text = row.Cells[5].Text;
                txtMileage.Text = row.Cells[6].Text;
                txtEngineType.Text = row.Cells[7].Text;
                txtTransmission.Text = row.Cells[8].Text;
                txtDescription.Text = row.Cells[9].Text;
                lblMessage.Text = string.Empty; // Clear any previous message
            }
        }

        private void BindGrid()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["CarShowroomConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Car";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gvCars.DataSource = dt;
                    gvCars.DataBind();
                }
            }
        }

        private void BindManufacturers()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["CarShowroomConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT ManufacturerId, Name FROM Manufacturer";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    ddlManufacturer.DataSource = dt;
                    ddlManufacturer.DataTextField = "Name";
                    ddlManufacturer.DataValueField = "ManufacturerId";
                    ddlManufacturer.DataBind();
                }
            }
        }

        private void ClearFields()
        {
            ddlManufacturer.SelectedIndex = 0;
            txtModel.Text = string.Empty;
            txtYear.Text = string.Empty;
            txtColour.Text = string.Empty;
            txtPrice.Text = string.Empty;
            txtMileage.Text = string.Empty;
            txtEngineType.Text = string.Empty;
            txtTransmission.Text = string.Empty;
            txtDescription.Text = string.Empty;
            selectedCarId = -1; // Reset the selected car ID
        }

        private int selectedCarId
        {
            get
            {
                if (ViewState["SelectedCarId"] != null)
                {
                    return Convert.ToInt32(ViewState["SelectedCarId"]);
                }
                return -1; // Default value if no ID is selected
            }
            set
            {
                ViewState["SelectedCarId"] = value;
            }
        }
    }
}
