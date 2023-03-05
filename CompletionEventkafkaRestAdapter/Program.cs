using Confluent.Kafka;
using CompletionEventkafkaRestAdapter;
using CompletionEventkafkaRestAdapter.Application;
using Kafka.Consumer;
using Kafka.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

Host.CreateDefaultBuilder(args)
   .ConfigureServices((context, services) =>
   {
       var clientConfig = new ClientConfig()
       {
           BootstrapServers = "pkc-1wvvj.westeurope.azure.confluent.cloud:9092",
           SaslUsername = "M2MDACJ5JLHT75AQ",
           SaslPassword = "iRl7o7QG2HbmvE6N1sBXJPJFhZKhhM8qePYqzVB+K0KKzrE9M28/oBChPuoLkvFt",
           SecurityProtocol = SecurityProtocol.SaslSsl,
           SaslMechanism = SaslMechanism.Plain,
           //Acks = Acks.All
       };

       var consumerConfig = new ConsumerConfig(clientConfig)
       {
           GroupId = "dss-kafka-Rest-adapter",
           //EnableAutoOffsetStore = false,
           EnableAutoCommit = true,
           AutoOffsetReset = AutoOffsetReset.Earliest,
           StatisticsIntervalMs = 5000,
           SessionTimeoutMs = 6000,
           //EnablePartitionEof = true,
           //PartitionAssignmentStrategy = PartitionAssignmentStrategy.CooperativeSticky
       };

       services.AddSingleton(consumerConfig);
       services.AddSingleton(typeof(IKafkaConsumer<,>), typeof(KafkaConsumer<,>));

       services.AddScoped<IKafkaHandler<string, StorageEventHubData>, MessageHandler>();
       services.AddHostedService<MessageConsumer>();

       services.AddTransient(typeof(IHttpClientWrapper), typeof(HttpClientWrapper));
   })
    .Build()
    .Run();
