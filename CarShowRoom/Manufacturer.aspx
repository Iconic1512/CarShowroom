<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Manufacturer.aspx.cs" Inherits="CarShowRoom.Manufacturer" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manufacturer Management</title>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />
    <style>
        .form-group label {
            font-weight: bold;
        }
        .form-container {
            margin-top: 20px;
        }
        .button-group {
            margin-top: 20px;
        }
        .gridview {
            margin-top: 30px;
        }
        .gridview th, .gridview td {
            text-align: center;
        }
        .alert-box {
            display: none;
            margin-top: 20px;
        }
        .alert-box.success {
            display: block;
            color: #155724;
            background-color: #d4edda;
            border-color: #c3e6cb;
        }
        .alert-box.error {
            display: block;
            color: #721c24;
            background-color: #f8d7da;
            border-color: #f5c6cb;
        }
        .card {
            margin-top: 20px;
        }
        .menu-icon {
            font-size: 30px;
            cursor: pointer;
            position: absolute;
            top: 20px;
            left: 20px;
            z-index: 1000;
        }
        .drawer {
            position: fixed;
            top: 0;
            left: -250px; /* Hide by default */
            width: 250px;
            height: 100%;
            background-color: #d3d3d3; /* Light grey color */
            transition: left 0.3s;
            padding-top: 60px;
            z-index: 999;
        }
        .drawer a {
            padding: 15px;
            text-decoration: none;
            color: black; /* Text color */
            display: block;
            transition: background-color 0.3s;
        }
        .drawer a:hover {
            background-color: #b0b0b0; /* Slightly darker on hover */
        }
        .drawer.open {
            left: 0; /* Show when open */
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" class="container form-container">
        <div class="menu-icon" onclick="toggleDrawer()">&#9776;</div> <!-- Hamburger icon -->
        <div class="drawer" id="drawer">
            <a href="Car.aspx">Manage Cars</a>
            <a href="Customer.aspx">Manage Customers</a>
            <a href="Sales.aspx">Manage Sales</a>
            <a href="Employee.aspx">Manage Employees</a>
            <a href="Inventory.aspx">Manage Inventory</a>
            <a href="Manufacturer.aspx">Manage Manufacturers</a>
            <a href="Service.aspx">Manage Service Records</a>
        </div>

        <div class="card">
            <div class="card-header">
                <h2 class="text-center">Manufacturer Management</h2>
            </div>
            <div class="card-body">
                <asp:Label ID="lblMessage" runat="server" ForeColor="Red" CssClass="alert alert-danger d-none" />
                <div id="alertBox" class="alert-box"></div>
                <div class="row">
                    <div class="col-md-6 form-group">
                        <label for="txtManufacturerName">Manufacturer Name:</label>
                        <asp:TextBox ID="txtManufacturerName" runat="server" Placeholder="Manufacturer Name" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-6 form-group">
                        <label for="txtCountry">Country:</label>
                        <asp:TextBox ID="txtCountry" runat="server" Placeholder="Country" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-6 form-group">
                        <label for="txtPhone">Phone:</label>
                        <asp:TextBox ID="txtPhone" runat="server" Placeholder="Phone" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-6 form-group">
                        <label for="txtEmail">Email:</label>
                        <asp:TextBox ID="txtEmail" runat="server" Placeholder="Email" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="button-group text-center">
                    <asp:Button ID="btnAddManufacturer" runat="server" Text="Add Manufacturer" OnClick="btnAddManufacturer_Click" CssClass="btn btn-primary mx-2" />
                    <asp:Button ID="btnViewManufacturers" runat="server" Text="View Manufacturers" CssClass="btn btn-info mx-2" OnClick="btnViewManufacturers_Click" />
                </div>
                <asp:GridView ID="gvManufacturers" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped gridview mt-4"
                    OnRowCommand="gvManufacturers_RowCommand" OnRowDeleting="gvManufacturers_RowDeleting" DataKeyNames="ManufacturerId" Visible="false">
                    <Columns>
                        <asp:BoundField DataField="ManufacturerId" HeaderText="Manufacturer ID" ReadOnly="True" />
                        <asp:BoundField DataField="Name" HeaderText="Name" />
                        <asp:BoundField DataField="Country" HeaderText="Country" />
                        <asp:BoundField DataField="Phone" HeaderText="Phone" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="Select" CommandArgument='<%# Container.DataItemIndex %>' CssClass="btn btn-secondary btn-sm mx-1" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>

        <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
        <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.5.2/dist/umd/popper.min.js"></script>
        <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
        <script>
            function toggleDrawer() {
                const drawer = document.getElementById('drawer');
                drawer.classList.toggle('open');
            }
        </script>
    </form>
</body>
</html>
