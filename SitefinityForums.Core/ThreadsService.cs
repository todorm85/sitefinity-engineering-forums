using System;
using System.Collections.Generic;
using System.Linq;
using SitefinityForums.Data.Crawler;

namespace SitefinityForums.Data
{
    public class ThreadsService : IThreadsService
    {
        private IForumThreadsRepository threadsRepo;
        private ForumsCrawler crawler;

        public ThreadsService(IForumThreadsRepository threadsRepo)
        {
            this.threadsRepo = threadsRepo;
            crawler = new ForumsCrawler();
        }

        public IEnumerable<ForumThread> GetThreads(Func<ForumThread, bool> filter)
        {
            var threads = threadsRepo.GetThreads();
            var remoteThreads = crawler.GetThreads();
            SynchronizeThreads(threads, remoteThreads);

            return threads.Where(filter);
        }

        public void UpdateThread(ForumThread thread)
        {
            var foundThread = threadsRepo.GetThread(thread.ID);
            if (foundThread == null)
            {
                throw new NullReferenceException($"Thread with id {thread.ID} not found");
            }

            foundThread.Opened = thread.Opened;
            threadsRepo.SaveChanges();
        }

        private static void SynchronizeThreadProperties(RemoteForumThread remoteThread, ForumThread matchedThread)
        {
            matchedThread.Link = remoteThread.Link;
            matchedThread.PostsCount = remoteThread.PostsCount;
            matchedThread.Title = remoteThread.Title;
            matchedThread.IsAnswered = remoteThread.IsAnswered;
            matchedThread.RemoteId = remoteThread.Id;
        }

        private void SynchronizeThreads(IEnumerable<ForumThread> threads, IEnumerable<RemoteForumThread> remoteThreads)
        {
            foreach (var remoteThread in remoteThreads)
            {
                // forum links are the only property of remote forums that does not change and we don`t have access to their ids
                var matchedThread = threads.FirstOrDefault(x => x.RemoteId == remoteThread.Id);
                if (matchedThread == null)
                {
                    matchedThread = threadsRepo.CreateThread();
                }

                SynchronizeThreadProperties(remoteThread, matchedThread);
            }

            threadsRepo.SaveChanges();
        }
    }
}