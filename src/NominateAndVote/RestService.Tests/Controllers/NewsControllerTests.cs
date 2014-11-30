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
        private NewsController controller;
        private DataModelManager dataManager;

        [TestInitialize]
        public void Initialize()
        {
            SimpleDataModel model = new SimpleDataModel();
            model.LoadSampleData();
            dataManager = new DataModelManager(model);

            controller = new NewsController(dataManager);
        }

        [TestMethod]
        public void Get()
        {
            // Arrange

            // Act
            var result = controller.Get() as List<News>;

            // Assert
            Assert.IsTrue((dataManager.QueryNews().Count == result.Count));
        }
    }
}