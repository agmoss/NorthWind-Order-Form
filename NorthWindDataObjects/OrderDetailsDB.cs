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
            List<OrderDetails> ordDtls = new List<OrderDetails>();
            OrderDetails odtl;
            SqlConnection con = Connection.GetConnection();

            string sql = "SELECT OrderID, ProductID, UnitPrice, Quantity, Discount" +  
                " FROM \"Order Details\"";

            SqlCommand cmd = new SqlCommand(sql, con);

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                odtl = new OrderDetails();
                while (reader.Read())
                {
                    odtl.OrderID = (int)reader["OrderID"];
                    odtl.ProductID = (int)reader["ProductID"];
                    odtl.UnitPrice = (decimal)reader["UnitPrice"];
                    odtl.Quantity = Convert.ToInt16(reader["Quantity"]);
                    odtl.Discount = (Single)reader["Discount"];

                    ordDtls.Add(odtl);
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

            return ordDtls;

        }
    }
}
