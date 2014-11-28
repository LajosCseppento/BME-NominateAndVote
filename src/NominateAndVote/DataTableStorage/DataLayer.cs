namespace NominateAndVote.DataTableStorage
{
    // TODO
    // Ported from the old project
    public interface DataLayer
    {
        //Lekerdezesek
        /*
         * Userek listája
         * User hozzaadasa
         * Le van-e tiltva a felhasznalo
         *
         * Aktiv pollok lekerdezese
         * Lezarult pollok lekerdezese
         * Jeloles alatt levo pollok lekerdezese
         *
         * Userhez tartozo Nominationok megtekintese
         * Nomination modositasok elkuldese
         * Nomination leadasa
         *
         * Vote elkuldese
         * Annak lekerdezese, hogy a felhasznalo szavazott-e mar az adott pollra
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