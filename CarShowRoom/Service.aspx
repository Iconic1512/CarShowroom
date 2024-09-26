<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Service.aspx.cs" Inherits="CarShowRoom.Service" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Car Service Management</title>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />
    <style>
        .alert-box {
            display: none; 
            margin-top: 20px;
            text-align: center;
            padding: 10px; 
            width: 100%;
            position: relative;
        }

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

        .card {
            margin-top: 20px;
        }

        .btn-group-sm {
            display: flex;
            gap: 10px; /* Adjust space between buttons */
        }

        /* Drawer Styles */
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
    <form id="serviceform" runat="server" class="container form-container">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
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
        
        <div class="card">
            <div class="card-header">
                <h2 class="text-center">Car Service Management</h2>
            </div>

            <br/><br/>
            <div class="row">
                <div class="col-12 text-center">
                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" CssClass="alert alert-danger d-none" />
                    <div id="alertBox1" class="alert-box"></div>
                </div>
            </div>

            <div class="card-body">          
                <div class="row">
                    <!-- CarId Field -->
                    <div class="col-md-6 form-group">
                        <label for="ddlCarId">Car ID:</label>
                        <asp:DropDownList ID="ddlCarId" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCarId_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col-md-6 form-group">
                        <label for="txtName">Car Name:</label>
                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="col-md-6 form-group">
                        <label for="txtModel">Car Model:</label>
                        <asp:TextBox ID="txtModel" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="col-md-6 form-group">
                        <label for="txtYear">Car Year:</label>
                        <asp:TextBox ID="txtYear" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="col-md-6 form-group">
                        <label for="txtColor">Car Color:</label>
                        <asp:TextBox ID="txtColor" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="col-md-6 form-group">
                        <label for="txtPrice">Car Price:</label>
                        <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="col-md-6 form-group">
                        <label for="txtMileage">Car Mileage:</label>
                        <asp:TextBox ID="txtMileage" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>

                    <!-- CustomerId Field -->
                    <div class="col-md-6 form-group">
                        <label for="ddlCustomerId">Customer ID:</label>
                        <asp:DropDownList ID="ddlCustomerId" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCustomerId_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col-md-6 form-group">
                        <label for="txtCustomername">Customer Name:</label>
                        <asp:TextBox ID="txtCustomername" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>

                    <!-- Service Cost Field -->
                    <div class="col-md-6 form-group">
                        <label for="txtCost">Service Cost:</label>
                        <asp:TextBox ID="txtCost" runat="server" Placeholder="Cost" CssClass="form-control"></asp:TextBox>
                    </div>
                    <!-- Service Date Field -->
                    <div class="col-md-6 form-group">
                        <label for="txtServiceDate">Service Date:</label>
                        <asp:TextBox ID="txtServiceDate" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                
                <div class="button-group text-center">
                    <asp:Button ID="btnAddService" runat="server" Text="Add Service" OnClick="btnAddService_Click" CssClass="btn btn-primary mx-2" />
                </div>
                
                <asp:GridView ID="gvServiceRecords" runat="server" AutoGenerateColumns="False" 
                    CssClass="table table-bordered table-striped gridview mt-4" 
                    OnRowDeleting="gvServiceRecords_RowDeleting"
                    DataKeyNames="ServiceRecordId" Visible="false">
                    <Columns>
                        <asp:BoundField DataField="ServiceRecordId" HeaderText="Service Record ID" ReadOnly="True" />
                        <asp:BoundField DataField="CarId" HeaderText="Car ID" />
                        <asp:BoundField DataField="CustomerId" HeaderText="Customer ID" />
                        <asp:BoundField DataField="Cost" HeaderText="Service Cost" />
                        <asp:BoundField DataField="ServiceDate" HeaderText="Service Date" DataFormatString="{0:yyyy-MM-dd}" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <div class="btn-group btn-group-sm">
                                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("ServiceRecordId") %>' CssClass="btn btn-danger btn-sm" />
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
        <script>
            function hideMessage() {
                setTimeout(function () {
                    var lblMessage = document.getElementById('<%= lblMessage.ClientID %>');
                    if (lblMessage) {
                        lblMessage.classList.add('d-none');
                    }
                }, 5000);
            }

            function toggleDrawer() {
                var drawer = document.getElementById('drawer');
                drawer.classList.toggle('open');
            }
        </script>
    </form>
</body>
</html>
