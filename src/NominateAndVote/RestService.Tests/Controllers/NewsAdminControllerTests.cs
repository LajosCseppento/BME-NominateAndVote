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
    public class NewsAdminControllerTests
    {
        private NewsAdminController _controller;
        private DataModelManager _dataManager;

        [TestInitialize]
        public void Initialize()
        {
            var model = new SimpleDataModel();
            model.LoadSampleData();
            _dataManager = new DataModelManager(model);

            _controller = new NewsAdminController(_dataManager);
        }

        // Save_Null
        [TestMethod]
        public void SaveNews_Null()
        {
            // Act
            var result = _controller.Save(null) as OkNegotiatedContentResult<News>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual(Guid.Empty, result.Content.Id);
            Assert.AreEqual("title", result.Content.Title);
            Assert.AreEqual("text", result.Content.Text);
            Assert.AreNotEqual(DateTime.MinValue, result.Content.PublicationDate);
            Assert.AreEqual(result.Content, _dataManager.QueryNews(result.Content.Id));
        }

        //Correct object
        [TestMethod]
        public void SaveNews_Correct()
        {
            // Arrange
            var bindingModel = new SaveNewsBindingModel
            {
                Title = "title",
                Text = "text"
            };

            // Act
            var result = _controller.Save(bindingModel) as OkNegotiatedContentResult<News>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual(Guid.Empty, result.Content.Id);
            Assert.AreEqual("title", result.Content.Title);
            Assert.AreEqual("text", result.Content.Text);
            Assert.AreNotEqual(DateTime.MinValue, result.Content.PublicationDate);
            Assert.AreEqual(result.Content, _dataManager.QueryNews(result.Content.Id));
        }

        // Save_Existing
        [TestMethod]
        public void SaveNews_Existing()
        {
            // Arrange
            var bindingModel = new SaveNewsBindingModel
            {
                Title = "title",
                Text = "text"
            };

            // Act
            var result = _controller.Save(bindingModel) as OkNegotiatedContentResult<News>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual(Guid.Empty, result.Content.Id);
            Assert.AreEqual("title", result.Content.Title);
            Assert.AreEqual("text", result.Content.Text);
            Assert.AreNotEqual(DateTime.MinValue, result.Content.PublicationDate);
            Assert.AreEqual(result.Content, _dataManager.QueryNews(result.Content.Id));
        }

        // Save_Invalid
        [TestMethod]
        public void SaveNews_Invalid()
        {
            // Arrange
            var bindingModel = new SaveNewsBindingModel
            {
                Title = "title"
            };

            // Act
            var result = _controller.Save(bindingModel) as OkNegotiatedContentResult<News>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual(Guid.Empty, result.Content.Id);
            Assert.AreEqual("title", result.Content.Title);
            Assert.AreEqual("text", result.Content.Text);
            Assert.AreNotEqual(DateTime.MinValue, result.Content.PublicationDate);
            Assert.AreEqual(result.Content, _dataManager.QueryNews(result.Content.Id));
        }

        [TestMethod]
        public void DeleteNews()
        {
            // Arrange
            var news = _dataManager.QueryNews().ElementAt(0);

            // Act
            _controller.Delete(news.Id.ToString());

            // Assert
            Assert.IsFalse(_dataManager.QueryNews().Contains(news));
        }
    }
}