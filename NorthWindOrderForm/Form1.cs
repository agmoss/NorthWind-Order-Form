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
            // Get order information
            var order = orderList.FirstOrDefault(i => i.OrderID == Convert.ToInt32(orderIDComboBox.Text));

            if (order != null)
            {
                customerIDTextBox.Text = order.CustomerID;
                employeeIDTextBox.Text = order.EmployeeID.ToString();
                freightTextBox.Text = order.Freight.ToString();
                orderDateDateTimePicker.Text = order.OrderDate.ToString();
                requiredDateDateTimePicker.Text = order.RequiredDate.ToString();
                shipAddressTextBox.Text = order.ShipAddress;
                shipCityTextBox.Text = order.ShipCity;
                shipCountryTextBox.Text = order.ShipCountry;
                shipNameTextBox.Text = order.ShipName;
                shippedDateDateTimePicker.Text = order.ShippedDate.ToString();
                shipPostalCodeTextBox.Text = order.ShipPostalCode;
                shipRegionTextBox.Text = order.ShipRegion;
                shipViaTextBox.Text = order.ShipVia.ToString();
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
            float total = 0;
            foreach (var row in orderDetails)
            {               
                OrderDetails details = new OrderDetails();
                details.ProductID = row.ProductID;
                details.Quantity = row.Quantity;
                details.Discount = row.Discount;
                details.UnitPrice = row.UnitPrice;

                // Order Total
                total += Convert.ToSingle(details.UnitPrice) * (1 - details.Discount) * (details.Quantity);

                newDetails.Add(details);
            }
            orderDetailsDataGridView.DataSource = newDetails;
            txtOrderTotal.Text = total.ToString();
        }
          
        
        private void shippedDateDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            //  ShippedDate cannot be earlier than OrderDate or later than RequiredDate, if these date values are not null
            try
            {
                DateTime sDate = Convert.ToDateTime(shippedDateDateTimePicker.Text);
                DateTime oDate = Convert.ToDateTime(orderDateDateTimePicker.Text);
                DateTime rDate = Convert.ToDateTime(requiredDateDateTimePicker.Text);

                if (sDate <= oDate || sDate >= rDate)
                {
                    // Shipped date is invalid
                    errorProvider1.SetError(shippedDateDateTimePicker, "Shipped date must be between the shipped date and the order date");                  
                }
                else
                {
                    errorProvider1.Clear();
                }
            }
            catch (Exception)
            {
                errorProvider1.SetError(requiredDateDateTimePicker, "Unable to change Shipped date");               
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int idToUpdate = Convert.ToInt32(orderIDComboBox.Text.Trim());
                NorthWindDataObjects.Update.UpdateItemInDB(idToUpdate, Convert.ToDateTime(shippedDateDateTimePicker.Text));
            }
            catch (Exception)
            {
                errorProvider1.SetError(btnSave, "Data cannot be saved");
            }        
        }           
    }
}
