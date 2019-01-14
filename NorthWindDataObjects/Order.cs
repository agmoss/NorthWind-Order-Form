using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWindDataObjects
{
    public class Order
    {
        private int orderID;
        private string customerID;
        private int employeeID;
        private DateTime? orderDate;
        private DateTime? requiredDate;
        private DateTime? shippedDate;
        private int shipVia;
        private decimal freight;
        private string shipName;
        private string shipAddress;
        private string shipCity;
        private string shipRegion;
        private string shipPostalCode;
        private string shipCountry;

        public int OrderID { get => orderID; set => orderID = value; }
        public string CustomerID { get => customerID; set => customerID = value; }
        public int EmployeeID { get => employeeID; set => employeeID = value; }
        public DateTime? OrderDate { get => orderDate; set => orderDate = value; }
        public DateTime? RequiredDate { get => requiredDate; set => requiredDate = value; }
        public DateTime? ShippedDate { get => shippedDate; set => shippedDate = value; }
        public int ShipVia { get => shipVia; set => shipVia = value; }
        public decimal Freight { get => freight; set => freight = value; }
        public string ShipName { get => shipName; set => shipName = value; }
        public string ShipAddress { get => shipAddress; set => shipAddress = value; }
        public string ShipCity { get => shipCity; set => shipCity = value; }
        public string ShipRegion { get => shipRegion; set => shipRegion = value; }
        public string ShipPostalCode { get => shipPostalCode; set => shipPostalCode = value; }
        public string ShipCountry { get => shipCountry; set => shipCountry = value; }

    }
}
