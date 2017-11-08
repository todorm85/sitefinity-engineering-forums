using AngleSharp.Dom;

namespace SitefinityForums.Data.Crawler
{
    public interface IMarkupProvider
    {
        IDocument GetDomDocument(string address);
    }
}