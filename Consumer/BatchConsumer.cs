using MassTransit;
using Receive;

namespace Consumer
{
    internal class BatchConsumer : IConsumer<Batch<Payload>>
    {
        public async Task Consume(ConsumeContext<Batch<Payload>> context)
        {
            var batch = context.Message;

            var count = batch.Length; //Message Limit

            var msg = batch.Select(b => b.Message);

            var repo = new MongoRepository(); //this is very bad practice, i`m creating a new repo every time the batch runs...

            await repo.BatchInsertAsync(msg);
        }
    }
}
