<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Car.aspx.cs" Inherits="CarShowRoom.Car" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Car Management</title>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />
    <style>
        /* Home.aspx Styles */
        body {
            font-family: Arial, sans-serif;
            background-color: #f0f0f0;
            margin: 0;
            padding: 0;
        }
        .container {
            max-width: 1200px;
            margin: 50px auto;
            padding: 20px;
            background-color: #fff;
            border-radius: 8px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            text-align: center;
        }
        header {
            text-align: center;
            margin-bottom: 20px;
        }
        header h1 {
            color: #333;
            font-size: 2.5em;
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
        .separator {
            height: 2px; /* Height of the horizontal line */
            background-color: #333; /* Color of the line */
            margin: 20px 0; /* Space above and below the line */
        }

        /* Car.aspx Styles */
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
        .card {
            margin-top: 20px;
        }
        .btn-group-sm {
            display: flex;
            gap: 10px; /* Adjust space between buttons */
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" class="container form-container">
        <!-- Navigation Bar -->
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
        <div class="separator"></div> <!-- Horizontal line -->

        <!-- Car Management Form -->
        <div class="card">
            <div class="card-header">
                <h2 class="text-center">Car Management</h2>
            </div>
            <div class="card-body">
                <asp:Label ID="lblMessage" runat="server" ForeColor="Red" CssClass="alert alert-danger d-none" />
                <div id="alertBox" class="alert-box"></div>
                <div class="row">
                    <div class="col-md-6 form-group">
                        <label for="ddlManufacturer">Manufacturer:</label>
                        <asp:DropDownList ID="ddlManufacturer" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                    <div class="col-md-6 form-group">
                        <label for="txtModel">Model:</label>
                        <asp:TextBox ID="txtModel" runat="server" Placeholder="Model" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-6 form-group">
                        <label for="txtYear">Year:</label>
                        <asp:TextBox ID="txtYear" runat="server" Placeholder="Year" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-6 form-group">
                        <label for="txtName">Name:</label>
                        <asp:TextBox ID="txtName" runat="server" Placeholder="Name" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-6 form-group">
                        <label for="txtColour">Colour:</label>
                        <asp:TextBox ID="txtColour" runat="server" Placeholder="Colour" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-6 form-group">
                        <label for="txtPrice">Price:</label>
                        <asp:TextBox ID="txtPrice" runat="server" Placeholder="Price" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-6 form-group">
                        <label for="txtMileage">Mileage:</label>
                        <asp:TextBox ID="txtMileage" runat="server" Placeholder="Mileage" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-6 form-group">
                        <label for="txtEngineType">Engine Type:</label>
                        <asp:TextBox ID="txtEngineType" runat="server" Placeholder="Engine Type" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-6 form-group">
                        <label for="txtTransmission">Transmission:</label>
                        <asp:TextBox ID="txtTransmission" runat="server" Placeholder="Transmission" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-12 form-group">
                        <label for="txtDescription">Description:</label>
                        <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="3" Placeholder="Description" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="button-group text-center">
                    <asp:Button ID="btnAddCar" runat="server" Text="Add Car" OnClick="btnAddCar_Click" CssClass="btn btn-primary mx-2" />
                    <asp:Button ID="btnViewCars" runat="server" Text="View Cars" CssClass="btn btn-info mx-2" OnClick="btnViewCars_Click" />
                </div>
                <asp:GridView ID="gvCars" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped gridview mt-4" OnRowCommand="gvCars_RowCommand" OnRowDeleting="gvCars_RowDeleting" DataKeyNames="CarId" Visible="false">
                    <Columns>
                        <asp:BoundField DataField="CarId" HeaderText="Car ID" ReadOnly="True" />
                        <asp:BoundField DataField="ManufacturerName" HeaderText="Manufacturer" />
                        <asp:BoundField DataField="Model" HeaderText="Model" />
                        <asp:BoundField DataField="Year" HeaderText="Year" />
                        <asp:BoundField DataField="Name" HeaderText="Name" />
                        <asp:BoundField DataField="Colour" HeaderText="Colour" />
                        <asp:BoundField DataField="Price" HeaderText="Price" />
                        <asp:BoundField DataField="Mileage" HeaderText="Mileage" />
                        <asp:BoundField DataField="EngineType" HeaderText="Engine Type" />
                        <asp:BoundField DataField="Transmission" HeaderText="Transmission" />
                        <asp:BoundField DataField="Description" HeaderText="Description" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnEdit" runat="server" CommandName="EditCar" CommandArgument='<%# Eval("CarId") %>' Text="Edit" CssClass="btn btn-sm btn-warning mx-1" />
                                <asp:Button ID="btnDelete" runat="server" CommandName="DeleteCar" CommandArgument='<%# Eval("CarId") %>' Text="Delete" CssClass="btn btn-sm btn-danger mx-1" OnClientClick="return confirm('Are you sure you want to delete this car?');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
    <script>
        function toggleDrawer() {
            var drawer = document.getElementById('drawer');
            if (drawer.classList.contains('open')) {
                drawer.classList.remove('open');
            } else {
                drawer.classList.add('open');
            }
        }
    </script>
</body>
</html>
