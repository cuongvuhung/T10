using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T10
{
    internal class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Username { get; set; }
        public string Position { get; set; }
        public Employee() { }
        public Employee(int id, string name, string password, string role, string username, string position)
        {
            this.Id = id;
            this.Name = name;
            this.Password = password;
            this.Role = role;
            this.Username = username;
            this.Position = position;
        }
        public string ToString()
        { 
            return Id + ","+ Name + "," + Password + "," + Role + "," + Username + "," + Position;
        }
    }
}
