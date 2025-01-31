using Microsoft.VisualStudio.TestTools.UnitTesting;
using PayrollLib;
using System;

namespace PayrollLibTests
{
    [TestClass]
    public class PayrollLibTests
    {
        private PayrollLib.PayrollLib payrollLib;

        [TestInitialize]
        public void Setup()
        {
            payrollLib = new PayrollLib.PayrollLib();
        }

        // CheckPhone Tests
        [TestMethod]
        public void CheckPhone_ValidGreekNumber_ReturnsTrue()
        {
            string phoneCountry = "";
            bool result = payrollLib.CheckPhone("+306911234567", ref phoneCountry);
            Console.WriteLine($"CheckPhone: {result}, Country: {phoneCountry}");
            Assert.IsTrue(result);
            Assert.AreEqual("Ελλάδα", phoneCountry);
        }

        [TestMethod]
        public void CheckPhone_ValidCyprusNumber_ReturnsTrue()
        {
            string phoneCountry = "";
            bool result = payrollLib.CheckPhone("+35799123456", ref phoneCountry);
            Console.WriteLine($"CheckPhone: {result}, Country: {phoneCountry}");
            Assert.IsTrue(result);
            Assert.AreEqual("Κύπρος", phoneCountry);
        }

        [TestMethod]
        public void CheckPhone_ValidItalyNumber_ReturnsTrue()
        {
            string phoneCountry = "";
            bool result = payrollLib.CheckPhone("+393491234567", ref phoneCountry);
            Console.WriteLine($"CheckPhone: {result}, Country: {phoneCountry}");
            Assert.IsTrue(result);
            Assert.AreEqual("Ιταλία", phoneCountry);
        }

        [TestMethod]
        public void CheckPhone_ValidUKNumber_ReturnsTrue()
        {
            string phoneCountry = "";
            bool result = payrollLib.CheckPhone("+447912345678", ref phoneCountry);
            Console.WriteLine($"CheckPhone: {result}, Country: {phoneCountry}");
            Assert.IsTrue(result);
            Assert.AreEqual("Αγγλία", phoneCountry);
        }


        [TestMethod]
        public void CheckPhone_InvalidNumber_ReturnsFalse()
        {
            string phoneCountry = "";
            bool result = payrollLib.CheckPhone("00123", ref phoneCountry);
            Console.WriteLine($"CheckPhone: {result}, Country: {phoneCountry}");
            if (result) Assert.Fail("Η συνάρτηση επέστρεψε true για άκυρο αριθμό τηλεφώνου!");
            Assert.AreEqual("", phoneCountry);
        }

        [TestMethod]
        public void CheckPhone_InvalidCountryCode_ReturnsFalse()
        {
            string phoneCountry = "";
            bool result = payrollLib.CheckPhone("+9991234567", ref phoneCountry); // +999 δεν αντιστοιχεί σε καμία από τις 4 χώρες
            Console.WriteLine($"CheckPhone: {result}, Country: {phoneCountry}");
            Assert.IsFalse(result);
            Assert.AreEqual("", phoneCountry);
        }


        // CheckIBAN Tests

        [TestMethod]
        public void CheckIBAN_ValidGreekIBAN_ReturnsTrue()
        {
            string ibanCountry = "";
            bool result = payrollLib.CheckIBAN("GR1601101250000000012300695", ref ibanCountry);
            Console.WriteLine($"CheckIBAN: {result}, Country: {ibanCountry}");
            Assert.IsTrue(result);
            Assert.AreEqual("Ελλάδα", ibanCountry);
        }

        [TestMethod]
        public void CheckIBAN_InvalidIBAN_ReturnsFalse()
        {
            string ibanCountry = "";
            bool result = payrollLib.CheckIBAN("XYZ123456789", ref ibanCountry);
            Console.WriteLine($"CheckIBAN: {result}, Country: {ibanCountry}");
            Assert.IsFalse(result);
            Assert.AreEqual("", ibanCountry);
        }

        [TestMethod]
        public void CheckIBAN_InvalidLength_ReturnsFalse()
        {
            string ibanCountry = "";
            bool result = payrollLib.CheckIBAN("GR12", ref ibanCountry);
            Console.WriteLine($"CheckIBAN: {result}, Country: {ibanCountry}");
            if (result) Assert.Fail("Η συνάρτηση επέστρεψε true για άκυρο IBAN μήκους 4 χαρακτήρων!");
            Assert.AreEqual("", ibanCountry);
        }

        // CheckZipCode Tests

        [TestMethod]
        public void CheckZipCode_ValidItalianZip_ReturnsTrue()
        {
            bool result = payrollLib.CheckZipCode(20121);
            Console.WriteLine($"CheckZipCode: {result}");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CheckZipCode_InvalidZip_ReturnsFalse()
        {
            bool result = payrollLib.CheckZipCode(999999);
            Console.WriteLine($"CheckZipCode: {result}");
            Assert.IsFalse(result);
        }


        [TestMethod]
        public void CheckZipCode_OutOfRange_ReturnsFalse()
        {
            bool result = payrollLib.CheckZipCode(50);
            Console.WriteLine($"CheckZipCode: {result}");
            if (result) Assert.Fail("Η συνάρτηση επέστρεψε true για άκυρο ΤΚ!");
        }

        // CalculateSalary Tests
        [TestMethod]
        public void CalculateSalary_InvalidExperience_ThrowsArgumentException()
        {
            var employee = new Employee("John", "Doe", 1, "Δίκτυα", "Junior Developer", -2, 1000);
            double annualGrossSalary = 0, netAnnualIncome = 0, netMonthIncome = 0, tax = 0, insurance = 0;

            Console.WriteLine("Testing CalculateSalary with negative experience...");
            Assert.ThrowsException<ArgumentException>(() =>
                payrollLib.CalculateSalary(employee, ref annualGrossSalary, ref netAnnualIncome, ref netMonthIncome, ref tax, ref insurance)
            );
        }


        // NumofEmployees Tests

        [TestMethod]
        public void NumofEmployees_CorrectCount_ReturnsTwo()
        {
            Employee[] employees =
            {
        new Employee("John", "Doe", 1, "Δίκτυα", "Mid-level Developer", 5, 1000),
        new Employee("Jane", "Smith", 2, "Δίκτυα", "Mid-level Developer", 3, 1200),
        new Employee("Jack", "Brown", 0, "Δίκτυα", "Senior Developer", 10, 2000)
    };
            int count = payrollLib.NumofEmployees(employees, "Mid-level Developer");
            Console.WriteLine($"NumofEmployees Count: {count}");
            Assert.AreEqual(2, count);
        }

        [TestMethod]
        public void NumofEmployees_NullList_ThrowsArgumentNullException()
        {
            Console.WriteLine("Testing NumofEmployees with null list...");
            Assert.ThrowsException<ArgumentNullException>(() => payrollLib.NumofEmployees(null, "Mid-level Developer"));
        }

        // CalculateSalary Tests
        [TestMethod]
        public void CalculateSalary_ValidData_CalculatesCorrectly()
        {
            var employee = new Employee("John", "Doe", 1, "Δίκτυα", "Mid-level Developer", 5, 1000);
            double annualGrossSalary = 0, netAnnualIncome = 0, netMonthIncome = 0, tax = 0, insurance = 0;
            payrollLib.CalculateSalary(employee, ref annualGrossSalary, ref netAnnualIncome, ref netMonthIncome, ref tax, ref insurance);
            Console.WriteLine($"Salary Calculation - Gross: {annualGrossSalary}, Net: {netAnnualIncome}, Tax: {tax}, Insurance: {insurance}");
            Assert.IsTrue(annualGrossSalary > 0);
        }


        // GetBonus Tests
        [TestMethod]
        public void GetBonus_DepartmentMeetsGoal_ReturnsTrue()
        {
            Employee[] employees =
            {
        new Employee("John", "Doe", 1, "Δίκτυα", "Mid-level Developer", 5, 30000),
        new Employee("Jane", "Smith", 2, "Δίκτυα", "Mid-level Developer", 3, 31000)
    };
            bool result = payrollLib.GetBonus(ref employees, "Δίκτυα", 50000, 5000);
            Console.WriteLine($"GetBonus: {result}, Employee 1 Bonus: {employees[0].Bonus}, Employee 2 Bonus: {employees[1].Bonus}");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetBonus_DepartmentFailsGoal_ReturnsFalse()
        {
            Employee[] employees =
            {
        new Employee("John", "Doe", 1, "Δίκτυα", "Mid-level Developer", 5, 20000),
        new Employee("Jane", "Smith", 2, "Δίκτυα", "Mid-level Developer", 3, 15000)
    };
            bool result = payrollLib.GetBonus(ref employees, "Δίκτυα", 50000, 5000);
            Console.WriteLine($"GetBonus: {result}");
            Assert.IsFalse(result);
        }
    }
}
