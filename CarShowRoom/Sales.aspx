<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sales.aspx.cs" Inherits="CarShowRoom.Sales" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sales Management</title>
    <style>
        /* Same CSS as previous pages */
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>Sales Management</h1>
            <div>
                <label for="txtCustomerId">Customer Id:</label>
                <asp:TextBox ID="txtCustomerId" runat="server"></asp:TextBox>
            </div>
            <div>
                <label for="txtCarId">Car Id:</label>
                <asp:TextBox ID="txtCarId" runat="server"></asp:TextBox>
            </div>
            <div>
                <label for="txtSaleDate">Sale Date:</label>
                <asp:TextBox ID="txtSaleDate" runat="server"></asp:TextBox>
            </div>
            <div>
                <label for="txtSaleAmount">Sale Amount:</label>
                <asp:TextBox ID="txtSaleAmount" runat="server"></asp:TextBox>
            </div>
            <div>
                <asp:Button ID="btnAddSale" runat="server" Text="Add Sale" OnClick="btnAddSale_Click" />
                <asp:Button ID="btnUpdateSale" runat="server" Text="Update Sale" OnClick="btnUpdateSale_Click" />
                <asp:Button ID="btnDeleteSale" runat="server" Text="Delete Sale" OnClick="btnDeleteSale_Click" />
            </div>
            <asp:GridView ID="gvSales" runat="server" AutoGenerateColumns="False" CssClass="grid" OnSelectedIndexChanged="gvSales_SelectedIndexChanged">
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
</body>
</html>
