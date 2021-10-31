using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            List<Employee> employeeList = new List<Employee> {
                new Employee("Kyei Samuel", "R1"),
                new Employee("Rex Osei", "R2"),
                new Employee("Lali Agbozo", "R3"),
                new Employee("Maureen Peprah", "R4")};

            Console.WriteLine("Welcome to QFace Payroll system \n Press 1: To view all Employee on Payroll \n Press 2: To add Employee to payroll \n Press 3: To Generate Payslip for all Employees \n Press 4: To remove an employee from payroll \n Press 0: To exit");
        start: String userInput = Console.ReadLine();
            switch (userInput)
            {
                case "1":
                    view_employees(employeeList);
                    goto start;
                case "2":
                    add_employee(employeeList);
                    goto start;
                case "3":
                    generate_paylips(employeeList);
                    goto start;
                case "4":
                    remove_employee(employeeList);
                    goto start;
                case "0":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid Input");
                    goto start;
            }

        }

        public static void view_employees(List<Employee> employees)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("EMPLOYEE ID\tNAME\t\tRANK");
            foreach (Employee employee in employees)
            {
                builder.AppendLine($"{employee.Id}\t\t{employee.Name}\t{employee.Rank}");
            }
            Console.WriteLine(builder.ToString());
        }

        public static void add_employee(List<Employee> employees)
        {
        name: Console.Write("Name of Employee: ");
            string name = Console.ReadLine();
            string empName = name.Trim();
            if (empName == "")
            {
                Console.WriteLine("Employee Name cannot be blank");
                goto name;
            }
        rank: Console.Write("Rank of Employee: ");
            string rank = Console.ReadLine();
            string empRank = rank.Trim();
            if (empRank == "")
            {
                Console.WriteLine("Employee Rank cannot be blank");
                goto rank;
            }

            employees.Add(new Employee(empName, empRank));
            Console.WriteLine("Employee added successfully");
        }

        public static void generate_paylips(List<Employee> employees)
        {
            Console.Write("Year: ");
            string year = Console.ReadLine();
            int newYear = Convert.ToInt32(year.Trim());
            Console.Write("Month (1 -> JAN 2 -> FEB ...): ");
            string month = Console.ReadLine();
            int newMonth = Convert.ToInt32(month.Trim());

            PaySlip slip = new PaySlip(newYear, newMonth);
            slip.generatePayslip(employees);
        }

        public static void remove_employee(List<Employee> employees)
        {
            Console.Write("Employee ID: ");
            string empID = Console.ReadLine();
            string ID = empID.Trim();
            bool deleted = false;
            foreach (Employee employee in employees)
            {
                if (employee.Id == ID)
                {
                    employees.Remove(employee);
                    Console.WriteLine("Employee removed successfully");
                    deleted = true;
                    break;
                }
            }
            if (!deleted)
                Console.WriteLine($"Employee with ID {ID} not found");
        }
    }
}
