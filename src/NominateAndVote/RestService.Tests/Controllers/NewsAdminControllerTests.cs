using Microsoft.VisualStudio.TestTools.UnitTesting;
using NominateAndVote.DataModel;
using NominateAndVote.DataModel.Poco;
using NominateAndVote.DataModel.Tests;
using NominateAndVote.DataTableStorage.Tests;
using NominateAndVote.RestService.Controllers;
using NominateAndVote.RestService.Models;
using System;
using System.Linq;
using System.Web.Http.Results;

namespace NominateAndVote.RestService.Tests.Controllers
{
    public abstract class NewsAdminControllerTests
    {
        private NewsAdminController _controller;
        private NewsController _controllerNews;
        private IDataManager _dataManager;

        public abstract void Initialize();

        private void DoInitialize(IDataManager dataManager)
        {
            _dataManager = dataManager;
            _controller = new NewsAdminController(_dataManager);
            _controllerNews = new NewsController(_dataManager);
        }

        public abstract void Save_New();

        private void DoSave_New()
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

        public abstract void Save_Update();

        private void DoSave_Update()
        {
            // Arrange
            var news = _controllerNews.List();

            var one = news.ElementAt(0);
            var bindingModel = new SaveNewsBindingModel
            {
                Title = "Ez",
                Text = one.Text,
                Id = one.Id.ToString()
            };

            // Act
            var result = _controller.Save(bindingModel) as OkNegotiatedContentResult<News>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual(Guid.Empty, result.Content.Id);
            Assert.AreEqual("Ez", result.Content.Title);
            Assert.AreNotEqual(DateTime.MinValue, result.Content.PublicationDate);
            Assert.AreEqual(result.Content, _dataManager.QueryNews(result.Content.Id));
        }

        public abstract void Save_Null();

        private void DoSave_Null()
        {
            // Act
            var result = _controller.Save(null) as BadRequestErrorMessageResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Message, "No data");
        }

        public abstract void Delete();

        private void DoDelete()
        {
            // Arrange
            var news = _dataManager.QueryNews()[0];

            // Act
            _controller.Delete(news.Id.ToString());

            // Assert
            Assert.IsFalse(_dataManager.QueryNews().Contains(news));
        }

        [TestClass]
        public class NewsAdminControllerMemoryTests : NewsAdminControllerTests
        {
            [TestInitialize]
            public override void Initialize()
            {
                DoInitialize(new SampleDataModel().CreateDataManager());
            }

            [TestMethod]
            [TestCategory("Integration/RestService/Memory/NewsAdminController")]
            public override void Save_New()
            {
                DoSave_New();
            }

            [TestMethod]
            [TestCategory("Integration/RestService/Memory/NewsAdminController")]
            public override void Save_Update()
            {
                DoSave_Update();
            }

            [TestMethod]
            [TestCategory("Integration/RestService/Memory/NewsAdminController")]
            public override void Save_Null()
            {
                DoSave_Null();
            }

            [TestMethod]
            [TestCategory("Integration/RestService/Memory/NewsAdminController")]
            public override void Delete()
            {
                DoDelete();
            }
        }

        [TestClass]
        public class NewsAdminControllerTableStorageTests : NewsAdminControllerTests
        {
            private DataTableStorageTestHelper _helper;

            [TestInitialize]
            public override void Initialize()
            {
                _helper = new DataTableStorageTestHelper();
                _helper.Initialize(new SampleDataModel());
                DoInitialize(_helper.TableStorageDataManager);
            }

            [TestCleanup]
            public void Cleanup()
            {
                _helper.CleanUp();
            }

            [TestMethod]
            [TestCategory("Integration/RestService/TableStorage/NewsAdminController")]
            public override void Save_New()
            {
                DoSave_New();
            }

            [TestMethod]
            [TestCategory("Integration/RestService/TableStorage/NewsAdminController")]
            public override void Save_Update()
            {
                DoSave_Update();
            }

            [TestMethod]
            [TestCategory("Integration/RestService/TableStorage/NewsAdminController")]
            public override void Save_Null()
            {
                DoSave_Null();
            }

            [TestMethod]
            [TestCategory("Integration/RestService/TableStorage/NewsAdminController")]
            public override void Delete()
            {
                DoDelete();
            }
        }
    }
}