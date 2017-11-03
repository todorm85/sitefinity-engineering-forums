namespace SitefinityForums.Data.Crawler
{
    public class ExternalThread
    {
        public string Id { get; set; }
        public string Title { get; internal set; }
        public bool IsAnswered { get; internal set; }
        public int PostsCount { get; internal set; }
    }
}