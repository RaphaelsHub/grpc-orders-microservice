using Grpc.Net.Client;
using OrdersService.Protos;

Console.WriteLine("Starting gRPC client...");

using var channel = GrpcChannel.ForAddress("http://localhost:5218");
var client = new OrderService.OrderServiceClient(channel);

var createResponse = await client.CreateOrderAsync(new CreateOrderRequest
{
    CustomerName = "Artem",
    Product = "Laptop",
    Quantity = 2
});
Console.WriteLine($"Order created. ID: {createResponse.Id}");

var orderResponse = await client.GetOrderAsync(new GetOrderRequest
{
    Id = createResponse.Id
});
Console.WriteLine($"Order fetched: {orderResponse.Order.CustomerName} — {orderResponse.Order.Product} x{orderResponse.Order.Quantity}");

var allOrders = await client.GetAllOrdersAsync(new Empty());
Console.WriteLine($"\nAll Orders:");
foreach (var o in allOrders.Orders)
    Console.WriteLine($"• [{o.Id}] {o.CustomerName} ordered {o.Product} x{o.Quantity}");

var wasDeleted = await client.DeleteOrderAsync(new DeleteOrderRequest
{
    Id = createResponse.Id
});
Console.WriteLine($"Order Deleted Status {wasDeleted}");
