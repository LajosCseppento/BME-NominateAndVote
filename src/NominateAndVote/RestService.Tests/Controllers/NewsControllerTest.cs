using Microsoft.VisualStudio.TestTools.UnitTesting;
using NominateAndVote.DataModel;
using NominateAndVote.RestService.Controllers;

namespace NominateAndVote.RestService.Tests.Controllers
{
    [TestClass]
    public class NewsControllerTest
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

        // Save_Null

        /*[TestMethod]
        public void Save_New()
        {
            // Arrange
            SaveNewsBindingModel bindingModel = new SaveNewsBindingModel()
            {
                Title = "title",
                Text = "text"
            };

            // Act
            var result = controller.Save(bindingModel) as OkNegotiatedContentResult<News>;

            // Assert
            Assert.AreNotEqual(Guid.Empty, result.Content.ID);
            Assert.AreEqual("title", result.Content.Title);
            Assert.AreEqual("text", result.Content.Text);
            Assert.AreNotEqual(DateTime.MinValue, result.Content.PublicationDate);
            Assert.AreEqual(result.Content, dataManager.QueryNews(result.Content.ID));
        }*/

        // Save_Existing

        // Save_Invalid
    }
}