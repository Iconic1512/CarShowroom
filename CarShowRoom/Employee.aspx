<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Employee.aspx.cs" Inherits="Employee" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employee Form</title>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        .form-container {
            max-width: 600px;
            margin: 50px auto;
        }
        .form-group {
            margin-bottom: 15px;
        }
        .btn-container {
            display: flex;
            justify-content: space-between;
        }
        .alert-container {
            margin-top: 20px;
        }
        .employee-list-container {
            margin-top: 30px;
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
    <form id="form1" runat="server">
        <div class="container form-container">
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
            <h2>Employee Form</h2>
            <div class="form-group">
                <label for="txtName">Name</label>
                <asp:TextBox ID="txtName" runat="server" CssClass="form-control" />
            </div>
            <div class="form-group">
                <label for="txtEmail">Email</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
            </div>
            <div class="form-group">
                <label for="txtPhoneNo">Phone No</label>
                <asp:TextBox ID="txtPhoneNo" runat="server" CssClass="form-control" />
            </div>
            <div class="form-group">
                <label for="txtAddress">Address</label>
                <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" />
            </div>
            <div class="form-group">
                <label for="txtHireDate">Hire Date</label>
                <asp:TextBox ID="txtHireDate" runat="server" CssClass="form-control" TextMode="Date" />
            </div>
            <div class="form-group">
                <label for="txtSalary">Salary</label>
                <asp:TextBox ID="txtSalary" runat="server" CssClass="form-control" />
            </div>
            <div class="form-group">
                <label for="txtDesignation">Designation</label>
                <asp:TextBox ID="txtDesignation" runat="server" CssClass="form-control" />
            </div>
            <div class="btn-container">
                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="btnSubmit_Click" />
                <asp:Button ID="btnViewEmployees" runat="server" CssClass="btn btn-secondary" Text="View Employees" OnClick="btnViewEmployees_Click" />
            </div>
            <div class="alert-container">
                <asp:Label ID="lblMessage" runat="server" CssClass="alert alert-success" Visible="false" />
            </div>
            <div class="employee-list-container">
                <asp:GridView ID="gvEmployees" runat="server" CssClass="table table-striped table-bordered" AutoGenerateColumns="False" Visible="false">
                    <Columns>
                        <asp:BoundField DataField="EmployeeId" HeaderText="Employee ID" />
                        <asp:BoundField DataField="Name" HeaderText="Name" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:BoundField DataField="PhoneNo" HeaderText="Phone No" />
                        <asp:BoundField DataField="Address" HeaderText="Address" />
                        <asp:BoundField DataField="HireDate" HeaderText="Hire Date" DataFormatString="{0:yyyy-MM-dd}" />
                        <asp:BoundField DataField="Salary" HeaderText="Salary" DataFormatString="{0:N2}" />
                        <asp:BoundField DataField="Designation" HeaderText="Designation" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>

    <script>
        function toggleDrawer() {
            const drawer = document.getElementById('drawer');
            drawer.classList.toggle('open');
        }
    </script>
</body>
</html>
