namespace T9
{

    internal class Screen
    {
        // User and dataexecute
        private User user = new();
        private DataExecute data = new ();
        
        // Start
        public void Start() 
        {
            
            do {
                Login();
                switch (user.role)
                {
                    case "manager":
                        ManagerScreen();
                        break;
                    case "user":
                        UserScreen();
                        break;
                    default:
                        Console.Write("No role for ur user" + user.role); Console.ReadLine();
                        break;
                }
            } while (user.role =="");
                
        }

        // Login Screen
        private void Login() 
        {
            do
            {
                Console.WriteLine("===== EMPLOYEE MANAGE =====");
                Console.WriteLine("=====       LOGIN     =====");
                Console.Write("User name:"); user.username = Console.ReadLine() + "";
                Console.Write("Password:"); user.password = Console.ReadLine() + "";
                string str = user.username + "," + user.password;
                List<string> list;
                try
                {
                    list = data.SQLQuery(str.ToSQLSelEmp());
                    foreach (string line in list) { Console.WriteLine(line); }                    
                    user.username = list[0].ToArrayString()[0];
                    user.role = list[0].ToArrayString()[2];
                    user.name = list[0].ToArrayString()[3];                    
                }
                catch
                {
                    Console.WriteLine("Cant login!");
                }
                Console.WriteLine(user.role);
            } while (user.role == "");            
        }


        // Module Manager Screen
        private void ManagerScreen()
        {
            int selected = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("***EMPLOYEE MANAGER***");
                Console.WriteLine("*** MANAGER SCREEN ***");
                Console.WriteLine("----------------------");
                Console.WriteLine("Username: {0}", user.username);
                Console.WriteLine("----------------------");
                Console.WriteLine("1. Search Employee by Name or EmpNo");
                Console.WriteLine("2. Add New Employee");
                Console.WriteLine("3. Update Employee");
                Console.WriteLine("4. Delete Employee");
                Console.WriteLine("5. Show a List of Employee Sorted");
                Console.WriteLine("6. Import a list of Employee");
                Console.WriteLine("7. Export a list of Employee");
                Console.WriteLine("8. Logout");
                Console.WriteLine("9. Exit");
                Console.Write("   Select (1-9): ");

                // Try get a select with right numeric
                try
                {
                    selected = Convert.ToInt16(Console.ReadLine());
                }
                catch { }

                // Route program
                switch (selected)
                {
                    case 1:
                        FindScreen();
                        break;
                    case 2:
                        AddNewScreen();
                        break;
                    case 3:
                        UpdateScreen();
                        break;
                    case 4:
                        DeleteScreen();
                        break;
                    case 5:
                        SortScreen();
                        break;
                    case 6:
                        ImportScreen();
                        break;
                    case 7:
                        ExportScreen();
                        break;
                    case 8:
                        Console.WriteLine("Logging out");
                        user.role = "";                        
                        break;
                    case 9:
                        Console.WriteLine("-------- END ---------");
                        break;
                    default:
                        Console.Write("Wrong format!"); Console.ReadLine();
                        break;
                }
            } while (selected != 9 && user.role !="");
        }

        // Module User Screen
        private void UserScreen()
        {
            int selected = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("***EMPLOYEE MANAGER***");
                Console.WriteLine("***  USER SCREEN   ***");
                Console.WriteLine("----------------------");
                Console.WriteLine("Username: {0}", user.username);
                Console.WriteLine("----------------------");
                Console.WriteLine("1. Search Employee by Name or EmpNo");
                Console.WriteLine("2. Show a List of Employee Sorted");
                Console.WriteLine("3. Log out");
                Console.WriteLine("4. Exit");
                Console.Write("   Select (1-4): ");

                // Try get a select with right numeric
                try
                {
                    selected = Convert.ToInt16(Console.ReadLine());
                }
                catch { }

                // Route program
                switch (selected)
                {
                    case 1:
                        FindScreen();
                        break;
                    case 2:
                        SortScreen();
                        break;
                    case 3:
                        Console.WriteLine("Logging out");
                        user.role = "";
                        break;
                    case 4:
                        break;
                    default:
                        Console.Write("Wrong format!"); Console.ReadLine();
                        break;
                }
            } while (selected != 4 && user.role !="");
        }

        // Find Screen
        private void FindScreen()
        {
            try {
                Console.Clear();
                Console.Write("What field number u want to search by:");
                string field = Console.ReadLine() + "";
                Console.Write("What value u want to search:");
                string value = Console.ReadLine() + "";
                List<string> list = data.SQLQuery(value.ToSQLSelEmp(field));
                foreach (string item in list)
                {
                    Console.WriteLine(item);
                }
                Console.ReadLine();
            } 
            catch 
            {
                Console.Write("Something wrong!"); Console.ReadLine();
            }
            
        }

        // AddNew Screen
        private void AddNewScreen()
        {
            try 
            {
                Console.Clear();
                Console.Write("Name:");
                string name = Console.ReadLine() + "";
                Console.Write("UserName:");
                string username = Console.ReadLine() + "";
                Console.Write("PassWord:");
                string password = Console.ReadLine() + "";
                string str = username + "," + password + "," + name;
                data.SQLExecute(str.ToSQLInsEmp());
            } 
            catch 
            {
                Console.Write("Something wrong!"); Console.ReadLine();
            }
            
        }

        // Update Screen
        private void UpdateScreen()
        {
            try 
            {
                Console.Clear();
                Console.Write("ID:");
                string id = Console.ReadLine() + "";
                Console.Write("Field:");
                string field = Console.ReadLine() + "";
                Console.Write("Value:");
                string value = Console.ReadLine() + "";
                string str = field + "," + value + "," + id;
                data.SQLExecute(str.ToSQLUpdEmp());
            }
            catch
            {
                Console.Write("Something wrong!"); Console.ReadLine();
            }
        }

        // Delete Screen
        private void DeleteScreen()
        {
            try 
            {
                Console.Clear();
                Console.Write("Field:");
                string field = Console.ReadLine() + "";
                Console.Write("Value:");
                string value = Console.ReadLine() + "";
                string str = field + "," + value;
                data.SQLExecute(str.ToSQLDelEmp());
            }
            catch
            {
                Console.Write("Something wrong!"); Console.ReadLine();
            }
        }

        // Import Screen
        private void ImportScreen()
        {
            try
            {
                Console.Clear();
                List<string> list = new();
                Console.Write("File name:");
                string filename = @"" + Console.ReadLine() + "";
                FileStream f = new(filename, FileMode.OpenOrCreate);
                StreamReader s = new(f);
                string? line;
                while ((line = s.ReadLine()) != null)
                {
                    list.Add(line + "");
                }
                s.Close();
                foreach (string strline in list)
                {
                    data.SQLExecute(strline.ToSQLInsEmp());
                }
            }
            catch
            {
                Console.Write("Something wrong!"); Console.ReadLine();
            }
            
        }

        // Export Screen
        private void ExportScreen()
        {
            try
            {
                Console.Clear();
                List<string> list = data.SQLQuery("All".ToSQLSelEmp());
                Console.Write("File name:");
                string filename = @"" + Console.ReadLine() + "";
                FileStream f = new(filename, FileMode.OpenOrCreate);
                StreamWriter w = new(f);
                foreach (string line in list)
                {
                    w.WriteLine(line + "");
                }
                w.Close();
            }
            catch
            {
                Console.Write("Something wrong!"); Console.ReadLine();
            }

        }

        // Sort Sreen
        private void SortScreen()
        {
            Console.Clear();
            List<string> list = data.SQLQuery("All".ToSQLSelEmp());
            list.Sort();
            foreach (string line in list) 
            {
                Console.WriteLine(line);
            }
            Console.ReadLine();
        }

        /*public event Status Loged;*/
        /*public virtual void OnLogged() 
        {            
            Console.WriteLine("Login successful!");
            Loged?.Invoke();
            switch (user.role)
            {
                case "manager":
                    ManagerScreen();
                    break;
                case "user":
                    UserScreen();
                    break;
                default:
                    Console.Write("No role for ur user" + user.role); Console.ReadLine();
                    break;
            }
        }*/
    }
    /*delegate void Status();*/
}
