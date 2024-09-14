<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Customer.aspx.cs" Inherits="CarShowroom.Customer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Customer Management</title>
    <style>
        body { font-family: Arial, sans-serif; background-color: #f0f0f0; }
        .container { max-width: 800px; margin: 50px auto; padding: 20px; background-color: #fff; border-radius: 8px; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1); }
        header { text-align: center; margin-bottom: 20px; }
        .form-group { margin-bottom: 15px; }
        label { display: block; margin-bottom: 5px; }
        input[type="text"], input[type="password"], input[type="date"] { width: 100%; padding: 10px; border: 1px solid #ccc; border-radius: 4px; }
        input[type="submit"] { width: 100%; padding: 10px; border: none; border-radius: 4px; background-color: #28a745; color: #fff; font-size: 16px; }
        .error { color: red; text-align: center; margin-top: 10px; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <header>
                <h1>Customer Management</h1>
            </header>
            <div class="form-group">
                <label for="txtCustomerId">Customer ID</label>
                <asp:TextBox ID="txtCustomerId" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtName">Name</label>
                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtEmail">Email</label>
                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtPhoneNo">Phone No</label>
                <asp:TextBox ID="txtPhoneNo" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtAddress">Address</label>
                <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtDOB">DOB</label>
                <asp:TextBox ID="txtDOB" runat="server" TextMode="Date"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtAadharCard">Aadhar Card</label>
                <asp:TextBox ID="txtAadharCard" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtGender">Gender</label>
                <asp:TextBox ID="txtGender" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtPassword">Password</label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
            </div>
            <asp:Label ID="lblMessage" runat="server" CssClass="error"></asp:Label>
            <div class="form-group">
                <asp:Button ID="btnAddCustomer" runat="server" Text="Add Customer" OnClick="btnAddCustomer_Click" />
                <asp:Button ID="btnUpdateCustomer" runat="server" Text="Update Customer" OnClick="btnUpdateCustomer_Click" />
                <asp:Button ID="btnDeleteCustomer" runat="server" Text="Delete Customer" OnClick="btnDeleteCustomer_Click" />
            </div>
            <asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="CustomerId" HeaderText="Customer ID" />
                    <asp:BoundField DataField="Name" HeaderText="Name" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                    <asp:BoundField DataField="PhoneNo" HeaderText="Phone No" />
                    <asp:BoundField DataField="Address" HeaderText="Address" />
                    <asp:BoundField DataField="DOB" HeaderText="DOB" />
                    <asp:BoundField DataField="AadharCard" HeaderText="Aadhar Card" />
                    <asp:BoundField DataField="Gender" HeaderText="Gender" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
