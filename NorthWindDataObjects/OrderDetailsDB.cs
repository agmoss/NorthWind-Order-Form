using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWindDataObjects
{
    public class OrderDetailsDB
    {
        public static List<OrderDetails> GetOrderDetails()
        {
            
            OrderDetails detail;
            SqlConnection con = Connection.GetConnection();

            string sql = "SELECT OrderID, ProductID, UnitPrice, Quantity, Discount" +  
                " FROM \"Order Details\"";

            SqlCommand cmd = new SqlCommand(sql, con);

            List<OrderDetails> detailsList = new List<OrderDetails>();

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
              
                while (reader.Read())
                {
                    detail = new OrderDetails();

                    detail.OrderID = (int)reader["OrderID"];
                    detail.ProductID = (int)reader["ProductID"];
                    detail.UnitPrice = (decimal)reader["UnitPrice"];
                    detail.Quantity = Convert.ToInt16(reader["Quantity"]);
                    detail.Discount = (Single)reader["Discount"];

                    detailsList.Add(detail);
                }

            }
            catch (SqlException ex)
            {

                throw ex;
            }
            finally
            {
                con.Close();
            }

            return detailsList;

        }
    }
}
