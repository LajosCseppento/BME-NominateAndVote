using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NominateAndVote.DataModel.Tests
{
    [TestClass]
    // TODO public
    public class MemoryDataManagerTests : DataManagerGenericTests
    {
        protected override IDataManager _createDataManager(IDataModel dataModel)
        {
            return new MemoryDataManager(dataModel);
        }
    }
}