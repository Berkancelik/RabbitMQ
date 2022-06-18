using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Linq;
using System.Text;
using System.Threading;

namespace RabbitMQ.subscriber
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://dhqyhvke:5ukMea46fWrkSxEq53cPQxBqr9N92sEQ@moose.rmq.cloudamqp.com/dhqyhvke");
            using var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            var randomQueueName = "log-database-save";// channel.QueueDeclare().QueueName;
            channel.QueueDeclare(randomQueueName, true, false, false);
            channel.QueueBind(randomQueueName, "logs-fanout", "", null);

            channel.BasicQos(0, 1, false);
            var consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume(randomQueueName, false, consumer);
            Console.WriteLine("Loglar dinleniyor..");

            consumer.Received += (object sender, BasicDeliverEventArgs e) =>
            {
                var message = Encoding.UTF8.GetString(e.Body.ToArray());

                Thread.Sleep(1500);
                Console.WriteLine("Gelen Mesaj" + " "+ message);
                channel.BasicAck(e.DeliveryTag, false);
            };
            Console.ReadLine();
        }


    }
}
