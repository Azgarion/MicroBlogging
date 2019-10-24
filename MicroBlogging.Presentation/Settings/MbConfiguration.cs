namespace MicroBlogging.Settings
{
        public class MicroBlogDatabaseSettings : IMicroBlogDatabaseSettings
        {
            public string ThreadCollectionName { get; set; }
            public string ConnectionString { get; set; }
            public string DatabaseName { get; set; }
        }

        public interface IMicroBlogDatabaseSettings
        {
            string ThreadCollectionName { get; set; }
            string ConnectionString { get; set; }
            string DatabaseName { get; set; }
        }
}