using Microsoft.VisualStudio.TestTools.UnitTesting;
using NominateAndVote.DataModel;
using NominateAndVote.DataModel.Model;
using NominateAndVote.DataModel.Tests;
using NominateAndVote.RestService.Controllers;
using System.Collections.Generic;

namespace NominateAndVote.RestService.Tests.Controllers
{

    [TestClass]
    public class NewsControllerMemoryTests : NewsControllerGenericTests
    {
        protected override IDataManager CreateDataManager()
        {
            return new SampleDataModel().CreateDataManager();
        }
    }

    [TestClass]
    public class NewsControllerTableStorageTests : NewsControllerGenericTests
    {
        protected override IDataManager CreateDataManager()
        {
            // TODO Lali
            return new SampleDataModel().CreateDataManager();
        }
    }

    public abstract class NewsControllerGenericTests
    {
        private NewsController _controller;
        private IDataManager _dataManager;

        [TestInitialize]
        public void Initialize()
        {
            _dataManager = new SampleDataModel().CreateDataManager();
            _controller = new NewsController(_dataManager);
        }

        protected abstract IDataManager CreateDataManager();

        [TestMethod]
        public void Get()
        {
            // Act
            var result = _controller.Get() as List<News>;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue((_dataManager.QueryNews().Count == result.Count));
        }
    }
}