<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="CarShowRoom.Home" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home</title>
    <style>
        body { font-family: Arial, sans-serif; background-color: #f0f0f0; }
        .container { max-width: 800px; margin: 50px auto; padding: 20px; background-color: #fff; border-radius: 8px; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1); }
        header { text-align: center; margin-bottom: 20px; }
        .menu { text-align: center; margin-bottom: 20px; }
        .menu a { margin: 0 10px; color: #007bff; text-decoration: none; }
        .menu a:hover { text-decoration: underline; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <header>
                <h1>Welcome to Car Showroom</h1>
            </header>
            <div class="menu">
                <a href="Car.aspx">Manage Cars</a>
                <a href="Customer.aspx">Manage Customers</a>
                <a href="Sales.aspx">Manage Sales</a>
                <a href="Employee.aspx">Manage Employees</a>
                <a href="Inventory.aspx">Manage Inventory</a>
                <a href="Manufacturer.aspx">Manage Manufacturers</a>
                <a href="Service_Record.aspx">Manage Service Records</a>
            </div>
        </div>
    </form>
</body>
</html>
