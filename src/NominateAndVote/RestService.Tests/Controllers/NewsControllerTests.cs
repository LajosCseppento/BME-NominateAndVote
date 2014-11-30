using Microsoft.VisualStudio.TestTools.UnitTesting;
using NominateAndVote.DataModel;
using NominateAndVote.DataModel.Model;
using NominateAndVote.RestService.Controllers;
using System.Collections.Generic;

namespace NominateAndVote.RestService.Tests.Controllers
{
    [TestClass]
    public class NewsControllerTests
    {
        private NewsController _controller;
        private DataModelManager _dataManager;

        [TestInitialize]
        public void Initialize()
        {
            var model = new SimpleDataModel();
            model.LoadSampleData();
            _dataManager = new DataModelManager(model);

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