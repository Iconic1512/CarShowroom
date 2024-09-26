using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarShowRoom
{
    public partial class Manufacturer : Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["CarShowroomConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Optionally, you could load manufacturers here if needed, but it’s handled by the View Manufacturers button
            }
        }

        protected void btnAddManufacturer_Click(object sender, EventArgs e)
        {
            if (!IsValidForm())
            {
                // Do not process the form if validation fails
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd;
                if (ViewState["ManufacturerId"] != null)
                {
                    // Update existing manufacturer
                    cmd = new SqlCommand("UPDATE MANUFACTURER SET Name=@Name, Country=@Country, Phone=@Phone, Email=@Email WHERE ManufacturerId=@ManufacturerId", con);
                    cmd.Parameters.AddWithValue("@ManufacturerId", ViewState["ManufacturerId"]);
                }
                else
                {
                    // Add new manufacturer
                    cmd = new SqlCommand("INSERT INTO MANUFACTURER (Name, Country, Phone, Email) VALUES (@Name, @Country, @Phone, @Email)", con);
                }

                cmd.Parameters.AddWithValue("@Name", txtManufacturerName.Text);
                cmd.Parameters.AddWithValue("@Country", txtCountry.Text);
                cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            ShowAlert(ViewState["ManufacturerId"] != null ? "Manufacturer successfully updated." : "Manufacturer successfully added.", "success");
            ClearForm();
            BindGrid();
        }

        protected void btnViewManufacturers_Click(object sender, EventArgs e)
        {
            gvManufacturers.Visible = !gvManufacturers.Visible;
            if (gvManufacturers.Visible)
            {
                BindGrid();
            }
        }

        protected void gvManufacturers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow selectedRow = gvManufacturers.Rows[index];
                ViewState["ManufacturerId"] = gvManufacturers.DataKeys[index].Value;

                txtManufacturerName.Text = selectedRow.Cells[1].Text;
                txtCountry.Text = selectedRow.Cells[2].Text;
                txtPhone.Text = selectedRow.Cells[3].Text;
                txtEmail.Text = selectedRow.Cells[4].Text;
            }
        }

        private void BindGrid()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT ManufacturerId, Name, Country, Phone, Email FROM MANUFACTURER", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                gvManufacturers.DataSource = dt;
                gvManufacturers.DataBind();
            }
        }

        private void ClearForm()
        {
            txtManufacturerName.Text = "";
            txtCountry.Text = "";
            txtPhone.Text = "";
            txtEmail.Text = "";
            ViewState["ManufacturerId"] = null;
        }

        private bool IsValidForm()
        {
            if (string.IsNullOrWhiteSpace(txtManufacturerName.Text) ||
                string.IsNullOrWhiteSpace(txtCountry.Text) ||
                string.IsNullOrWhiteSpace(txtPhone.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                ShowAlert("Please fill in all required fields.", "error");
                return false;
            }
            return true;
        }

        private void ShowAlert(string message, string type)
        {
            lblMessage.Text = message;
            lblMessage.CssClass = type == "success" ? "alert alert-success" : "alert alert-danger";
            lblMessage.Visible = true;
        }
    }
}


