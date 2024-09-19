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
                <label for="txtEmployeeId">Employee Id:</label>
                <asp:TextBox ID="txtEmployeeId" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtSaleDate">Sale Date:</label>
                <asp:TextBox ID="txtSaleDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtSalePrice">Sale Price:</label>
                <asp:TextBox ID="txtSalePrice" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtPaymentMethod">Payment Method:</label>
                <asp:TextBox ID="txtPaymentMethod" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtStatus">Status:</label>
                <asp:TextBox ID="txtStatus" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="btn-group">
                <asp:Button ID="btnAddSale" runat="server" Text="Add Sale" CssClass="btn btn-primary" OnClick="btnAddSale_Click" />
                <asp:Button ID="btnViewSales" runat="server" Text="View Sales" CssClass="btn btn-info" OnClick="btnViewSales_Click" />
            </div>
            <asp:GridView ID="gvSales" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered grid"
                OnRowEditing="gvSales_RowEditing"
                OnRowCancelingEdit="gvSales_RowCancelingEdit"
                OnRowUpdating="gvSales_RowUpdating"
                OnRowDeleting="gvSales_RowDeleting">
                <Columns>
                    <asp:TemplateField HeaderText="SaleId">
                        <ItemTemplate>
                            <asp:Label ID="lblSaleId" runat="server" Text='<%# Eval("SaleId") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CustomerId">
                        <ItemTemplate>
                            <asp:Label ID="lblCustomerId" runat="server" Text='<%# Eval("CustomerId") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCustomerIdGrid" runat="server" Text='<%# Bind("CustomerId") %>' CssClass="form-control" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CarId">
                        <ItemTemplate>
                            <asp:Label ID="lblCarId" runat="server" Text='<%# Eval("CarId") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCarIdGrid" runat="server" Text='<%# Bind("CarId") %>' CssClass="form-control" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="EmployeeId">
                        <ItemTemplate>
                            <asp:Label ID="lblEmployeeId" runat="server" Text='<%# Eval("EmployeeId") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEmployeeIdGrid" runat="server" Text='<%# Bind("EmployeeId") %>' CssClass="form-control" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SaleDate">
                        <ItemTemplate>
                            <asp:Label ID="lblSaleDate" runat="server" Text='<%# Eval("SaleDate") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtSaleDateGrid" runat="server" Text='<%# Bind("SaleDate") %>' CssClass="form-control" TextMode="Date" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SalePrice">
                        <ItemTemplate>
                            <asp:Label ID="lblSalePrice" runat="server" Text='<%# Eval("SalePrice") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtSalePriceGrid" runat="server" Text='<%# Bind("SalePrice") %>' CssClass="form-control" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PaymentMethod">
                        <ItemTemplate>
                            <asp:Label ID="lblPaymentMethod" runat="server" Text='<%# Eval("PaymentMethod") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtPaymentMethodGrid" runat="server" Text='<%# Bind("PaymentMethod") %>' CssClass="form-control" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtStatusGrid" runat="server" Text='<%# Bind("Status") %>' CssClass="form-control" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="Edit" CssClass="btn btn-info btn-sm" />
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="Delete" CssClass="btn btn-danger btn-sm" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Button ID="btnUpdate" runat="server" Text="Update" CommandName="Update" CssClass="btn btn-secondary btn-sm" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CommandName="Cancel" CssClass="btn btn-secondary btn-sm" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.2/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</body>
</html>
