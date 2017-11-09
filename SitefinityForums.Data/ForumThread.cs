using System;

namespace SitefinityForums.Data
{
    public class ForumThread
    {
        private bool opened;
        private int postsCount;

        public ForumThread()
        {
            this.Opened = true;
        }

        public int ID { get; set; }

        public string Link { get; set; }

        public int PostsCount
        {
            get
            {
                return this.postsCount;
            }
            set
            {
                if (this.postsCount < value)
                {
                    // forum has new post on remote site
                    this.Opened = true;
                }

                this.postsCount = value;
            }
        }

        public string Title { get; set; }

        public bool Opened
        {
            get
            {
                return this.opened;
            }
            set
            {
                if (value && !this.opened)
                {
                    this.LastOpened = DateTime.UtcNow;
                }

                this.opened = value;
            }
        }

        public string AssignedTo { get; set; }
        public DateTime LastOpened { get; set; }
        public bool IsAnswered { get; set; }
        public string RemoteId { get; set; }
    }
}