using RabbitMQ.Client;
using System;
using System.Linq;
using System.Text;

namespace RabbitMQ.publisher
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://dhqyhvke:5ukMea46fWrkSxEq53cPQxBqr9N92sEQ@moose.rmq.cloudamqp.com/dhqyhvke");
            using var connection = factory.CreateConnection();
            var channel = connection.CreateModel();           
            channel.QueueDeclare("hello-queue", true, false, false);
            Enumerable.Range(1, 50).ToList().ForEach(x =>
            {
                string message = "hello world";
                var messageBody = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(string.Empty, "hello-queue", null, messageBody);
                Console.WriteLine("Mesaj başarılı şekilde gönderilmiştir");

            });
            Console.ReadLine();
        }


    }
}
