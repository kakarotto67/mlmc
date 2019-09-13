using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Mlmc.EnterpriseServiceBus.RabbitMq.MessageBus
{
    public class MessageBus : IDisposable
    {
        private readonly IConnection connection;
        private readonly IModel channel;

        private bool disposed = false;

        public MessageBus(IConnectionFactory connectionFactory)
        {
            connection = connectionFactory.CreateConnection();
            channel = connection.CreateModel();
        }

        public void PublishMessage<T>(string queue, T eventMessage)
        {
            if (String.IsNullOrEmpty(queue))
            {
                return;
            }

            // Create queue
            channel.QueueDeclare(queue, false, false, false, null);

            // Encode event message
            var eventJson = JsonConvert.SerializeObject(eventMessage);
            var eventBytes = Encoding.UTF8.GetBytes(eventJson);

            // Publish the message
            channel.BasicPublish(String.Empty, queue, null, eventBytes);
        }

        public void ConsumeMessage<T>(string queue, Action<T> handleMessage)
        {
            if (String.IsNullOrEmpty(queue))
            {
                return;
            }

            // Create queue
            channel.QueueDeclare(queue, false, false, false, null);

            // Setup Consumer
            var consumer = new EventingBasicConsumer(channel);

            // Add handler to Consumer's Received event to receive a message
            consumer.Received += (model, eventArgs) =>
            {
                var eventBytesFromMessageBus = eventArgs.Body;
                var eventJsonFromMessageBus = Encoding.UTF8.GetString(eventBytesFromMessageBus);
                var eventMessageFromMessageBus = JsonConvert.DeserializeObject<T>(eventJsonFromMessageBus);

                handleMessage(eventMessageFromMessageBus);
            };

            // Publish the message
            channel.BasicConsume(queue, true, consumer);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
            }

            if (channel != null)
            {
                channel.Dispose();
            }

            if (connection != null)
            {
                connection.Dispose();
            }

            disposed = true;
        }

        ~MessageBus()
        {
            Dispose(false);
        }
    }
}