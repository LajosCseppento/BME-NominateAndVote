using Microsoft.VisualStudio.TestTools.UnitTesting;
using NominateAndVote.DataModel;
using NominateAndVote.DataModel.Model;
using NominateAndVote.RestService.Controllers;

namespace NominateAndVote.RestService.Tests.Controllers
{
    [TestClass]
    public class NominationControllerTest
    {
        private NominationController controller;
        private DataModelManager dataManager;

        [TestInitialize]
        public void Initialize()
        {
            SimpleDataModel model = new SimpleDataModel();
            model.LoadSampleData();
            dataManager = new DataModelManager(model);

            controller = new NominationController(dataManager);
        }

        [TestMethod]
        public void DeleteNomination()
        {
            // Arrange
            Nomination nomination = dataManager.QueryPolls()[1].Nominations[0];

            // Act
            controller.Delete(nomination.ID.ToString());

            // Assert
            Assert.IsFalse(dataManager.QueryPolls()[1].Nominations.Contains(nomination));
        }
    }

    
}