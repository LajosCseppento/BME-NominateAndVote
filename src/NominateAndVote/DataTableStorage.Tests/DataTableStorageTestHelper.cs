using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using NominateAndVote.DataModel;
using NominateAndVote.DataTableStorage.Entity;
using System;

namespace NominateAndVote.DataTableStorage.Tests
{
    public class DataTableStorageTestHelper
    {
        public string TablePrefix { get; private set; }

        public TableStorageDataManager TableStorageDataManager { get; private set; }

        public DataTableStorageTestHelper()
        {
            TablePrefix = "test" + DateTime.Now.Ticks;
        }

        public void Initialize(IDataModel dataModel)
        {
            // set table names
            TableNames.ResetToDefault(TablePrefix);

            // connect and create tables
            var storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            TableStorageDataManager = new TableStorageDataManager(storageAccount);
            TableStorageDataManager.CreateTablesIfNeeded();

            // add data
            foreach (var poco in dataModel.Administrators)
            {
                TableStorageDataManager.SaveEntity(new AdministratorEntity(poco));
            }
            foreach (var poco in dataModel.News)
            {
                TableStorageDataManager.SaveEntity(new NewsEntity(poco));
            }
            foreach (var poco in dataModel.Nominations)
            {
                TableStorageDataManager.SaveEntity(new NominationEntity(poco));
            }
            foreach (var poco in dataModel.Polls)
            {
                TableStorageDataManager.SaveEntity(new PollEntity(poco));
            }
            foreach (var poco in dataModel.PollSubjects)
            {
                TableStorageDataManager.SaveEntity(new PollSubjectEntity(poco));
            }
            foreach (var poco in dataModel.Users)
            {
                TableStorageDataManager.SaveEntity(new UserEntity(poco));
            }
            foreach (var poco in dataModel.Votes)
            {
                TableStorageDataManager.SaveEntity(new VoteEntity(poco));
            }
        }

        public void CleanUp()
        {
            // set table names again
            TableNames.ResetToDefault(TablePrefix);

            TableStorageDataManager.DeleteTablesIfNeeded();
        }
    }
}