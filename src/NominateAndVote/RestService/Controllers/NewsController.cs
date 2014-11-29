using NominateAndVote.DataModel;
using NominateAndVote.DataModel.Model;
using NominateAndVote.RestService.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace NominateAndVote.RestService.Controllers
{
    [RoutePrefix("api/News")]
    public class NewsController : ApiController
    {
        private IDataManager dataManager;

        public NewsController()
            : base()
        {
            // a modellt ne nagyon haszbált, csak azt, amid a datamanager enged elérni! így csak kicseréljük pár helyen és megy majd a felhővel is. Msot a te adataiddal dolgozik
            SimpleDataModel model = new SimpleDataModel();
            model.LoadSampleData();
            dataManager = new DataModelManager(model);
        }

        public NewsController(IDataManager dataManager)
            : base()
        {
            if (dataManager == null)
            {
                throw new ArgumentNullException("The data manager must not be null", "dataManager");
            }

            this.dataManager = dataManager;
        }

        // GET: api/News
        public IEnumerable<News> Get()
        {
            return dataManager.QueryNews();
        }

        // GET: api/News/5
        public News Get(int id)
        {
            /*News news;
            for(News n: dataManager.QueryNews()){
                n.ID
            }*/
            return null;
        }

        // POST: api/News/Save
        [Route("Save")]
        [HttpPost]
        public IHttpActionResult Save(SaveNewsBindingModel newsBindingModel)
        {
            if (newsBindingModel == null)
            {
                return BadRequest("No data");
            }
            else if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            News news = newsBindingModel.ToPoco();
            if (news.ID.Equals(Guid.Empty))
            {
                news.ID = Guid.NewGuid();
                news.PublicationDate = DateTime.Now;
            }
            else
            {
                News oldNews = dataManager.QueryNews(news.ID);
                if (oldNews == null)
                {
                    news.PublicationDate = DateTime.Now;
                }
                else
                {
                    news.PublicationDate = oldNews.PublicationDate;
                }
            }

            dataManager.SaveNews(news);

            return Ok(news);
        }

        // PUT: api/News/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/News/5
        public void Delete(int id)
        {
            /*dataManager.QueryNews.
            dataManager.DeleteNews(news);*/
        }
    }
}