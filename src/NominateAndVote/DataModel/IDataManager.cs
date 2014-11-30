using NominateAndVote.DataModel.Model;
using System;
using System.Collections.Generic;

namespace NominateAndVote.DataModel
{
    public interface IDataManager
    {
        #region Administrator

        bool IsAdmin(User user);

        #endregion Administrator

        #region News

        List<News> QueryNews();

        News QueryNews(Guid id);

        void SaveNews(News news);

        void DeleteNews(Guid id);

        #endregion News

        #region Nominations

        List<Nomination> QueryNominations(Poll poll);

        List<Nomination> QueryNominations(Poll poll, User user);

        List<Nomination> QueryNominations(User user);

        void SaveNomination(Nomination nomination);

        void DeleteNomination(Guid id);

        #endregion Nominations

        #region Polls

        List<Poll> QueryPolls();

        List<Poll> QueryPolls(PollState state);

        Poll QueryPoll(Guid id);

        void SavePoll(Poll poll);

        #endregion Polls

        #region PollSubject

        PollSubject QueryPollSubject(long id);

        List<PollSubject> SearchPollSubjects(string term);

        void SavePollSubject(PollSubject pollSubject);

        void SavePollSubjectsBatch(IEnumerable<PollSubject> pollSubjects);

        #endregion PollSubject

        #region User

        List<User> QueryBannedUsers();

        User QueryUser(Guid id);

        List<User> SearchUsers(string term);

        void SaveUser(User user);

        #endregion User

        #region Vote

        Vote QueryVote(Poll poll, User user);

        void SaveVote(Vote vote);

        #endregion Vote
    }
}