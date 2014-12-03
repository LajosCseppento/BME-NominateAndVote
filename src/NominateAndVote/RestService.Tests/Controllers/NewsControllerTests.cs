using Microsoft.VisualStudio.TestTools.UnitTesting;
using NominateAndVote.DataModel;
using NominateAndVote.DataModel.Tests;
using NominateAndVote.DataTableStorage.Tests;
using NominateAndVote.RestService.Controllers;
using System.Linq;

namespace NominateAndVote.RestService.Tests.Controllers
{
    public abstract class NewsControllerTests
    {
        private NewsController _controller;
        private IDataManager _dataManager;

        public abstract void Initialize();

        private void DoInitialize(IDataManager dataManager)
        {
            _dataManager = dataManager;
            _controller = new NewsController(_dataManager);
        }

        public abstract void Get();

        private void DoGet()
        {
            // Act
            var result = _controller.List();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(_dataManager.QueryNews().Count, result.Count());
        }

        [TestClass]
        public class NewsControllerMemoryTests : NewsControllerTests
        {
            [TestInitialize]
            public override void Initialize()
            {
                DoInitialize(new SampleDataModel().CreateDataManager());
            }

            [TestMethod]
            [TestCategory("Integration/RestService/Memory/NewsController")]
            public override void Get()
            {
                DoGet();
            }
        }

        [TestClass]
        public class NewsControllerTableStorageTests : NewsControllerTests
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
            [TestCategory("Integration/RestService/TableStorage/NewsController")]
            public override void Get()
            {
                DoGet();
            }
        }
    }
}