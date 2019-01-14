using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWindDataObjects
{
    public class OrdersDB
    {
        public static List<Order> GetOrders()
        {
            Order order = null;

            SqlConnection conn = Connection.GetConnection();

            string query = "SELECT OrderID, CustomerID, EmployeeID, OrderDate, RequiredDate," +
            " ShippedDate, ShipVia, Freight, ShipName, ShipAddress, ShipCity, ShipRegion, ShipPostalCode, ShipCountry " +
                 "FROM Orders " +
                 "ORDER BY OrderID";

            SqlCommand cmd = new SqlCommand(query, conn);

            List<Order> orders = new List<Order>();

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    order = new Order();

                    order.OrderID = (int)reader["OrderID"];
                    order.CustomerID = reader["CustomerID"].ToString();
                    order.EmployeeID = (int)reader["EmployeeID"];
                    order.OrderDate = (DateTime)reader["OrderDate"];
                    order.RequiredDate = (DateTime)reader["RequiredDate"];
                    if (reader["ShippedDate"] == DBNull.Value)
                        order.ShippedDate = null;
                    else
                    {
                        order.ShippedDate = (DateTime)reader["ShippedDate"];
                    }
                    order.ShipVia = (int)reader["ShipVia"];
                    order.Freight = (decimal)reader["Freight"];
                    order.ShipName = reader["ShipName"].ToString();
                    order.ShipVia = (int)reader["ShipVia"];
                    order.ShipAddress = reader["ShipAddress"].ToString();
                    order.ShipCity = reader["ShipCity"].ToString();
                    order.ShipRegion = reader["ShipRegion"].ToString();
                    order.ShipPostalCode = reader["ShipPostalCode"].ToString();
                    order.ShipCountry = reader["ShipCountry"].ToString();

                    orders.Add(order); // Add the  order object to the return list

                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            finally
            {
                conn.Close();
            }
            return orders;
        }
    }
}
