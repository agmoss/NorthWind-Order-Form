using NorthWindDataObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NorthWindOrderForm
{
    public partial class Form1 : Form
    {
        // List of DB records from  NorthWindDataObjects (ADO.NET)
        List<OrderDetails> orderDetailsList = OrderDetailsDB.GetOrderDetails();
        List<Order> orderList = OrdersDB.GetOrders();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            List<Order> ordersList = OrdersDB.GetOrders();

            var linqOrds = from ord in ordersList
                           select new
                           {
                               ord.OrderID
                           };

            foreach (var ord in linqOrds)
            {
                orderIDComboBox.Items.Add(ord.OrderID);
            }
        }

        private void orderIDComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            // Retrieve Order Info
            var order = from ord in orderList
                         where ord.OrderID == Convert.ToInt32(orderIDComboBox.Text)
                            select new
                            {
                                ord.CustomerID,
                                ord.EmployeeID,
                                ord.OrderDate,
                                ord.RequiredDate,
                                ord.ShippedDate,
                                ord.ShipVia,
                                ord.Freight,
                                ord.ShipName,
                                ord.ShipAddress,
                                ord.ShipCity,
                                ord.ShipRegion,
                                ord.ShipPostalCode,
                                ord.ShipCountry

                            };

            foreach (var item in order)
            {              
                customerIDTextBox.Text = item.CustomerID;
                employeeIDTextBox.Text = item.EmployeeID.ToString();
                orderDateDateTimePicker.Text = item.OrderDate.ToString();              
            }

            // Retrieve Order Detail Information (this is not working)
            var orderDetails = from detail in orderDetailsList
                         where detail.OrderID == Convert.ToInt32(orderIDComboBox.Text)
                         select new
                         {
                             detail.ProductID,
                             detail.Quantity,
                             detail.Discount,
                             detail.UnitPrice                                
                         };

            List<OrderDetails> newDetails = new List<OrderDetails>();
            foreach (var row in orderDetails)
            {
                OrderDetails details = new OrderDetails();
                details.ProductID = row.ProductID;
                details.Quantity = row.Quantity;
                details.Discount = row.Discount;
                details.UnitPrice = row.UnitPrice;
                newDetails.Add(details);
            }

            orderDetailsDataGridView.DataSource = newDetails ;


        }
    }
}
