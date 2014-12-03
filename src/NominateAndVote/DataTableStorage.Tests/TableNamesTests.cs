using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace NominateAndVote.DataTableStorage.Tests
{
    [TestClass]
    public class TableNamesTests
    {
        [TestInitialize]
        public void Initialize()
        {
            TableNames.Clear();
        }

        [TestMethod]
        public void Clear()
        {
            // Act & Assert
            Assert.AreEqual(0, TableNames.GetDictionary().Count);
            Assert.AreEqual(0, TableNames.GetEntityTypes().Count);
            Assert.AreEqual(0, TableNames.GetTableNames().Count);
        }

        [TestMethod]
        public void SetTableName()
        {
            // Act
            TableNames.SetTableName(typeof(MyEntity), "MyTable");
            TableNames.SetTableName(typeof(MyEntity), "MyTablex");
            TableNames.SetTableName(typeof(MyEntity2), "MyTable2");

            // Assert
            Assert.AreEqual(2, TableNames.GetDictionary().Count);
            Assert.AreEqual(2, TableNames.GetTableNames().Count);

            Assert.AreEqual(typeof(MyEntity), TableNames.GetEntityTypes()[0]);
            Assert.AreEqual(typeof(MyEntity2), TableNames.GetEntityTypes()[1]);

            Assert.AreEqual("mytable2", TableNames.GetTableNames()[0]);
            Assert.AreEqual("mytablex", TableNames.GetTableNames()[1]);

            Assert.AreEqual(typeof(MyEntity), TableNames.GetEntityType("MyTablex"));
            Assert.AreEqual("mytablex", TableNames.GetTableName(typeof(MyEntity)));

            Assert.AreEqual(typeof(MyEntity2), TableNames.GetEntityType("MyTable2"));
            Assert.AreEqual("mytable2", TableNames.GetTableName(typeof(MyEntity2)));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetTableName_InvalidType()
        {
            // Act
            TableNames.SetTableName(typeof(string), "MyTable");

            // Assert
            // expected exception
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetTableName_NullName()
        {
            // Act
            TableNames.SetTableName(typeof(MyEntity), null);

            // Assert
            // expected exception
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetTableName_NullType()
        {
            // Act
            TableNames.SetTableName(null, "MyTable");

            // Assert
            // expected exception
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetTableName_InvalidName()
        {
            // Act
            TableNames.SetTableName(typeof(MyEntity), "My-Table");

            // Assert
            // expected exception
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetTableName_DuplicateName()
        {
            // Act
            TableNames.SetTableName(typeof(MyEntity), "MyTable");
            TableNames.SetTableName(typeof(MyEntity2), "MyTable");

            // Assert
            // expected exception
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetEntityType_NotFound()
        {
            // Act
            TableNames.GetEntityType("notable");

            // Assert
            // expected exception
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetTableName_NotFound()
        {
            // Act
            TableNames.GetEntityType("notable");

            // Assert
            // expected exception
        }

        private class MyEntity : TableEntity { }

        private class MyEntity2 : TableEntity { }
    }
}