using NominateAndVote.DataModel;
using NominateAndVote.DataModel.Model;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Mvc;

namespace NominateAndVote.RestService.Controllers
{
    public class NewsAdminController : Controller
    {
        private IDataManager dataManager;

        public NewsAdminController()
            : base()
        {
            // a modellt ne nagyon haszbált, csak azt, amid a datamanager enged elérni! így csak kicseréljük pár helyen és megy majd a felhővel is. Msot a te adataiddal dolgozik
            SimpleDataModel model = new SimpleDataModel();
            model.LoadSampleData();
            dataManager = new DataModelManager(model);
        }

        // GET: NewsAdmin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: NewsAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // GET: api/News
        public List<News> Get()
        {
            return dataManager.QueryNews();
        }

        //PUT api/News/Create
        public void Create([FromBody]string title, [FromBody]string text)
        {
            new News
            {
                ID = Guid.NewGuid(),
                Title = title,
                Text = text,
                PublicationDate = DateTime.Now
            };
        }

        /*
        // POST: NewsAdmin/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: NewsAdmin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: NewsAdmin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: NewsAdmin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: NewsAdmin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }*/
    }
}