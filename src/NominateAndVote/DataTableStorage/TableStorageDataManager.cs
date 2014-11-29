using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using NominateAndVote.DataModel;
using NominateAndVote.DataModel.Model;
using System;
using System.Collections.Generic;

namespace NominateAndVote.DataTableStorage
{
    public class TableStorageDataManager : IDataManager
    {
        private CloudStorageAccount storageAccount;
        private CloudTableClient tableClient;

        public TableStorageDataManager(CloudStorageAccount storageAccount)
        {
            if (storageAccount == null)
            {
                throw new ArgumentException("The storage account must not be null", "storageAccount");
            }

            this.storageAccount = storageAccount;
            this.tableClient = storageAccount.CreateCloudTableClient();
        }

        public bool CreateTablesIfNeeded()
        {
            bool allOk = true;
            foreach (var entityType in TableNames.GetEntityTypes())
            {
                bool ok = getTableReference(entityType).CreateIfNotExists();
                allOk = ok && allOk;
            }

            return allOk;
        }

        public bool DeleteTablesIfNeeded()
        {
            bool allOk = true;
            foreach (var entityType in TableNames.GetEntityTypes())
            {
                bool ok = getTableReference(entityType).DeleteIfExists();
                allOk = ok && allOk;
            }

            return allOk;
        }

        private bool SaveEntity(ITableEntity entity)
        {
            CloudTable table = getTableReference(entity.GetType());

            TableOperation op = TableOperation.InsertOrReplace(entity);
            TableResult result = table.Execute(op);
            return result.Result.Equals(entity);
        }

        private CloudTable getTableReference(Type entityType)
        {
            return tableClient.GetTableReference(TableNames.GetTableName(entityType));
        }

        public bool IsAdmin(User user)
        {
            throw new NotImplementedException();
        }

        public List<News> QueryNews()
        {
            throw new NotImplementedException();
        }

        public News QueryNews(Guid id)
        {
            throw new NotImplementedException();
        }

        public void SaveNews(News news)
        {
            throw new NotImplementedException();
        }

        public void DeleteNews(News news)
        {
            throw new NotImplementedException();
        }

        public List<Nomination> QueryNominations(Poll poll)
        {
            throw new NotImplementedException();
        }

        public List<Nomination> QueryNominations(Poll poll, User user)
        {
            throw new NotImplementedException();
        }

        public List<Nomination> QueryNominations(User user) 
        {
            throw new NotImplementedException();
        }

        public void SaveNomination(Nomination nomination)
        {
            throw new NotImplementedException();
        }

        public void DeleteNomination(Nomination nomination)
        {
            throw new NotImplementedException();
        }

        public List<Poll> QueryPolls()
        {
            throw new NotImplementedException();
        }

        public Poll QueryPoll(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Poll> QueryPolls(PollState state)
        {
            throw new NotImplementedException();
        }

        public void SavePoll(Poll poll) {
            throw new NotImplementedException();
        }

        public PollSubject QueryPollSubject(long id)
        {
            throw new NotImplementedException();
        }

        public List<PollSubject> SearchPollSubjects(string term)
        {
            throw new NotImplementedException();
        }

        public void SavePollSubject(PollSubject pollSubject)
        {
            throw new NotImplementedException();
        }

        public void SavePollSubjectsBatch(List<PollSubject> pollSubjects)
        {
            throw new NotImplementedException();
        }

        public List<User> QueryBannedUsers()
        {
            throw new NotImplementedException();
        }

        public User QueryUser(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<User> SearchUsers(string term)
        {
            throw new NotImplementedException();
        }

        public void SaveUser(User user)
        {
            throw new NotImplementedException();
        }

        public Vote QueryVote(Poll poll, User user)
        {
            throw new NotImplementedException();
        }

        public void SaveVote(Vote vote)
        {
            throw new NotImplementedException();
        }
    }
}