using System.Data.SqlClient;
using System.Runtime.Intrinsics.Arm;

namespace T10
{
    internal class DataExecute
    {
        // MS SQL Worker

        // Excute SQL non querry
        private readonly Config config = new ();
        public void SQLExecute(string str)
        {
            string sql;
            SqlConnection cnn = new(config.conStr);
            cnn.Open();
            SqlCommand cmd;
            switch (str.Count())
            {
                case 1:
                    sql = "Delete from Employees where name = @0";
                    cmd = new SqlCommand(sql, cnn);
                    cmd.Parameters.AddWithValue("0", str);
                    break;                                    
                case 3:
                    sql = "Update Employees set "+ str.ToArrayString()[1] + " = @1 where username = @2";
                    cmd = new(sql, cnn);                    
                    cmd.Parameters.AddWithValue("1", str.ToArrayString()[2]);
                    cmd.Parameters.AddWithValue("2", str.ToArrayString()[0]);
                    break;
                case 4:
                    sql = "Insert into Employees(name,password,role,username,position) values (@0,@1,'user',@2,@3)";
                    cmd = new(sql, cnn);
                    cmd.Parameters.AddWithValue("0", str.ToArrayString()[0]);
                    cmd.Parameters.AddWithValue("1", str.ToArrayString()[1]);
                    cmd.Parameters.AddWithValue("2", str.ToArrayString()[2]);
                    cmd.Parameters.AddWithValue("3", str.ToArrayString()[3]);
                    break;
                default:
                    sql = "Delete from Employees where 1 = 0";
                    cmd = new(sql, cnn);
                    break;
            }
                cmd.ExecuteNonQuery();
                Console.Write("Execute successful!"); Console.ReadLine();
            cnn.Close();
        }

        // Excute SQL SELECT 
        public List<Employee> SQLQuery(string str)
        {
            string sql;
            SqlConnection cnn = new(config.conStr);
            cnn.Open();
            SqlCommand cmd;
            List<Employee> result = new();
            switch (str.Count())
            {
                case 1:
                    if (str == "All")
                    {
                        sql = "Select * from Employees";
                        cmd = new SqlCommand(sql, cnn);
                    }
                    else 
                    {
                        sql = "Select * from Employees where name = @0;";
                        cmd = new(sql, cnn);
                        cmd.Parameters.AddWithValue("@0", str);
                    }
                    break;
                case 2:
                    sql = "Select * from Employees where username =@0 and password =@1;";
                    cmd = new(sql, cnn);
                    cmd.Parameters.AddWithValue("@0", str.ToArrayString()[0]);
                    cmd.Parameters.AddWithValue("@1", str.ToArrayString()[1]);
                    break;
                default:
                    cnn.Close();
                    return new List<Employee>();                    
            }
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Employee emp = new()
                {
                    Id = Convert.ToInt32(rdr["Id"]),
                    Username = rdr["username"].ToString() + "",
                    Name = rdr["name"].ToString() + "",
                    Password = rdr["password"].ToString() + "",
                    Role = rdr["role"].ToString() + "",
                    Position = rdr["position"].ToString() + ""
                };
                result.Add(emp);
            }
            Console.Write("Query successful!"); Console.ReadLine();
            cnn.Close();
            return result;
        }
        
    }
}
