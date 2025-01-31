using Microsoft.VisualStudio.TestTools.UnitTesting;
using PayrollLib;

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
            Assert.IsTrue(result);
            Assert.AreEqual("Ελλάδα", phoneCountry);
        }

        [TestMethod]
        public void CheckPhone_InvalidNumber_ReturnsFalse()
        {
            string phoneCountry = "";
            bool result = payrollLib.CheckPhone("00123", ref phoneCountry);
            Assert.IsFalse(result);
            Assert.AreEqual("", phoneCountry);
        }

        // CheckIBAN Tests
        [TestMethod]
        public void CheckIBAN_ValidGreekIBAN_ReturnsTrue()
        {
            string ibanCountry = "";
            bool result = payrollLib.CheckIBAN("GR1601101250000000012300695", ref ibanCountry);
            Assert.IsTrue(result);
            Assert.AreEqual("Ελλάδα", ibanCountry);
        }

        [TestMethod]
        public void CheckIBAN_InvalidIBAN_ReturnsFalse()
        {
            string ibanCountry = "";
            bool result = payrollLib.CheckIBAN("XYZ123456789", ref ibanCountry);
            Assert.IsFalse(result);
            Assert.AreEqual("", ibanCountry);
        }

        // CheckZipCode Tests
        [TestMethod]
        public void CheckZipCode_ValidItalianZip_ReturnsTrue()
        {
            bool result = payrollLib.CheckZipCode(20121);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CheckZipCode_InvalidZip_ReturnsFalse()
        {
            bool result = payrollLib.CheckZipCode(999999);
            Assert.IsFalse(result);
        }

        // CalculateSalary Tests
        [TestMethod]
        public void CalculateSalary_ValidData_CalculatesCorrectly()
        {
            var employee = new Employee("John", "Doe", 1, "Δίκτυα", "Junior Developer", 5, 1000);
            double annualGrossSalary = 0, netAnnualIncome = 0, netMonthIncome = 0, tax = 0, insurance = 0;
            payrollLib.CalculateSalary(employee, ref annualGrossSalary, ref netAnnualIncome, ref netMonthIncome, ref tax, ref insurance);
            Assert.IsTrue(annualGrossSalary > 0);
            Assert.IsTrue(netAnnualIncome > 0);
            Assert.IsTrue(netMonthIncome > 0);
            Assert.IsTrue(tax > 0);
            Assert.IsTrue(insurance > 0);
        }

        [TestMethod]
        public void CalculateSalary_InvalidExperience_ReturnsFalse()
        {
            var employee = new Employee("John", "Doe", 1, "Δίκτυα", "Junior Developer", -2, 1000);
            double annualGrossSalary = 0, netAnnualIncome = 0, netMonthIncome = 0, tax = 0, insurance = 0;
            payrollLib.CalculateSalary(employee, ref annualGrossSalary, ref netAnnualIncome, ref netMonthIncome, ref tax, ref insurance);
            Assert.IsTrue(annualGrossSalary >= 0);
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
            Assert.AreEqual(2, count);
        }

        [TestMethod]
        public void NumofEmployees_EmptyList_ReturnsZero()
        {
            Employee[] employees = { };
            int count = payrollLib.NumofEmployees(employees, "Mid-level Developer");
            Assert.AreEqual(0, count);
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
            Assert.IsFalse(result);
        }
    }
}
