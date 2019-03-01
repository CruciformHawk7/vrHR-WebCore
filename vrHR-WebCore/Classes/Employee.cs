using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vrHR_WebCore.Classes
{
    public class Employee
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Group { get; set; }
        public int DaysAttended { get; set; }
        public int TotalDays { get; set; }
        public double BasicPay { get; set; }
        public double AllotedResourceCost { get; set; }

        public string Email { get; set; }

        public List<Message> messages;

        public void addMessage(Message message)
        {
            messages.Add(message);
        }

        public double computeAttendance()
        {
            return DaysAttended / TotalDays * 100;
        }
    }

    public class HR: Employee
    {
        public List<Employee> Employees { get; set; }

        public string EmployeeList { get; set; }

        public HR ()
        {
            EmployeeList = "";
        }

        public void AddToEmpList (string Employee)
        {
            this.EmployeeList += Employee;
        }

        public void AddEmployee(Employee employee)
        {
            this.Employees.Add(employee);
        }

        public void UpdateEmployeeList()
        {
            EmployeeList = "";
            foreach(var Employee in Employees)
            {
                EmployeeList += Employee.ID;
            }
        }
    }
}
