using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SitefinityForums.Data.Crawler;

namespace SitefinityForums.TestsIntegration
{
    [TestClass]
    public class ForumsCrawlerTests
    {
        [TestMethod]
        public void GetThreads_ReturnUniqueObjectsWithPropertiesParsedCorrectly()
        {
            var crawler = new ForumsCrawler();
            var threads = crawler.GetThreads();
            Assert.IsTrue(threads.Count() > 0);
            foreach (var t in threads)
            {
                Assert.IsTrue(!string.IsNullOrEmpty(t.Link));
                Assert.IsTrue(threads.Count(x => x.Link == t.Link) == 1);
                Assert.IsTrue(t.PostsCount > 0);
                Assert.IsTrue(!string.IsNullOrEmpty(t.Title));
            }
        }
    }
}
