<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="inventory.aspx.cs" Inherits="CarShowRoom.inventory" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Car Inventory Management</title>
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
    </style>
</head>
<body>
    <form id="inventoryform" runat="server" class="container form-container">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <div class="card">
            <div class="card-header">
                <h2 class="text-center">Car Inventory Management</h2>
            </div>
            <div class="card-body">
                <!-- Message box -->
                <div class="row">
                    <div class="col-12 text-center">
                        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" CssClass="alert alert-danger d-none" />
                        <div id="alertBox1" class="alert-box"></div>
                    </div>
                </div>

                <!-- Inventory Grid -->
                <asp:GridView ID="gvInventory" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped gridview mt-4" OnRowCommand="gvInventory_RowCommand" DataKeyNames="InventoryId">
                    <Columns>
                        <asp:BoundField DataField="InventoryId" HeaderText="Inventory ID" ReadOnly="True" />
                        <asp:BoundField DataField="CarId" HeaderText="Car ID" />
                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                        <asp:BoundField DataField="Status" HeaderText="Status" />
                        <asp:BoundField DataField="LastDate" HeaderText="Last Updated" DataFormatString="{0:yyyy-MM-dd}" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <div class="btn-group btn-group-sm">
                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CommandName="UpdateRow" CommandArgument='<%# Eval("InventoryId") %>' CssClass="btn btn-warning btn-sm" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

                <!-- Update Form -->
                <div class="row" id="updateForm" style="display: none;">
                    <div class="col-md-6 form-group">
                        <label for="txtQuantity">Quantity:</label>
                        <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-6 form-group">
                        <label for="txtStatus">Status:</label>
                        <asp:TextBox ID="txtStatus" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="button-group text-center">
                        <asp:Button ID="btnSubmitUpdate" runat="server" Text="Update" CssClass="btn btn-success" OnClick="btnSubmitUpdate_Click" />
                        <asp:HiddenField ID="hfInventoryId" runat="server" />
                    </div>
                </div>
            </div>
        </div>

        <!-- Scripts -->
        <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
        <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.5.2/dist/umd/popper.min.js"></script>
        <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
        <script>
            function showUpdateForm() {
                document.getElementById('updateForm').style.display = 'block';
            }
        </script>
            <script>
                function hideMessage() {
                    setTimeout(function () {
                        var lblMessage = document.getElementById('<%= lblMessage.ClientID %>');
                if (lblMessage) {
                    lblMessage.classList.add('d-none');  // Ensure the label is hidden
                    lblMessage.innerHTML = "";  // Clear the message text
                }
            }, 3000); // 3 seconds delay
                }

            </script>
    </form>
</body>
</html>
