<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Car.aspx.cs" Inherits="CarShowRoom.Car" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Car Management</title>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />
    <style>
        .form-group label {
            font-weight: bold;
        }
        .form-container {
            margin-top: 20px;
        }
        .button-group {
            margin-top: 20px;
        }
        .gridview {
            margin-top: 30px;
        }
        .gridview th, .gridview td {
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" class="container form-container">
        <div>
            <h1 class="text-center">Car Management</h1>
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red" CssClass="alert alert-danger d-none" />
            <div class="row">
                <div class="col-md-6 form-group">
                    <label for="ddlManufacturer">Manufacturer:</label>
                    <asp:DropDownList ID="ddlManufacturer" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="col-md-6 form-group">
                    <label for="txtModel">Model:</label>
                    <asp:TextBox ID="txtModel" runat="server" Placeholder="Model" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-6 form-group">
                    <label for="txtYear">Year:</label>
                    <asp:TextBox ID="txtYear" runat="server" Placeholder="Year" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-6 form-group">
                    <label for="txtColour">Colour:</label>
                    <asp:TextBox ID="txtColour" runat="server" Placeholder="Colour" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-6 form-group">
                    <label for="txtPrice">Price:</label>
                    <asp:TextBox ID="txtPrice" runat="server" Placeholder="Price" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-6 form-group">
                    <label for="txtMileage">Mileage:</label>
                    <asp:TextBox ID="txtMileage" runat="server" Placeholder="Mileage" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-6 form-group">
                    <label for="txtEngineType">Engine Type:</label>
                    <asp:TextBox ID="txtEngineType" runat="server" Placeholder="Engine Type" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-6 form-group">
                    <label for="txtTransmission">Transmission:</label>
                    <asp:TextBox ID="txtTransmission" runat="server" Placeholder="Transmission" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-12 form-group">
                    <label for="txtDescription">Description:</label>
                    <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="3" Placeholder="Description" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="button-group text-center">
                <asp:Button ID="btnAddCar" runat="server" Text="Add Car" OnClick="btnAddCar_Click" CssClass="btn btn-primary mx-2" />
                <asp:Button ID="btnUpdateCar" runat="server" Text="Update Car" OnClick="btnUpdateCar_Click" CssClass="btn btn-secondary mx-2" />
                <asp:Button ID="btnDeleteCar" runat="server" Text="Delete Car" OnClick="btnDeleteCar_Click" CssClass="btn btn-danger mx-2" />
            </div>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="text-danger mt-3" />
            <asp:GridView ID="gvCars" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped gridview mt-4" OnRowCommand="gvCars_RowCommand" DataKeyNames="CarId">
                <Columns>
                    <asp:BoundField DataField="CarId" HeaderText="Car ID" ReadOnly="True" />
                    <asp:BoundField DataField="ManufacturerId" HeaderText="Manufacturer ID" ReadOnly="True" />
                    <asp:BoundField DataField="Model" HeaderText="Model" />
                    <asp:BoundField DataField="Year" HeaderText="Year" />
                    <asp:BoundField DataField="Colour" HeaderText="Colour" />
                    <asp:BoundField DataField="Price" HeaderText="Price" />
                    <asp:BoundField DataField="Mileage" HeaderText="Mileage" />
                    <asp:BoundField DataField="EngineType" HeaderText="Engine Type" />
                    <asp:BoundField DataField="Transmission" HeaderText="Transmission" />
                    <asp:BoundField DataField="Description" HeaderText="Description" />
                    <asp:ButtonField ButtonType="Link" CommandName="Select" Text="Select" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.5.2/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</body>
</html>
