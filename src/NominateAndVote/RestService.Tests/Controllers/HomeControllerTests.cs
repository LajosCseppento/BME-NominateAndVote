using Microsoft.VisualStudio.TestTools.UnitTesting;
using NominateAndVote.RestService.Controllers;
using System.Web.Mvc;

namespace NominateAndVote.RestService.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Home Page", result.ViewBag.Title);
        }
    }
}