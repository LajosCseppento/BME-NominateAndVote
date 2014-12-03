using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NominateAndVote.DataTableStorage
{
    public abstract class TableStorageDataManagerBase
    {
        private readonly CloudStorageAccount _storageAccount;
        private readonly CloudTableClient _tableClient;

        public TableStorageDataManagerBase(CloudStorageAccount storageAccount)
        {
            if (storageAccount == null)
            {
                throw new ArgumentNullException("storageAccount", "The storage account must not be null");
            }

            _storageAccount = storageAccount;
            _tableClient = storageAccount.CreateCloudTableClient();
        }

        public void CreateTablesIfNeeded()
        {
            foreach (var entityType in TableNames.GetEntityTypes())
            {
                GetTableReference(entityType).CreateIfNotExists();
            }
        }

        public void DeleteTablesIfNeeded()
        {
            foreach (var entityType in TableNames.GetEntityTypes())
            {
                GetTableReference(entityType).DeleteIfExists();
            }
        }

        private CloudTable GetTableReference(Type entityType)
        {
            if (entityType == null)
            {
                throw new ArgumentNullException("entityType", "The entity type must not be null");
            }

            return _tableClient.GetTableReference(TableNames.GetTableName(entityType));
        }

        protected TEntity RetrieveEntity<TEntity>(TEntity entity) where TEntity : class, ITableEntity
        {
            return RetrieveEntity<TEntity>(entity.PartitionKey, entity.RowKey);
        }

        private TEntity RetrieveEntity<TEntity>(string partitionKey, string rowKey) where TEntity : class, ITableEntity
        {
            var table = GetTableReference(typeof(TEntity));

            var retrieveOperation = TableOperation.Retrieve<TEntity>(partitionKey, rowKey);
            var result = table.Execute(retrieveOperation);
            var entity = result.Result as TEntity;

            return entity;
        }

        protected List<TEntity> RetrieveEntities<TEntity>() where TEntity : ITableEntity, new()
        {
            var table = GetTableReference(typeof(TEntity));

            TableContinuationToken token = null;
            var entities = new List<TEntity>();
            do
            {
                var result = table.ExecuteQuerySegmented(new TableQuery<TEntity>(), token);
                entities.AddRange(result);

                token = result.ContinuationToken;
            } while (token != null);

            return entities;
        }

        protected List<TEntity> RetrieveEntitiesByPartition<TEntity>(TEntity search) where TEntity : ITableEntity, new()
        {
            var filter = TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal,
                search.PartitionKey);
            return RetrieveEntitiesByFilter<TEntity>(filter);
        }

        private List<TEntity> RetrieveEntitiesByFilter<TEntity>(string filter) where TEntity : ITableEntity, new()
        {
            var table = GetTableReference(typeof(TEntity));

            TableContinuationToken token = null;
            var entities = new List<TEntity>();
            do
            {
                var query = new TableQuery<TEntity>().Where(filter);
                var result = table.ExecuteQuerySegmented(query, token);
                entities.AddRange(result);

                token = result.ContinuationToken;
            } while (token != null);

            return entities;
        }

        public void SaveEntity(ITableEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity", "The entity must not be null");
            }

            var table = GetTableReference(entity.GetType());

            var op = TableOperation.InsertOrReplace(entity);
            table.Execute(op);
        }

        public void DeleteEntity(ITableEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity", "The entity must not be null");
            }

            var table = GetTableReference(entity.GetType());

            entity.ETag = "*";
            var op = TableOperation.Delete(entity);
            try
            {
                table.Execute(op);
            }
            catch (StorageException exception)
            {
                if (exception.RequestInformation.HttpStatusCode == 404)
                {
                    // not found, no problem
                    return;
                }
                throw;
            }
        }
    }
}