using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SitefinityForums.Data.Crawler;

namespace SitefinityForums.Data
{
    public class ThreadsService : IThreadsService
    {
        private SitefinityForumsContext context;
        private ForumsCrawler crawler;

        public ThreadsService(SitefinityForumsContext sfContext)
        {
            context = sfContext;
            crawler = new ForumsCrawler();
        }

        public IEnumerable<LocalForumThread> GetTodoThreads()
        {
            var savedThreads = context.Threads;
            var externalThreads = crawler.GetUnansweredThreads();
            UpdateSavedThreads(savedThreads, externalThreads);

            return savedThreads.Where(x => !x.Closed && externalThreads.Any(y => y.Id == x.ID));
        }

        private void UpdateSavedThreads(IQueryable<LocalForumThread> savedThreads, IEnumerable<RemoteForumThread> externalThreads)
        {
            foreach (var ext in externalThreads)
            {
                var relatedInternalThread = savedThreads.FirstOrDefault(x => x.ID == ext.Id);
                if (relatedInternalThread != null)
                {
                    if (relatedInternalThread.PostsCount < ext.PostsCount)
                    {
                        // external forum has been updated
                        relatedInternalThread.Closed = false;
                    }
                }
                else
                {
                    CreateInternalThread(ext);
                }
            }

            context.SaveChanges();
        }

        private void CreateInternalThread(RemoteForumThread ext)
        {
            context.Add(new LocalForumThread()
            {
                ID = ext.Id,
                PostsCount = ext.PostsCount,
            });
        }
    }
}
