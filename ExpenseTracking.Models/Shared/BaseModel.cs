using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpenseTracking.Models.Shared
{
    public class BaseModel
    {
        public bool IsHasError { get; set; }
        public string Message { get; set; }
        public string PageTitle { get; set; }
    }
}