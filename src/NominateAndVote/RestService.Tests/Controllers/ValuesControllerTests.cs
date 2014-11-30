using Microsoft.VisualStudio.TestTools.UnitTesting;
using NominateAndVote.RestService.Controllers;
using System.Collections.Generic;
using System.Linq;

namespace NominateAndVote.RestService.Tests.Controllers
{
    [TestClass]
    public class ValuesControllerTests
    {
        [TestMethod]
        public void GetValue()
        {
            // Arrange
            var controller = new ValuesController();

            // Act
            var result = controller.Get();

            // Assert
            var list = result as IList<string> ?? result.ToList();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, list.Count());
            Assert.AreEqual("value1", list.ElementAt(0));
            Assert.AreEqual("value2", list.ElementAt(1));
        }

        [TestMethod]
        public void GetById()
        {
            // Arrange
            var controller = new ValuesController();

            // Act
            var result = controller.Get(5);

            // Assert
            Assert.AreEqual("value", result);
        }

        [TestMethod]
        public void Post()
        {
            // Arrange
            var controller = new ValuesController();

            // Act
            controller.Post("value");

            // Assert
            // TODO
        }

        [TestMethod]
        public void PutValue()
        {
            // Arrange
            var controller = new ValuesController();

            // Act
            controller.Put(5, "value");

            // Assert
            // TODO
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            var controller = new ValuesController();

            // Act
            controller.Delete(5);

            // Assert
            // TODO
        }
    }
}