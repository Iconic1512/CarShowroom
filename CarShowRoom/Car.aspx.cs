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
            }
        }

        protected void btnAddCar_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["CarShowroomConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Car (Model, Year, Colour, Price) VALUES (@Model, @Year, @Colour, @Price)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Model", txtModel.Text);
                    cmd.Parameters.AddWithValue("@Year", txtYear.Text);
                    cmd.Parameters.AddWithValue("@Colour", txtColour.Text);
                    cmd.Parameters.AddWithValue("@Price", txtPrice.Text);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            BindGrid();
        }

        protected void btnUpdateCar_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["CarShowroomConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Car SET Model=@Model, Year=@Year, Colour=@Colour, Price=@Price WHERE CarId=@CarId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CarId", selectedCarId); // You need to manage the selected car ID
                    cmd.Parameters.AddWithValue("@Model", txtModel.Text);
                    cmd.Parameters.AddWithValue("@Year", txtYear.Text);
                    cmd.Parameters.AddWithValue("@Colour", txtColour.Text);
                    cmd.Parameters.AddWithValue("@Price", txtPrice.Text);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            BindGrid();
        }

        protected void btnDeleteCar_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["CarShowroomConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Car WHERE CarId=@CarId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CarId", selectedCarId); // You need to manage the selected car ID

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            BindGrid();
        }

        protected void gvCars_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvCars.Rows[index];
                selectedCarId = Convert.ToInt32(gvCars.DataKeys[row.RowIndex].Value);
                txtModel.Text = row.Cells[1].Text;
                txtYear.Text = row.Cells[2].Text;
                txtColour.Text = row.Cells[3].Text;
                txtPrice.Text = row.Cells[4].Text;
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
