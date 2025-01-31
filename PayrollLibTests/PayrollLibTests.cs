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

        [TestMethod]
        public void CheckPhone_ValidPhone_ReturnsTrue()
        {
            string phoneCountry = "";
            bool result = payrollLib.CheckPhone("+306911234567", ref phoneCountry);

            Assert.IsTrue(result);
            Assert.AreEqual("Ελλάδα", phoneCountry);
        }

        [TestMethod]
        public void CheckPhone_InvalidPhone_ReturnsFalse()
        {
            string phoneCountry = "";
            bool result = payrollLib.CheckPhone("123456", ref phoneCountry);

            Assert.IsFalse(result);
            Assert.AreEqual("", phoneCountry);
        }

        [TestMethod]
        public void CheckIBAN_InvalidIBAN_ReturnsFalse()
        {
            string ibanCountry = "";
            bool result = payrollLib.CheckIBAN("XYZ123456789", ref ibanCountry);

            Assert.IsFalse(result);
            Assert.AreEqual("", ibanCountry);
        }

        [TestMethod]
        public void CheckZipCode_InvalidZipCode_ReturnsFalse()
        {
            bool result = payrollLib.CheckZipCode(999999); // Έξω από το εύρος της Ιταλίας

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CalculateSalary_NegativeExperience_HandlesGracefully()
        {
            var employee = new PayrollLib.Employee("Γιώργος", "Παπαδόπουλος", 1, "Δίκτυα", "Mid-level Developer", -3, 1000);
            double annualGrossSalary = 0, netAnnualIncome = 0, netMonthIncome = 0, tax = 0, insurance = 0;

            payrollLib.CalculateSalary(employee, ref annualGrossSalary, ref netAnnualIncome, ref netMonthIncome, ref tax, ref insurance);

            // Έλεγχος ότι δεν προκύπτουν αρνητικά ποσά
            Assert.IsTrue(annualGrossSalary >= 0);
            Assert.IsTrue(netAnnualIncome >= 0);
            Assert.IsTrue(netMonthIncome >= 0);
            Assert.IsTrue(tax >= 0);
            Assert.IsTrue(insurance >= 0);
        }

        [TestMethod]
        public void CalculateSalary_CorrectCalculations()
        {
            var payrollLib = new PayrollLib.PayrollLib();
            var employee = new PayrollLib.Employee("Γιώργος", "Παπαδόπουλος", 1, "Δίκτυα", "Mid-level Developer", 5, 1000);

            double annualGrossSalary = 0, netAnnualIncome = 0, netMonthIncome = 0, tax = 0, insurance = 0;
            payrollLib.CalculateSalary(employee, ref annualGrossSalary, ref netAnnualIncome, ref netMonthIncome, ref tax, ref insurance);

            // Εκτύπωση αποτελεσμάτων
            Console.WriteLine($"Annual Gross Salary: {annualGrossSalary}");
            Console.WriteLine($"Net Annual Income: {netAnnualIncome}");
            Console.WriteLine($"Net Monthly Income: {netMonthIncome}");
            Console.WriteLine($"Tax: {tax}");
            Console.WriteLine($"Insurance: {insurance}");

            Assert.IsTrue(annualGrossSalary > 0);
            Assert.IsTrue(netAnnualIncome > 0);
            Assert.IsTrue(netMonthIncome > 0);
            Assert.IsTrue(tax > 0);
            Assert.IsTrue(insurance > 0);
        }

    }
}
