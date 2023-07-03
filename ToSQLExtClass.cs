namespace T9
{
    public static class ToSQLExtClass
    {
        //=======================================================================================================
        // Gen a SQL to SEL emp with instr username and instr password
        public static string ToSQLSelEmp(this string str)
        {
            if (str == "All") return ("Select * from Employees");
            if (str.Count() == 2)
            {
                string[] words = str.ToArrayString();
                //Selecting.invoke();
                return ("Select * from Employees where username= '" + words[0] + "' and password= '" + words[1] + "';");

            }
            return ("");
        }
        // Gen a SQL to SEL emp with join field and instr value
        public static string ToSQLSelEmp(this string str,string field)
        {
            return ("Select * from Employees where " + field + "='" + str + "';");            
        }

        // Gen a SQL to INSERT emp with instr username, instr password, instr name
        public static string ToSQLInsEmp(this string str)
        {
            if (str.Count() == 3)
            {
                string[] words = str.ToArrayString(); 
                //Inserting.invoke();
                return ("Insert into Employees (username, password, name, role, position) values ('"
                    + words[0] + "','" + words[1] + "','" + words[2] + "','User','Staff');");
            }
            return ("");
        }

        // Gen a SQL to UPDATE emp with instr field with instr value where instr id
        public static string ToSQLUpdEmp(this string str)
        {
            if (str.Count() == 3)
            {
                string[] words = str.ToArrayString();
                //Updating.invoke();
                return ("Update Employees set " + words[0] + " = '" + words[1] + "' where id =" + words[2] + ";");
            }
            else
            return ("");
        }

        // Gen a SQL to DELETE employee with instr id or instr id and instr field with instr value
        public static string ToSQLDelEmp(this string str)
        {
            if (str.Count() == 2)
            {
                string[] words = str.ToArrayString();
                return ("Delete from Employees where " + words[0] + " = '" + words[1] + "';");
            }
            else
            {
                return ("");
            }
        }

        // Count how many cell in string
        public static int Count(this string str)
        {
            return str.ToArrayString().Length;
        }

        // Gen a array of cell
        public static string[] ToArrayString(this string str)
        {
            return str.Split(',') ;
        }
    }
}
