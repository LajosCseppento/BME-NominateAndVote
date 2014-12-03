using Microsoft.VisualStudio.TestTools.UnitTesting;
using NominateAndVote.DataModel.Common;
using System.Linq;

namespace NominateAndVote.DataModel.Tests.Common
{
    [TestClass]
    public class PocoWithIdStoreTests
    {
        private PocoWithIdStore<int, Poco> _store;

        [TestInitialize]
        public void Initialize()
        {
            _store = new PocoWithIdStore<int, Poco>();

            // Arrange
            _store.AddOrUpdate(new Poco { Id = 1, Name = "1" });
            _store.AddOrUpdate(new Poco { Id = 2, Name = "2" });
            _store.AddOrUpdate(new Poco { Id = 3, Name = "3" });
        }

        [TestMethod]
        [TestCategory("Unit/DataModel/PocoWithIdStore")]
        public void Count()
        {
            // Act & Assert
            Assert.AreEqual(3, _store.Count);
        }

        [TestMethod]
        [TestCategory("Unit/DataModel/PocoWithIdStore")]
        public void Contains_Poco()
        {
            // Act & Assert
            Assert.IsTrue(_store.Contains(new Poco { Id = 1 }));
            Assert.IsTrue(_store.Contains(new Poco { Id = 1, Name = "1" }));
            Assert.IsTrue(_store.Contains(new Poco { Id = 1, Name = "1 not" }));
            Assert.IsTrue(_store.Contains(new Poco { Id = 1, Name = "2" }));

            Assert.IsFalse(_store.Contains(new Poco()));

            Assert.IsFalse(_store.Contains(new Poco { Id = 9 }));
            Assert.IsFalse(_store.Contains(new Poco { Id = 9, Name = "9" }));
            Assert.IsFalse(_store.Contains(new Poco { Id = 9, Name = "9 not" }));
            Assert.IsFalse(_store.Contains(new Poco { Id = 9, Name = "2" }));
        }

        [TestMethod]
        [TestCategory("Unit/DataModel/PocoWithIdStore")]
        public void Contains_Id()
        {
            // Act & Assert
            Assert.IsTrue(_store.Contains(1));
            Assert.IsFalse(_store.Contains(0));
            Assert.IsFalse(_store.Contains(9));
        }

        [TestMethod]
        [TestCategory("Unit/DataModel/PocoWithIdStore")]
        public void AddOrUpdate()
        {
            // Act
            var b2 = _store.AddOrUpdate(new Poco { Id = 2, Name = "2 update" });
            var b3 = _store.AddOrUpdate(new Poco { Id = 3 });
            var b9 = _store.AddOrUpdate(new Poco { Id = 9, Name = "9" });

            // Assert
            Assert.IsFalse(b2); // update
            Assert.IsFalse(b3); // update
            Assert.IsTrue(b9); // add

            Assert.AreEqual(4, _store.Count);

            var list = _store.ToSortedList();

            Assert.AreEqual("1", list[0].Name);
            Assert.AreEqual("2 update", list[1].Name);
            Assert.AreEqual(null, list[2].Name);
            Assert.AreEqual("9", list[3].Name);
        }

        [TestMethod]
        [TestCategory("Unit/DataModel/PocoWithIdStore")]
        public void Get_Poco()
        {
            // Act
            var p2 = _store.Get(new Poco { Id = 2 });
            var p9 = _store.Get(new Poco { Id = 9 });

            // Assert
            Assert.IsNotNull(p2);
            Assert.AreEqual(2, p2.Id);
            Assert.AreEqual("2", p2.Name);
            Assert.IsNull(p9);
        }

        [TestMethod]
        [TestCategory("Unit/DataModel/PocoWithIdStore")]
        public void Get_Id()
        {
            // Act
            var p2 = _store.Get(2);
            var p9 = _store.Get(9);

            // Assert
            Assert.IsNotNull(p2);
            Assert.AreEqual(2, p2.Id);
            Assert.AreEqual("2", p2.Name);
            Assert.IsNull(p9);
        }

        [TestMethod]
        [TestCategory("Unit/DataModel/PocoWithIdStore")]
        public void Remove_Poco()
        {
            // Act
            var b2 = _store.Remove(new Poco { Id = 2, Name = "2" });
            var b3 = _store.Remove(new Poco { Id = 3 });
            var b9 = _store.Remove(new Poco { Id = 9, Name = "9" });

            // Assert
            Assert.IsTrue(b2); // found
            Assert.IsTrue(b3); // found
            Assert.IsFalse(b9); // not found

            Assert.AreEqual(1, _store.Count);
            Assert.AreEqual("1", _store.ElementAt(0).Name);
        }

        [TestMethod]
        [TestCategory("Unit/DataModel/PocoWithIdStore")]
        public void Remove_Id()
        {
            // Act
            var b2 = _store.Remove(2);
            var b3 = _store.Remove(3);
            var b9 = _store.Remove(9);

            // Assert
            Assert.IsTrue(b2); // found
            Assert.IsTrue(b3); // found
            Assert.IsFalse(b9); // not found

            Assert.AreEqual(1, _store.Count);
            Assert.AreEqual("1", _store.ElementAt(0).Name);
        }

        [TestMethod]
        [TestCategory("Unit/DataModel/PocoWithIdStore")]
        public void Clear()
        {
            // Act
            _store.Clear();

            // Assert
            Assert.AreEqual(0, _store.Count);
        }

        private class Poco : BasePocoWithId<int, Poco>
        {
            public string Name { get; set; }

            public override int CompareTo(Poco other)
            {
                // Id ASC
                if (ReferenceEquals(null, other)) return 1;
                if (ReferenceEquals(this, other)) return 0;
                return Id.CompareTo(Id);
            }
        }
    }
}