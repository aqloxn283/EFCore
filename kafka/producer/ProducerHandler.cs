
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;

namespace producer
{
    public class ProducerHandler
    {
       public  static void ProducerTest(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Please provide the configuration file path as command line arguemnt");
            }

            IConfiguration configuration = new ConfigurationBuilder().AddIniFile(args[0]).Build();
            const string topic = "purchases";
            string[] users = { "alice", "bob", "charlie", "dave", "eve", "frank" };
            string[] items = { "apple", "banana", "carrot", "donut", "eggplant", "fig"};
            using (var produce=new ProducerBuilder<string, string>(configuration.AsEnumerable()).Build())
            {
                var numProduced = 0;
                Random rnd = new Random();
                const int numMessages = 100;
                while (numProduced < numMessages)
                {
                    var user = users[rnd.Next(users.Length)];
                    var item = items[rnd.Next(items.Length)];
                    var purchase = $"----{user}\t{item}\t{rnd.Next(1, 5)}";
                    produce.Produce(topic, new Message<string, string> { Key = user, Value = purchase }, (deleveryReport) =>
                    {
                        if (deleveryReport.Error.Code!=ErrorCode.NoError)
                        {
                            Console.WriteLine($"Delivery Error: {deleveryReport.Error.Reason}");
                        }
                        else
                        {
                            Console.WriteLine($"Delivered message to {deleveryReport.TopicPartitionOffset}");
                            numProduced++;
                        }
                    });
                    //通常应该在销毁生产者实例之前调用此方法，以确保所有排队的和正在运行的生产请求都在终止之前完成。
                    produce.Flush(TimeSpan.FromSeconds(10));
                        
                    numProduced += 1;
                  
                }
            }


            Console.Read();



        }
    }
}
