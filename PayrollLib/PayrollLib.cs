using System;
using System.Collections.Generic;
using System.IO;

namespace PayrollLib
{
    public struct Employee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Children { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public int WorkExperience { get; set; }
        public int Income { get; set; }
        public double Bonus { get; set; }

        public Employee(string firstName, string lastName, int children, string department, string position, int workExperience, int income, double bonus = 0)
        {
            FirstName = firstName;
            LastName = lastName;
            Children = children;
            Department = department;
            Position = position;
            WorkExperience = workExperience;
            Income = income;
            Bonus = bonus;
        }
    }

    public class PayrollLib
    {
        private List<string> errorLog = new List<string>();

        private void LogError(string message)
        {
            errorLog.Add(message);
            File.AppendAllText("error_log.txt", message + "\n");
        }

        public bool CheckPhone(string phone, ref string phoneCountry)
        {
            if (phone.StartsWith("+30") || phone.StartsWith("0030"))
            {
                phoneCountry = "Ελλάδα";
                return true;
            }
            else if (phone.StartsWith("+357") || phone.StartsWith("00357"))
            {
                phoneCountry = "Κύπρος";
                return true;
            }
            else if (phone.StartsWith("+39") || phone.StartsWith("0039"))
            {
                phoneCountry = "Ιταλία";
                return true;
            }
            else if (phone.StartsWith("+44") || phone.StartsWith("0044"))
            {
                phoneCountry = "Αγγλία";
                return true;
            }
            phoneCountry = "";
            LogError("Invalid phone number: " + phone);
            return false;
        }

        public bool CheckIBAN(string iban, ref string ibanCountry)
        {
            if (iban.Length < 15 || iban.Length > 34)
            {
                LogError("Invalid IBAN length: " + iban);
                return false;
            }

            if (iban.StartsWith("GR")) ibanCountry = "Ελλάδα";
            else if (iban.StartsWith("CY")) ibanCountry = "Κύπρος";
            else if (iban.StartsWith("IT")) ibanCountry = "Ιταλία";
            else if (iban.StartsWith("GB")) ibanCountry = "Αγγλία";
            else
            {
                LogError("Unknown IBAN country: " + iban);
                ibanCountry = "";
                return false;
            }
            return true;
        }

        public bool CheckZipCode(int zipCode)
        {
            if (zipCode < 1000 || zipCode > 99999)
            {
                LogError("Invalid Italian ZIP Code: " + zipCode);
                return false;
            }
            return true;
        }

        public void CalculateSalary(Employee empl, ref double annualGrossSalary, ref double netAnnualIncome, ref double netMonthIncome, ref double tax, ref double insurance)
        {
            if (empl.WorkExperience < 0)
            {
                throw new ArgumentException("Η προϋπηρεσία δεν μπορεί να είναι αρνητική.", nameof(empl.WorkExperience));
            }

            double baseSalary = empl.Position switch
            {
                "Junior Developer" => 1200,
                "Mid-level Developer" => 1750,
                "Senior Developer" => 2400,
                "IT Manager" => 4000,
                _ => 0
            };

            baseSalary += baseSalary * 0.03 * empl.WorkExperience;

            annualGrossSalary = baseSalary * 12;
            insurance = annualGrossSalary * 0.13867;
            double taxableIncome = annualGrossSalary - insurance;

            if (taxableIncome <= 10000)
                tax = taxableIncome * 0.09;
            else if (taxableIncome <= 20000)
                tax = 900 + (taxableIncome - 10000) * 0.22;
            else if (taxableIncome <= 30000)
                tax = 3100 + (taxableIncome - 20000) * 0.28;
            else if (taxableIncome <= 40000)
                tax = 5900 + (taxableIncome - 30000) * 0.36;
            else
                tax = 9500 + (taxableIncome - 40000) * 0.44;

            netAnnualIncome = taxableIncome - tax;
            netMonthIncome = netAnnualIncome / 12;
        }


        public int NumofEmployees(Employee[] empls, string position)
        {
            if (empls == null)
                throw new ArgumentNullException(nameof(empls), "Η λίστα υπαλλήλων δεν μπορεί να είναι null");

            int count = 0;
            foreach (var emp in empls)
            {
                if (emp.Position == position)
                {
                    count++;
                }
            }
            return count;
        }


        public bool GetBonus(ref Employee[] empls, string department, double incomeGoal, double bonus)
        {
            double totalIncome = 0;
            for (int i = 0; i < empls.Length; i++)
            {
                if (empls[i].Department == department)
                {
                    totalIncome += empls[i].Income;
                }
            }

            if (totalIncome >= incomeGoal)
            {
                for (int i = 0; i < empls.Length; i++)
                {
                    if (empls[i].Department == department)
                    {
                        empls[i].Bonus = (empls[i].Income / totalIncome) * bonus;
                    }
                }
                return true;
            }

            LogError("Department " + department + " did not reach income goal.");
            return false;
        }

    }
}
