using System;
using System.Collections.Generic;
using System.Linq;
using FinanceBuddyWPF.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FinanceBuddyWPFTests.Controllers {
    [TestClass()]
    public class DatabaseActionsTests {
        private readonly DatabaseActions dbActions = new DatabaseActions();
        private readonly DatabaseCleaning dbCleaning = new DatabaseCleaning();
        private readonly DataUtilites dbUtil = new DataUtilites();

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
            var lastName = "Testing";
            var firstName = "Unit";
            var userName = "UnitTesting";
            var password = "mypassword";
            
            var actual = dbActions.CreateUser(lastName, firstName, userName, password);
            dbCleaning.DeleteUserTests(userName);
            Assert.AreEqual(actual, true);
        }

        [TestMethod()]
        public void CreateIncomeInvalidUserTest()
        {
            float amount = 1000;
            var date = DateTime.Now.ToString("yyyy-MM-dd");
            var username = "wrongUser";
            var description = "UnitTesting";
            var actual =  dbActions.CreateIncome(amount, date, username, description);

            Assert.AreNotEqual(actual, true);
        }

        [TestMethod()]
        public void CreateIncomeUserTest() {
            var lastName = "Testing";
            var firstName = "Unit";
            var userName = "UnitTesting";
            var password = "mypassword";
            dbActions.CreateUser(lastName, firstName, userName, password);
            float amount = 1000;
            var date = DateTime.Now.ToString("yyyy-MM-dd");
            var description = "UnitTesting";
            var actual = dbActions.CreateIncome(amount, date, userName, description);
            dbCleaning.DeleteIncomeTests();
            dbCleaning.DeleteUserTests(userName);
            Assert.AreEqual(actual, true);
        }

        [TestMethod()]
        public void UserLoginTest()
        {
            var username = "sama";
            var password = "lamepassword";
            var hashedPass = dbUtil.HashPassword(password);
            var actual = dbActions.UserLogin(username, hashedPass);
            Assert.AreEqual(actual, true);
        }

        [TestMethod()]
        public void InvalidUserLoginTest() {
            var username = "wrongUser";
            var password = "wrongPassword";
            var actual = dbActions.UserLogin(username, password);
            Assert.AreNotEqual(actual, true);
        }


        [TestMethod()]
        public void CreateExpenseTest()
        {
            var category = "Lån & Regninger";
            var description = "Unit Testing";
            var date = DateTime.Now.ToString("yyyy-MM-dd");
            var lastName = "Testing";
            var firstName = "Unit";
            var userName = "UnitTesting";
            var password = "mypassword";
            dbActions.CreateUser(lastName, firstName, userName, password);

            float amount = 1000;
            var actual = dbActions.CreateExpense(category, description, date, userName, amount);
            dbCleaning.DeleteExpensesTests();
            dbCleaning.DeleteUserTests(userName);
            Assert.AreEqual(actual, true);
        }

        [TestMethod()]
        public void CreateExpenseWithIncorrectCategoryTest() {
            var category = "Test category";
            var description = "Unit Testing";
            var date = DateTime.Now.ToString("yyyy-MM-dd");
            var userName = "sama";
            float amount = 1000;
            var actual = dbActions.CreateExpense(category, description, date, userName, amount);
            Assert.AreNotEqual(actual, true);
        }

        [TestMethod()]
        public void CreateBudgetTest()
        {
            var lastName = "Testing";
            var firstName = "Unit";
            var userName = "UnitTesting";
            var password = "mypassword";
            dbActions.CreateUser(lastName, firstName, userName, password);

            var username = "UnitTesting";
            float loanLimit = 3000;
            float groceryLimit = 3000;
            float houseHoldLimit= 3000;
            float consumptionLimit = 3000;
            float transportLimit = 3000;
            float savingsLimit= 3000;
            var actual = dbActions.CreateBudget(username, loanLimit, groceryLimit, houseHoldLimit, consumptionLimit, transportLimit, savingsLimit);
            dbCleaning.DeleteBudgetTests(username);
            dbCleaning.DeleteUserTests(username);
            Assert.AreEqual(actual, true);
        }

        [TestMethod()]
        public void GetBudgetLimitsTest() {
            var lastName = "Testing";
            var firstName = "Unit";
            var userName = "UnitTesting";
            var password = "mypassword";
            dbActions.CreateUser(lastName, firstName, userName, password);

           
            float loanLimit = 3000;
            float groceryLimit = 3000;
            float houseHoldLimit = 3000;
            float consumptionLimit = 3000;
            float transportLimit = 3000;
            float savingsLimit = 3000;
            dbActions.CreateBudget(userName, loanLimit, groceryLimit, houseHoldLimit, consumptionLimit, transportLimit, savingsLimit);
            float totalLimit = 18000;
            List<float> actual = dbActions.GetBudgetLimits(userName);
            var actualSum = actual.Sum();
            dbCleaning.DeleteBudgetTests(userName);
            dbCleaning.DeleteUserTests(userName);
            Assert.AreEqual(actualSum, totalLimit);
           
        }

        [TestMethod()]
        public void UpdateBudgetTest() {
            var lastName = "Testing";
            var firstName = "Unit";
            var userName = "UnitTesting";
            var password = "mypassword";
            dbActions.CreateUser(lastName, firstName, userName, password);

            var username = "UnitTesting";
            float loanLimit = 3000;
            float groceryLimit = 3000;
            float houseHoldLimit = 3000;
            float consumptionLimit = 3000;
            float transportLimit = 3000;
            float savingsLimit = 3000;
            dbActions.CreateBudget(username, loanLimit, groceryLimit, houseHoldLimit, consumptionLimit, transportLimit, savingsLimit);

            var actual = dbActions.UpdateBudget(username, 2000, 2000, 2000, 2000, 2000, 2000);

            dbCleaning.DeleteBudgetTests(username);
            dbCleaning.DeleteUserTests(username);
            Assert.AreEqual(actual, true);

        }


        [TestMethod()]
        public void GetIncomeTest() {
            var lastName = "Testing2";
            var firstName = "Unit2";
            var userName = "UnitTesting";
            var password = "mypassword";
            dbActions.CreateUser(lastName, firstName, userName, password);

            float amount = 1000;
            var date = "2019-04-02";
            var description = "UnitTesting";
            dbActions.CreateIncome(amount, date, userName, description);
            float expected = 1000;

            var dayfrom = "2019-04-01";
            var dayto = "2019-04-03";
            var actual = dbActions.GetIncome(userName, dayfrom, dayto);
            dbCleaning.DeleteIncomeTests();
            dbCleaning.DeleteUserTests(userName);
            Assert.AreEqual(actual, expected);
        }

      

        [TestMethod()]
        public void GetAvgExpensesTest() {
            var lastName = "Testing2";
            var firstName = "Unit2";
            var userName = "UnitTesting";
            var password = "mypassword";
            dbActions.CreateUser(lastName, firstName, userName, password);

            var category = "Lån & Regninger";
            var description = "Unit Testing";
            var date = "2019-04-02";
            float amount = 1000;
            dbActions.CreateExpense(category, description, date, userName, amount);

            dbActions.CreateExpense(category, description, date, userName, amount);
            var dayfrom = "2019-04-01";
            var dayto = "2019-04-03";
            float expenseAVG = amount + amount;
            float actualExpense = dbActions.GetAvgExpenses(userName, category, dayfrom, dayto);

            dbCleaning.DeleteExpensesTests();
            dbCleaning.DeleteUserTests(userName);
            Assert.AreEqual(actualExpense, expenseAVG);

        }

        [TestMethod()]
        public void GetExpensesTest() {
            var lastName = "Testing2";
            var firstName = "Unit2";
            var userName = "UnitTesting";
            var password = "mypassword";
            dbActions.CreateUser(lastName, firstName, userName, password);

            var category = "Lån & Regninger";
            var description = "Unit Testing";
            var date = "2019-04-02";
            float amount = 1000;
            dbActions.CreateExpense(category, description, date, userName, amount);
            

            float expected = 1000;
            var dayfrom = "2019-04-01";
            var dayto = "2019-04-03";
            List<KeyValuePair<string, float>> actualExpense = dbActions.GetExpenses(userName, dayfrom, dayto);
            var myResults = actualExpense.GroupBy(p => p.Key).ToDictionary(g => g.Key, g => g.Sum(p => p.Value));
            myResults.TryGetValue(category, out float actual);

            dbCleaning.DeleteExpensesTests();
            dbCleaning.DeleteUserTests(userName);
            Assert.AreEqual(expected, actual);
        }
    }
}