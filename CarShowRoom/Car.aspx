﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Car.aspx.cs" Inherits="CarShowRoom.Car" %>
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
        .alert-box {
            display: none;
            margin-top: 20px;
        }
        .card {
            margin-top: 20px;
        }
        .btn-group-sm {
            display: flex;
            gap: 10px; /* Adjust space between buttons */
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" class="container form-container">
        <div class="card">
            <div class="card-header">
                <h2 class="text-center">Car Management</h2>
            </div>
            <div class="card-body">
                <asp:Label ID="lblMessage" runat="server" ForeColor="Red" CssClass="alert alert-danger d-none" />
                <div id="alertBox" class="alert-box"></div>
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
                    <asp:Button ID="btnViewCars" runat="server" Text="View Cars" CssClass="btn btn-info mx-2" OnClick="btnViewCars_Click" />
                </div>
                <asp:GridView ID="gvCars" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped gridview mt-4" OnRowCommand="gvCars_RowCommand" DataKeyNames="CarId" Visible="false">
                    <Columns>
                        <asp:BoundField DataField="CarId" HeaderText="Car ID" ReadOnly="True" />
                        <asp:BoundField DataField="ManufacturerName" HeaderText="Manufacturer" />
                        <asp:BoundField DataField="Model" HeaderText="Model" />
                        <asp:BoundField DataField="Year" HeaderText="Year" />
                        <asp:BoundField DataField="Colour" HeaderText="Colour" />
                        <asp:BoundField DataField="Price" HeaderText="Price" />
                        <asp:BoundField DataField="Mileage" HeaderText="Mileage" />
                        <asp:BoundField DataField="EngineType" HeaderText="Engine Type" />
                        <asp:BoundField DataField="Transmission" HeaderText="Transmission" />
                        <asp:BoundField DataField="Description" HeaderText="Description" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <div class="btn-group btn-group-sm">
                                    <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="Select" CommandArgument='<%# Container.DataItemIndex %>' CssClass="btn btn-secondary btn-sm" />
                                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("CarId") %>' CssClass="btn btn-danger btn-sm" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
        <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.5.2/dist/umd/popper.min.js"></script>
        <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    </form>
</body>
</html>