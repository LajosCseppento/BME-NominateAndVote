using Microsoft.VisualStudio.TestTools.UnitTesting;
using NominateAndVote.DataModel.Model;

namespace NominateAndVote.DataModel.Tests.Unit
{
    [TestClass]
    public class DefaultDataModelTests
    {
        private IDataModel _data;

        [TestInitialize]
        public void Initialize()
        {
            _data = new DefaultDataModel();
        }

        [TestMethod]
        public void Constructor()
        {
            Assert.AreEqual(0, _data.Administrators.Count);
            Assert.AreEqual(0, _data.News.Count);
            Assert.AreEqual(0, _data.Nominations.Count);
            Assert.AreEqual(0, _data.Polls.Count);
            Assert.AreEqual(0, _data.PollSubjects.Count);
            Assert.AreEqual(0, _data.Users.Count);
            Assert.AreEqual(0, _data.Votes.Count);
        }

        [TestMethod]
        public void RefreshPocoRelationalLists_Poll_Nominations_Add()
        {
            var p = new Poll();
            var n1 = new Nomination { Poll = p };
            var n2 = new Nomination { Poll = p };

            _data.Polls.Add(p);
            _data.Nominations.Add(n1);
            _data.Nominations.Add(n2);
            _data.RefreshPocoRelationalLists();

            Assert.AreEqual(2, p.Nominations.Count);
            Assert.IsTrue(p.Nominations.Contains(n1));
            Assert.IsTrue(p.Nominations.Contains(n2));
        }

        [TestMethod]
        public void RefreshPocoRelationalLists_Poll_Nominations_Delete()
        {
            var p = new Poll();
            var n1 = new Nomination { Poll = p };
            var n2 = new Nomination { Poll = p };

            _data.Polls.Add(p);
            _data.Nominations.Add(n1);
            _data.Nominations.Add(n2);
            _data.RefreshPocoRelationalLists();

            n2.Poll = null;
            _data.RefreshPocoRelationalLists();

            Assert.AreEqual(1, p.Nominations.Count);
            Assert.IsTrue(p.Nominations.Contains(n1));
            Assert.IsFalse(p.Nominations.Contains(n2));
        }

        [TestMethod]
        public void RefreshPocoRelationalLists_Poll_Nominations_ListAdd()
        {
            var p = new Poll();
            var n = new Nomination();
            p.Nominations.Add(n);

            _data.Polls.Add(p);
            _data.Nominations.Add(n);
            _data.RefreshPocoRelationalLists();

            Assert.AreEqual(0, p.Nominations.Count);
            Assert.IsFalse(p.Nominations.Contains(n));
        }

        [TestMethod]
        public void RefreshPocoRelationalLists_User_Nominations_Add()
        {
            var u = new User();
            var n1 = new Nomination { User = u };
            var n2 = new Nomination { User = u };

            _data.Users.Add(u);
            _data.Nominations.Add(n1);
            _data.Nominations.Add(n2);
            _data.RefreshPocoRelationalLists();

            Assert.AreEqual(2, u.Nominations.Count);
            Assert.IsTrue(u.Nominations.Contains(n1));
            Assert.IsTrue(u.Nominations.Contains(n2));
        }

        [TestMethod]
        public void RefreshPocoRelationalLists_User_Nominations_Delete()
        {
            var u = new User();
            var n1 = new Nomination { User = u };
            var n2 = new Nomination { User = u };

            _data.Users.Add(u);
            _data.Nominations.Add(n1);
            _data.Nominations.Add(n2);
            _data.RefreshPocoRelationalLists();

            n2.User = null;
            _data.RefreshPocoRelationalLists();

            Assert.AreEqual(1, u.Nominations.Count);
            Assert.IsTrue(u.Nominations.Contains(n1));
            Assert.IsFalse(u.Nominations.Contains(n2));
        }

        [TestMethod]
        public void RefreshPocoRelationalLists_User_Nominations_ListAdd()
        {
            var u = new User();
            var n = new Nomination();
            u.Nominations.Add(n);

            _data.Users.Add(u);
            _data.Nominations.Add(n);
            _data.RefreshPocoRelationalLists();

            Assert.AreEqual(0, u.Nominations.Count);
            Assert.IsFalse(u.Nominations.Contains(n));
        }

        [TestMethod]
        public void RefreshPocoRelationalLists_Nomination_Votes_Add()
        {
            var n = new Nomination();
            var v1 = new Vote { Nomination = n };
            var v2 = new Vote { Nomination = n };

            _data.Nominations.Add(n);
            _data.Votes.Add(v1);
            _data.Votes.Add(v2);
            _data.RefreshPocoRelationalLists();

            Assert.AreEqual(2, n.Votes.Count);
            Assert.IsTrue(n.Votes.Contains(v1));
            Assert.IsTrue(n.Votes.Contains(v2));
        }

        [TestMethod]
        public void RefreshPocoRelationalLists_Nomination_Votes_Delete()
        {
            var n = new Nomination();
            var v1 = new Vote { Nomination = n };
            var v2 = new Vote { Nomination = n };

            _data.Nominations.Add(n);
            _data.Votes.Add(v1);
            _data.Votes.Add(v2);
            _data.RefreshPocoRelationalLists();

            v2.Nomination = null;
            _data.RefreshPocoRelationalLists();

            Assert.AreEqual(1, n.Votes.Count);
            Assert.IsTrue(n.Votes.Contains(v1));
            Assert.IsFalse(n.Votes.Contains(v2));
        }

        [TestMethod]
        public void RefreshPocoRelationalLists_Nomination_Votes_ListAdd()
        {
            var n = new Nomination();
            var v = new Vote();
            n.Votes.Add(v);

            _data.Nominations.Add(n);
            _data.Votes.Add(v);
            _data.RefreshPocoRelationalLists();

            Assert.AreEqual(0, n.Votes.Count);
            Assert.IsFalse(n.Votes.Contains(v));
        }
    }
}