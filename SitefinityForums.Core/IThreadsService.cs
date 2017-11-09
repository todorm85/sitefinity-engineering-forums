using System;
using System.Collections.Generic;
using System.Text;

namespace SitefinityForums.Data
{
    public interface IThreadsService
    {
        IEnumerable<ForumThread> GetThreads(Func<ForumThread, bool> filter);
        void UpdateThread(ForumThread thread);
    }
}
