using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NominateAndVote.DataModel.Model;
using NominateAndVote.DataModel;

namespace DataModel.Tests
{
    [TestClass]
    public class NominateAndVoteModelTest
    {
        [TestMethod]
        public void NominationAddToPoll()
        {
            NominateAndVoteModel d = new NominateAndVoteModel();

            Poll p = new Poll();
            Nomination n = new Nomination() { Poll = p };

            d.Polls.Add(p);
            d.Nominations.Add(n);
            d.RefreshPocoRelationalLists();

            Assert.IsTrue(p.Nominations.Contains(n));
        }

        [TestMethod]
        public void NominationsAddToPoll()
        {
            NominateAndVoteModel d = new NominateAndVoteModel();

            Poll p = new Poll();
            Nomination n = new Nomination() { Poll = p };
            Nomination n2 = new Nomination() { Poll = p };

            d.Polls.Add(p);
            d.Nominations.Add(n);
            d.Nominations.Add(n2);
            d.RefreshPocoRelationalLists();

            Assert.IsTrue(p.Nominations.Contains(n));
            Assert.IsTrue(p.Nominations.Contains(n2));
        }

        [TestMethod]
        public void NominationDeleteFromPoll()
        {
            NominateAndVoteModel d = new NominateAndVoteModel();

            Poll p = new Poll();
            Nomination n = new Nomination() { Poll = p };
            Nomination n2 = new Nomination() { Poll = p };

            d.Polls.Add(p);
            d.Nominations.Add(n);
            d.Nominations.Add(n2);
            d.RefreshPocoRelationalLists();
            n2.Poll = null;
            d.RefreshPocoRelationalLists();

            Assert.IsTrue(p.Nominations.Contains(n));
            Assert.IsFalse(p.Nominations.Contains(n2));
        }

        [TestMethod]
        public void NominationAddToPollList()
        {
            NominateAndVoteModel d = new NominateAndVoteModel();

            Poll p = new Poll();
            Nomination n = new Nomination();
            p.Nominations.Add(n);

            d.Polls.Add(p);
            d.Nominations.Add(n);
            d.RefreshPocoRelationalLists();

            Assert.IsFalse(p.Nominations.Contains(n));
        }

        [TestMethod]
        public void NominationAddToUser()
        {
            NominateAndVoteModel d = new NominateAndVoteModel();

            User u = new User();
            Nomination n = new Nomination() { User=u };

            d.Users.Add(u);
            d.Nominations.Add(n);
            d.RefreshPocoRelationalLists();

            Assert.IsTrue(u.Nominations.Contains(n));
        }

        [TestMethod]
        public void NominationsAddToUser()
        {
            NominateAndVoteModel d = new NominateAndVoteModel();

            User u = new User();
            Nomination n = new Nomination() { User = u };
            Nomination n2 = new Nomination() { User = u };

            d.Users.Add(u);
            d.Nominations.Add(n);
            d.Nominations.Add(n2);
            d.RefreshPocoRelationalLists();

            Assert.IsTrue(u.Nominations.Contains(n));
            Assert.IsTrue(u.Nominations.Contains(n2));
        }

        [TestMethod]
        public void NominationDeleteFromUser()
        {
            NominateAndVoteModel d = new NominateAndVoteModel();

            User u = new User();
            Nomination n = new Nomination() { User = u };
            Nomination n2 = new Nomination() { User = u };

            d.Users.Add(u);
            d.Nominations.Add(n);
            d.Nominations.Add(n2);
            d.RefreshPocoRelationalLists();
            n2.User = null;
            d.RefreshPocoRelationalLists();

            Assert.IsTrue(u.Nominations.Contains(n));
            Assert.IsFalse(u.Nominations.Contains(n2));
        }

        [TestMethod]
        public void NominationAddToUserList()
        {
            NominateAndVoteModel d = new NominateAndVoteModel();

            User u = new User();
            Nomination n = new Nomination();

            d.Users.Add(u);
            d.Nominations.Add(n);
            d.RefreshPocoRelationalLists();

            Assert.IsFalse(u.Nominations.Contains(n));
        }

        [TestMethod]
        public void VoteAddToNomination()
        {
            NominateAndVoteModel d = new NominateAndVoteModel();

            Nomination n = new Nomination();
            Vote v = new Vote(){ Nomination = n };

            d.Votes.Add(v);
            d.Nominations.Add(n);
            d.RefreshPocoRelationalLists();

            Assert.IsTrue(n.Votes.Contains(v));
        }

        [TestMethod]
        public void VotesAddToNomination()
        {
            NominateAndVoteModel d = new NominateAndVoteModel();

            Nomination n = new Nomination();
            Vote v = new Vote() { Nomination = n };
            Vote v2 = new Vote() { Nomination = n };

            d.Votes.Add(v);
            d.Votes.Add(v2);
            d.Nominations.Add(n);
            d.RefreshPocoRelationalLists();

            Assert.IsTrue(n.Votes.Contains(v));
            Assert.IsTrue(n.Votes.Contains(v2));
        }

        [TestMethod]
        public void VoteDeleteFromNomination()
        {
            NominateAndVoteModel d = new NominateAndVoteModel();

            Nomination n = new Nomination();
            Vote v = new Vote() { Nomination = n };
            Vote v2 = new Vote() { Nomination = n };

            d.Votes.Add(v);
            d.Votes.Add(v2);
            d.Nominations.Add(n);
            d.RefreshPocoRelationalLists();
            v2.Nomination = null;
            d.RefreshPocoRelationalLists();

            Assert.IsTrue(n.Votes.Contains(v));
            Assert.IsFalse(n.Votes.Contains(v2));
        }

        [TestMethod]
        public void VoteAddToNominationList()
        {
            NominateAndVoteModel d = new NominateAndVoteModel();

            Nomination n = new Nomination();
            Vote v = new Vote();

            d.Votes.Add(v);
            d.Nominations.Add(n);
            d.RefreshPocoRelationalLists();

            Assert.IsFalse(n.Votes.Contains(v));
        }


    }
}
