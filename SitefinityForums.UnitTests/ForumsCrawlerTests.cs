using System;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SitefinityForums.Data.Crawler;

namespace SitefinityForums.UnitTests
{
    [TestClass]
    public class ForumsCrawlerTests
    {
        private HtmlParser parser = new HtmlParser();

        [TestMethod]
        public void MarkupWithThreads_ReturnsCorrectNumberOfThreads()
        {
            var testPagesWithThreads = GetPagesWithThreads(5, 2);
            var provider = GetMarkupProviderMock(testPagesWithThreads);
            var sut = new ForumsCrawler();
            sut.MarkupProvider = provider;

            var result = sut.GetThreads();
            Assert.IsTrue(result.Count() == 10);

            var threads = result.ToList();

            for (int i = 1; i <= threads.Count; i++)
            {
                var thread = threads[i - 1];
                Assert.IsTrue(thread.Id == "id" + i);
                Assert.IsTrue(thread.Title == "title" + i);
                Assert.IsTrue(thread.IsAnswered == (i % 2 == 0));
                Assert.IsTrue(thread.PostsCount == i);
            }
        }

        private IDictionary<int, List<IElement>> GetPagesWithThreads(int threadsPerPage, int pageCount)
        {
            var pagesWithThreads = new Dictionary<int, List<IElement>>();

            for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
            {
                var threadList = new List<IElement>();
                for (int i = 1; i <= threadsPerPage; i++)
                {
                    var index = (pageNumber - 1) * threadsPerPage + i;
                    var thread = this.GetThreadElement("id" + index, "title" + index, index % 2 == 0, index);
                    threadList.Add(thread);
                }

                pagesWithThreads.Add(pageNumber, threadList);
            }

            return pagesWithThreads;
        }

        private IMarkupProvider GetMarkupProviderMock(IDictionary<int, List<IElement>> pagesWithThreads)
        {
            var sut = new ForumsCrawler();
            var markupProviderMock = new Mock<IMarkupProvider>(MockBehavior.Strict);
            foreach (var key in pagesWithThreads.Keys)
            {
                var docMock = this.GetDoc(pagesWithThreads[key]);
                AddPager(pagesWithThreads.Keys.Count, docMock);

                markupProviderMock
                    .Setup(x => x.GetDomDocument(It.Is<string>(s => s.EndsWith(key.ToString()))))
                    .Returns(docMock.Object);
            }

            return markupProviderMock.Object;
        }

        private static void AddPager(int pageCount, Mock<IDocument> docMock)
        {
            var pageLinks = new Mock<IHtmlCollection<IElement>>();
            pageLinks.Setup(pl => pl.Length).Returns(pageCount);
            docMock.Setup(d => d.QuerySelectorAll(ForumsCrawler.PageLinksSelector)).Returns(pageLinks.Object);
        }

        private Mock<IDocument> GetDoc(List<IElement> threadList)
        {
            var threadCollection = new Mock<IHtmlCollection<IElement>>(MockBehavior.Strict);
            threadCollection.Setup(tc => tc.GetEnumerator())
                    .Returns(GetEnumerator(threadList));

            var doc = new Mock<IDocument>(MockBehavior.Strict);
            doc.Setup(d => d.QuerySelectorAll(ForumsCrawler.ThreadsSelector))
                .Returns(threadCollection.Object);
            return doc;
        }

        private IEnumerator<IElement> GetEnumerator(List<IElement> threadList)
        {
            foreach (var t in threadList)
            {
                yield return t;
            }
        }

        private IElement GetThreadElement(string id, string title, bool isAnswered, int postsCount)
        {
            var threadElementMock = new Mock<IElement>(MockBehavior.Strict);
            var idAndTitleElement = GetIdAndTitleElements(id, title);
            threadElementMock.Setup(tem => tem.QuerySelector(ForumsCrawler.ThreadIdSelector))
                .Returns(idAndTitleElement);
            threadElementMock.Setup(tem => tem.QuerySelector(ForumsCrawler.ThreadTitleSelector))
                .Returns(idAndTitleElement);
            threadElementMock.Setup(tem => tem.QuerySelector(ForumsCrawler.ThreadPostsCountSelector))
                .Returns(GetPostsCountElement(postsCount));
            if (isAnswered)
            {
                var isAnsweredMock = new Mock<IElement>(MockBehavior.Strict);
                threadElementMock.Setup(e => e.QuerySelector(ForumsCrawler.ThreadIsAnsweredSelector))
                    .Returns(isAnsweredMock.Object);
            }
            else
            {
                threadElementMock.Setup(e => e.QuerySelector(ForumsCrawler.ThreadIsAnsweredSelector))
                    .Returns<IElement>(null);
            }

            return threadElementMock.Object;
        }

        private IElement GetIdAndTitleElements(string id, string title)
        {
            var el = new Mock<IElement>();
            el.Setup(e => e.GetAttribute(ForumsCrawler.ThreadIdAttribute))
                .Returns(id);
            el.Setup(e => e.GetAttribute(ForumsCrawler.ThreadTitleAttribute))
                .Returns(title);
            return el.Object;
        }

        private IElement GetPostsCountElement(int count)
        {
            var mock = new Mock<IElement>();
            mock.Setup(e => e.TextContent).Returns(count.ToString());
            return mock.Object;
        }
    }
}