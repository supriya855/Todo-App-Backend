using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using TodoApplicationWebAPI.Model;

namespace TodoApplicationWebAPI.Services
{
    public class ApplicationDBContext
    {
        private readonly IMongoCollection<TodoModel> _todo;

        public ApplicationDBContext(ITodoDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _todo = database.GetCollection<TodoModel>(settings.TodoCollectionName);
        }
        public List<TodoModel> GetAllTodos()
        {
            return _todo.Find(todo => true).ToList();
        }

       // public List<TodoModel> Get() => _todo.Find(todo => true).ToList();
        //public List<TodoModel> Get() => _todo.Find(new BsonDocument()).ToList();

        //public List<TodoModel> Get() => _todo.Find(Builders<TodoModel>.Filter.Empty).ToList();

        public TodoModel Create(TodoModel input)
        {
            _todo.InsertOne(input);
            return input;
        }
        public TodoModel Get(string id) => _todo.Find(Tasks => Tasks.Id == id).FirstOrDefault();

        public void Update(string id, TodoModel updatedTask) => _todo.ReplaceOne(Tasks => Tasks.Id == id, updatedTask);

        public void Delete(string id) => _todo.DeleteOne(Tasks => Tasks.Id == id);
    }
}