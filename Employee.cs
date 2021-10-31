using System;
using System.Collections.Generic;

namespace Payroll
{
    class Employee
    {
        public string _name;

        private string _rank;
        public string Id { get; }

        public decimal BasicSalary { get; private set; }

        private static Dictionary<string, decimal> salaryDict = new Dictionary<string, decimal>() {
            {"R1", 2100},
            {"R2", 790},
            {"R3", 1680},
            {"R4", 810}
            };
        private static int employeeID = 1001;
        public Employee(string name, string rank)
        {
            this.Id = employeeID.ToString();
            employeeID++;
            Name = name;
            Rank = rank;
            this.BasicSalary = salaryDict[rank];
        }

        public string Rank
        {
            get { return _rank; }
            set
            {
                if (salaryDict.ContainsKey(value))
                {
                    _rank = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(Rank), "Rank not found");
                }
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentException("Invalid Employee name!");
                }
                _name = value;
            }
        }
    };
}