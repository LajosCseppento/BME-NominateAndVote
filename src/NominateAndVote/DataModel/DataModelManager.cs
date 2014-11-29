using NominateAndVote.DataModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NominateAndVote.DataModel
{
    public class DataModelManager : IDataManager
    {
        public IDataModel DataModel { get; private set; }

        public DataModelManager(IDataModel dataModel)
        {
            if (dataModel == null)
            {
                throw new ArgumentNullException("The data model must not be null", "dataModel");
            }

            DataModel = dataModel;
        }

        public bool IsAdmin(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("The user must not be null", "user");
            }

            return (from a in DataModel.Administrators
                    where a.UserID.Equals(user.ID)
                    select a).Count() > 0;
        }

        public List<News> QueryNews()
        {
            return (from n in DataModel.News
                    orderby n.PublicationDate descending
                    select n).ToList();
        }

        public News QueryNews(Guid id)
        {
            return (from n in DataModel.News
                    where n.ID.Equals(id)
                    select n).SingleOrDefault();
        }

        public void SaveNews(News news)
        {
            if (news == null)
            {
                throw new ArgumentNullException("The news must not be null", "news");
            }

            if (news.ID.Equals(Guid.Empty))
            {
                // now ID, new object
                news.ID = Guid.NewGuid();
            }
            else
            {
                // maybe a new, maybe an old object
                News oldNews = (from n in DataModel.News
                                where n.ID.Equals(news.ID)
                                select n).SingleOrDefault();

                if (oldNews != null)
                {
                    // remove old
                    DataModel.News.Remove(oldNews);
                }
            }

            // add new
            DataModel.News.Add(news);
        }

        public void DeleteNews(News news)
        {
            if (news == null)
            {
                throw new ArgumentNullException("The news must not be null", "news");
            }

            DataModel.News.Remove(news);
        }

        public List<Nomination> QueryNominations(Poll poll)
        {
            if (poll == null)
            {
                throw new ArgumentNullException("The poll must not be null", "poll");
            }

            return (from n in poll.Nominations
                    orderby n.Subject.Title, n.Subject.Year
                    select n).ToList();
        }

        public List<Nomination> QueryNominations(Poll poll, User user)
        {
            if (poll == null)
            {
                throw new ArgumentNullException("The poll must not be null", "poll");
            }
            else if (user == null)
            {
                throw new ArgumentNullException("The user must not be null", "user");
            }

            return (from n in poll.Nominations
                    where n.User != null && n.User.ID.Equals(user.ID)
                    orderby n.Subject.Title, n.Subject.Year
                    select n).ToList();
        }

        public List<Nomination> QueryNominations(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("The user must not be null", "user");
            }

            return (from n in user.Nominations
                    select n).ToList();
        }

        public void SaveNomination(Nomination nomination)
        {
            if (nomination == null)
            {
                throw new ArgumentNullException("The nomination must not be null", "nomination");
            }

            if (nomination.ID.Equals(Guid.Empty))
            {
                // now ID, new object
                nomination.ID = Guid.NewGuid();
            }
            else
            {
                // maybe a new, maybe an old object
                Nomination oldNomination = (from n in DataModel.Nominations
                                            where n.ID.Equals(nomination.ID)
                                            select n).SingleOrDefault();

                if (oldNomination != null)
                {
                    // remove old
                    DataModel.Nominations.Remove(oldNomination);
                }
            }

            // add new
            DataModel.Nominations.Add(nomination);
        }

        public void DeleteNomination(Nomination nomination)
        {
            if (nomination == null)
            {
                throw new ArgumentNullException("The nomination must not be null", "nomination");
            }

            DataModel.Nominations.Remove(nomination);
        }

        public List<Poll> QueryPolls()
        {
            return (from p in DataModel.Polls
                    orderby p.AnnouncementDate descending
                    select p).ToList();
        }

        public Poll QueryPoll(Guid id)
        {
            return (from p in DataModel.Polls
                    where p.ID.Equals(id)
                    select p).SingleOrDefault();
        }

        public List<Poll> QueryPolls(PollState state)
        {
            return (from p in DataModel.Polls
                    where p.State.Equals(state)
                    orderby p.AnnouncementDate descending
                    select p).ToList();
        }

        public void SavePoll(Poll poll) {
            if (poll == null)
            {
                throw new ArgumentNullException("The poll must not be null", "poll");
            }

            if (poll.ID.Equals(Guid.Empty))
            {
                // now ID, new object
                poll.ID = Guid.NewGuid();
            }
            else
            {
                // maybe a new, maybe an old object
                Poll oldPoll = (from p in DataModel.Polls
                                            where p.ID.Equals(poll.ID)
                                            select p).SingleOrDefault();

                if (oldPoll != null)
                {
                    // remove old
                    DataModel.Polls.Remove(oldPoll);
                }
            }

            // add new
            DataModel.Polls.Add(poll);
        }

        public PollSubject QueryPollSubject(long id)
        {
            return (from ps in DataModel.PollSubjects
                    where ps.ID == id
                    select ps).SingleOrDefault();
        }

        public List<PollSubject> SearchPollSubjects(string term)
        {
            if (term == null)
            {
                throw new ArgumentNullException("The search term must not be null", "term");
            }

            return (from ps in DataModel.PollSubjects
                    where ps.Title.StartsWith(term)
                    select ps).ToList();
        }

        public void SavePollSubject(PollSubject pollSubject)
        {
            if (pollSubject == null)
            {
                throw new ArgumentNullException("The poll subject must not be null", "pollSubject");
            }

            // maybe a new, maybe an old object
            PollSubject oldPollSubject = (from ps in DataModel.PollSubjects
                                          where ps.ID == pollSubject.ID
                                          select ps).SingleOrDefault();

            if (oldPollSubject != null)
            {
                // remove old
                DataModel.PollSubjects.Remove(oldPollSubject);
            }

            // add new
            DataModel.PollSubjects.Add(pollSubject);
        }

        public void SavePollSubjectsBatch(List<PollSubject> pollSubjects)
        {
            // it makes sense during the long import into the cloud
            foreach (var ps in pollSubjects)
            {
                SavePollSubject(ps);
            }
        }

        public List<User> QueryBannedUsers()
        {
            return (from u in DataModel.Users
                    where u.IsBanned
                    select u).ToList();
        }

        public User QueryUser(Guid id)
        {
            return (from u in DataModel.Users
                    where u.ID.Equals(id)
                    select u).SingleOrDefault();
        }

        public List<User> SearchUsers(string term)
        {
            if (term == null)
            {
                throw new ArgumentNullException("The search term must not be null", "term");
            }

            return (from u in DataModel.Users
                    where u.Name.StartsWith(term)
                    select u).ToList();
        }

        public void SaveUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("The user must not be null", "user");
            }

            if (user.ID.Equals(Guid.Empty))
            {
                // now ID, new object
                user.ID = Guid.NewGuid();
            }
            else
            {
                // maybe a new, maybe an old object
                User oldUser = (from n in DataModel.Users
                                where n.ID.Equals(user.ID)
                                select n).SingleOrDefault();

                if (oldUser != null)
                {
                    // remove old
                    DataModel.Users.Remove(oldUser);
                }
            }

            // add new
            DataModel.Users.Add(user);
        }

        public Vote QueryVote(Poll poll, User user)
        {
            if (poll == null)
            {
                throw new ArgumentNullException("The poll must not be null", "poll");
            }
            else if (user == null)
            {
                throw new ArgumentNullException("The user must not be null", "user");
            }

            return (from v in DataModel.Votes
                    where v.Nomination.Poll.ID.Equals(poll.ID) && v.User.ID.Equals(user.ID)
                    select v).SingleOrDefault();
        }

        public void SaveVote(Vote vote)
        {
            if (vote == null)
            {
                throw new ArgumentNullException("The vote must not be null", "vote");
            }

            // maybe a new, maybe an old object
            Vote oldVote = (from v in DataModel.Votes
                            where v.Nomination.ID.Equals(vote.Nomination.ID) && v.User.ID.Equals(vote.User.ID)
                            select v).SingleOrDefault();

            if (oldVote != null)
            {
                // remove old
                DataModel.Votes.Remove(oldVote);
            }

            // add new
            DataModel.Votes.Add(vote);
        }
    }
}