using AngleSharp;
using AngleSharp.Dom;

namespace SitefinityForums.Data.Crawler
{
    internal class MarkupProvider : IMarkupProvider
    {
        public IDocument GetDomDocument(string address)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var doc = BrowsingContext.New(config).OpenAsync(address);
            return doc.Result;
        }
    }
}