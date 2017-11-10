using System.Collections.Generic;
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
            service.UpdateThread(threadModel.GetDomainModel());
        }
    }
}