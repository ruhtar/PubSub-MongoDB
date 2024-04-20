using Consumer.Repository;
using MassTransit;
using System.Diagnostics;

namespace Consumer
{
    internal class BatchConsumer : IConsumer<Batch<Payload>>
    {
        private readonly IMongoRepository _mongoRepository;

        public BatchConsumer(IMongoRepository mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task Consume(ConsumeContext<Batch<Payload>> context)
        {
            var stopwatch = Stopwatch.StartNew();
            var batch = context.Message;

            var count = batch.Length; //Message Limit

            var msg = batch.Select(b => b.Message);

            await _mongoRepository.BatchInsertAsync(msg);

            stopwatch.Stop();

            Console.WriteLine($"Inserção dos dados no banco de dados concluída em {stopwatch.ElapsedMilliseconds} milissegundos.");
        }
    }
}
