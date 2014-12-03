using Microsoft.VisualStudio.TestTools.UnitTesting;
using NominateAndVote.DataModel;
using NominateAndVote.DataModel.Poco;
using NominateAndVote.DataModel.Tests;
using NominateAndVote.DataTableStorage.Tests;
using NominateAndVote.RestService.Controllers;
using System.Web.Http.Results;

namespace NominateAndVote.RestService.Tests.Controllers
{
    public abstract class AdminControllerTests
    {
        private AdminController _controller;

        private IDataManager _dataManager;

        public abstract void Initialize();

        private void DoInitialize(IDataManager dataManager)
        {
            _dataManager = dataManager;
            _controller = new AdminController(_dataManager);
        }

        public abstract void BanUser();

        private void DoBanUser()
        {
            // Arrange
            var user = new User { Id = 999, IsBanned = false, Name = "Kiss Bela" };
            _dataManager.SaveUser(user);

            // Act
            var result = _controller.BanUser(user.Id.ToString("D8")) as OkNegotiatedContentResult<User>;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(_dataManager.QueryBannedUsers().Contains(user));
        }

        public abstract void BanInvalidUser();

        private void DoBanInvalidUser()
        {
            // Arrange

            // Act
            var result = _controller.BanUser(56.ToString("D8")) as NotFoundResult;

            // Assert
            Assert.IsNotNull(result);
        }

        public abstract void UnBanUser();

        private void DoUnBanUser()
        {
            // Arrange
            var user = new User { Id = 999, IsBanned = true, Name = "Kiss Bela" };
            _dataManager.SaveUser(user);

            // Act
            var result = _controller.UnBanUser(user.Id.ToString("D8")) as OkNegotiatedContentResult<User>;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(_dataManager.QueryBannedUsers().Contains(user));
        }

        public abstract void UnBanInvalidUser();

        private void DoUnBanInvalidUser()
        {
            // Arrange

            // Act
            var result = _controller.UnBanUser(56.ToString("D8")) as NotFoundResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestClass]
        public class AdminControllerMemoryTests : AdminControllerTests
        {
            [TestInitialize]
            public override void Initialize()
            {
                DoInitialize(new SampleDataModel().CreateDataManager());
            }

            [TestMethod]
            [TestCategory("Integration/RestService/Memory/AdminController")]
            public override void BanUser()
            {
                DoBanUser();
            }

            [TestMethod]
            [TestCategory("Integration/RestService/Memory/AdminController")]
            public override void BanInvalidUser()
            {
                DoBanInvalidUser();
            }

            [TestMethod]
            [TestCategory("Integration/RestService/Memory/AdminController")]
            public override void UnBanUser()
            {
                DoUnBanUser();
            }

            [TestMethod]
            [TestCategory("Integration/RestService/Memory/AdminController")]
            public override void UnBanInvalidUser()
            {
                DoUnBanInvalidUser();
            }
        }

        [TestClass]
        public class AdminControllerTableStorageTests : AdminControllerTests
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
            [TestCategory("Integration/RestService/TableStorage/AdminController")]
            public override void BanUser()
            {
                DoBanUser();
            }

            [TestMethod]
            [TestCategory("Integration/RestService/TableStorage/AdminController")]
            public override void BanInvalidUser()
            {
                DoBanInvalidUser();
            }

            [TestMethod]
            [TestCategory("Integration/RestService/TableStorage/AdminController")]
            public override void UnBanUser()
            {
                DoUnBanUser();
            }

            [TestMethod]
            [TestCategory("Integration/RestService/TableStorage/AdminController")]
            public override void UnBanInvalidUser()
            {
                DoUnBanInvalidUser();
            }
        }
    }
}