using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWindDataObjects
{
    public class OrderDetails
    {
        private int orderID;
        private int productID;
        private decimal unitPrice;
        private int quantity;
        private Single discount;

        public int OrderID { get => orderID; set => orderID = value; }
        public int ProductID { get => productID; set => productID = value; }
        public decimal UnitPrice { get => unitPrice; set => unitPrice = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public Single Discount { get => discount; set => discount = value; }
    }
}
