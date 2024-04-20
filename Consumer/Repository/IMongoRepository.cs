
namespace Consumer.Repository
{
    public interface IMongoRepository
    {
        Task BatchInsertAsync(IEnumerable<Payload> elements);
        Task InsertJsonAsync(string json);
    }
}