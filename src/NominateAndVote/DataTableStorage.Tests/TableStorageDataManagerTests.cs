using Microsoft.VisualStudio.TestTools.UnitTesting;
using NominateAndVote.DataModel;
using NominateAndVote.DataModel.Tests;

namespace NominateAndVote.DataTableStorage.Tests
{
    [TestClass]
    public class TableStorageDataManagerTests : DataManagerGenericTests
    {
        private DataTableStorageTestHelper _helper;

        protected override IDataManager _createDataManager(IDataModel dataModel)
        {
            _helper = new DataTableStorageTestHelper();
            _helper.Initialize(dataModel);
            return _helper.TableStorageDataManager;
        }

        [TestCleanup]
        public void Cleanup()
        {
            _helper.CleanUp();
        }
    }
}