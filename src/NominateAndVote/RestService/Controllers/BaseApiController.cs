using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using NominateAndVote.DataModel;
using NominateAndVote.DataTableStorage;
using System.Web.Http;

namespace NominateAndVote.RestService.Controllers
{
    public abstract class BaseApiController : ApiController
    {
        protected readonly IDataManager DataManager;

        protected BaseApiController()
            : this(null)
        {
        }

        protected BaseApiController(IDataManager dataManager)
        {
            // table storage

            // set table names
            TableNames.ResetToDefault();

            // connect and create tables
            var storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            var manager = new TableStorageDataManager(storageAccount);
            manager.CreateTablesIfNeeded();

            DataManager = manager;

            // temporary memory
            // DataManager = dataManager ?? new MemoryDataManager(new DefaultDataModel());
        }
    }
}