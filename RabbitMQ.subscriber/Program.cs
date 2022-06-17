using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

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
            // eğer publisher'in gerçektend kuyruğu oluşturduğundan emin isek aşağıdaki tanımlamayı silebiliriz
            //  NOT: her iki tarafın da konfigüsasyonu ayanı olmalıdır. Diğer türlü hata verecektir.
            //channel.QueueDeclare("hello-queue", true, false, false);

            var consumer = new EventingBasicConsumer(channel);
            // autoAck: bu property : biz buna true verir isek RabbitMQ subscriber'e bir mesaj gönderidiğinde bu mesaj doğru da işlense yanlış da işlense 
            // kuyruktan siler, şayet biz bunu false yapar isek RabbitMQ'ya diyoruz ki sen bunu kuyruktan silme, ben gelen mesajı doğru bir şekilde işler isem o zaman ben sana haber vericem anlamını taşımaktadır.
            channel.BasicConsume("hello-queue", true, consumer);
            consumer.Received += (object sender, BasicDeliverEventArgs e) =>
            {
                var message = Encoding.UTF8.GetString(e.Body.ToArray());
            };
            Console.ReadLine();
        }


    }
}
