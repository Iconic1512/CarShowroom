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
    </style>
</head>
<body>
    <form id="serviceform" runat="server" class="container form-container">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
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
                        <label for="txtMileage">Car Mialege:</label>
                        <asp:TextBox ID="txtMileage" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                   </div> 




                    <!-- CustomerId Field -->
                    <div class="col-md-6 form-group">
                        <label for="ddlCustomerId">Customer ID:</label>
                        <asp:DropDownList ID="ddlCustomerId" runat="server"  CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCustomerId_SelectedIndexChanged"></asp:DropDownList>
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
            lblMessage.classList.add('d-none');  // Ensure the label is hidden
            lblMessage.innerHTML = "";  // Clear the message text
        }
    }, 3000); // 3 seconds delay
            }

        </script>
          

    </form>
</body>
</html>
