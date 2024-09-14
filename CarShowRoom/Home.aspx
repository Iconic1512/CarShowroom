<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="CarShowRoom.Home" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home</title>
    <style>
        body { 
            font-family: Arial, sans-serif; 
            background-color: #f0f0f0; 
            margin: 0; 
            padding: 0; 
        }
        .container { 
            max-width: 1200px; 
            margin: 50px auto; 
            padding: 20px; 
            background-color: #fff; 
            border-radius: 8px; 
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1); 
            text-align: center;
        }
        header { 
            text-align: center; 
            margin-bottom: 20px; 
        }
        header h1 { 
            color: #333; 
            font-size: 2.5em;
        }
        .menu { 
            display: flex; 
            justify-content: center; 
            flex-wrap: wrap; 
            gap: 20px; 
            margin-bottom: 30px; 
        }
        .menu a { 
            display: block; 
            padding: 10px 20px; 
            color: #fff; 
            background-color: #007bff; 
            text-decoration: none; 
            border-radius: 4px; 
            transition: background-color 0.3s; 
            font-size: 1.1em;
        }
        .menu a:hover { 
            background-color: #0056b3; 
        }
        .car-gallery { 
            display: flex; 
            justify-content: space-around; 
            flex-wrap: wrap; 
            gap: 20px; 
        }
        .car-item { 
            background-color: #f9f9f9; 
            border: 1px solid #ddd; 
            border-radius: 8px; 
            overflow: hidden; 
            width: 300px; 
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1); 
            text-align: left;
        }
        .car-item img { 
            width: 300px; 
            height: 200px; 
            object-fit: cover; 
            border-bottom: 1px solid #ddd; 
        }
        .car-item .description { 
            padding: 15px; 
        }
        .car-item .description h3 { 
            margin: 0 0 10px; 
            font-size: 1.2em;
        }
        .car-item .description p { 
            margin: 0; 
            color: #555;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <header>
                <h1>Welcome to Car Showroom</h1>
            </header>
            <div class="menu">
                <a href="Car.aspx">Manage Cars</a>
                <a href="Customer.aspx">Manage Customers</a>
                <a href="Sales.aspx">Manage Sales</a>
                <a href="Employee.aspx">Manage Employees</a>
                <a href="Inventory.aspx">Manage Inventory</a>
                <a href="Manufacturer.aspx">Manage Manufacturers</a>
                <a href="Service_Record.aspx">Manage Service Records</a>
            </div>
            <div class="car-gallery">
                <div class="car-item">
                    <img src="images/image1.jpg" alt="Car Model 1" />
                    <div class="description">
                        <h3>Model 1</h3>
                        <p>The Model 1 is a stylish car with top-of-the-line features. Enjoy a smooth ride with excellent fuel efficiency.</p>
                    </div>
                </div>
                <div class="car-item">
                    <img src="images/image2.jpg" alt="Car Model 2" />
                    <div class="description">
                        <h3>Model 2</h3>
                        <p>The Model 2 offers advanced safety features and a sleek design. Perfect for families and road trips.</p>
                    </div>
                </div>
                <div class="car-item">
                    <img src="images/image3.jpg" alt="Car Model 3" />
                    <div class="description">
                        <h3>Model 3</h3>
                        <p>With high performance and luxury features, the Model 3 is ideal for those who love driving.</p>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
