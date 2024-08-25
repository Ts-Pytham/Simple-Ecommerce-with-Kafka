using Confluent.Kafka;

namespace Ecommerce.OrderService.Kafka;

public interface IKafkaProducer
{
    Task ProduceAsync(string topic, Message<string, string> message);
}
