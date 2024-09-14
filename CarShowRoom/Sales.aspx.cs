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
                string query = "INSERT INTO Sales (CustomerId, CarId, SaleDate, SaleAmount) VALUES (@CustomerId, @CarId, @SaleDate, @SaleAmount)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CustomerId", txtCustomerId.Text);
                    cmd.Parameters.AddWithValue("@CarId", txtCarId.Text);
                    cmd.Parameters.AddWithValue("@SaleDate", txtSaleDate.Text);
                    cmd.Parameters.AddWithValue("@SaleAmount", txtSaleAmount.Text);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            BindGrid();
        }

        protected void btnUpdateSale_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["CarShowroomConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Sales SET CustomerId=@CustomerId, CarId=@CarId, SaleDate=@SaleDate, SaleAmount=@SaleAmount WHERE SaleId=@SaleId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SaleId", ViewState["SaleId"]);
                    cmd.Parameters.AddWithValue("@CustomerId", txtCustomerId.Text);
                    cmd.Parameters.AddWithValue("@CarId", txtCarId.Text);
                    cmd.Parameters.AddWithValue("@SaleDate", txtSaleDate.Text);
                    cmd.Parameters.AddWithValue("@SaleAmount", txtSaleAmount.Text);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            BindGrid();
        }

        protected void btnDeleteSale_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["CarShowroomConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Sales WHERE SaleId=@SaleId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SaleId", ViewState["SaleId"]);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            BindGrid();
        }

        protected void gvSales_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvSales.SelectedRow;
            ViewState["SaleId"] = row.Cells[1].Text;
            txtCustomerId.Text = row.Cells[2].Text;
            txtCarId.Text = row.Cells[3].Text;
            txtSaleDate.Text = row.Cells[4].Text;
            txtSaleAmount.Text = row.Cells[5].Text;
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
    }
}
