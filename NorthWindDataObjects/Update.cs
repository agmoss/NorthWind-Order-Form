using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWindDataObjects
{
    public class Update
    {

        public static void UpdateItemInDB(int IDtoUpdate, DateTime value)
        {

            SqlConnection tConn = Connection.GetConnection();
            tConn.Open();

            try
            {              
                SqlCommand tCommand = new SqlCommand();
                tCommand.Connection = tConn;
                tCommand.CommandText = "UPDATE Orders SET ShippedDate = @sDate WHERE OrderID = @id";

                // Date parameter
                SqlParameter dtPar = new SqlParameter("@sDate", SqlDbType.DateTime);
                dtPar.Value = value; // or any DateTime you have
                tCommand.Parameters.Add(dtPar);

                // Id parameter
                SqlParameter idPar = new SqlParameter("@id", SqlDbType.Int);
                idPar.Value = IDtoUpdate;
                tCommand.Parameters.Add(idPar);
              
                tCommand.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                tConn.Close();
            }                             
        }
    }
}
