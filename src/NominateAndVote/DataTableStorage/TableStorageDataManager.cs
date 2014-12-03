using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using NominateAndVote.DataModel;
using NominateAndVote.DataModel.Common;
using NominateAndVote.DataModel.Poco;
using NominateAndVote.DataTableStorage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

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

        public void DeleteNews(News news)
        {
            DeleteEntity(new NewsEntity(news));
        }

        public List<Nomination> QueryNominations(Poll poll)
        {
            // check
            if (poll == null) { throw new ArgumentNullException("poll", "The poll must not be null"); }

            var dbPoll = RetrieveEntity(new PollEntity(poll));
            if (dbPoll == null) { throw new DataException("The given poll does not exists") { DataElement = poll }; }

            // query
            var q = from e in RetrieveEntitiesByPartition<NominationEntity>(dbPoll.Id.ToString())
                    select e.ToPoco();
            var nominations = q.ToList();
            FillNominationObjects(nominations, dbPoll.ToPoco());
            return nominations.ToSortedList();
        }

        public List<Nomination> QueryNominations(Poll poll, User user)
        {
            // check
            if (poll == null) { throw new ArgumentNullException("poll", "The poll must not be null"); }
            if (user == null) { throw new ArgumentNullException("user", "The user must not be null"); }

            var dbPoll = RetrieveEntity(new PollEntity(poll));
            var dbUser = RetrieveEntity(new UserEntity(user));
            if (dbPoll == null) { throw new DataException("The given poll does not exists") { DataElement = poll }; }
            if (dbUser == null) { throw new DataException("The given user does not exists") { DataElement = user }; }

            // query
            var userFilter = TableQuery.GenerateFilterConditionForLong("UserId", QueryComparisons.Equal, dbUser.Id);
            var q = from e in RetrieveEntitiesByPartition<NominationEntity>(dbPoll.Id.ToString(), userFilter)
                    select e.ToPoco();
            var nominations = q.ToList();
            FillNominationObjects(nominations, dbPoll.ToPoco());
            return nominations.ToSortedList();
        }

        public List<Nomination> QueryNominations(User user)
        {
            // check
            if (user == null) { throw new ArgumentNullException("user", "The user must not be null"); }

            var dbUser = RetrieveEntity(new UserEntity(user));
            if (dbUser == null) { throw new DataException("The given user does not exists") { DataElement = user }; }

            // query
            var userFilter = TableQuery.GenerateFilterConditionForLong("UserId", QueryComparisons.Equal, dbUser.Id);
            var q = from e in RetrieveEntitiesByFilter<NominationEntity>(userFilter)
                    select e.ToPoco();
            var nominations = q.ToList();
            FillNominationObjects(nominations);
            return nominations.ToSortedList();
        }

        private void FillNominationObjects(List<Nomination> nominations, Poll dbPoll = null)
        {
            // connect poll
            if (dbPoll != null)
            {
                foreach (var nomination in nominations) { nomination.Poll = dbPoll; }
            }
            else
            {
                foreach (var nomination in nominations) { nomination.Poll = QueryPoll(nomination.Poll.Id); }
            }

            // connect poll subject
            var ids = new Dictionary<long, List<Nomination>>();

            foreach (var nomination in nominations)
            {
                var subjectId = nomination.Subject.Id;
                List<Nomination> subList;
                if (ids.TryGetValue(subjectId, out subList))
                {
                    subList.Add(nomination);
                }
                else
                {
                    ids.Add(subjectId, new List<Nomination> { nomination });
                }
            }

            foreach (var entry in ids)
            {
                var subject = QueryPollSubject(entry.Key);

                foreach (var nomination in entry.Value)
                {
                    nomination.Subject = subject;
                }
            }
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

        public void DeleteNomination(Nomination nomination)
        {
            DeleteEntity(new NominationEntity(nomination));
        }

        public List<Poll> QueryPolls()
        {
            var q = from e in RetrieveEntities<PollEntity>()
                    select e.ToPoco();
            return q.ToSortedList();
        }

        public Poll QueryPoll(Guid id)
        {
            var filter = TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, id.ToString());
            var entities = RetrieveEntitiesByFilter<PollEntity>(filter);
            var entity = entities.FirstOrDefault();

            if (entity == null) { return null; }

            return entity.ToPoco();
        }

        public List<Poll> QueryPolls(PollState state)
        {
            var q = from e in RetrieveEntitiesByPartition<PollEntity>(state.ToString())
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
            // check
            if (term == null) { throw new ArgumentNullException("term", "The search term must not be null"); }

            // query
            var filter = CreateSearchFilter("Title", term);
            var q = from e in RetrieveEntitiesByFilter<PollSubjectEntity>(filter)
                    select e.ToPoco();
            return q.ToSortedList();
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
            // check
            if (pollSubjects == null) { throw new ArgumentNullException("pollSubjects", "The poll subjects must not be null"); }

            // save
            var q = pollSubjects.Where(ps => ps != null);
            var list = q.ToList();

            var toProcess = list.Count();
            using (var resetEvent = new ManualResetEvent(false))
            {
                WaitCallback waitCallback = (
                    ps =>
                    {
                        SaveEntity(new PollSubjectEntity((PollSubject)ps));
                        if (Interlocked.Decrement(ref toProcess) == 0)
                        {
                            resetEvent.Set();
                        }
                    });

                foreach (var pollSubject in list)
                {
                    ThreadPool.QueueUserWorkItem(waitCallback, pollSubject);
                }

                resetEvent.WaitOne();
            }
        }

        public List<User> QueryBannedUsers()
        {
            var filter = TableQuery.GenerateFilterConditionForBool("IsBanned", QueryComparisons.Equal, true);
            var q = from e in RetrieveEntitiesByFilter<UserEntity>(filter)
                    select e.ToPoco();
            return q.ToSortedList();
        }

        public User QueryUser(long id)
        {
            var entity = RetrieveEntity(new UserEntity(new User { Id = id }));

            if (entity == null) { return null; }

            return entity.ToPoco();
        }

        public List<User> SearchUsers(string term)
        {
            // check
            if (term == null) { throw new ArgumentNullException("term", "The search term must not be null"); }

            // query
            var filter = CreateSearchFilter("Name", term);
            var q = from e in RetrieveEntitiesByFilter<UserEntity>(filter)
                    select e.ToPoco();
            return q.ToSortedList();
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
            // check
            if (poll == null) { throw new ArgumentNullException("poll", "The poll must not be null"); }
            if (user == null) { throw new ArgumentNullException("user", "The user must not be null"); }

            var dbPoll = RetrieveEntity(new PollEntity(poll));
            var dbUser = RetrieveEntity(new UserEntity(user));
            if (dbPoll == null) { throw new DataException("The given poll does not exists") { DataElement = poll }; }
            if (dbUser == null) { throw new DataException("The given user does not exists") { DataElement = user }; }

            // get nominations
            var nominations = QueryNominations(poll);

            // determine whether the user has a vote for a nomination

            foreach (var nomination in nominations)
            {
                // query
                var vote = new VoteEntity(new Vote { User = user, Nomination = nomination });
                vote = RetrieveEntity(vote);

                if (vote != null)
                {
                    // found
                    var votePoco = vote.ToPoco();
                    votePoco.Nomination = nomination;
                    return votePoco;
                }
            }

            // not found, not voted yet
            return null;
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