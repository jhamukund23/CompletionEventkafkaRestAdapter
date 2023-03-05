using Kafka.Constants;
using Kafka.Interfaces;
using Microsoft.Extensions.Hosting;
using System.Net;

namespace CompletionEventkafkaRestAdapter
{
    public class MessageConsumer : BackgroundService
    {
        private readonly IKafkaConsumer<string, StorageEventHubData> _consumer;
        public MessageConsumer(IKafkaConsumer<string, StorageEventHubData> kafkaConsumer)
        {
            _consumer = kafkaConsumer;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                await _consumer.Consume(KafkaTopics.BlobUploadCompletedEvent, stoppingToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{(int)HttpStatusCode.InternalServerError} ConsumeFailedOnTopic - {KafkaTopics.BlobUploadCompletedEvent}, {ex}");
            }
        }

        public override void Dispose()
        {
            _consumer.Close();
            _consumer.Dispose();

            base.Dispose();
        }
    }
}
