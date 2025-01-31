using System;

namespace PayrollLib
{
    public struct Employee
    {
        // Βασικά Πεδία
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Children { get; set; }
        public string Department { get; set; } // Δημόσια Έργα, Τραπεζικά Έργα, Δίκτυα
        public string Position { get; set; } // Junior Developer, Mid-level Developer, Senior Developer, IT Manager
        public int WorkExperience { get; set; } // Από 0 έως 38 έτη
        public int Income { get; set; } // Έσοδα από συμμετοχή σε έργα
        public double Bonus { get; set; } // Μπόνους επίδοσης

        // Κατασκευαστής για αρχικοποίηση
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
        // Μέθοδος για έλεγχο τηλεφώνου
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
            return false;
        }

        // Μέθοδος για έλεγχο IBAN
        public bool CheckIBAN(string iban, ref string ibanCountry)
        {
            if (iban.StartsWith("GR"))
            {
                ibanCountry = "Ελλάδα";
                return true;
            }
            else if (iban.StartsWith("CY"))
            {
                ibanCountry = "Κύπρος";
                return true;
            }
            else if (iban.StartsWith("IT"))
            {
                ibanCountry = "Ιταλία";
                return true;
            }
            else if (iban.StartsWith("GB"))
            {
                ibanCountry = "Αγγλία";
                return true;
            }
            ibanCountry = "";
            return false;
        }

        // Μέθοδος για έλεγχο ΤΚ Ιταλίας
        public bool CheckZipCode(int zipCode)
        {
            // Προσάρμοσε την υλοποίηση ανάλογα με τον κατάλογο των ΤΚ της Ιταλίας
            return zipCode >= 1000 && zipCode <= 99999; // Απλοποιημένη υλοποίηση
        }

        // Μέθοδος για υπολογισμό μισθού
        public void CalculateSalary(Employee empl, ref double annualGrossSalary, ref double netAnnualIncome, ref double netMonthIncome, ref double tax, ref double insurance)
        {
            // Απλοποιημένη υλοποίηση με βάση τον βασικό μισθό ανά θέση εργασίας
            double baseSalary = empl.Position switch
            {
                "Junior Developer" => 1200,
                "Mid-level Developer" => 1750,
                "Senior Developer" => 2400,
                "IT Manager" => 4000,
                _ => 0
            };

            // Προσαύξηση μισθού βάσει προϋπηρεσίας
            baseSalary += baseSalary * 0.03 * empl.WorkExperience;

            // Υπολογισμός ετήσιων μικτών αποδοχών
            annualGrossSalary = baseSalary * 12;

            // Υπολογισμός φόρου και ασφαλιστικών εισφορών
            tax = annualGrossSalary * 0.2; // Π.χ., 20% φόρος
            insurance = annualGrossSalary * 0.1; // Π.χ., 10% ασφαλιστικές εισφορές

            // Υπολογισμός καθαρών αποδοχών
            netAnnualIncome = annualGrossSalary - tax - insurance;
            netMonthIncome = netAnnualIncome / 12;
        }

        // Μέθοδος για πλήθος υπαλλήλων σε συγκεκριμένη θέση εργασίας
        public int NumofEmployees(Employee[] empls, string position)
        {
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

        // Μέθοδος για υπολογισμό μπόνους επίδοσης
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
                        Employee emp = empls[i];
                        emp.Bonus = (emp.Income / totalIncome) * bonus;
                        empls[i] = emp;
                    }
                }
                return true;
            }
            return false;
        }
    }
}
