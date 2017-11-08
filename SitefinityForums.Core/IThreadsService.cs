using System;
using System.Collections.Generic;
using System.Text;

namespace SitefinityForums.Data
{
    public interface IThreadsService
    {
        IEnumerable<LocalForumThread> GetTodoThreads();
    }
}
