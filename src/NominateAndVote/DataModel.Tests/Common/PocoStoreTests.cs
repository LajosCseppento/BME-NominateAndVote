using Microsoft.VisualStudio.TestTools.UnitTesting;
using NominateAndVote.DataModel.Common;
using System.Linq;

namespace NominateAndVote.DataModel.Tests.Common
{
    [TestClass]
    public class PocoStoreTests
    {
        private PocoStore<Poco> _store;

        [TestInitialize]
        public void Initialize()
        {
            _store = new PocoStore<Poco>();

            // Arrange
            _store.AddOrUpdate(new Poco { Number = 1, Name = "1" });
            _store.AddOrUpdate(new Poco { Number = 2, Name = "2" });
            _store.AddOrUpdate(new Poco { Number = 3, Name = "3" });
        }

        [TestMethod]
        public void Count()
        {
            // Act & Assert
            Assert.AreEqual(3, _store.Count);
        }

        [TestMethod]
        public void Contains_Poco()
        {
            // Act & Assert
            Assert.IsTrue(_store.Contains(new Poco { Number = 1 }));
            Assert.IsTrue(_store.Contains(new Poco { Number = 1, Name = "1" }));
            Assert.IsTrue(_store.Contains(new Poco { Number = 1, Name = "1 not" }));
            Assert.IsTrue(_store.Contains(new Poco { Number = 1, Name = "2" }));

            Assert.IsFalse(_store.Contains(new Poco()));

            Assert.IsFalse(_store.Contains(new Poco { Number = 9 }));
            Assert.IsFalse(_store.Contains(new Poco { Number = 9, Name = "9" }));
            Assert.IsFalse(_store.Contains(new Poco { Number = 9, Name = "9 not" }));
            Assert.IsFalse(_store.Contains(new Poco { Number = 9, Name = "2" }));
        }

        [TestMethod]
        public void Get_Poco()
        {
            // Act
            var p2 = _store.Get(new Poco { Number = 2 });
            var p9 = _store.Get(new Poco { Number = 9 });

            // Assert
            Assert.IsNotNull(p2);
            Assert.AreEqual(2, p2.Number);
            Assert.AreEqual("2", p2.Name);
            Assert.IsNull(p9);
        }

        [TestMethod]
        public void AddOrUpdate()
        {
            // Act
            var b2 = _store.AddOrUpdate(new Poco { Number = 2, Name = "2 update" });
            var b3 = _store.AddOrUpdate(new Poco { Number = 3 });
            var b9 = _store.AddOrUpdate(new Poco { Number = 9, Name = "9" });

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
        public void Remove_Poco()
        {
            // Act
            var b2 = _store.Remove(new Poco { Number = 2, Name = "2" });
            var b3 = _store.Remove(new Poco { Number = 3 });
            var b9 = _store.Remove(new Poco { Number = 9, Name = "9" });

            // Assert
            Assert.IsTrue(b2); // found
            Assert.IsTrue(b3); // found
            Assert.IsFalse(b9); // not found

            Assert.AreEqual(1, _store.Count);
            Assert.AreEqual("1", _store.ElementAt(0).Name);
        }

        [TestMethod]
        public void Clear()
        {
            // Act
            _store.Clear();

            // Assert
            Assert.AreEqual(0, _store.Count);
        }

        private class Poco : BasePoco<Poco>
        {
            public int Number { get; set; }

            public string Name { get; set; }

            public override bool Equals(Poco other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return Number == other.Number;
            }

            public override int GetHashCode()
            {
                return Number.GetHashCode();
            }

            public override int CompareTo(Poco other)
            {
                // Id ASC
                if (ReferenceEquals(null, other)) return 1;
                if (ReferenceEquals(this, other)) return 0;
                return Number.CompareTo(Number);
            }
        }
    }
}