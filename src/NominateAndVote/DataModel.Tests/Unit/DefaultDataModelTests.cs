using Microsoft.VisualStudio.TestTools.UnitTesting;
using NominateAndVote.DataModel.Model;

namespace NominateAndVote.DataModel.Tests
{
    [TestClass]
    public class DefaultDataModelTests
    {
        private IDataModel data;

        [TestInitialize]
        public void Initialize()
        {
            data = new DefaultDataModel();
        }

        [TestMethod]
        public void Constructor()
        {
            Assert.AreEqual(0, data.Administrators.Count);
            Assert.AreEqual(0, data.News.Count);
            Assert.AreEqual(0, data.Nominations.Count);
            Assert.AreEqual(0, data.Polls.Count);
            Assert.AreEqual(0, data.PollSubjects.Count);
            Assert.AreEqual(0, data.Users.Count);
            Assert.AreEqual(0, data.Votes.Count);
        }

        [TestMethod]
        public void RefreshPocoRelationalLists_Poll_Nominations_Add()
        {
            Poll p = new Poll();
            Nomination n1 = new Nomination() { Poll = p };
            Nomination n2 = new Nomination() { Poll = p };

            data.Polls.Add(p);
            data.Nominations.Add(n1);
            data.Nominations.Add(n2);
            data.RefreshPocoRelationalLists();

            Assert.AreEqual(2, p.Nominations.Count);
            Assert.IsTrue(p.Nominations.Contains(n1));
            Assert.IsTrue(p.Nominations.Contains(n2));
        }

        [TestMethod]
        public void RefreshPocoRelationalLists_Poll_Nominations_Delete()
        {
            Poll p = new Poll();
            Nomination n1 = new Nomination() { Poll = p };
            Nomination n2 = new Nomination() { Poll = p };

            data.Polls.Add(p);
            data.Nominations.Add(n1);
            data.Nominations.Add(n2);
            data.RefreshPocoRelationalLists();

            n2.Poll = null;
            data.RefreshPocoRelationalLists();

            Assert.AreEqual(1, p.Nominations.Count);
            Assert.IsTrue(p.Nominations.Contains(n1));
            Assert.IsFalse(p.Nominations.Contains(n2));
        }

        [TestMethod]
        public void RefreshPocoRelationalLists_Poll_Nominations_ListAdd()
        {
            Poll p = new Poll();
            Nomination n = new Nomination();
            p.Nominations.Add(n);

            data.Polls.Add(p);
            data.Nominations.Add(n);
            data.RefreshPocoRelationalLists();

            Assert.AreEqual(0, p.Nominations.Count);
            Assert.IsFalse(p.Nominations.Contains(n));
        }

        [TestMethod]
        public void RefreshPocoRelationalLists_User_Nominations_Add()
        {
            User u = new User();
            Nomination n1 = new Nomination() { User = u };
            Nomination n2 = new Nomination() { User = u };

            data.Users.Add(u);
            data.Nominations.Add(n1);
            data.Nominations.Add(n2);
            data.RefreshPocoRelationalLists();

            Assert.AreEqual(2, u.Nominations.Count);
            Assert.IsTrue(u.Nominations.Contains(n1));
            Assert.IsTrue(u.Nominations.Contains(n2));
        }

        [TestMethod]
        public void RefreshPocoRelationalLists_User_Nominations_Delete()
        {
            User u = new User();
            Nomination n1 = new Nomination() { User = u };
            Nomination n2 = new Nomination() { User = u };

            data.Users.Add(u);
            data.Nominations.Add(n1);
            data.Nominations.Add(n2);
            data.RefreshPocoRelationalLists();

            n2.User = null;
            data.RefreshPocoRelationalLists();

            Assert.AreEqual(1, u.Nominations.Count);
            Assert.IsTrue(u.Nominations.Contains(n1));
            Assert.IsFalse(u.Nominations.Contains(n2));
        }

        [TestMethod]
        public void RefreshPocoRelationalLists_User_Nominations_ListAdd()
        {
            User u = new User();
            Nomination n = new Nomination();
            u.Nominations.Add(n);

            data.Users.Add(u);
            data.Nominations.Add(n);
            data.RefreshPocoRelationalLists();

            Assert.AreEqual(0, u.Nominations.Count);
            Assert.IsFalse(u.Nominations.Contains(n));
        }

        [TestMethod]
        public void RefreshPocoRelationalLists_Nomination_Votes_Add()
        {
            Nomination n = new Nomination();
            Vote v1 = new Vote() { Nomination = n };
            Vote v2 = new Vote() { Nomination = n };

            data.Nominations.Add(n);
            data.Votes.Add(v1);
            data.Votes.Add(v2);
            data.RefreshPocoRelationalLists();

            Assert.AreEqual(2, n.Votes.Count);
            Assert.IsTrue(n.Votes.Contains(v1));
            Assert.IsTrue(n.Votes.Contains(v2));
        }

        [TestMethod]
        public void RefreshPocoRelationalLists_Nomination_Votes_Delete()
        {
            Nomination n = new Nomination();
            Vote v1 = new Vote() { Nomination = n };
            Vote v2 = new Vote() { Nomination = n };

            data.Nominations.Add(n);
            data.Votes.Add(v1);
            data.Votes.Add(v2);
            data.RefreshPocoRelationalLists();

            v2.Nomination = null;
            data.RefreshPocoRelationalLists();

            Assert.AreEqual(1, n.Votes.Count);
            Assert.IsTrue(n.Votes.Contains(v1));
            Assert.IsFalse(n.Votes.Contains(v2));
        }

        [TestMethod]
        public void RefreshPocoRelationalLists_Nomination_Votes_ListAdd()
        {
            Nomination n = new Nomination();
            Vote v = new Vote();
            n.Votes.Add(v);

            data.Nominations.Add(n);
            data.Votes.Add(v);
            data.RefreshPocoRelationalLists();

            Assert.AreEqual(0, n.Votes.Count);
            Assert.IsFalse(n.Votes.Contains(v));
        }
    }
}