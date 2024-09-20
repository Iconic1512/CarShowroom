using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace CarShowRoom
{
    public partial class inventory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindInventoryGrid();
            }
        }

        private void BindInventoryGrid()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["CarShowRoomConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Inventory";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                gvInventory.DataSource = cmd.ExecuteReader();
                gvInventory.DataBind();
            }
        }

        protected void gvInventory_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            lblMessage.CssClass = "alert alert-danger d-none"; // Show the message (removes 'd-none')
            ScriptManager.RegisterStartupScript(this, this.GetType(), "HideLabel", "hideMessage();", true);
            if (e.CommandName == "UpdateRow")
            {
                int inventoryId = Convert.ToInt32(e.CommandArgument);
                hfInventoryId.Value = inventoryId.ToString();
                ShowUpdateForm(inventoryId);
            }
        }

        private void ShowUpdateForm(int inventoryId)
        {
            lblMessage.CssClass = "alert alert-danger d-none"; // Show the message (removes 'd-none')
            ScriptManager.RegisterStartupScript(this, this.GetType(), "HideLabel", "hideMessage();", true);

            string connectionString = ConfigurationManager.ConnectionStrings["CarShowRoomConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Quantity, Status FROM Inventory WHERE InventoryId = @InventoryId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@InventoryId", inventoryId);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtQuantity.Text = reader["Quantity"].ToString();
                    txtStatus.Text = reader["Status"].ToString();
                }
                reader.Close();
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "showUpdateForm", "showUpdateForm();", true);
        }

        protected void btnSubmitUpdate_Click(object sender, EventArgs e)
        {
            int inventoryId = Convert.ToInt32(hfInventoryId.Value);
            int quantity = int.Parse(txtQuantity.Text);
            string status = txtStatus.Text;

            string connectionString = ConfigurationManager.ConnectionStrings["CarShowRoomConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Inventory SET Quantity = @Quantity, Status = @Status, LastDate = @LastDate WHERE InventoryId = @InventoryId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Quantity", quantity);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@LastDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@InventoryId", inventoryId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }

            lblMessage.Text = "Inventory updated successfully.";
            lblMessage.CssClass = "alert alert-success";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "HideLabel", "hideMessage();", true);
            BindInventoryGrid();
        }
    }
}
