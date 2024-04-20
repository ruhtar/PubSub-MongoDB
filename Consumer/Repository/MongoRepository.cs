using Consumer.Options;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Consumer.Repository
{
    public class MongoRepository : IMongoRepository
    {
        private readonly BcodexMongoOptions _options;
        private readonly IMongoCollection<Payload> _pixCollection;

        public MongoRepository(IOptions<BcodexMongoOptions> bcodexMongoOptions)
        {
            _options = bcodexMongoOptions.Value;
            var client = new MongoClient(_options.ConnectionString);
            var database = client.GetDatabase(_options.Database);
            _pixCollection = database.GetCollection<Payload>(_options.MongoCollection);
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
