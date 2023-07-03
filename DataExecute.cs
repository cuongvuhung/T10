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
            Console.Write(str);Console.ReadLine();
            SqlConnection cnn = new(config.conStr);
            cnn.Open();
            if (str != "")
            {
                SqlCommand cmd = new(str, cnn);
                cmd.ExecuteNonQuery();
            }
            else { }
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
                Console.Write(str);Console.ReadLine();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    string row = rdr["username"].ToString() 
                        + "," + rdr["password"].ToString()
                        + "," + rdr["role"].ToString()
                        + "," + rdr["name"].ToString();
                    result.Add(row);
                }
            }
            else { result = new(); }
            cnn.Close();
            return result;
        }
        
    }
}
