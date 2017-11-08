using System.Collections.Generic;

namespace SitefinityForums.Data.Crawler
{
    public interface IForumsCrawler
    {
        IEnumerable<RemoteForumThread> GetThreads();
    }
}