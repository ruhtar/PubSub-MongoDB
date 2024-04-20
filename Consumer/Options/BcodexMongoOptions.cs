namespace Consumer.Options;

public class BcodexMongoOptions
{
    public required string ConnectionString { get; set; }
    public required string Database { get; set; }
    public required string MongoCollection { get; set; }
}
