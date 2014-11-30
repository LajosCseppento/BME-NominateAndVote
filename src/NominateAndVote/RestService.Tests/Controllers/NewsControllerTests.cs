using Microsoft.VisualStudio.TestTools.UnitTesting;
using NominateAndVote.DataModel;
using NominateAndVote.DataModel.Model;
using NominateAndVote.DataModel.Tests;
using NominateAndVote.RestService.Controllers;
using System.Collections.Generic;

namespace NominateAndVote.RestService.Tests.Controllers
{
    [TestClass]
    public class NewsControllerTests
    {
        private NewsController _controller;
        private IDataManager _dataManager;

        [TestInitialize]
        public void Initialize()
        {
            _dataManager = new SampleDataModel().CreateDataManager();
            _controller = new NewsController(_dataManager);
        }

        [TestMethod]
        public void GetList()
        {
            // Act
            var result = _controller.Get() as List<News>;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue((_dataManager.QueryNews().Count == result.Count));
        }
    }
}