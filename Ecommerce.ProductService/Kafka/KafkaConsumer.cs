
using Confluent.Kafka;
using Ecommerce.ProductService.Infraestructure;
using Ecommerce.Shared.Models;
using Ecommerce.Shared.Models.Orders;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Ecommerce.ProductService.Kafka;

public class KafkaConsumer(IServiceScopeFactory scopeFactory) : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.Run(() => ConsumeAsync("order-topic", stoppingToken), stoppingToken);
    }

    private async Task ConsumeAsync(string topic, CancellationToken stoppingToken)
    {
        var config = new ConsumerConfig
        {
            GroupId = "ecommerce-product-service",
            BootstrapServers = "localhost:9092",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        using var consumer = new ConsumerBuilder<string, string>(config).Build();
        consumer.Subscribe(topic);

        while(!stoppingToken.IsCancellationRequested)
        {
            var consumeResult = consumer.Consume(stoppingToken);

            var order = JsonConvert.DeserializeObject<CreateOrder>(consumeResult.Message.Value)!;
            using var scope = scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ProductDbContext>();

            var product = await dbContext.Set<Product>().FirstOrDefaultAsync(p => p.Id == order.ProductId);

            if (product is not null)
            {
                if (product.Quantity - order.Quantity < 0)
                {
                    continue;
                }

                product.Quantity -= order.Quantity;
                await dbContext.SaveChangesAsync();
            }
        }
        consumer.Close();
    }
}
