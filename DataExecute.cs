using System.Data.SqlClient;

namespace T9
{
    internal class DataExecute
    {
        // MS SQL Worker

        // Excute SQL non querry
        private readonly Config config = new ();
        public void SQLExecute(string str)
        {            
            SqlConnection cnn = new(config.conStr);
            cnn.Open();
            if (str != "")
            {
                SqlCommand cmd = new(str, cnn);
                cmd.ExecuteNonQuery();
                Console.Write("Execute successful!"); Console.ReadLine();
            }
            else 
            {
                Console.Write("Execute failed!"); Console.ReadLine();
            }
            cnn.Close();
        }

        // Excute SQL SELECT 
        public List<string> SQLQuery(string str)
        {
            SqlConnection cnn = new (config.conStr);
            cnn.Open();
            List<string> result = new();
            if (str != "")
            {                
                SqlCommand cmd = new(str, cnn);                
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    string row = rdr["username"].ToString() 
                        + "," + rdr["password"].ToString()
                        + "," + rdr["role"].ToString()
                        + "," + rdr["name"].ToString();
                    result.Add(row);
                }
                Console.Write("Query successful!"); Console.ReadLine();
            }
            else 
            { 
                result = new();
                Console.Write("Query failed!"); Console.ReadLine();
            }
            cnn.Close();
            return result;
        }
        
    }
}
