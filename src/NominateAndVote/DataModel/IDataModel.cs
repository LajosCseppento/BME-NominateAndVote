using NominateAndVote.DataModel.Common;
using NominateAndVote.DataModel.Poco;
using System;

namespace NominateAndVote.DataModel
{
    public interface IDataModel
    {
        PocoStore<Administrator> Administrators { get; }

        PocoWithIdStore<Guid, News> News { get; }

        PocoWithIdStore<Guid, Nomination> Nominations { get; }

        PocoWithIdStore<Guid, Poll> Polls { get; }

        PocoWithIdStore<long, PollSubject> PollSubjects { get; }

        PocoWithIdStore<long, User> Users { get; }

        PocoStore<Vote> Votes { get; }

        void Clear();

        void RefreshPocoRelationalLists();
    }
}