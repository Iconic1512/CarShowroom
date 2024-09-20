using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

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
                SqlCommand cmd = new SqlCommand("INSERT INTO CAR (ManufacturerId, Model, Year, Colour, Name, Price, Mileage, EngineType, Transmission, Description) OUTPUT INSERTED.CarId VALUES (@ManufacturerId, @Model, @Year, @Colour, @Name, @Price, @Mileage, @EngineType, @Transmission, @Description)", con);

                cmd.Parameters.AddWithValue("@ManufacturerId", ddlManufacturer.SelectedValue);
                cmd.Parameters.AddWithValue("@Model", txtModel.Text);
                cmd.Parameters.AddWithValue("@Year", int.Parse(txtYear.Text));
                cmd.Parameters.AddWithValue("@Colour", txtColour.Text);
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@Price", decimal.Parse(txtPrice.Text));
                cmd.Parameters.AddWithValue("@Mileage", decimal.Parse(txtMileage.Text));
                cmd.Parameters.AddWithValue("@EngineType", txtEngineType.Text);
                cmd.Parameters.AddWithValue("@Transmission", txtTransmission.Text);
                cmd.Parameters.AddWithValue("@Description", txtDescription.Text);

                con.Open();

                // Execute the insert and retrieve the generated CarId
                int carId = (int)cmd.ExecuteScalar(); // Get the inserted CarId

                // Insert the record into the Inventory table
                SqlCommand cmdInventory = new SqlCommand("INSERT INTO Inventory (CarId) VALUES (@CarId)", con);
                cmdInventory.Parameters.AddWithValue("@CarId", carId);

                cmdInventory.ExecuteNonQuery(); // Insert into Inventory with default values

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
                SqlCommand cmd = new SqlCommand("UPDATE CAR SET ManufacturerId=@ManufacturerId, Model=@Model, Year=@Year, Colour=@Colour,Name=@Name, Price=@Price, Mileage=@Mileage, EngineType=@EngineType, Transmission=@Transmission, Description=@Description WHERE CarId=@CarId", con);
                cmd.Parameters.AddWithValue("@CarId", ViewState["CarId"]);
                cmd.Parameters.AddWithValue("@ManufacturerId", ddlManufacturer.SelectedValue);
                cmd.Parameters.AddWithValue("@Model", txtModel.Text);
                cmd.Parameters.AddWithValue("@Year", int.Parse(txtYear.Text));
                cmd.Parameters.AddWithValue("@Colour", txtColour.Text);
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
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
                // Delete from Inventory table using CarId
                SqlCommand deleteInventoryCmd = new SqlCommand("DELETE FROM Inventory WHERE CarId = @CarId", con);
                deleteInventoryCmd.Parameters.AddWithValue("@CarId", ViewState["CarId"]);

                // Open connection and execute delete command for Inventory
                con.Open();
                deleteInventoryCmd.ExecuteNonQuery();

                // Then delete from CAR table
                SqlCommand deleteCarCmd = new SqlCommand("DELETE FROM CAR WHERE CarId=@CarId", con);
                deleteCarCmd.Parameters.AddWithValue("@CarId", ViewState["CarId"]);

                // Execute delete command for CAR
                deleteCarCmd.ExecuteNonQuery();
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
                txtName.Text = selectedRow.Cells[4].Text;
                txtColour.Text = selectedRow.Cells[5].Text;
                txtPrice.Text = selectedRow.Cells[6].Text;
                txtMileage.Text = selectedRow.Cells[7].Text;
                txtEngineType.Text = selectedRow.Cells[8].Text;
                txtTransmission.Text = selectedRow.Cells[9].Text;
                txtDescription.Text = selectedRow.Cells[10].Text;
            }
            else if (e.CommandName == "Delete")
            {
                int carId = Convert.ToInt32(e.CommandArgument);

                // Log carId for debugging purposes
                Console.WriteLine("CarId to delete: " + carId);

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand deleteInventoryCmd = new SqlCommand("DELETE FROM Inventory WHERE CarId = @CarId", con);
                    deleteInventoryCmd.Parameters.AddWithValue("@CarId", carId);

                    con.Open();
                    deleteInventoryCmd.ExecuteNonQuery();

                    SqlCommand deleteCarCmd = new SqlCommand("DELETE FROM CAR WHERE CarId=@CarId", con);
                    deleteCarCmd.Parameters.AddWithValue("@CarId", carId);

                    deleteCarCmd.ExecuteNonQuery();
                    con.Close();
                }

            }
            BindGrid();


        }

        protected void gvCars_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Do nothing because deletion is handled in RowCommand
        }


        private void BindGrid()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT CarId, (SELECT Name FROM MANUFACTURER WHERE MANUFACTURER.ManufacturerId = CAR.ManufacturerId) AS ManufacturerName, Model, Year, Name,Colour, Price, Mileage, EngineType, Transmission, Description FROM CAR", con);
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
            txtName.Text = "";
            txtPrice.Text = "";
            txtMileage.Text = "";
            txtEngineType.Text = "";
            txtTransmission.Text = "";
            txtDescription.Text = "";
            ViewState["CarId"] = null;
        }
    }
}