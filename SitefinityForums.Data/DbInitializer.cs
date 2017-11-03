namespace SitefinityForums.Data
{
    public class DbInitializer
    {
        public static void Initialize(SitefinityForumsContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}