using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace ExpenseTracking.Service.Interface.Account
{
    public interface IAccountService
    {
        bool ValidateUser(string username, string password, bool remember);
        void LogOff();
        MembershipCreateStatus CreateUser(string username, string password, string email, bool isAutoLog);
        bool ChangePassword(string username, string oldPassword, string newPassword);
    }
}