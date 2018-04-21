using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using ExpenseTracking.Models;
using ExpenseTracking.Models.Tag;
using ExpenseTracking.Service.Tag.Interface;

namespace ExpenseTracking.Service.Tag
{
    public class TagService : ITagService
    {
        private FooEntities _entity;

        public TagService(FooEntities entity)
        {
            _entity = entity;
            
        }

        public bool CreateTag(TagModel model)
        {
            _entity.Connection.Open();
            var transaction = _entity.Connection.BeginTransaction();

            try
            {
                var tag = new Tag_tbl();
                tag.Description = model.TagDescription;

                _entity.AddToTag_tbl(tag);
                _entity.SaveChanges();
                transaction.Commit();
                _entity.Connection.Close();
                model.IsHasError = false;
                model.Message = "Succesfully saved new tag";
                return true;
            }
            catch(Exception e)
            {
                transaction.Rollback();
                _entity.Connection.Close();
                model.IsHasError = true;
                model.Message = e.Message;
                return false;
            }
           
        }

        public bool EditTag(TagModel model)
        {
            _entity.Connection.Open();
            var transaction = _entity.Connection.BeginTransaction();

            try
            {
                var tag = _entity.Tag_tbl.FirstOrDefault(e => e.ID == model.ID);
                tag.Description = model.TagDescription;

                _entity.SaveChanges();
                transaction.Commit();
                _entity.Connection.Close();
                model.IsHasError = false;
                model.Message = "Succesfully updated tag";
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

        public bool DeleteTag(TagModel model)
        {
            _entity.Connection.Open();
            var transaction = _entity.Connection.BeginTransaction();

            try
            {
                var tag = _entity.Tag_tbl.FirstOrDefault(e => e.ID == model.ID);

                _entity.DeleteObject(tag);
                _entity.SaveChanges();
                transaction.Commit();
                _entity.Connection.Close();
                model.IsHasError = false;
                model.Message = "Succesfully deleted tag";
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

        public List<TagModel> GetModel()
        {
            var tagList = _entity.Tag_tbl.ToList();

            return tagList.Select(
                listItems => new TagModel()
                {
                    ID = listItems.ID,
                    TagDescription = listItems.Description,
                }).ToList();
        }

        public TagModel GetModelByID(int id)
        {
            var tag = _entity.Tag_tbl.FirstOrDefault(e=> e.ID == id);

            return new TagModel{ID = tag.ID, TagDescription = tag.Description};
        }
    }
}