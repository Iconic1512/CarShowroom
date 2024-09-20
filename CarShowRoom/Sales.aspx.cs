using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarShowRoom
{
    public partial class Sales : Page
    {
        private int _currentSaleId = 0; // To store the ID of the currently edited sale

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        protected void btnAddSale_Click(object sender, EventArgs e)
        {
            if (_currentSaleId == 0) // If no sale is being edited
            {
                AddSale();
            }
            else // If a sale is being edited
            {
                UpdateSale(_currentSaleId);
            }
        }

        protected void btnViewSales_Click(object sender, EventArgs e)
        {
            BindGrid();
            ClearForm();
        }

        protected void gvSales_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "UpdateSale")
            {
                _currentSaleId = Convert.ToInt32(e.CommandArgument);
                PopulateSaleDetails(_currentSaleId);
            }
            else if (e.CommandName == "DeleteSale")
            {
                int saleId = Convert.ToInt32(e.CommandArgument);
                DeleteSale(saleId);
            }
        }

        private void AddSale()
        {
            // Check for required fields
            if (string.IsNullOrWhiteSpace(txtCustomerId.Text))
            {
                ShowModal("Customer ID is required.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtCarId.Text))
            {
                ShowModal("Car ID is required.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtEmployeeId.Text))
            {
                ShowModal("Employee ID is required.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtSaleDate.Text))
            {
                ShowModal("Sale Date is required.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtSalePrice.Text))
            {
                ShowModal("Sale Price is required.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPaymentMethod.Text))
            {
                ShowModal("Payment Method is required.");
                return;
            }

            // Check if Sale Price is a valid decimal
            decimal salePrice;
            if (!decimal.TryParse(txtSalePrice.Text, out salePrice))
            {
                ShowModal("Sale Price must be a valid number.");
                return;
            }

            // If all required fields are provided, proceed to add the sale
            string connectionString = ConfigurationManager.ConnectionStrings["CarShowroomConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Sales (CustomerId, CarId, EmployeeId, SaleDate, SalePrice, PaymentMethod, Status) " +
                               "VALUES (@CustomerId, @CarId, @EmployeeId, @SaleDate, @SalePrice, @PaymentMethod, 'Active')";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CustomerId", txtCustomerId.Text);
                    cmd.Parameters.AddWithValue("@CarId", txtCarId.Text);
                    cmd.Parameters.AddWithValue("@EmployeeId", txtEmployeeId.Text);
                    cmd.Parameters.AddWithValue("@SaleDate", txtSaleDate.Text);
                    cmd.Parameters.AddWithValue("@SalePrice", salePrice); // Use validated salePrice
                    cmd.Parameters.AddWithValue("@PaymentMethod", txtPaymentMethod.Text);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            BindGrid();
            ClearForm();
            ShowModal("Sale added successfully.");
        }



        private void UpdateSale(int saleId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["CarShowroomConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Sales SET CustomerId=@CustomerId, CarId=@CarId, EmployeeId=@EmployeeId, SaleDate=@SaleDate, SalePrice=@SalePrice, PaymentMethod=@PaymentMethod " +
                               "WHERE SaleId=@SaleId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SaleId", saleId);
                    cmd.Parameters.AddWithValue("@CustomerId", txtCustomerId.Text);
                    cmd.Parameters.AddWithValue("@CarId", txtCarId.Text);
                    cmd.Parameters.AddWithValue("@EmployeeId", txtEmployeeId.Text);
                    cmd.Parameters.AddWithValue("@SaleDate", txtSaleDate.Text);
                    cmd.Parameters.AddWithValue("@SalePrice", txtSalePrice.Text);
                    cmd.Parameters.AddWithValue("@PaymentMethod", txtPaymentMethod.Text);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            BindGrid();
            ClearForm();
            ShowModal("Sale updated successfully.");
            _currentSaleId = 0; // Reset the current sale ID after updating
        }

        private void DeleteSale(int saleId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["CarShowroomConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Sales WHERE SaleId=@SaleId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SaleId", saleId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            BindGrid();
            ShowModal("Sale deleted successfully.");
        }

        private void PopulateSaleDetails(int saleId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["CarShowroomConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Sales WHERE SaleId=@SaleId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SaleId", saleId);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        txtCustomerId.Text = reader["CustomerId"].ToString();
                        txtCarId.Text = reader["CarId"].ToString();
                        txtEmployeeId.Text = reader["EmployeeId"].ToString();
                        txtSaleDate.Text = Convert.ToDateTime(reader["SaleDate"]).ToString("yyyy-MM-dd");
                        txtSalePrice.Text = reader["SalePrice"].ToString();
                        txtPaymentMethod.Text = reader["PaymentMethod"].ToString();
                    }
                }
            }
        }

        private void BindGrid()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["CarShowroomConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Sales", conn))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        gvSales.DataSource = dt;
                        gvSales.DataBind();
                    }
                }
            }
        }

        private void ClearForm()
        {
            txtCustomerId.Text = "";
            txtCarId.Text = "";
            txtEmployeeId.Text = "";
            txtSaleDate.Text = "";
            txtSalePrice.Text = "";
            txtPaymentMethod.Text = "";
        }

        protected void ShowModal(string message)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"showModal('{message}');", true);
        }
    }
}
