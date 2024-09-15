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
                // Optionally, you could load cars here if needed, but it’s handled by the View Cars button
            }
        }

        protected void btnAddCar_Click(object sender, EventArgs e)
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
        }

        protected void btnUpdateCar_Click(object sender, EventArgs e)
        {
            if (ViewState["CarId"] == null)
            {
                lblMessage.Text = "Select a car to update.";
                lblMessage.CssClass = "alert alert-danger";
                return;
            }

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
        }

        protected void btnDeleteCar_Click(object sender, EventArgs e)
        {
            if (ViewState["CarId"] == null)
            {
                lblMessage.Text = "Select a car to delete.";
                lblMessage.CssClass = "alert alert-danger";
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
                ViewState["CarId"] = gvCars.DataKeys[index].Value;

                ddlManufacturer.SelectedValue = gvCars.DataKeys[index].Values["ManufacturerId"].ToString();
                txtModel.Text = selectedRow.Cells[2].Text;
                txtYear.Text = selectedRow.Cells[3].Text;
                txtColour.Text = selectedRow.Cells[4].Text;
                txtPrice.Text = selectedRow.Cells[5].Text;
                txtMileage.Text = selectedRow.Cells[6].Text;
                txtEngineType.Text = selectedRow.Cells[7].Text;
                txtTransmission.Text = selectedRow.Cells[8].Text;
                txtDescription.Text = selectedRow.Cells[9].Text;
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
            }
        }

        private void BindGrid()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT CarId, (SELECT Name FROM MANUFACTURER WHERE MANUFACTURER.ManufacturerId = CAR.ManufacturerId) AS ManufacturerName, Model, Year, Colour, Price, Mileage, EngineType, Transmission, Description FROM CAR", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                gvCars.DataSource = dt;
                gvCars.DataBind();
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
    }
}