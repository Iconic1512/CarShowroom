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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        protected void btnAddSale_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["CarShowroomConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Sales (CustomerId, CarId, EmployeeId, SaleDate, SalePrice, PaymentMethod, Status) VALUES (@CustomerId, @CarId, @EmployeeId, @SaleDate, @SalePrice, @PaymentMethod, @Status)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CustomerId", txtCustomerId.Text);
                    cmd.Parameters.AddWithValue("@CarId", txtCarId.Text);
                    cmd.Parameters.AddWithValue("@EmployeeId", txtEmployeeId.Text);
                    cmd.Parameters.AddWithValue("@SaleDate", txtSaleDate.Text);
                    cmd.Parameters.AddWithValue("@SalePrice", txtSalePrice.Text);
                    cmd.Parameters.AddWithValue("@PaymentMethod", txtPaymentMethod.Text);
                    cmd.Parameters.AddWithValue("@Status", txtStatus.Text);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                BindGrid();
            }
        }

        protected void btnViewSales_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void BindGrid()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["CarShowroomConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Sales";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gvSales.DataSource = dt;
                    gvSales.DataBind();
                }
            }
        }

        protected void gvSales_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvSales.EditIndex = e.NewEditIndex;
            BindGrid();
        }

        protected void gvSales_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvSales.EditIndex = -1;
            BindGrid();
        }

        protected void gvSales_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvSales.Rows[e.RowIndex];
            Label lblSaleId = (Label)row.FindControl("lblSaleId");
            TextBox txtCustomerIdGrid = (TextBox)row.FindControl("txtCustomerIdGrid");
            TextBox txtCarIdGrid = (TextBox)row.FindControl("txtCarIdGrid");
            TextBox txtEmployeeIdGrid = (TextBox)row.FindControl("txtEmployeeIdGrid");
            TextBox txtSaleDateGrid = (TextBox)row.FindControl("txtSaleDateGrid");
            TextBox txtSalePriceGrid = (TextBox)row.FindControl("txtSalePriceGrid");
            TextBox txtPaymentMethodGrid = (TextBox)row.FindControl("txtPaymentMethodGrid");
            TextBox txtStatusGrid = (TextBox)row.FindControl("txtStatusGrid");

            string connectionString = ConfigurationManager.ConnectionStrings["CarShowroomConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Sales SET CustomerId = @CustomerId, CarId = @CarId, EmployeeId = @EmployeeId, SaleDate = @SaleDate, SalePrice = @SalePrice, PaymentMethod = @PaymentMethod, Status = @Status WHERE SaleId = @SaleId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SaleId", lblSaleId.Text);
                    cmd.Parameters.AddWithValue("@CustomerId", txtCustomerIdGrid.Text);
                    cmd.Parameters.AddWithValue("@CarId", txtCarIdGrid.Text);
                    cmd.Parameters.AddWithValue("@EmployeeId", txtEmployeeIdGrid.Text);
                    cmd.Parameters.AddWithValue("@SaleDate", txtSaleDateGrid.Text);
                    cmd.Parameters.AddWithValue("@SalePrice", txtSalePriceGrid.Text);
                    cmd.Parameters.AddWithValue("@PaymentMethod", txtPaymentMethodGrid.Text);
                    cmd.Parameters.AddWithValue("@Status", txtStatusGrid.Text);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                gvSales.EditIndex = -1;
                BindGrid();
            }
        }

        protected void gvSales_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = gvSales.Rows[e.RowIndex];
            Label lblSaleId = (Label)row.FindControl("lblSaleId");

            string connectionString = ConfigurationManager.ConnectionStrings["CarShowroomConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Sales WHERE SaleId = @SaleId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SaleId", lblSaleId.Text);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                BindGrid();
            }
        }
    }
}
