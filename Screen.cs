namespace T10
{

    internal class Screen
    {
        // User and dataexecute
        private Employee user = new();
        private DataExecute data = new ();
        
        // Start
        public void Start() 
        {
            
            do {
                Login();
                switch (user.Role)
                {
                    case "manager":
                        ManagerScreen();
                        break;
                    case "user":
                        UserScreen();
                        break;
                    default:
                        Console.Write("No role for ur user" + user.Role); Console.ReadLine();
                        break;
                }
            } while (user.Role =="");
                
        }

        // Login Screen
        private void Login() 
        {
            do
            {
                Console.WriteLine("===== EMPLOYEE MANAGE =====");
                Console.WriteLine("=====       LOGIN     =====");
                Console.Write("User name:"); user.Username = Console.ReadLine() + "";
                Console.Write("Password:"); user.Password = Console.ReadLine() + "";
                string str = user.Username + "," + user.Password;
                List<Employee> list;
                try
                {
                    list = data.SQLQuery(str);
                    foreach (Employee line in list) 
                    {
                        user.Role = line.Role;
                        user.Name = line.Name;
                    }                                                                                
                }
                catch
                {
                    Console.WriteLine("Cant login!");
                }
                Console.WriteLine(user.Role);
            } while (user.Role == "");            
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
                Console.WriteLine("Username: {0}", user.Username);
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
                        user.Role = "";                        
                        break;
                    case 9:
                        Console.WriteLine("-------- END ---------");
                        break;
                    default:
                        Console.Write("Wrong format!"); Console.ReadLine();
                        break;
                }
            } while (selected != 9 && user.Role !="");
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
                Console.WriteLine("Username: {0}", user.Username);
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
                        user.Role = "";
                        break;
                    case 4:
                        break;
                    default:
                        Console.Write("Wrong format!"); Console.ReadLine();
                        break;
                }
            } while (selected != 4 && user.Role !="");
        }

        // Find Screen
        private void FindScreen()
        {
            try {
                Console.Clear();
                Console.Write("Name:");
                string str = Console.ReadLine() + "";
                Console.WriteLine(str);
                foreach (Employee item in data.SQLQuery(str))
                {
                    Console.WriteLine(item.ToString());
                    Console.ReadLine();
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
                Console.Write("PassWord:");
                string password = Console.ReadLine() + "";
                Console.Write("UserName:");
                string username = Console.ReadLine() + "";
                Console.Write("Position:");
                string position = Console.ReadLine() + "";
                string str = name + "," + password + "," + username + "," + position;
                data.SQLExecute(str);
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
                Console.Write("Username:");
                string username = Console.ReadLine() + "";
                Console.Write("Field:");
                string field = Console.ReadLine() + "";
                Console.Write("Value:");
                string value = Console.ReadLine() + "";
                string str = username +","+ field + "," + value;
                data.SQLExecute(str);
            }
            catch
            {
                Console.Write("Something wrong!"); Console.ReadLine();
            }
        }

        // Delete Screen
        private void DeleteScreen()
        {
            //try 
            //{
                Console.Clear();
                Console.Write("Name:");
                string str = Console.ReadLine() + "";
                data.SQLExecute(str);
            //}
            //catch
            //{
            //    Console.Write("Something wrong!"); Console.ReadLine();
            //}
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
                foreach (string str in list)
                {
                    try
                    {
                        data.SQLExecute(str);
                    }
                    catch { }
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
                List<Employee> list = data.SQLQuery("All");
                Console.Write("File name:");
                string filename = @"" + Console.ReadLine() + "";
                FileStream f = new(filename, FileMode.OpenOrCreate);
                StreamWriter w = new(f);
                foreach (Employee line in list)
                {
                    w.WriteLine(line.ToString() + "");
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
            List<Employee> list = data.SQLQuery("All");
            List<Employee> Sortedlist = list.OrderByDescending(x=>x.Position).ToList();
            foreach (Employee line in Sortedlist) 
            {
                Console.WriteLine(line.ToString());
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
