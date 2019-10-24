using System.Collections.Generic;
using MicroBlogging.Entity;
using MicroBlogging.Settings;
using MongoDB.Driver;

namespace MicroBlogging.Service
{
    public class ThreadService
    {
        private readonly IMongoCollection<Thread> _thread;

        public ThreadService(IMicroBlogDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _thread = database.GetCollection<Thread>(settings.ThreadCollectionName);
        }

        public List<Thread> Get() =>
            _thread.Find(thread => true).ToList();

        public Thread Get(string id) =>
            _thread.Find<Thread>(thread => thread.Id == id).FirstOrDefault();

        public Thread Create(Thread thread)
        {
            _thread.InsertOne(thread);
            return thread;
        }

        public void Update(string id, Thread threadIn) =>
            _thread.ReplaceOne(thread => thread.Id == id, threadIn);

        public void Remove(Thread threadIn) =>
            _thread.DeleteOne(thread => thread.Id == threadIn.Id);

        public void Remove(string id) => 
            _thread.DeleteOne(thread => thread.Id == id);
    }
}