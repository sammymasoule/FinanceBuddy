using FinanceBuddyWPF.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FinanceBuddyWPFTests.Controllers {
    [TestClass()]
    public class DatabaseActionsTests {
        private readonly DatabaseActions dbActions = new DatabaseActions();

        [TestMethod()]
        public void CreateExistingUserTest()
        {
            var lastName = "Jespersen";
            var firstName = "Flemming";
            var userName = "sama";
            var password = "mypassword";
            var actual = dbActions.CreateUser(lastName, firstName, userName, password);
            Assert.AreEqual(actual, false);
        }

        [TestMethod()]
        public void CreateUserTest()
        {
            var i = 0;
            var lastName = "Jespersen";
            var firstName = "Flemming";
            var userName = "sama" + i;
            var password = "mypassword";
            var actual = dbActions.CreateUser(lastName, firstName, userName, password);
            Assert.AreEqual(actual, false);
        }

        [TestMethod()]
        public void CreateIncomeInvalidUserTest()
        {
            float amount = 1000;
            var date = "2019-05-01";
            var username = "wrongUser";
            var description = "Fødselsdagsgave";
            var actual =  dbActions.CreateIncome(amount, date, username, description);
            Assert.AreEqual(actual, false);
        }

        [TestMethod()]
        public void CreateIncomeUserTest() {
            float amount = 1000;
            var date = "2019-05-01";
            var username = "sama";
            var description = "Fødselsdagsgave";
            var actual = dbActions.CreateIncome(amount, date, username, description);
            Assert.AreEqual(actual, true);
        }

        [TestMethod()]
        public void UserLoginTest() {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetIncomeTest() {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetAvgExpensesOthersTest() {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetAvgExpensesTest() {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetExpensesTest() {
            Assert.Fail();
        }

        [TestMethod()]
        public void CreateExpenseTest() {
            Assert.Fail();
        }

        [TestMethod()]
        public void CreateBudgetTest() {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetBudgetLimitsTest() {
            Assert.Fail();
        }

        [TestMethod()]
        public void UpdateBudgetTest() {
            Assert.Fail();
        }
    }
}