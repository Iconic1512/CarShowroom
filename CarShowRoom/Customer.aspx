<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Customer.aspx.cs" Inherits="CarShowRoom.Customer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Customer Management</title>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        .container { max-width: 900px; margin-top: 30px; }
        .form-group label { font-weight: bold; }
        .error { color: red; text-align: center; margin-top: 10px; }
        .btn-group { margin-bottom: 20px; }
        .btn { margin-right: 10px; }
        .panel { margin-top: 20px; }
        .panel h3 { margin-bottom: 20px; }
        .form-actions { margin-top: 20px; }
        .form-actions .btn { margin-right: 10px; }
        .form-control { border-radius: 0.25rem; }
        .btn-primary { background-color: #007bff; border-color: #007bff; }
        .btn-secondary { background-color: #6c757d; border-color: #6c757d; }
        .btn-danger { background-color: #dc3545; border-color: #dc3545; }
        .btn-info { background-color: #17a2b8; border-color: #17a2b8; }
        .form-container { background-color: #f8f9fa; padding: 20px; border-radius: 0.25rem; box-shadow: 0 0 15px rgba(0, 0, 0, 0.1); }
        .form-container h2 { margin-bottom: 20px; }
        .form-actions { text-align: center; }
        .btn-group { margin-top: 20px; }
        .modal-header { background-color: #007bff; color: #fff; }
        .modal-footer { justify-content: center; }
        .modal-body { text-align: center; font-size: 1.2em; }
        .btn-container { display: flex; gap: 10px; justify-content: center; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <header class="text-center mb-4">
                <h1>Customer Management</h1>
            </header>

            <!-- Panel for Customer Form -->
            <asp:Panel ID="pnlCustomerForm" runat="server" Visible="True" CssClass="form-container">
                <h2 class="text-center">Customer Details</h2>
                <div class="form-group">
                    <label for="txtCustomerId">Customer ID</label>
                    <asp:TextBox ID="txtCustomerId" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="txtName">Name</label>
                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="txtEmail">Email</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
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
                    <label for="txtDOB">DOB</label>
                    <asp:TextBox ID="txtDOB" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="txtAadharCard">Aadhar Card</label>
                    <asp:TextBox ID="txtAadharCard" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="txtGender">Gender</label>
                    <asp:TextBox ID="txtGender" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="txtPassword">Password</label>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                </div>
                <asp:Label ID="lblMessage" runat="server" CssClass="error"></asp:Label>

                <div class="form-actions">
                    <asp:Button ID="btnAddCustomer" runat="server" Text="Add Customer" OnClick="btnAddCustomer_Click" CssClass="btn btn-primary" />
                    <asp:Button ID="btnViewCustomers" runat="server" Text="View Customers" OnClick="btnViewCustomers_Click" CssClass="btn btn-info" />
                </div>
            </asp:Panel>

            <!-- Panel for Customer List -->
            <asp:Panel ID="pnlCustomers" runat="server" Visible="False" CssClass="panel">
                <h3 class="text-center">Customer List</h3>
                <asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered" OnRowCommand="gvCustomers_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="CustomerId" HeaderText="Customer ID" />
                        <asp:BoundField DataField="Name" HeaderText="Name" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:BoundField DataField="PhoneNo" HeaderText="Phone No" />
                        <asp:BoundField DataField="Address" HeaderText="Address" />
                        <asp:BoundField DataField="DOB" HeaderText="DOB" />
                        <asp:BoundField DataField="AadharCard" HeaderText="Aadhar Card" />
                        <asp:BoundField DataField="Gender" HeaderText="Gender" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <div class="btn-container">
                                    <asp:Button ID="btnUpdate" runat="server" CommandName="UpdateCustomer" CommandArgument='<%# Eval("CustomerId") %>' Text="Update" CssClass="btn btn-secondary btn-sm" />
                                    <asp:Button ID="btnDelete" runat="server" CommandName="DeleteCustomer" CommandArgument='<%# Eval("CustomerId") %>' Text="Delete" CssClass="btn btn-danger btn-sm" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <div class="text-center mt-3">
                    <asp:Button ID="btnBackToManagement" runat="server" Text="Back to Management" OnClick="btnBackToManagement_Click" CssClass="btn btn-secondary" />
                </div>
            </asp:Panel>

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
        </div>
    </form>

    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.5.2/dist/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    <script type="text/javascript">
        function showModal() {
            $('#messageModal').modal('show');
        }
    </script>
</body>
</html>