using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using ExpenseTracking.Controllers.Shared;
using ExpenseTracking.Models.Expense;
using ExpenseTracking.Service.Expense.Interface;

namespace ExpenseTracking.Controllers
{
    public class ExpenseController : Controller
    {
        private IExpenseService _expenseService;

        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        public ActionResult Index()
        {
            var model = _expenseService.GetModelByUser(this.GetUserName());
            return View("Index", model);
        }

        [HttpPost]
        public ActionResult CreateNewExpense(IndexViewModel model)
        {
            if (_expenseService.CreateNewExpense(model, this.GetUserName()))
            {              
                return RedirectToAction("Index", "Expense");
            }
            else
            {
                ModelState.AddModelError("", model.Message);
            }

            var newModel = _expenseService.GetModelByUser(this.GetUserName());
            newModel.IsHasError = model.IsHasError;
            newModel.Message = model.Message;

            return View("CreateNewExpense", newModel);
        }

        [HttpPost]
        public ActionResult FilterByDate(IndexViewModel model)
        {
            var newModel = _expenseService.GetModelByUserAndDate(this.GetUserName(), model.ExpenseFilter.FromDate, model.ExpenseFilter.ToDate);
            return View("FilterByDate", newModel);
        }

        public ActionResult ExpensesChart()
        {
            var model = _expenseService.GetModelByUser(this.GetUserName());

            var groupByTagList = from m in model.Expenses
                                 group m by m.Tag
                                 into groupTag
                                 select new {groupTag.Key, SumAmount = groupTag.Sum(e => e.Amount)};


            var labelList = groupByTagList.Select(e => e.Key).ToArray();
            var valueList = groupByTagList.Select(e => e.SumAmount).ToArray();

            var bytes = new Chart(width: 400, height: 200, theme:ChartTheme.Blue)
                .AddSeries(
                    chartType: "bar",
                    xValue: labelList,
                    yValues: valueList)
                .GetBytes("png");
            return File(bytes, "image/png");
        }

        [HttpPost]
        public JsonResult GetExpensesTable(string tag)
        {
            var model = _expenseService.GetModelByUser(this.GetUserName());
            var expenses = model.Expenses.Where(e => e.Tag == tag);

            try
            {
                var strTable = "<fieldset><legend>Expenses Table</legend><div class='scroll'><table><tr><th>Date</th><th>Description</th><th>Amount</th><th>Tag</th><th>Action</th></tr>";

                foreach (var item in expenses)
                {
                    strTable += string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td></tr>", 
                        item.ExpenseDate.ToShortDateString(),
                        item.Description, 
                        item.Amount,
                        item.Tag,
                        string.Format("<a href=\"/Expense/Edit/{0}\">Edit</a> | <a href=\"/Expense/Details/{1}\">Details</a> | <a href=\"/Expense/Delete/{2}\">Delete</a>", item.ID, item.ID, item.ID));
                }

                strTable += "</table></div></fieldset>";
                return Json(strTable);
            }
            catch
            {}

            return Json(string.Empty);
        }

        public ActionResult Details(int id)
        {
            var model = new IndexViewModel();
            model.ExpenseModelEntry = _expenseService.GetExpenseModelEditByUserEmail(this.GetUserName(), id);
            model.TagList = _expenseService.GetTagModel();
            return View("Details", model);
        }

        public ActionResult Edit(int id)
        {
            var model = new IndexViewModel();
            model.ExpenseModelEntry = _expenseService.GetExpenseModelEditByUserEmail(this.GetUserName(), id);
            model.TagList = _expenseService.GetTagModel();
            return View("Edit", model);
        }

        [HttpPost]
        public ActionResult Edit(IndexViewModel model)
        {
            var isHasError = false;
            var errMessage = string.Empty;

            if (_expenseService.UpdateNewExpense(model.ExpenseModelEntry, this.GetUserName(), out isHasError, out errMessage))
            {
                return RedirectToAction("Index", "Expense");
            }
            else
            {
                ModelState.AddModelError("", errMessage);
            }

            var inputmodel = new IndexViewModel();
            inputmodel.ExpenseModelEntry = _expenseService.GetExpenseModelEditByUserEmail(this.GetUserName(), model.ExpenseModelEntry.ID);
            inputmodel.TagList = _expenseService.GetTagModel();
            // If we got this far, something failed, redisplay form
            return View("Edit", inputmodel);
        }

        public ActionResult Delete(int id)
        {
            var model = new IndexViewModel();
            model.ExpenseModelEntry = _expenseService.GetExpenseModelEditByUserEmail(this.GetUserName(), id);
            model.TagList = _expenseService.GetTagModel();
            return View("Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(IndexViewModel model)
        {
            var isHasError = false;
            var errMessage = string.Empty;

            if (_expenseService.DeleteNewExpense(model.ExpenseModelEntry, out isHasError, out errMessage))
            {
                return RedirectToAction("Index", "Expense");
            }
            else
            {
                ModelState.AddModelError("", errMessage);
            }

            var inputmodel = new IndexViewModel();
            inputmodel.ExpenseModelEntry = _expenseService.GetExpenseModelEditByUserEmail(this.GetUserName(), model.ExpenseModelEntry.ID);
            inputmodel.TagList = _expenseService.GetTagModel();
            // If we got this far, something failed, redisplay form
            return View("Delete", inputmodel);
        }
    }
}
