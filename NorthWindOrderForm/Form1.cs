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

            // Grey out the datetime controls
            foreach (Control c in this.Controls)
            {
                if (c is DateTimePicker)
                {
                    ((DateTimePicker)c).CustomFormat = " ";
                    ((DateTimePicker)c).Format = DateTimePickerFormat.Custom;
                }
            }

            List<Order> ordersList = OrdersDB.GetOrders();

            var linqOrds = from ord in ordersList
                           select new
                           {
                               ord.OrderID
                           };

            foreach (var ord in linqOrds)
            {
                lstOrderID.Items.Add(ord.OrderID);
            }
        }

        /// <summary>
        ///  Determine if the datetime object property from the database is null
        /// </summary>
        public void EvaluateDateTime(DateTime? dateProperty, DateTimePicker dateControl)
        {
            if (dateProperty != null)
            {
                dateControl.CustomFormat = null;
                dateControl.Format = DateTimePickerFormat.Long; 
                dateControl.Value = (DateTime)dateProperty; 
            }
            else
            {
                dateControl.CustomFormat = " ";
                dateControl.Format = DateTimePickerFormat.Custom;
            }
        }

        /// <summary>
        ///  Determine if the user provided shipped date is vald
        /// </summary>
        private bool ValidDateSelection()
        {
            //  ShippedDate cannot be earlier than OrderDate or later than RequiredDate, if these date values are not null

            errorProvider1.Clear();
          
            DateTime sDate = Convert.ToDateTime(shippedDateDateTimePicker.Text);
            DateTime oDate = Convert.ToDateTime(orderDateDateTimePicker.Text);
            DateTime rDate = Convert.ToDateTime(requiredDateDateTimePicker.Text);

            if (sDate <= oDate || sDate >= rDate)
            {
                // Shipped date is invalid
                errorProvider1.Clear();
                errorProvider1.SetError(shippedDateDateTimePicker, "Shipped date must be between the shipped date and the order date");
                return false;
            }
            else
            {                
                return true;
            }
        }

        /// <summary>
        /// Save the new shipped date if it is valid
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            lblSuccess.Visible = false;

            if (ValidDateSelection())
            {               
                try
                {
                    // Call method to update the database
                    int idToUpdate = Convert.ToInt32(lstOrderID.Text.Trim());
                    NorthWindDataObjects.Update.UpdateItemInDB(idToUpdate, Convert.ToDateTime(shippedDateDateTimePicker.Text));

                    // Display success
                    lblSuccess.Visible = true;
                    lblSuccess.Text = "Record updated successfully";
                    lblSuccess.BackColor = Color.LawnGreen;
                }
                catch (Exception)
                {
                    // Unexpected error
                    errorProvider1.SetError(btnSave, "Data cannot be saved");
                }
            }   
        }

        /// <summary>
        /// Display information for the currently selected order id
        /// </summary>
        private void lstOrderID_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get order information
            var order = orderList.First(i => i.OrderID == Convert.ToInt32(lstOrderID.Text));

            if (order != null)
            {
                // Display order info
                customerIDTextBox.Text = order.CustomerID;
                employeeIDTextBox.Text = order.EmployeeID.ToString();
                freightTextBox.Text = order.Freight.ToString();

                EvaluateDateTime(order.ShippedDate, shippedDateDateTimePicker);
                EvaluateDateTime(order.OrderDate, orderDateDateTimePicker);
                EvaluateDateTime(order.RequiredDate, requiredDateDateTimePicker);

                shipAddressTextBox.Text = order.ShipAddress;
                shipCityTextBox.Text = order.ShipCity;
                shipCountryTextBox.Text = order.ShipCountry;
                shipNameTextBox.Text = order.ShipName;
                shipPostalCodeTextBox.Text = order.ShipPostalCode;
                shipRegionTextBox.Text = order.ShipRegion;
                shipViaTextBox.Text = order.ShipVia.ToString();
            }

            // Retrieve Order Detail Information 
            var orderDetails = from item in orderDetailsList
                               where item.OrderID == Convert.ToInt32(lstOrderID.Text)
                               select new
                               {
                                   item.OrderID,
                                   item.ProductID,
                                   item.Quantity,
                                   item.Discount,
                                   item.UnitPrice
                               };

            // Display the order details in the datagridview
            List<OrderDetails> newDetails = new List<OrderDetails>();
            float total = 0;
            foreach (var item in orderDetails)
            {
                OrderDetails detail = new OrderDetails();
                detail.OrderID = item.OrderID;
                detail.ProductID = item.ProductID;
                detail.Quantity = item.Quantity;
                detail.Discount = item.Discount;
                detail.UnitPrice = item.UnitPrice;

                // Order Total
                total += Convert.ToSingle(detail.UnitPrice) * (1 - detail.Discount) * (detail.Quantity);
                
                // Append to list
                newDetails.Add(detail);
            }
            orderDetailsDataGridView.DataSource = newDetails;
            txtOrderTotal.Text = total.ToString();
        }
    }
}
