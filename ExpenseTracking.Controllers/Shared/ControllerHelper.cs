using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpenseTracking.Controllers.Shared
{
    public static class ControllerHelper
    {
        public static string GetUserName(this Controller controller)
        {
            return controller.User == null ? string.Empty : controller.User.Identity.Name;
        }
    }
}