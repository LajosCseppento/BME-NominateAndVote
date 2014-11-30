using Microsoft.VisualStudio.TestTools.UnitTesting;
using NominateAndVote.DataModel;
using NominateAndVote.DataModel.Model;
using NominateAndVote.DataModel.Tests;
using NominateAndVote.RestService.Controllers;
using System;
using System.Web.Http.Results;

namespace NominateAndVote.RestService.Tests.Controllers
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class AdminControllerTests
    {
        [TestClass]
        public class AdminControllerMemoryTests : AdminControllerGenericTests
        {
            protected override IDataManager CreateDataManager()
            {
                return new SampleDataModel().CreateDataManager();
            }
        }

        [TestClass]
        public class AdminControllerTableStorageTests : AdminControllerGenericTests
        {
            protected override IDataManager CreateDataManager()
            {
                // TODO Lali
                return new SampleDataModel().CreateDataManager();
            }
        }

        public abstract class AdminControllerGenericTests
        {
            private AdminController _controller;
            private IDataManager _dataManager;

            [TestInitialize]
            public void Initialize()
            {
                _dataManager = new SampleDataModel().CreateDataManager();
                _controller = new AdminController(_dataManager);
            }

            protected abstract IDataManager CreateDataManager();

            [TestMethod]
            public void BanUser()
            {
                // Act
                var user = new User { Id = Guid.NewGuid(), IsBanned = false, Name = "Kis Bela" };
                _dataManager.SaveUser(user);
                var result = _controller.BanUser(user.Id.ToString()) as OkNegotiatedContentResult<User>;

                // Assert
                Assert.IsNotNull(result);
                Assert.IsTrue((result.Content.IsBanned == true));
            }

            [TestMethod]
            public void UnBanUser()
            {
                // Act
                var user = new User { Id = Guid.NewGuid(), IsBanned = true, Name = "Kis Bela" };
                _dataManager.SaveUser(user);
                var result = _controller.UnBanUser(user.Id.ToString()) as OkNegotiatedContentResult<User>;

                // Assert
                Assert.IsNotNull(result);
                Assert.IsTrue((result.Content.IsBanned == false));
            }
        }
    }
}