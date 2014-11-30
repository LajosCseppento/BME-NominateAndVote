using Microsoft.VisualStudio.TestTools.UnitTesting;
using NominateAndVote.DataModel;
using NominateAndVote.DataModel.Tests;
using NominateAndVote.RestService.Controllers;

namespace NominateAndVote.RestService.Tests.Controllers
{
    [TestClass]
    public class NominationControllerTests
    {
        private NominationController _controller;
        private IDataManager _dataManager;

        [TestInitialize]
        public void Initialize()
        {
            _dataManager = new SampleDataModel().CreateDataManager();
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