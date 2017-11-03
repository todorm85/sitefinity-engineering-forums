using System;
using System.Collections.Generic;
using System.Text;

namespace SitefinityForums.Data
{
    public interface IThreadsRepository
    {
        IEnumerable<SavedThread> GetTodoThreads();
    }
}
