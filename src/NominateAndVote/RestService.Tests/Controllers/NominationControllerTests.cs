using Microsoft.VisualStudio.TestTools.UnitTesting;
using NominateAndVote.DataModel;
using NominateAndVote.RestService.Controllers;

namespace NominateAndVote.RestService.Tests.Controllers
{
    [TestClass]
    public class NominationControllerTests
    {
        private NominationController _controller;
        private DataModelManager _dataManager;

        [TestInitialize]
        public void Initialize()
        {
            var model = new SimpleDataModel();
            model.LoadSampleData();
            _dataManager = new DataModelManager(model);

            _controller = new NominationController(_dataManager);
        }

        [TestMethod]
        public void DeleteNomination()
        {
            // Arrange
            var nomination = _dataManager.QueryPolls()[1].Nominations[0];

            // Act
            _controller.Delete(nomination.Id.ToString());

            // Assert
            Assert.IsFalse(_dataManager.QueryPolls()[1].Nominations.Contains(nomination));
        }
    }
}