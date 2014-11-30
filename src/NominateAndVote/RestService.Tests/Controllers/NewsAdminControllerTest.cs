using Microsoft.VisualStudio.TestTools.UnitTesting;
using NominateAndVote.DataModel;
using NominateAndVote.DataModel.Model;
using NominateAndVote.RestService.Controllers;
using NominateAndVote.RestService.Models;
using System;
using System.Linq;
using System.Web.Http.Results;

namespace NominateAndVote.RestService.Tests.Controllers
{
    [TestClass]
    public class NewsAdminControllerTest
    {
        private NewsAdminController controller;
        private DataModelManager dataManager;

        [TestInitialize]
        public void Initialize()
        {
            SimpleDataModel model = new SimpleDataModel();
            model.LoadSampleData();
            dataManager = new DataModelManager(model);

            controller = new NewsAdminController(dataManager);
        }

        // Save_Null
        [TestMethod]
        public void SaveNews_Null()
        {
            // Arrange
            SaveNewsBindingModel bindingModel = null;

            // Act
            var result = controller.Save(bindingModel) as OkNegotiatedContentResult<News>;

            // Assert
            Assert.AreNotEqual(Guid.Empty, result.Content.ID);
            Assert.AreEqual("title", result.Content.Title);
            Assert.AreEqual("text", result.Content.Text);
            Assert.AreNotEqual(DateTime.MinValue, result.Content.PublicationDate);
            Assert.AreEqual(result.Content, dataManager.QueryNews(result.Content.ID));
        }

        //Correct object
        [TestMethod]
        public void SaveNews_Correct()
        {
            // Arrange
            SaveNewsBindingModel bindingModel = new SaveNewsBindingModel()
            {
                Title = "title",
                Text = "text"
            };

            // Act
            var result = controller.Save(bindingModel) as OkNegotiatedContentResult<News>;

            // Assert
            Assert.AreNotEqual(Guid.Empty, result.Content.ID);
            Assert.AreEqual("title", result.Content.Title);
            Assert.AreEqual("text", result.Content.Text);
            Assert.AreNotEqual(DateTime.MinValue, result.Content.PublicationDate);
            Assert.AreEqual(result.Content, dataManager.QueryNews(result.Content.ID));
        }

        // Save_Existing
        [TestMethod]
        public void SaveNews_Existing()
        {
            // Arrange
            SaveNewsBindingModel bindingModel = new SaveNewsBindingModel()
            {
                Title = "title",
                Text = "text"
            };

            // Act
            var result = controller.Save(bindingModel) as OkNegotiatedContentResult<News>;

            // Assert
            Assert.AreNotEqual(Guid.Empty, result.Content.ID);
            Assert.AreEqual("title", result.Content.Title);
            Assert.AreEqual("text", result.Content.Text);
            Assert.AreNotEqual(DateTime.MinValue, result.Content.PublicationDate);
            Assert.AreEqual(result.Content, dataManager.QueryNews(result.Content.ID));
        }

        // Save_Invalid
        [TestMethod]
        public void SaveNews_Invalid()
        {
            // Arrange
            SaveNewsBindingModel bindingModel = new SaveNewsBindingModel()
            {
                Title = "title"
            };

            // Act
            var result = controller.Save(bindingModel) as OkNegotiatedContentResult<News>;

            // Assert
            Assert.AreNotEqual(Guid.Empty, result.Content.ID);
            Assert.AreEqual("title", result.Content.Title);
            Assert.AreEqual("text", result.Content.Text);
            Assert.AreNotEqual(DateTime.MinValue, result.Content.PublicationDate);
            Assert.AreEqual(result.Content, dataManager.QueryNews(result.Content.ID));
        }

        [TestMethod]
        public void DeleteNews()
        {
            // Arrange
            News news = dataManager.QueryNews().ElementAt(0);

            // Act
            controller.Delete(news.ID.ToString());

            // Assert
            Assert.IsFalse(dataManager.QueryNews().Contains(news));
        }
    }
}