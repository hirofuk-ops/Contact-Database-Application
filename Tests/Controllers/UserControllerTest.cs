using Microsoft.VisualStudio.TestTools.UnitTesting;
using CRUD_application_2.Controllers;
using CRUD_application_2.Models;
using System.Web.Mvc;
using System.Collections.Generic;

namespace CRUD_application_2.Tests.Controllers
{
    [TestClass]
    public class UserControllerTest
    {
        private UserController _controller;
        private List<User> _userList;

        [TestInitialize]
        public void Setup()
        {
            _userList = new List<User>
            {
                new User { Id = 1, Name = "Test User 1", Email = "test1@example.com" },
                new User { Id = 2, Name = "Test User 2", Email = "test2@example.com" }
            };

            UserController.userlist = _userList;
            _controller = new UserController();
        }

        [TestMethod]
        public void Index_ReturnsCorrectView()
        {
            var result = _controller.Index() as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(_userList, result.Model);
        }

        [TestMethod]
        public void Details_ReturnsCorrectView()
        {
            var result = _controller.Details(1) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(_userList[0], result.Model);
        }

        [TestMethod]
        public void Create_Post_AddsUserToListAndRedirects()
        {
            var newUser = new User { Id = 3, Name = "Test User 3", Email = "test3@example.com" };
            var result = _controller.Create(newUser) as RedirectToRouteResult;

            Assert.AreEqual(3, UserController.userlist.Count);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void Edit_Post_UpdatesUserAndRedirects()
        {
            var updatedUser = new User { Id = 1, Name = "Updated User", Email = "updated@example.com" };
            var result = _controller.Edit(1, updatedUser) as RedirectToRouteResult;

            Assert.AreEqual("Updated User", UserController.userlist[0].Name);
            Assert.AreEqual("updated@example.com", UserController.userlist[0].Email);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void Delete_Post_RemovesUserAndRedirects()
        {
            var result = _controller.Delete(1, null) as RedirectToRouteResult;

            Assert.AreEqual(1, UserController.userlist.Count);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }
    }
}