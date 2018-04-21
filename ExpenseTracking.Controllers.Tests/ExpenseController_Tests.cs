using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using ExpenseTracking.Models.Expense;
using ExpenseTracking.Service.Expense.Interface;
using NUnit.Framework;
using Rhino.Mocks;

namespace ExpenseTracking.Controllers.Tests
{
    public class ExpenseController_Tests
    {
        private IExpenseService _expenseService;
        private IPrincipal _user;

        [SetUp]
        public void Init()
        {
            _expenseService = MockRepository.GenerateMock<IExpenseService>();
            _user = MockRepository.GenerateMock<IPrincipal>();
        }

        [Test]
        public void Index_Should_Return_Index_View()
        {
            //arrange
            var expenseController = GetExpenseController();

            //act
            var viewResult = expenseController.Index() as ViewResult;

            //assert
            Assert.AreEqual("Index", viewResult.ViewName);
        }

        [Test]
        public void CreateNewExpense_Should_Return_Index_View_When_Valid()
        {
            //arrange
            var expenseController = GetExpenseController();

            var model = new IndexViewModel();
            _expenseService.Expect(e => e.CreateNewExpense(model, "")).Return(true);

            //act
            var viewResult = expenseController.CreateNewExpense(model) as RedirectToRouteResult;

            //assert
            Assert.AreEqual("Index", viewResult.RouteValues["action"]);
        }

        [Test]
        public void CreateNewExpense_Should_Return_CreateNewExpense_View_When_Not_Valid()
        {
            //arrange
            var expenseController = GetExpenseController();

            var model = new IndexViewModel();
            _expenseService.Expect(e => e.CreateNewExpense(model, "")).Return(false);
            _expenseService.Expect(e => e.GetModelByUser("")).Return(new IndexViewModel());
            //act
            var viewResult = expenseController.CreateNewExpense(model) as ViewResult;

            //assert
            Assert.AreEqual("CreateNewExpense", viewResult.ViewName);
        }

        [Test]
        public void FilterByDate_Should_Return_FilterByDate_View_When_Valid()
        {
            //arrange
            var expenseController = GetExpenseController();

            var model = new IndexViewModel(){ExpenseFilter = new ExpenseFilter(){FromDate = DateTime.Now, ToDate = DateTime.Now.AddDays(-3)}};
            _expenseService.Expect(e => e.GetModelByUserAndDate("user", model.ExpenseFilter.FromDate, model.ExpenseFilter.ToDate)).Return(model);

            //act
            var viewResult = expenseController.FilterByDate(model) as ViewResult;

            //assert
            Assert.AreEqual("FilterByDate", viewResult.ViewName);
        }

        private ExpenseController GetExpenseController()
        {
            return new ExpenseController(_expenseService);
        }
    }
}