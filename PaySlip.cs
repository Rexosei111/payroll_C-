using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Payroll
{
    class PaySlip
    {
        private int _year = 0;
        private int _month = 0;

        private enum monthsOfYear
        {
            January = 1,    // 0
            February = 2,   // 1
            March,    // 6
            April,      // 7
            May,        // 8
            June,       // 9
            July
        }

        Dictionary<string, decimal> allowances = new Dictionary<string, decimal>{
            {"Entertainment", 120m},
            {"Book", 112m},
            {"Car", 800m}
        };

        Dictionary<string, decimal> deductions = new Dictionary<string, decimal>{
            {"SNNIT", 0.03m},
            {"NHIS", 0.043m}
        };

        public PaySlip(int year, int month)
        {
            Year = year;
            Month = month;
        }

        public int Year
        {
            get { return _year; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("year cannot be negetive");
                }
                _year = value;
            }
        }

        public int Month
        {
            get { return _month; }
            set
            {
                if (value < 1 || value > 12)
                {
                    throw new ArgumentException("Month is out of range");
                }
                _month = value;
            }
        }

        public decimal get_sum(Dictionary<string, decimal>.ValueCollection values)
        {
            decimal total_amount = 0;
            foreach (var value in values)
            {
                total_amount += value;
            }

            return total_amount;
        }

        public void generatePayslip(List<Employee> employees)
        {
            decimal total_allowances = get_sum(allowances.Values);
            decimal total_deductions = get_sum(deductions.Values);
            var path = $"Slips/{Year.ToString()}/{(monthsOfYear)Month}";
            Console.WriteLine("Generating slips...");
            Directory.CreateDirectory(@path);

            foreach (Employee employee in employees)
            {
                StringBuilder builder = new StringBuilder();
                decimal vatDeduction = 0;
                if (employee.BasicSalary > 300)
                {
                    vatDeduction = 0.03m * employee.BasicSalary;
                }
                else if (employee.BasicSalary > 100)
                {
                    vatDeduction = 0.075m * employee.BasicSalary;
                }
                builder.AppendLine("\tQFACE GROUP PAYROLL SYSTEM");
                builder.AppendLine("\t\t\tPAY SLIP");
                builder.AppendLine("\t________________________\n");
                builder.AppendLine(string.Format("{0, 25} {1}", (monthsOfYear)Month, Year));
                builder.AppendLine("------------------------------");
                builder.AppendLine(string.Format("{0,-10} {1,10} {2,7}", employee.Id, employee.Name, employee.Rank));
                builder.AppendLine("------------------------------");
                builder.AppendLine(string.Format("{0} {1, 17}", "Basic Salary", employee.BasicSalary.ToString("F")));
                foreach (KeyValuePair<string, decimal> allowance in allowances)
                {
                    if (allowance.Key.Length < 5)
                    {
                        builder.AppendLine(string.Format("{0} {1, 25}", allowance.Key, allowance.Value.ToString("F")));
                    }
                    else
                        builder.AppendLine(string.Format("{0,-10} {1, 16}", allowance.Key, allowance.Value.ToString("F")));
                }
                foreach (KeyValuePair<string, decimal> deduction in deductions)
                {
                    builder.AppendLine(string.Format("{0} {1, 25}", deduction.Key, (employee.BasicSalary * deduction.Value).ToString("F")));
                }
                builder.AppendLine(string.Format("{0} {1, 25}", "VAT", vatDeduction));
                builder.AppendLine("__________________________________");
                builder.AppendLine(string.Format("{0} {1, 23}", "Net Pay", $"₵{(employee.BasicSalary + total_allowances) - total_deductions}"));

                File.WriteAllText($"{path}/{employee.Name}.txt", builder.ToString());

            }

            Console.WriteLine("Done");

        }


    }
}