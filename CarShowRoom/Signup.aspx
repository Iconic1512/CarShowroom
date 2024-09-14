<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Signup.aspx.cs" Inherits="CarShowRoom.Signup" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Signup</title>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body { font-family: Arial, sans-serif; background-color: #f0f0f0; }
        .container { max-width: 500px; margin: 50px auto; padding: 20px; background-color: #fff; border-radius: 8px; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1); }
        header { text-align: center; margin-bottom: 20px; }
        .form-group { margin-bottom: 15px; }
        label { display: block; margin-bottom: 5px; }
        .form-control { width: 100%; padding: 10px; border: 1px solid #ccc; border-radius: 4px; }
        .btn-primary { width: 100%; padding: 10px; border-radius: 4px; }
        .modal-content { padding: 20px; }
        .modal-header { background-color: #007bff; color: #fff; }
        .modal-footer { display: flex; justify-content: center; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <header>
                <h1>Signup</h1>
            </header>
            <div class="form-group">
                <label for="txtName">Name</label>
                <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtEmail">Email</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtPassword">Password</label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtPhoneNo">Phone No</label>
                <asp:TextBox ID="txtPhoneNo" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtAddress">Address</label>
                <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtDOB">Date of Birth</label>
                <asp:TextBox ID="txtDOB" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtAadharCard">Aadhar Card</label>
                <asp:TextBox ID="txtAadharCard" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="ddlGender">Gender</label>
                <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control">
                    <asp:ListItem Text="Select Gender" Value="" />
                    <asp:ListItem Text="Male" Value="M"></asp:ListItem>
                    <asp:ListItem Text="Female" Value="F"></asp:ListItem>
                    <asp:ListItem Text="Other" Value="O"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <asp:Button ID="btnSignup" runat="server" Text="Signup" OnClick="btnSignup_Click" CssClass="btn btn-primary" />
            </div>
        </div>

        <!-- Modal for displaying messages -->
        <div class="modal fade" id="messageModal" tabindex="-1" role="dialog" aria-labelledby="messageModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="messageModalLabel">Notification</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <asp:Label ID="lblModalMessage" runat="server" CssClass="text-danger"></asp:Label>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.5.2/dist/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    <script type="text/javascript">
        function showModal(message) {
            var lblModalMessage = document.getElementById('<%= lblModalMessage.ClientID %>');
            lblModalMessage.innerHTML = message; // Use innerHTML to allow HTML tags in the message
            $('#messageModal').modal('show');
        }
    </script>
</body>
</html>
