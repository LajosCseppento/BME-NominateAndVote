using Microsoft.WindowsAzure.Storage;
using NominateAndVote.DataModel;
using NominateAndVote.DataModel.Common;
using NominateAndVote.DataModel.Poco;
using NominateAndVote.DataTableStorage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NominateAndVote.DataTableStorage
{
    public class TableStorageDataManager : TableStorageDataManagerBase, IDataManager
    {
        public TableStorageDataManager(CloudStorageAccount storageAccount)
            : base(storageAccount)
        {
        }

        public bool IsAdmin(User user)
        {
            // check
            if (user == null) { throw new ArgumentNullException("user", "The user must not be null"); }

            // query
            return RetrieveEntity(new AdministratorEntity(new Administrator { UserId = user.Id })) != null;
        }

        public List<News> QueryNews()
        {
            var q = from e in RetrieveEntities<NewsEntity>()
                    select e.ToPoco();
            return q.ToSortedList();
        }

        public News QueryNews(Guid id)
        {
            var entity = RetrieveEntity(new NewsEntity(new News { Id = id }));

            if (entity == null) { return null; }

            return entity.ToPoco();
        }

        public void SaveNews(News news)
        {
            // check
            if (news == null) { throw new ArgumentNullException("news", "The news must not be null"); }

            // new object, new ID
            if (news.Id == Guid.Empty) { news.Id = Guid.NewGuid(); }

            // save
            SaveEntity(new NewsEntity(news));
        }

        public void DeleteNews(Guid id)
        {
            DeleteEntity(new NewsEntity(new News { Id = id }));
        }

        public List<Nomination> QueryNominations(Poll poll)
        {
            // check
            if (poll == null) { throw new ArgumentNullException("poll", "The poll must not be null"); }

            var dbPoll = RetrieveEntity(new PollEntity(poll));
            if (dbPoll == null) { throw new DataException("The given poll does not exists") { DataElement = poll }; }

            // query
            var q = from e in RetrieveEntitiesByPartition(new NominationEntity(new Nomination { Poll = dbPoll.ToPoco(), User = new User(), Subject = new PollSubject() }))
                    select e.ToPoco();
            return q.ToSortedList();
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
            // check
            if (nomination == null) { throw new ArgumentNullException("nomination", "The nomination must not be null"); }

            // new object, new ID
            if (nomination.Id == Guid.Empty) { nomination.Id = Guid.NewGuid(); }

            // save
            SaveEntity(new NominationEntity(nomination));
        }

        public void DeleteNomination(Guid id)
        {
            DeleteEntity(new NominationEntity(new Nomination { Id = id }));
        }

        public List<Poll> QueryPolls()
        {
            var q = from e in RetrieveEntities<PollEntity>()
                    select e.ToPoco();
            return q.ToSortedList();
        }

        public Poll QueryPoll(Guid id)
        {
            var entity = RetrieveEntity(new PollEntity(new Poll { Id = id }));

            if (entity == null) { return null; }

            return entity.ToPoco();
        }

        public List<Poll> QueryPolls(PollState state)
        {
            var q = from e in RetrieveEntitiesByPartition(new PollEntity(new Poll { State = state }))
                    select e.ToPoco();
            return q.ToSortedList();
        }

        public void SavePoll(Poll poll)
        {
            // check
            if (poll == null) { throw new ArgumentNullException("poll", "The poll must not be null"); }

            // new object, new ID
            if (poll.Id == Guid.Empty) { poll.Id = Guid.NewGuid(); }

            // save
            SaveEntity(new PollEntity(poll));
        }

        public PollSubject QueryPollSubject(long id)
        {
            var entity = RetrieveEntity(new PollSubjectEntity(new PollSubject { Id = id }));

            if (entity == null) { return null; }

            return entity.ToPoco();
        }

        public List<PollSubject> SearchPollSubjects(string term)
        {
            throw new NotImplementedException();
        }

        public void SavePollSubject(PollSubject pollSubject)
        {
            // check
            if (pollSubject == null) { throw new ArgumentNullException("pollSubject", "The poll subject must not be null"); }

            // save
            SaveEntity(new PollSubjectEntity(pollSubject));
        }

        public void SavePollSubjectsBatch(IEnumerable<PollSubject> pollSubjects)
        {
            throw new NotImplementedException();
        }

        public List<User> QueryBannedUsers()
        {
            throw new NotImplementedException();
        }

        public User QueryUser(long id)
        {
            var entity = RetrieveEntity(new UserEntity(new User { Id = id }));

            if (entity == null) { return null; }

            return entity.ToPoco();
        }

        public List<User> SearchUsers(string term)
        {
            throw new NotImplementedException();
        }

        public void SaveUser(User user)
        {
            // check
            if (user == null) { throw new ArgumentNullException("user", "The user must not be null"); }

            // save
            SaveEntity(new UserEntity(user));
        }

        public Vote QueryVote(Poll poll, User user)
        {
            throw new NotImplementedException();
        }

        public void SaveVote(Vote vote)
        {
            // check
            if (vote == null) { throw new ArgumentNullException("vote", "The vote must not be null"); }

            // save
            SaveEntity(new VoteEntity(vote));
        }
    }
}