using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consumer
{
    public class ConsumerHandler
    {
        public static void ConsumerTest(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Please provide the configuration file path as command line arguemnt");
            }

            IConfiguration configuration = new ConfigurationBuilder().AddIniFile(args[0]).Build();
            configuration["group.id"] = "kafka-dotnet-getting-started";
            configuration["auto.offset.reset"] = "earliest";

            const string topic = "purchases";
            CancellationTokenSource cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true; // prevent the process from terminating.
                cts.Cancel();
            };

            using (var consumer = new ConsumerBuilder<string, string>(configuration.AsEnumerable()).Build())
            {
                consumer.Subscribe(topic);
                try
                {
                    while (true)
                    {
                        var cr = consumer.Consume(cts.Token);
                        Console.WriteLine($"Consumed event from topic {topic} with key {cr.Message.Key,-10} and value {cr.Message.Value}");
                    }
                }
                catch (OperationCanceledException)
                {
                    // Ctrl-C was pressed.
                }
                finally
                {
                    consumer.Close();
                }
            }
        }
    }
}
