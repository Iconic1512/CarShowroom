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
        string connectionString = ConfigurationManager.ConnectionStrings["CarShowroomConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindManufacturerDropdown();
                lblMessage.Visible = false; // Hide message label on initial load
            }
        }

        protected void btnAddCar_Click(object sender, EventArgs e)
        {
            if (IsValidForm())
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO CAR (ManufacturerId, Model, Year, Colour, Price, Mileage, EngineType, Transmission, Description) VALUES (@ManufacturerId, @Model, @Year, @Colour, @Price, @Mileage, @EngineType, @Transmission, @Description)", con);
                    cmd.Parameters.AddWithValue("@ManufacturerId", ddlManufacturer.SelectedValue);
                    cmd.Parameters.AddWithValue("@Model", txtModel.Text);
                    cmd.Parameters.AddWithValue("@Year", int.Parse(txtYear.Text));
                    cmd.Parameters.AddWithValue("@Colour", txtColour.Text);
                    cmd.Parameters.AddWithValue("@Price", decimal.Parse(txtPrice.Text));
                    cmd.Parameters.AddWithValue("@Mileage", decimal.Parse(txtMileage.Text));
                    cmd.Parameters.AddWithValue("@EngineType", txtEngineType.Text);
                    cmd.Parameters.AddWithValue("@Transmission", txtTransmission.Text);
                    cmd.Parameters.AddWithValue("@Description", txtDescription.Text);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                ClearForm();
                BindGrid();
                ShowMessage("Car added successfully.", "alert-success");
            }
            else
            {
                ShowMessage("Please fill in all fields.", "alert-danger");
            }
        }

        protected void btnUpdateCar_Click(object sender, EventArgs e)
        {
            if (ViewState["CarId"] == null)
            {
                ShowMessage("Select a car to update.", "alert-danger");
                return;
            }

            if (IsValidForm())
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("UPDATE CAR SET ManufacturerId=@ManufacturerId, Model=@Model, Year=@Year, Colour=@Colour, Price=@Price, Mileage=@Mileage, EngineType=@EngineType, Transmission=@Transmission, Description=@Description WHERE CarId=@CarId", con);
                    cmd.Parameters.AddWithValue("@CarId", ViewState["CarId"]);
                    cmd.Parameters.AddWithValue("@ManufacturerId", ddlManufacturer.SelectedValue);
                    cmd.Parameters.AddWithValue("@Model", txtModel.Text);
                    cmd.Parameters.AddWithValue("@Year", int.Parse(txtYear.Text));
                    cmd.Parameters.AddWithValue("@Colour", txtColour.Text);
                    cmd.Parameters.AddWithValue("@Price", decimal.Parse(txtPrice.Text));
                    cmd.Parameters.AddWithValue("@Mileage", decimal.Parse(txtMileage.Text));
                    cmd.Parameters.AddWithValue("@EngineType", txtEngineType.Text);
                    cmd.Parameters.AddWithValue("@Transmission", txtTransmission.Text);
                    cmd.Parameters.AddWithValue("@Description", txtDescription.Text);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                ClearForm();
                BindGrid();
                ShowMessage("Car updated successfully.", "alert-success");
            }
            else
            {
                ShowMessage("Please fill in all fields.", "alert-danger");
            }
        }

        protected void btnDeleteCar_Click(object sender, EventArgs e)
        {
            if (ViewState["CarId"] == null)
            {
                ShowMessage("Select a car to delete.", "alert-danger");
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM CAR WHERE CarId=@CarId", con);
                cmd.Parameters.AddWithValue("@CarId", ViewState["CarId"]);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            ClearForm();
            BindGrid();
            ShowMessage("Car deleted successfully.", "alert-success");
        }

        protected void btnViewCars_Click(object sender, EventArgs e)
        {
            gvCars.Visible = !gvCars.Visible;
            if (gvCars.Visible)
            {
                BindGrid();
            }
        }

        protected void gvCars_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow selectedRow = gvCars.Rows[index];

                if (selectedRow != null)
                {
                    ViewState["CarId"] = gvCars.DataKeys[index].Value;

                    // Fetch the ManufacturerId based on the CarId
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("SELECT ManufacturerId FROM CAR WHERE CarId=@CarId", con);
                        cmd.Parameters.AddWithValue("@CarId", ViewState["CarId"]);
                        con.Open();
                        var manufacturerId = cmd.ExecuteScalar();
                        ddlManufacturer.SelectedValue = manufacturerId.ToString();
                        con.Close();
                    }

                    txtModel.Text = selectedRow.Cells[2].Text;
                    txtYear.Text = selectedRow.Cells[3].Text;
                    txtColour.Text = selectedRow.Cells[4].Text;
                    txtPrice.Text = selectedRow.Cells[5].Text;
                    txtMileage.Text = selectedRow.Cells[6].Text;
                    txtEngineType.Text = selectedRow.Cells[7].Text;
                    txtTransmission.Text = selectedRow.Cells[8].Text;
                    txtDescription.Text = selectedRow.Cells[9].Text;

                    // Rebind the grid to refresh the data
                    BindGrid();
                }
            }
            else if (e.CommandName == "Delete")
            {
                int carId = Convert.ToInt32(e.CommandArgument);
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM CAR WHERE CarId=@CarId", con);
                    cmd.Parameters.AddWithValue("@CarId", carId);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                BindGrid();
                ShowMessage("Car deleted successfully.", "alert-success");
            }
        }

        protected void gvCars_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int carId = Convert.ToInt32(gvCars.DataKeys[e.RowIndex].Value);
            string query = "DELETE FROM CAR WHERE CarId = @CarId";

            string constr = ConfigurationManager.ConnectionStrings["CarShowroomConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@CarId", carId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            lblMessage.Text = "Car deleted successfully.";
            lblMessage.CssClass = "alert alert-success";
            BindGrid(); // Rebind the GridView to refresh the data
        }

        private void BindGrid()
        {
            string query = "SELECT CarId, (SELECT Name FROM MANUFACTURER WHERE MANUFACTURER.ManufacturerId = CAR.ManufacturerId) AS ManufacturerName, Model, Year, Colour, Price, Mileage, EngineType, Transmission, Description FROM CAR";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        gvCars.DataSource = dt;
                        gvCars.DataBind();
                    }
                }
            }
        }

        private void BindManufacturerDropdown()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT ManufacturerId, Name FROM MANUFACTURER", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                ddlManufacturer.DataSource = dt;
                ddlManufacturer.DataTextField = "Name";
                ddlManufacturer.DataValueField = "ManufacturerId";
                ddlManufacturer.DataBind();
            }
        }

        private void ClearForm()
        {
            ddlManufacturer.SelectedIndex = -1;
            txtModel.Text = "";
            txtYear.Text = "";
            txtColour.Text = "";
            txtPrice.Text = "";
            txtMileage.Text = "";
            txtEngineType.Text = "";
            txtTransmission.Text = "";
            txtDescription.Text = "";
            ViewState["CarId"] = null;
        }

        private bool IsValidForm()
        {
            return ddlManufacturer.SelectedIndex != -1 &&
                   !string.IsNullOrWhiteSpace(txtModel.Text) &&
                   !string.IsNullOrWhiteSpace(txtYear.Text) &&
                   !string.IsNullOrWhiteSpace(txtColour.Text) &&
                   !string.IsNullOrWhiteSpace(txtPrice.Text) &&
                   !string.IsNullOrWhiteSpace(txtMileage.Text) &&
                   !string.IsNullOrWhiteSpace(txtEngineType.Text) &&
                   !string.IsNullOrWhiteSpace(txtTransmission.Text) &&
                   !string.IsNullOrWhiteSpace(txtDescription.Text);
        }

        private void ShowMessage(string message, string cssClass)
        {
            lblMessage.Text = message;
            lblMessage.CssClass = "alert " + cssClass;
            lblMessage.Visible = true;
        }
    }
}
