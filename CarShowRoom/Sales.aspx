<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sales.aspx.cs" Inherits="CarShowRoom.Sales" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sales Management</title>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body { font-family: Arial, sans-serif; background-color: #f0f0f0; }
        .container { max-width: 800px; margin: 50px auto; padding: 20px; background-color: #fff; border-radius: 8px; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1); }
        header { text-align: center; margin-bottom: 20px; }
        .form-group { margin-bottom: 15px; }
        .form-control { width: 100%; padding: 10px; border: 1px solid #ccc; border-radius: 4px; }
        .btn-primary { width: 48%; padding: 10px; border-radius: 4px; }
        .modal-content { padding: 20px; }
        .modal-header { background-color: #007bff; color: #fff; }
        .modal-footer { display: flex; justify-content: center; }
        /* Drawer styles */
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

        <div class="container">
            <header>
                <h1>Sales Management</h1>
            </header>
            <div class="form-group">
                <label for="txtCustomerId">Customer ID</label>
                <asp:TextBox ID="txtCustomerId" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtCarId">Car ID</label>
                <asp:TextBox ID="txtCarId" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtEmployeeId">Employee ID</label>
                <asp:TextBox ID="txtEmployeeId" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtSaleDate">Sale Date</label>
                <asp:TextBox ID="txtSaleDate" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtSalePrice">Sale Price</label>
                <asp:TextBox ID="txtSalePrice" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtPaymentMethod">Payment Method</label>
                <asp:TextBox ID="txtPaymentMethod" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Button ID="btnAddSale" runat="server" Text="Add Sale" OnClick="btnAddSale_Click" CssClass="btn btn-primary" />
                <asp:Button ID="btnViewSales" runat="server" Text="View Sales" OnClick="btnViewSales_Click" CssClass="btn btn-primary" />
            </div>
            <asp:GridView ID="gvSales" runat="server" AutoGenerateColumns="False" OnRowCommand="gvSales_RowCommand">
                <Columns>
                    <asp:BoundField DataField="SaleId" HeaderText="Sale ID" />
                    <asp:BoundField DataField="CustomerId" HeaderText="Customer ID" />
                    <asp:BoundField DataField="CarId" HeaderText="Car ID" />
                    <asp:BoundField DataField="EmployeeId" HeaderText="Employee ID" />
                    <asp:BoundField DataField="SaleDate" HeaderText="Sale Date" />
                    <asp:BoundField DataField="SalePrice" HeaderText="Sale Price" />
                    <asp:BoundField DataField="PaymentMethod" HeaderText="Payment Method" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnUpdate" runat="server" Text="Update" CommandArgument='<%# Eval("SaleId") %>' CommandName="UpdateSale" CssClass="btn btn-warning btn-sm" />
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandArgument='<%# Eval("SaleId") %>' CommandName="DeleteSale" CssClass="btn btn-danger btn-sm" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
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
            lblModalMessage.innerHTML = message;
            $('#messageModal').modal('show');
        }

        // Function to toggle the drawer visibility
        function toggleDrawer() {
            const drawer = document.getElementById('drawer');
            drawer.classList.toggle('open');
        }
    </script>
</body>
</html>
