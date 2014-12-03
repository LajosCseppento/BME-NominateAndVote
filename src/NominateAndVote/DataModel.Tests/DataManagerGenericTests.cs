using Microsoft.VisualStudio.TestTools.UnitTesting;
using NominateAndVote.DataModel.Poco;
using System;

namespace NominateAndVote.DataModel.Tests
{
    public abstract class DataManagerGenericTests
    {
        // TODO a null paramétereket NEM kell tesztelni! Ez integrációs teszt!
        // TODO hibás argumentum esetén (pl null) argumentexception / argumentnullexception jön
        // TODO hibás ADAT esetén DataExceptionnek kell jönnie (pl olyan cucc részeit kérjük le ami nem létezik!)
        // TODO ezeket valószínűleg rosszul tudja a program
        // TODO a futtatást a memorydatamanager-en végezd. A sampledatamodel-en futnak a tesztek!
        // TODO ez azért fontos mert bugol a db és így a leggyorsabb megtalálni a hibát
        // TODO amikor nézed az eredményeket, MINDEN rendezve kell, hogy visszajöjjön. Mi alapján rendezünk? Lásd POCO osztályok compareto függvénye

        /*
         * Tippek:
         * - lekérdezésnél megnézed a sorrendet és PÁR fontos adattagot (mindet felesleges) ; ha valami valamijét kérdezzük le, és az első valami nem ltezik akkor DataException-t kell várni ([ExpectedException])
         * - mentésnél: példát lásd híreknél (meg kell erősíteni, hogy ÚJRA lekérdezés után BEKERÜLT, és ID-t is kapott)
         * - törlésnél: ha olyat törlünk, ami nem létezik, azt nemes egyszerűséggel lefossuk, DE ellenőrizni kell hogy nem okoz-e hibát
         * - search: ezek általában szó eleji egyezést néznek, szóval érdemes kipróálni! ha valami nincs akkor üres lista értelemszerűen
         *
         * Persze ha lekérdezésnél ID alapján kérdezek valamit, és nem találom, akkor null-t kell visszaadni, ha több elemet akkor meg öres liste (tehát ha X szavazáshpzkérek jelöléseket és NINCS, akkor az üres lista, ha NEM LÉTEZIK A SZAVAZÁS, az exception)
         */

        protected IDataManager _dataManager;

        [TestInitialize]
        public void Initialize()
        {
            _dataManager = _createDataManager(new SampleDataModel());
        }

        protected abstract IDataManager _createDataManager(IDataModel dataModel);

        [TestMethod]
        public void IsAdmin()
        {
            // Act & Assert
            Assert.AreEqual(false, _dataManager.IsAdmin(new User { Id = 0 }));
            Assert.AreEqual(false, _dataManager.IsAdmin(new User { Id = 1 }));
            Assert.AreEqual(false, _dataManager.IsAdmin(new User { Id = 2 }));
            Assert.AreEqual(false, _dataManager.IsAdmin(new User { Id = 3 }));
            Assert.AreEqual(true, _dataManager.IsAdmin(new User { Id = 4 }));
            Assert.AreEqual(false, _dataManager.IsAdmin(new User { Id = 5 }));
        }

        [TestMethod]
        public void QueryNews()
        {
            // Act
            var list = _dataManager.QueryNews();

            // Assert
            Assert.AreEqual(2, list.Count);
            Assert.AreEqual("Second", list[0].Title);
            Assert.AreEqual("First", list[1].Title);
        }

        [TestMethod]
        public void SaveNews()
        {
            // Arrange
            var list = _dataManager.QueryNews();

            // Act
            // create
            var news = new News { Id = Guid.Empty, PublicationDate = DateTime.Now, Title = "Third", Text = "x" };
            _dataManager.SaveNews(news);

            // update
            list[0].Title = "Second2";
            _dataManager.SaveNews(list[0]);

            // Assert
            list = _dataManager.QueryNews();
            Assert.AreEqual(3, list.Count);
            Assert.AreNotEqual(Guid.Empty, list[0].Id); // new id should have been assigned
            Assert.AreEqual("Third", list[0].Title);
            Assert.AreEqual("Second2", list[1].Title);
            Assert.AreEqual("First", list[2].Title);
        }

        [TestMethod]
        public void DeleteNews(Guid id)
        {
            // Act
            var list = _dataManager.QueryNews();

            // exists, should delete
            _dataManager.DeleteNews(list[1].Id);

            // not exists, should not cause exception
            _dataManager.DeleteNews(Guid.NewGuid());

            // Assert
            list = _dataManager.QueryNews();
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual("Second", list[0].Title);
        }

        [TestMethod]
        public void QueryNominations()
        {
            // TODO ennek HÁROM fajtája van! (Poll-t vára, user-t vár, mindkettőt vár
            // TODO elég egy pollra lekérdezni, lehetőleg ahol több van
            // TODO emelett kell az az eset, amikor nem létezik a poll / user
            // TODO mindhárom függvényt le kéne valahogy
            throw new NotImplementedException();
        }

        [TestMethod]
        public void SaveNomination(Nomination nomination)
        {
            // TODO
            throw new NotImplementedException();
        }

        [TestMethod]
        public void DeleteNomination()
        {
            // TODO
            throw new NotImplementedException();
        }

        [TestMethod]
        public void QueryPolls()
        {
            // TODO külön tesztelt, mikor mindet lekérdezi illetvem ikor state alapján
            // TODO state: elég egy state-re kipróbálni, csak ne a legelső eleme legyen az enumnak
            throw new NotImplementedException();
        }

        [TestMethod]
        public void QueryPoll()
        {
            // TODO
            throw new NotImplementedException();
        }

        [TestMethod]
        public void SavePoll(Poll poll)
        {
            // TODO
            throw new NotImplementedException();
        }

        [TestMethod]
        public void QueryPollSubject()
        {
            // TODO
            throw new NotImplementedException();
        }

        [TestMethod]
        public void SearchPollSubjects()
        {
            // TODO
            throw new NotImplementedException();
        }

        [TestMethod]
        public void SavePollSubject()
        {
            // TODO
            throw new NotImplementedException();
        }

        [TestMethod]
        public void SavePollSubjectsBatch()
        {
            // TODO
            throw new NotImplementedException();
        }

        [TestMethod]
        public void QueryBannedUsers()
        {
            // TODO
            throw new NotImplementedException();
        }

        [TestMethod]
        public void QueryUser()
        {
            // TODO
            throw new NotImplementedException();
        }

        [TestMethod]
        public void SearchUsers()
        {
            // TODO
            throw new NotImplementedException();
        }

        [TestMethod]
        public void SaveUser()
        {
            // TODO
            throw new NotImplementedException();
        }

        [TestMethod]
        public void QueryVote()
        {
            /*
             * Létező poll és user és SZAVAZOTT
             * Létező poll és user és NEM SZAVAZOTT
             * Nem létező poll
             * Nem létező user
             * */
            // TODO
            throw new NotImplementedException();
        }

        [TestMethod]
        public void SaveVote()
        {
            // TODO
            throw new NotImplementedException();
        }
    }
}