using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Receive
{
    public class MongoRepository
    {
        private readonly string connectionString = "mongodb://localhost:27017";
        private readonly IMongoCollection<BsonDocument> _pixCollection;

        public MongoRepository()
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("transacoes");
            _pixCollection = database.GetCollection<BsonDocument>("pix");
        }

        public async Task InsertJsonAsync(string json)
        {
            var document = BsonSerializer.Deserialize<BsonDocument>(json);
            await _pixCollection.InsertOneAsync(document);
        }
    }
}
