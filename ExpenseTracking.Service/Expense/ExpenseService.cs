using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ExpenseTracking.Models;
using ExpenseTracking.Models.Expense;
using ExpenseTracking.Models.Tag;
using ExpenseTracking.Service.Expense.Interface;

namespace ExpenseTracking.Service.Expense
{
    public class ExpenseService : IExpenseService
    {
        private FooEntities _entity;

        public ExpenseService(FooEntities entity)
        {
            _entity = entity;
        }

        public bool CreateNewExpense(IndexViewModel model, string user)
        {
            _entity.Connection.Open();
            var transaction = _entity.Connection.BeginTransaction();
            var currentUser = Membership.GetUser(user, true);

            try
            {
                var expense = new Expense_tbl();
                expense.ExpenseDate = model.ExpenseModelEntry.ExpenseDate;
                expense.Amount = model.ExpenseModelEntry.Amount;
                expense.Description = model.ExpenseModelEntry.Description;
                expense.TagID = int.Parse(model.ExpenseModelEntry.Tag);
                if (currentUser != null) expense.UserID = currentUser.Email;

                _entity.AddToExpense_tbl(expense);
                _entity.SaveChanges();
                transaction.Commit();
                _entity.Connection.Close();
                model.IsHasError = false;
                model.Message = "Succesfully saved new expense";
                return true;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                _entity.Connection.Close();
                model.IsHasError = true;
                model.Message = e.Message;
                return false;
            }
        }

        public bool UpdateNewExpense(ExpenseModel model, string user, out bool isHasError, out string errMessage)
        {
            _entity.Connection.Open();
            var transaction = _entity.Connection.BeginTransaction();
            var currentUser = Membership.GetUser(user, true);

            try
            {
                var expense = _entity.Expense_tbl.Where(e=> e.ID == model.ID).First();
                expense.ExpenseDate = model.ExpenseDate;
                expense.Amount = model.Amount;
                expense.Description = model.Description;
                expense.TagID = int.Parse(model.Tag);
                if (currentUser != null) expense.UserID = currentUser.Email;

                _entity.SaveChanges();
                transaction.Commit();
                _entity.Connection.Close();
                isHasError = false;
                errMessage = "Succesfully saved new expense";
                return true;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                _entity.Connection.Close();
                isHasError = true;
                errMessage = e.Message;
                return false;
            }
        }

        public bool DeleteNewExpense(ExpenseModel model, out bool isHasError, out string errMessage)
        {
            _entity.Connection.Open();
            var transaction = _entity.Connection.BeginTransaction();

            try
            {
                var expense = _entity.Expense_tbl.Where(e => e.ID == model.ID).First();

                _entity.DeleteObject(expense);
                _entity.SaveChanges();
                transaction.Commit();
                _entity.Connection.Close();
                isHasError = false;
                errMessage = "Succesfully saved new expense";
                return true;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                _entity.Connection.Close();
                isHasError = true;
                errMessage = e.Message;
                return false;
            }
        }

        public IndexViewModel GetModelByUserAndDate(string user, DateTime from, DateTime to)
        {
            MembershipUser currentUser;
            var model = GetModel(user, out currentUser);
            var dateInfo = new System.Globalization.DateTimeFormatInfo();
            var selectedMonth = dateInfo.GetMonthName(from.Month);
            model.HiChartTitle = "Expenses Chart As Of " + selectedMonth;

            if (currentUser != null)
            {
                model.Expenses = GetExpenseModelByUserEmailAndDate(currentUser.Email, from, to);
                model.ExpenseChartData = GetExpensesChart(model.Expenses);
                model.ExpenseTagData = GetExpensesTag(model.Expenses);
            }
            model.TagList = GetTagModel();
            model.MonthsList = GetMonthsList();
            model.ExpenseFilter.FromDate = from;
            model.ExpenseFilter.ToDate = to;

            return model;
        }

        public IndexViewModel GetModelByUser(string user)
        {
            MembershipUser currentUser;
            var model = GetModel(user, out currentUser);
            model.HiChartTitle = "Expenses Chart";

            if (currentUser != null)
            {
                model.Expenses = GetExpenseModelByUserEmail(currentUser.Email);
                model.ExpenseChartData = GetExpensesChart(model.Expenses);
                model.ExpenseTagData = GetExpensesTag(model.Expenses);
            }
            model.TagList = GetTagModel();
            model.MonthsList = GetMonthsList();
            model.ExpenseFilter.FromDate = model.Expenses.Count <= 0? DateTime.Now: model.Expenses.Min(e => e.ExpenseDate);
            model.ExpenseFilter.ToDate = model.Expenses.Count <= 0 ? DateTime.Now : model.Expenses.Max(e => e.ExpenseDate);

            return model;
        }

        private static IndexViewModel GetModel(string user, out MembershipUser currentUser)
        {
            currentUser = Membership.GetUser(user, true);
            var model = new IndexViewModel();
            model.Expenses = new List<ExpenseModel>();
            model.TagList = new List<SelectListItem>();
            model.ExpenseModelEntry = new ExpenseModel(){ExpenseDate = DateTime.Now};
            model.MonthsList = new List<SelectListItem>();
            model.ExpenseChartData = new HtmlString(string.Empty);
            model.ExpenseFilter = new ExpenseFilter();
            return model;
        }

        private List<ExpenseModel> GetExpenseModelByUserEmail(string email)
        {
            var expenseList = _entity.Expense_tbl.Where(e => e.UserID == email);

            return expenseList.Select((items) =>
                new ExpenseModel
                    {
                        ID = items.ID,
                        ExpenseDate = items.ExpenseDate ?? DateTime.Now,
                        Description = items.Description,
                        Amount = items.Amount ?? 0,
                        Tag = (items.Tag_tbl != null ? items.Tag_tbl.Description : string.Empty)
                    }).ToList();
        }

        public ExpenseModel GetExpenseModelEditByUserEmail(string user, int id)
        {
            var currentUser = Membership.GetUser(user, true);
            var expense = _entity.Expense_tbl.Where(e => e.UserID == currentUser.Email && e.ID == id).FirstOrDefault();

            return new ExpenseModel
                {
                    ID = expense.ID,
                    ExpenseDate = expense.ExpenseDate ?? DateTime.Now,
                    Description = expense.Description,
                    Amount = expense.Amount ?? 0,
                    Tag = (expense.Tag_tbl != null ? expense.Tag_tbl.Description : string.Empty)
                };
        }

        private List<ExpenseModel> GetExpenseModelByUserEmailAndDate(string email, DateTime from, DateTime to)
        {
            var expenseList = _entity.Expense_tbl.Where(e => e.UserID == email && e.ExpenseDate >= from && e.ExpenseDate <= to);

            return expenseList.Select((items) =>
                new ExpenseModel
                {
                    ID = items.ID,
                    ExpenseDate = items.ExpenseDate ?? DateTime.Now,
                    Description = items.Description,
                    Amount = items.Amount ?? 0,
                    Tag = (items.Tag_tbl != null ? items.Tag_tbl.Description : string.Empty)
                }).ToList();
        }

        public IEnumerable<SelectListItem> GetTagModel()
        {
            var tagList = _entity.Tag_tbl;

            return tagList.ToList().Select((item) =>
            new SelectListItem
            {
                Text = item.Description,
                Value = item.ID.ToString()
            });
        }

        private IEnumerable<SelectListItem> GetMonthsList()
        {
            var monthsList = new List<string>();
            var dateInfo = new DateTimeFormatInfo();

            Enumerable.Range(1, 12).ToList().ForEach((item) =>
                monthsList.Add(dateInfo.GetMonthName(item) + ":" + item));

            var selectList = monthsList.ToList().Select((item) =>
            new SelectListItem
            {
                Text = item.Split(':').First(),
                Value = item.Split(':').Last(),
                Selected = item.Split(':').Last() == DateTime.Now.Month.ToString()
            });

            return selectList;
        }

        public IHtmlString GetExpensesChart(List<ExpenseModel> model)
        {
            var groupByTagList = from m in model
                                 group m by m.Tag
                                     into groupTag
                                     select new { groupTag.Key, SumAmount = groupTag.Sum(e => e.Amount) };

            var sumOfAll = groupByTagList.Sum(e => e.SumAmount);
            var list = groupByTagList.Select((item) => string.Format("['{0}',{1}]", item.Key, ((item.SumAmount/sumOfAll) * 100).ToString("#,###.00")));

            return new HtmlString(string.Join(",", list));
        }

        public IHtmlString GetExpensesTag(List<ExpenseModel> model)
        {
            var groupByTagList = from m in model
                                 group m by m.Tag
                                     into groupTag
                                     select new { groupTag.Key, SumAmount = groupTag.Sum(e => e.Amount) };

            var sumOfAll = groupByTagList.Sum(e => e.SumAmount);
            var list = groupByTagList.Select((item) => "{" + string.Format("text: '{0}', weight: {1}, url: '{2}', title: '{3}' ", 
                item.Key,
                ((item.SumAmount / sumOfAll) * 100).ToString("#,###.00"),
                "#cloudtag", 
                item.Key) + "}");

            return new HtmlString(string.Join(",", list));
        }
    }
}