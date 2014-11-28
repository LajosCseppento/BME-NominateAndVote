using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NominateAndVote.DataModel.Model;
using NominateAndVote.DataModel;

namespace DataModel.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            NominateAndVoteModel d = new NominateAndVoteModel();

            Poll p = new Poll();
            Nomination n = new Nomination() { Poll = p };

            d.Polls.Add(p);
            d.Nominations.Add(n);
            d.RefreshPocoRelationalLists();

            Assert.IsTrue(p.Nominations.Contains(n));
        }
    }
}
