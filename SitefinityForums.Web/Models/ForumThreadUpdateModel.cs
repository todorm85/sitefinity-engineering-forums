using SitefinityForums.Data;

namespace SitefinityForums.Web.Controllers
{
    public class ForumThreadUpdateModel
    {
        public int Id { get; set; }
        public bool Opened { get; set; }
        public string AssignedTo { get; set; }

        public ForumThread GetDomainModel()
        {
            var thread = new ForumThread();

            thread.ID = this.Id;
            thread.Opened = this.Opened;
            thread.AssignedTo = this.AssignedTo;

            return thread;
        }
    }
}