using Grpc.Core;
using OrdersService.Protos;

namespace OrdersService.Services
{
    public class OrderService : Protos.OrderService.OrderServiceBase
    {
        private static readonly List<Order> _orders = new();

        public override Task<GetOrderResponse> GetOrder(GetOrderRequest request, ServerCallContext context)
        {
            var order = _orders.FirstOrDefault(o => o.Id == request.Id);

            if (order == null)
                throw new RpcException(new Status(StatusCode.NotFound, "Order not found"));

            return Task.FromResult(new GetOrderResponse { Order = order });
        }

        public override Task<GetAllOrdersResponse> GetAllOrders(Empty request, ServerCallContext context)
        {
            var response = new GetAllOrdersResponse();
            response.Orders.AddRange(_orders);
            return Task.FromResult(response);
        }

        public override Task<CreateOrderResponse> CreateOrder(CreateOrderRequest request, ServerCallContext context)
        {
            var newOrder = new Order
            {
                Id = Guid.NewGuid().ToString(),
                CustomerName = request.CustomerName,
                Product = request.Product,
                Quantity = request.Quantity
            };

            _orders.Add(newOrder);

            return Task.FromResult(new CreateOrderResponse { Id = newOrder.Id });
        }

        public override Task<DeleteOrderResponse> DeleteOrder(DeleteOrderRequest request, ServerCallContext context)
        {
            var order = _orders.FirstOrDefault(o => o.Id == request.Id);

            if (order == null)
                return Task.FromResult(new DeleteOrderResponse { IsDeleted = false });

            _orders.Remove(order);

            return Task.FromResult(new DeleteOrderResponse { IsDeleted = true });
        }
    }
}
