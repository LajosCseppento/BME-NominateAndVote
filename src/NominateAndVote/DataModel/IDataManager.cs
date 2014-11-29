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

        void DeleteNews(News news);

        #endregion News

        #region Nominations

        List<Nomination> QueryNominations(Poll poll);

        List<Nomination> QueryNominations(Poll poll, User user);

        void SaveNomination(Nomination nomination);

        void DeleteNomination(Nomination nomination);

        #endregion Nominations

        #region Polls

        List<Poll> QueryPolls();

        List<Poll> QueryPolls(PollState state);

        #endregion Polls

        #region PollSubject

        PollSubject QueryPollSubject(long id);

        List<PollSubject> SearchPollSubjects(string term);

        void SavePollSubject(PollSubject pollSubject);

        void SavePollSubjectsBatch(List<PollSubject> pollSubjects);

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

        //Lekerdezesek
        /*
         * Userek listája -> nem kell, csak a bannoltaké (QueryBannedUsers), valamint név szerint keresünk (SearchUsers)
         * User hozzaadasa -> SaveUser
         * Le van-e tiltva a felhasznalo -> QueryUser
         *
         * Aktiv pollok lekerdezese -> QueryPolls(PollState state) - gondolom itt arra gondoltál amire szavaznak
         * Lezarult pollok lekerdezese -> QueryPolls(PollState state)
         * Jeloles alatt levo pollok lekerdezese -> QueryPolls(PollState state)
         *
         * Userhez tartozo Nominationok megtekintese -> QueryNominations(Poll poll, User)
         * Nomination modositasok elkuldese - SaveNomination
         * Nomination leadasa - SaveNomination
         * Nomination torlese - DeleteNomination
         *
         * Vote elkuldese -> SaveVote
         * Annak lekerdezese, hogy a felhasznalo szavazott-e mar az adott pollra -> QueryVote
        */

        /* List<User> GetUsers();
         void AddUser(User user);
         Boolean IsBanned(User user);
         Boolean userIsVoted(User user, Poll poll);
         List<Nomination> getUserNomination(User user);
         void modifyNominations(List<Nomination> nominations, User user);
         Boolean userIsVoted(Poll poll, User user);

         List<Poll> getActivePolls();
         List<Poll> getClosedPolls();
         List<Poll> getPollsUnderNomination();
         void addNomination(Nomination nomination, Poll poll, User user);
         void addVote(Vote vote);

         List<PollSubject> getPollSubjects();
         List<Nomination> getPollNominations(Poll poll);

         List<News> getNews();
         void addNews(News news);*/
    }
}