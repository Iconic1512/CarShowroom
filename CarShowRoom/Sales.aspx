<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sales.aspx.cs" Inherits="CarShowRoom.Sales" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sales Management</title>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        .container {
            margin-top: 50px;
        }
        h1 {
            margin-bottom: 30px;
            text-align: center;
        }
        .form-group {
            margin-bottom: 20px;
        }
        .btn-group {
            margin-top: 20px;
            text-align: center;
        }
        .grid {
            margin-top: 30px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>Sales Management</h1>
            <div class="form-group">
                <label for="txtCustomerId">Customer Id:</label>
                <asp:TextBox ID="txtCustomerId" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtCarId">Car Id:</label>
                <asp:TextBox ID="txtCarId" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtSaleDate">Sale Date:</label>
                <asp:TextBox ID="txtSaleDate" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtSaleAmount">Sale Amount:</label>
                <asp:TextBox ID="txtSaleAmount" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="btn-group">
                <asp:Button ID="btnAddSale" runat="server" Text="Add Sale" CssClass="btn btn-primary" OnClick="btnAddSale_Click" />
                <asp:Button ID="btnUpdateSale" runat="server" Text="Update Sale" CssClass="btn btn-secondary" OnClick="btnUpdateSale_Click" />
                <asp:Button ID="btnDeleteSale" runat="server" Text="Delete Sale" CssClass="btn btn-danger" OnClick="btnDeleteSale_Click" />
            </div>
            <asp:GridView ID="gvSales" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered grid" OnSelectedIndexChanged="gvSales_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField DataField="SaleId" HeaderText="SaleId" />
                    <asp:BoundField DataField="CustomerId" HeaderText="CustomerId" />
                    <asp:BoundField DataField="CarId" HeaderText="CarId" />
                    <asp:BoundField DataField="SaleDate" HeaderText="Sale Date" />
                    <asp:BoundField DataField="SaleAmount" HeaderText="Sale Amount" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.2/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</body>
</html>
