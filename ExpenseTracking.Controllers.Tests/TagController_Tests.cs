using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExpenseTracking.Models.Tag;
using ExpenseTracking.Service.Tag.Interface;
using NUnit.Framework;
using Rhino.Mocks;

namespace ExpenseTracking.Controllers.Tests
{
    [TestFixture]
    public class TagController_Tests
    {
        private ITagService _tagService;

        [SetUp]
        public void Init()
        {
            _tagService = MockRepository.GenerateMock<ITagService>();
        }

        [Test]
        public void Index_Should_Return_Index_View()
        {
            //arrange
            var tagController = GetTagController();

            //act
            var viewResult = tagController.Index() as ViewResult;

            //assert
            Assert.AreEqual("Index", viewResult.ViewName);
        }

        [Test]
        public void Create_Should_Return_Index_View_When_Valid()
        {
            //arrange
            var tagController = GetTagController();

            var tagModel = new TagModel();
            _tagService.Expect(e => e.CreateTag(tagModel)).Return(true);

            //act
            var viewResult = tagController.Create(tagModel) as RedirectToRouteResult;

            //assert
            Assert.AreEqual("Index", viewResult.RouteValues["action"]);
        }

        [Test]
        public void Create_Should_Return_Create_View_When_Not_Valid()
        {
            //arrange
            var tagController = GetTagController();

            var tagModel = new TagModel();
            _tagService.Expect(e => e.CreateTag(tagModel)).Return(false);

            //act
            var viewResult = tagController.Create(tagModel) as ViewResult;

            //assert
            Assert.AreEqual("Create", viewResult.ViewName);
        }

        [Test]
        public void Edit_Should_Return_Index_View_When_Valid()
        {
            //arrange
            var tagController = GetTagController();

            var tagModel = new TagModel();
            _tagService.Expect(e => e.EditTag(tagModel)).Return(true);

            //act
            var viewResult = tagController.Edit(tagModel) as RedirectToRouteResult;

            //assert
            Assert.AreEqual("Index", viewResult.RouteValues["action"]);
        }

        [Test]
        public void Edit_Should_Return_Edit_View_When_Not_Valid()
        {
            //arrange
            var tagController = GetTagController();

            var tagModel = new TagModel();
            _tagService.Expect(e => e.EditTag(tagModel)).Return(false);

            //act
            var viewResult = tagController.Edit(tagModel) as ViewResult;

            //assert
            Assert.AreEqual("Edit", viewResult.ViewName);
        }

        [Test]
        public void Delete_Should_Return_Index_View_When_Valid()
        {
            //arrange
            var tagController = GetTagController();

            var tagModel = new TagModel();
            _tagService.Expect(e => e.DeleteTag(tagModel)).Return(true);

            //act
            var viewResult = tagController.Delete(tagModel) as RedirectToRouteResult;

            //assert
            Assert.AreEqual("Index", viewResult.RouteValues["action"]);
        }

        [Test]
        public void Delete_Should_Return_Delete_View_When_Not_Valid()
        {
            //arrange
            var tagController = GetTagController();

            var tagModel = new TagModel();
            _tagService.Expect(e => e.DeleteTag(tagModel)).Return(false);

            //act
            var viewResult = tagController.Delete(tagModel) as ViewResult;

            //assert
            Assert.AreEqual("Delete", viewResult.ViewName);
        }

        private TagController GetTagController()
        {
            return new TagController(_tagService);
        }
    }
}