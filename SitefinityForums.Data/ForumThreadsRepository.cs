using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace SitefinityForums.Data
{
    public class ForumThreadsRepository : IForumThreadsRepository
    {
        private SitefinityForumsContext context;
        private DbSet<ForumThread> threads;

        public ForumThreadsRepository(SitefinityForumsContext ctx) => this.context = ctx ?? throw new ArgumentNullException(nameof(ctx));

        public ForumThread CreateThread()
        {
            var thread = new ForumThread();
            this.context.Threads.Add(thread);
            return thread;
        }

        public IEnumerable<ForumThread> GetThreads() => this.context.Threads;

        public ForumThread GetThread(int id) => this.context.Threads.SingleOrDefault(t => t.ID == id);

        public void SaveChanges() => this.context.SaveChanges();
    }
}
