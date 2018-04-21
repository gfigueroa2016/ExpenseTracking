using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using ExpenseTracking.Service.Interface.Account;

namespace ExpenseTracking.Service.Account
{
    public class AccountService : IAccountService
    {
        public bool ValidateUser(string username, string password, bool remember = false)
        {
            if(Membership.ValidateUser(username, password))
            {
                FormsAuthentication.SetAuthCookie(username, remember);
                return true;
            }
            return false;
        }

        public void LogOff()
        {
            FormsAuthentication.SignOut();
        }

        public MembershipCreateStatus CreateUser(string username, string password, string email, bool isAutoLog)
        {
            MembershipCreateStatus createStatus;
            Membership.CreateUser(username, password, email, null, null, true, null, out createStatus);

            if (createStatus == MembershipCreateStatus.Success && isAutoLog)
            {
                FormsAuthentication.SetAuthCookie(username, false);
            }
            return createStatus;
        }

        public bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            var currentUser = Membership.GetUser(username, true);
            return currentUser.ChangePassword(oldPassword, newPassword);
        }
    }
}