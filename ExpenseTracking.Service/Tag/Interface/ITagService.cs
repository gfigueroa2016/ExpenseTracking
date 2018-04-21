using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExpenseTracking.Models.Tag;

namespace ExpenseTracking.Service.Tag.Interface
{
    public interface ITagService
    {
        bool CreateTag(TagModel model);
        bool EditTag(TagModel model);
        bool DeleteTag(TagModel model);
        List<TagModel> GetModel();
        TagModel GetModelByID(int id);
    }
}