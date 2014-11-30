using Microsoft.VisualStudio.TestTools.UnitTesting;
using NominateAndVote.DataModel;
using NominateAndVote.DataModel.Poco;
using NominateAndVote.DataModel.Tests;
using NominateAndVote.RestService.Controllers;
using NominateAndVote.RestService.Models;
using System;
using System.Linq;
using System.Web.Http.Results;

namespace NominateAndVote.RestService.Tests.Controllers
{
    [TestClass]
    public class NewsAdminControllerMemoryTests : NewsAdminControllerGenericTests
    {
        protected override IDataManager CreateDataManager()
        {
            return new SampleDataModel().CreateDataManager();
        }
    }

    [TestClass]
    public class NewsAdminControllerTableStorageTests : NewsAdminControllerGenericTests
    {
        protected override IDataManager CreateDataManager()
        {
            // TODO Lali
            return new SampleDataModel().CreateDataManager();
        }
    }

    public abstract class NewsAdminControllerGenericTests
    {
        private NewsAdminController _controller;
        private IDataManager _dataManager;

        [TestInitialize]
        public void Initialize()
        {
            _dataManager = CreateDataManager();
            _controller = new NewsAdminController(_dataManager);
        }

        protected abstract IDataManager CreateDataManager();

        // Save_Null
        [TestMethod]
        public void SaveNews_Null()
        {
            // Act
            var result = _controller.Save(null) as BadRequestErrorMessageResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Message, "No data");
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