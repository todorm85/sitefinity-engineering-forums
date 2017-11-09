using System.Collections.Generic;

namespace SitefinityForums.Data
{
    public interface IForumThreadsRepository
    {
        ForumThread CreateThread();

        IEnumerable<ForumThread> GetThreads();

        ForumThread GetThread(int id);

        void SaveChanges();
    }
}