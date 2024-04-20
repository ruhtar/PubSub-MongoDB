using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Consumer.Repository
{
    public class MongoRepository : IMongoRepository
    {
        private readonly string connectionString = "mongodb://localhost:27017";
        private readonly IMongoCollection<Payload> _pixCollection;

        public MongoRepository()
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("transacoes");
            _pixCollection = database.GetCollection<Payload>("pix");
        }

        public async Task BatchInsertAsync(IEnumerable<Payload> elements)
        {
            await _pixCollection.InsertManyAsync(elements);
        }

        public async Task InsertJsonAsync(string json)
        {
            var document = BsonSerializer.Deserialize<Payload>(json);
            await _pixCollection.InsertOneAsync(document);
        }
    }
}
