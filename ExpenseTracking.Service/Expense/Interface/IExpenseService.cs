using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExpenseTracking.Models.Expense;

namespace ExpenseTracking.Service.Expense.Interface
{
    public interface IExpenseService
    {
        bool CreateNewExpense(IndexViewModel model, string user);
        IndexViewModel GetModelByUser(string user);
        IndexViewModel GetModelByUserAndDate(string user, DateTime from, DateTime to);
        ExpenseModel GetExpenseModelEditByUserEmail(string email, int id);
        bool UpdateNewExpense(ExpenseModel model, string user, out bool isHasError, out string errMessage);
        bool DeleteNewExpense(ExpenseModel model, out bool isHasError, out string errMessage);
        IEnumerable<SelectListItem> GetTagModel();
    }
}