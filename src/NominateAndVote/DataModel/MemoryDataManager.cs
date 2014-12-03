using NominateAndVote.DataModel.Common;
using NominateAndVote.DataModel.Poco;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NominateAndVote.DataModel
{
    public class MemoryDataManager : IDataManager
    {
        private IDataModel DataModel { get; set; }

        public MemoryDataManager(IDataModel dataModel)
        {
            if (dataModel == null) { throw new ArgumentNullException("dataModel", "The data model must not be null"); }

            DataModel = dataModel;
        }

        public bool IsAdmin(User user)
        {
            if (user == null) { throw new ArgumentNullException("user", "The user must not be null"); }

            return DataModel.Administrators.Any(a => (a.UserId == user.Id));
        }

        public List<News> QueryNews()
        {
            return DataModel.News.ToSortedList();
        }

        public News QueryNews(Guid id)
        {
            return DataModel.News.Get(id);
        }

        public void SaveNews(News news)
        {
            // check
            if (news == null) { throw new ArgumentNullException("news", "The news must not be null"); }

            // new object, new ID
            if (news.Id == Guid.Empty) { news.Id = Guid.NewGuid(); }

            // save
            DataModel.News.AddOrUpdate(news);

            // refresh
            DataModel.RefreshPocoRelationalLists();
        }

        public void DeleteNews(Guid id)
        {
            // remove
            DataModel.News.Remove(id);

            // refresh
            DataModel.RefreshPocoRelationalLists();
        }

        public List<Nomination> QueryNominations(Poll poll)
        {
            // check
            if (poll == null) { throw new ArgumentNullException("poll", "The poll must not be null"); }

            var dbPoll = DataModel.Polls.Get(poll);
            if (dbPoll == null) { throw new DataException("The given poll does not exists") { DataElement = poll }; }

            // query
            return dbPoll.Nominations.ToSortedList();
        }

        public List<Nomination> QueryNominations(Poll poll, User user)
        {
            // check
            if (poll == null) { throw new ArgumentNullException("poll", "The poll must not be null"); }
            if (user == null) { throw new ArgumentNullException("user", "The user must not be null"); }

            var dbPoll = DataModel.Polls.Get(poll);
            var dbUser = DataModel.Users.Get(user);
            if (dbPoll == null) { throw new DataException("The given poll does not exists") { DataElement = poll }; }
            if (dbUser == null) { throw new DataException("The given user does not exists") { DataElement = user }; }

            // query
            var q = dbPoll.Nominations.Where(n => (n.User != null && n.User.Id == dbUser.Id));
            return q.ToSortedList();
        }

        public List<Nomination> QueryNominations(User user)
        {
            // check
            if (user == null) { throw new ArgumentNullException("user", "The user must not be null"); }

            var dbUser = DataModel.Users.Get(user);
            if (dbUser == null) { throw new DataException("The given user does not exists") { DataElement = user }; }

            // query
            return dbUser.Nominations.ToSortedList();
        }

        public void SaveNomination(Nomination nomination)
        {
            // check
            if (nomination == null) { throw new ArgumentNullException("nomination", "The nomination must not be null"); }

            // new object, new ID
            if (nomination.Id == Guid.Empty) { nomination.Id = Guid.NewGuid(); }

            // save
            DataModel.Nominations.AddOrUpdate(nomination);

            // refresh
            DataModel.RefreshPocoRelationalLists();
        }

        public void DeleteNomination(Guid id)
        {
            // remove
            DataModel.Nominations.Remove(id);

            // refresh
            DataModel.RefreshPocoRelationalLists();
        }

        public List<Poll> QueryPolls()
        {
            return DataModel.Polls.ToSortedList();
        }

        public Poll QueryPoll(Guid id)
        {
            return DataModel.Polls.Get(id);
        }

        public List<Poll> QueryPolls(PollState state)
        {
            var q = DataModel.Polls.Where(p => (p.State == state));
            return q.ToSortedList();
        }

        public void SavePoll(Poll poll)
        {
            // check
            if (poll == null) { throw new ArgumentNullException("poll", "The poll must not be null"); }

            // new object, new ID
            if (poll.Id == Guid.Empty) { poll.Id = Guid.NewGuid(); }

            // save
            DataModel.Polls.AddOrUpdate(poll);

            // refresh
            DataModel.RefreshPocoRelationalLists();
        }

        public PollSubject QueryPollSubject(long id)
        {
            return DataModel.PollSubjects.Get(id);
        }

        public List<PollSubject> SearchPollSubjects(string term)
        {
            // check
            if (term == null) { throw new ArgumentNullException("term", "The search term must not be null"); }

            // query
            var q = DataModel.PollSubjects.Where(ps => (ps.Title.StartsWith(term)));
            return q.ToSortedList();
        }

        public void SavePollSubject(PollSubject pollSubject)
        {
            // check
            if (pollSubject == null) { throw new ArgumentNullException("pollSubject", "The poll subject must not be null"); }

            // save
            DataModel.PollSubjects.AddOrUpdate(pollSubject);

            // refresh
            DataModel.RefreshPocoRelationalLists();
        }

        public void SavePollSubjectsBatch(IEnumerable<PollSubject> pollSubjects)
        {
            // check
            if (pollSubjects == null) { throw new ArgumentNullException("pollSubjects", "The poll subjects must not be null"); }

            // save
            foreach (var ps in pollSubjects)
            {
                DataModel.PollSubjects.AddOrUpdate(ps);
            }

            // refresh
            DataModel.RefreshPocoRelationalLists();
        }

        public List<User> QueryBannedUsers()
        {
            var q = DataModel.Users.Where(u => u.IsBanned);
            return q.ToSortedList();
        }

        public User QueryUser(long id)
        {
            return DataModel.Users.Get(id);
        }

        public List<User> SearchUsers(string term)
        {
            // check
            if (term == null) { throw new ArgumentNullException("term", "The search term must not be null"); }

            // query
            var q = DataModel.Users.Where(u => u.Name.StartsWith(term));
            return q.ToSortedList();
        }

        public void SaveUser(User user)
        {
            // check
            if (user == null) { throw new ArgumentNullException("user", "The user must not be null"); }

            // save
            DataModel.Users.AddOrUpdate(user);

            // refresh
            DataModel.RefreshPocoRelationalLists();
        }

        public Vote QueryVote(Poll poll, User user)
        {
            // check
            if (poll == null) { throw new ArgumentNullException("poll", "The poll must not be null"); }
            if (user == null) { throw new ArgumentNullException("user", "The user must not be null"); }

            // query
            var q = from v in DataModel.Votes
                    where v.Nomination.Poll.Id == poll.Id && v.User.Id == user.Id
                    select v;

            return q.SingleOrDefault();
        }

        public void SaveVote(Vote vote)
        {
            // check
            if (vote == null) { throw new ArgumentNullException("vote", "The vote must not be null"); }

            // save
            DataModel.Votes.AddOrUpdate(vote);

            // refresh
            DataModel.RefreshPocoRelationalLists();
        }
    }
}