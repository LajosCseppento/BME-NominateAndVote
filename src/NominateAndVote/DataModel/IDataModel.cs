using NominateAndVote.DataModel.Model;
using System.Collections.Generic;

namespace NominateAndVote.DataModel
{
    public interface IDataModel
    {
        List<Administrator> Administrators { get; }

        List<News> News { get; }

        List<Nomination> Nominations { get; }

        List<Poll> Polls { get; }

        List<PollSubject> PollSubjects { get; }

        List<User> Users { get; }

        List<Vote> Votes { get; }

        void Clear();

        void RefreshPocoRelationalLists();
    }
}