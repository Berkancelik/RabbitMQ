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
            
            channel.BasicQos(0, 1, false);
            var consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume("hello-queue", false, consumer);

            consumer.Received += (object sender, BasicDeliverEventArgs e) =>
            {
                var message = Encoding.UTF8.GetString(e.Body.ToArray());

                Thread.Sleep(1500);
                Console.WriteLine("Gelen Mesaj" + message);
                channel.BasicAck(e.DeliveryTag, false);
            };
            Console.ReadLine();
        }


    }
}
