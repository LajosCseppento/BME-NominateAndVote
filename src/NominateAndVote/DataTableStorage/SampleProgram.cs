namespace NominateAndVote.DataTableStorage
{
    using Microsoft.WindowsAzure;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Table;
    using NominateAndVote.DataModel.Model;
    using NominateAndVote.DataTableStorage.Model;
    using System;
    using System.Collections.Generic;

    public class SampleProgram
    {
        internal const string TableName = "testpollsubject";

        public static void Main(string[] args)
        {
            /*
            CloudStorageAccount storageAccount = CreateStorageAccountFromConnectionString(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            TableStorageDataManager d = new TableStorageDataManager(storageAccount);

            d.CreateTablesIfNeeded();

            Console.ReadLine();

            PollSubject ps = new PollSubject() { ID = 1,  Year = 200 };
            bool ret = d.SaveEntity(new PollSubjectEntity(ps));
            Console.WriteLine(ret);
            // d.DeleteTablesIfNeeded();

            throw new NotImplementedException("END");
            */

            Console.WriteLine("Azure Storage Table Sample\n");

            CloudTable table = CreateTablePollSubjectEntity();

            BasicTableOperationsPollSubjectEntity(table);

            AdvancedTableOperationsPollSubjectEntity(table);

            Console.WriteLine("Press Enter if you are ready to delete the table");
            Console.ReadLine();

            DeleteTablePollSubjectEntity(table);

            Console.WriteLine("Press any key to exit");
            Console.Read();
        }

        private static CloudTable CreateTablePollSubjectEntity()
        {
            // Retrieve storage account information from connection string.
            CloudStorageAccount storageAccount = CreateStorageAccountFromConnectionString(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create a table client for interacting with the table service
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            Console.WriteLine("1. Create a Table for the demo");

            // Create a table client for interacting with the table service
            CloudTable table = tableClient.GetTableReference(TableName);

            try
            {
                if (table.CreateIfNotExists())
                {
                    Console.WriteLine("Created Table named: {0}", TableName);
                }
                else
                {
                    Console.WriteLine("Table {0} already exists", TableName);
                }
            }
            catch (StorageException)
            {
                Console.WriteLine("If you are running with the default configuration please make sure you have started the storage emulator. Press the Windows key and type Azure Storage to select and run it from the list of applications - then restart the sample.");
                Console.ReadLine();
                throw;
            }

            return table;
        }

        private static void BasicTableOperationsPollSubjectEntity(CloudTable table)
        {
            // Create an instance of a customer entity. See the Model\PollSubjectEntity.cs for a description of the entity.
            PollSubjectEntity entity = new PollSubjectEntity(new PollSubject { ID = 1, Title = "Hunger Games", Year = 2012 }
                );

            // Demonstrate how to Update the entity by changing the phone number
            Console.WriteLine("2. Update an existing Entity using the InsertOrMerge Upsert Operation.");
            entity.Title = "Hunger Games I";
            entity = InsertOrMergeEntityPollSubjectEntity(table, entity);

            // Demonstrate how to Read the updated entity using a point query
            Console.WriteLine("3. Reading the updated Entity.");
            entity = RetrieveEntityUsingPointQueryPollSubjectEntity(table, "00000001", "");

            // Demonstrate how to Delete an entity
            Console.WriteLine("4. Delete the entity. ");
            DeleteEntityPollSubjectEntity(table, entity);
        }

        private static void AdvancedTableOperationsPollSubjectEntity(CloudTable table)
        {
            // Demonstrate insert, update and batch table operations
            Console.WriteLine("4. Inserting a batch of entities. ");
            BatchInsertOfCustomerEntitiesPollSubjectEntity(table);

            // Query a range of data within a partition
            Console.WriteLine("5. Retrieving entities with surname of Smith and first names >= 1 and <= 75");
            PartitionRangeQueryPollSubjectEntity(table, "Smith", "0001", "0075");

            // Query for all the data within a partition
            Console.WriteLine("6. Retrieve entities with surname of Smith.");
            PartitionScanPollSubjectEntity(table, "Smith");
        }

        private static CloudStorageAccount CreateStorageAccountFromConnectionString(string storageConnectionString)
        {
            CloudStorageAccount storageAccount;
            try
            {
                storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the application.");
                throw;
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the sample.");
                Console.ReadLine();
                throw;
            }

            return storageAccount;
        }

        private static PollSubjectEntity InsertOrMergeEntityPollSubjectEntity(CloudTable table, PollSubjectEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            // Create the InsertOrReplace  TableOperation
            TableOperation insertOrMergeOperation = TableOperation.InsertOrMerge(entity);

            // Execute the operation.
            TableResult result = table.Execute(insertOrMergeOperation);
            PollSubjectEntity insertedCustomer = result.Result as PollSubjectEntity;
            return insertedCustomer;
        }

        private static PollSubjectEntity RetrieveEntityUsingPointQueryPollSubjectEntity(CloudTable table, string partitionKey, string rowKey)
        {
            TableOperation retrieveOperation = TableOperation.Retrieve<PollSubjectEntity>(partitionKey, rowKey);
            TableResult result = table.Execute(retrieveOperation);
            PollSubjectEntity entity = result.Result as PollSubjectEntity;
            if (entity != null)
            {
                Console.WriteLine("\t{0}\t{1}\t{2}\t{3}", entity.PartitionKey, entity.RowKey, entity.Title, entity.Year);
            }

            return entity;
        }

        private static void DeleteEntityPollSubjectEntity(CloudTable table, PollSubjectEntity deleteEntity)
        {
            if (deleteEntity == null)
            {
                throw new ArgumentNullException("deleteEntity");
            }

            TableOperation deleteOperation = TableOperation.Delete(deleteEntity);
            table.Execute(deleteOperation);
        }

        private static void BatchInsertOfCustomerEntitiesPollSubjectEntity(CloudTable table)
        {
            // Create the batch operation.
            TableBatchOperation batchOperation = new TableBatchOperation();

            // The following code  generates test data for use during the query samples.
            for (int i = 0; i < 100; i++)
            {
                batchOperation.InsertOrMerge(new PollSubjectEntity(new PollSubject { ID = 10000, Title = "Hunger Games (" + i + ")", Year = 2012 }) { RowKey = i.ToString() });
            }

            // Execute the batch operation.
            IList<TableResult> results = table.ExecuteBatch(batchOperation);

            foreach (var res in results)
            {
                var entityInserted = res.Result as PollSubjectEntity;
                Console.WriteLine("Inserted entity with\t Etag = {0} and PartitionKey = {1}, RowKey = {2}, Title = {3}", entityInserted.ETag, entityInserted.PartitionKey, entityInserted.RowKey, entityInserted.Title);
            }
        }

        private static void PartitionRangeQueryPollSubjectEntity(CloudTable table, string partitionKey, string startRowKey, string endRowKey)
        {
            // Create the range query using the fluid API
            TableQuery<PollSubjectEntity> rangeQuery = new TableQuery<PollSubjectEntity>().Where(
                TableQuery.CombineFilters(
                        TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey),
                        TableOperators.And,
                        TableQuery.CombineFilters(
                            TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.GreaterThanOrEqual, startRowKey),
                            TableOperators.And,
                            TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.LessThanOrEqual, endRowKey))));

            // Page through the results - requesting 50 results at a time from the server.
            TableContinuationToken token = null;
            rangeQuery.TakeCount = 50;
            do
            {
                TableQuerySegment<PollSubjectEntity> segment = table.ExecuteQuerySegmented(rangeQuery, token);
                token = segment.ContinuationToken;
                foreach (PollSubjectEntity entity in segment)
                {
                    Console.WriteLine("Customer: {0},{1}\t{2}\t{3}", entity.PartitionKey, entity.RowKey, entity.Title, entity.Year);
                }
            }
            while (token != null);
        }

        private static void PartitionScanPollSubjectEntity(CloudTable table, string partitionKey)
        {
            TableQuery<PollSubjectEntity> partitionScanQuery = new TableQuery<PollSubjectEntity>().Where
                (TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey));

            TableContinuationToken token = null;
            // Page through the results
            do
            {
                TableQuerySegment<PollSubjectEntity> segment = table.ExecuteQuerySegmented(partitionScanQuery, token);
                token = segment.ContinuationToken;
                foreach (PollSubjectEntity entity in segment)
                {
                    Console.WriteLine("Customer: {0},{1}\t{2}\t{3}", entity.PartitionKey, entity.RowKey, entity.Title, entity.Year);
                }
            }
            while (token != null);
        }

        private static void DeleteTablePollSubjectEntity(CloudTable table)
        {
            table.DeleteIfExists();
        }
    }
}