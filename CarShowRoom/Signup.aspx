<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Signup.aspx.cs" Inherits="CarShowRoom.Signup" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Signup</title>
    <style>
        body { font-family: Arial, sans-serif; background-color: #f0f0f0; }
        .container { max-width: 400px; margin: 50px auto; padding: 20px; background-color: #fff; border-radius: 8px; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1); }
        header { text-align: center; margin-bottom: 20px; }
        .form-group { margin-bottom: 15px; }
        label { display: block; margin-bottom: 5px; }
        input[type="text"], input[type="password"], input[type="date"] { width: 100%; padding: 10px; border: 1px solid #ccc; border-radius: 4px; }
        input[type="submit"] { width: 100%; padding: 10px; border: none; border-radius: 4px; background-color: #007bff; color: #fff; font-size: 16px; }
        .error { color: red; text-align: center; margin-to<a href="Signup.aspx">Signup.aspx</a>p: 10px; }
    </style>
</head>
<body>
    <a href="Signup.aspx">Signup.aspx</a>
    <form id="form1" runat="server">
        <div class="container">
            <header>
                <h1>Signup</h1>
            </header>
            <div class="form-group">
                <label for="txtName">Name</label>
                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtEmail">Email</label>
                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtPassword">Password</label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
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
                <label for="txtDOB">Date of Birth</label>
                <asp:TextBox ID="txtDOB" runat="server" TextMode="Date"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtAadharCard">Aadhar Card</label>
                <asp:TextBox ID="txtAadharCard" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="ddlGender">Gender</label>
                <asp:DropDownList ID="ddlGender" runat="server">
                    <asp:ListItem Text="Male" Value="M"></asp:ListItem>
                    <asp:ListItem Text="Female" Value="F"></asp:ListItem>
                    <asp:ListItem Text="Other" Value="O"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <asp:Label ID="lblMessage" runat="server" CssClass="error"></asp:Label>
            <div class="form-group">
                <asp:Button ID="btnSignup" runat="server" Text="Signup" OnClick="btnSignup_Click" />
            </div>
        </div>
    </form>
</body>
</html>
