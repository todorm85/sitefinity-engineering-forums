using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SitefinityForums.Data;

namespace SitefinityForums.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Threads")]
    public class ThreadsController : Controller
    {
        private IThreadsService service;

        public ThreadsController(IThreadsService threadsSrvc)
        {
            this.service = threadsSrvc;
        }

        [HttpGet]
        public IEnumerable<ForumThread> GetAll()
        {
            return service.GetThreads(x => !x.IsAnswered);
        }

        [HttpPost("update")]
        public void Update([FromBody]ForumThreadUpdateModel threadModel)
        {
            var thread = new ForumThread();
            thread.ID = threadModel.Id;
            thread.Opened = threadModel.Opened;
            service.UpdateThread(thread);
        }
    }
}