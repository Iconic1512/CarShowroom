<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Car.aspx.cs" Inherits="CarShowRoom.Car" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Car Management</title>
    <style>
        .gridview {
            width: 100%;
            border-collapse: collapse;
        }
        .gridview th, .gridview td {
            border: 1px solid #ddd;
            padding: 8px;
        }
        .gridview th {
            background-color: #f2f2f2;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="txtModel" runat="server" Placeholder="Model"></asp:TextBox>
            <asp:TextBox ID="txtYear" runat="server" Placeholder="Year"></asp:TextBox>
            <asp:TextBox ID="txtColour" runat="server" Placeholder="Colour"></asp:TextBox>
            <asp:TextBox ID="txtPrice" runat="server" Placeholder="Price"></asp:TextBox>
            <asp:Button ID="btnAddCar" runat="server" Text="Add Car" OnClick="btnAddCar_Click" />
            <asp:Button ID="btnUpdateCar" runat="server" Text="Update Car" OnClick="btnUpdateCar_Click" />
            <asp:Button ID="btnDeleteCar" runat="server" Text="Delete Car" OnClick="btnDeleteCar_Click" />
            <asp:GridView ID="gvCars" runat="server" AutoGenerateColumns="False" CssClass="gridview" OnRowCommand="gvCars_RowCommand" DataKeyNames="CarId">
                <Columns>
                    <asp:BoundField DataField="CarId" HeaderText="Car ID" ReadOnly="True" />
                    <asp:BoundField DataField="Model" HeaderText="Model" />
                    <asp:BoundField DataField="Year" HeaderText="Year" />
                    <asp:BoundField DataField="Colour" HeaderText="Colour" />
                    <asp:BoundField DataField="Price" HeaderText="Price" />
                    <asp:ButtonField ButtonType="Link" CommandName="Select" Text="Select" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
