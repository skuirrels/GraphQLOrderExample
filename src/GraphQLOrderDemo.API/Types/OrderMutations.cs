using GraphQLOrderExample.Business.Interfaces;
using GraphQLOrderExample.Business.Models;
using GraphQLOrderExample.DomainModels;

namespace GraphQLOrderExample.Types;

[MutationType]
public class OrderMutations
{
    /// <summary>
    /// Creates a new order
    /// </summary>
    public async Task<CreateOrderPayload> CreateOrderAsync(
        CreateOrderInput input,
        [Service] IOrderService orderService,
        CancellationToken cancellationToken)
    {
        try
        {
            var order = await orderService.CreateOrderAsync(input, cancellationToken);
            return new CreateOrderPayload(order);
        }
        catch (Exception ex)
        {
            return new CreateOrderPayload(new UserError(ex.Message, "CREATE_ORDER_ERROR"));
        }
    }

    /// <summary>
    /// Updates an existing order
    /// </summary>
    public async Task<UpdateOrderPayload> UpdateOrderAsync(
        Guid id,
        UpdateOrderInput input,
        [Service] IOrderService orderService,
        CancellationToken cancellationToken)
    {
        try
        {
            var order = await orderService.UpdateOrderAsync(id, input, cancellationToken);
            return new UpdateOrderPayload(order);
        }
        catch (Exception ex)
        {
            return new UpdateOrderPayload(new UserError(ex.Message, "UPDATE_ORDER_ERROR"));
        }
    }

    /// <summary>
    /// Updates the state of an order
    /// </summary>
    public async Task<Order> UpdateOrderStateAsync(
        Guid id,
        OrderState newState,
        [Service] IOrderService orderService,
        CancellationToken cancellationToken)
    {
        return await orderService.UpdateOrderStateAsync(id, newState, cancellationToken);
    }

    /// <summary>
    /// Deletes an order (only if provisional)
    /// </summary>
    public async Task<DeleteOrderPayload> DeleteOrderAsync(
        Guid id,
        [Service] IOrderService orderService,
        CancellationToken cancellationToken)
    {
        try
        {
            var success = await orderService.DeleteOrderAsync(id, cancellationToken);
            return new DeleteOrderPayload(success);
        }
        catch (Exception ex)
        {
            return new DeleteOrderPayload(new UserError(ex.Message, "DELETE_ORDER_ERROR"));
        }
    }

    /// <summary>
    /// Adds a new order line to an existing order
    /// </summary>
    public async Task<Order> AddOrderLineAsync(
        Guid orderId,
        CreateOrderLineInput input,
        [Service] IOrderService orderService,
        CancellationToken cancellationToken)
    {
        return await orderService.AddOrderLineAsync(orderId, input, cancellationToken);
    }

    /// <summary>
    /// Removes an order line from an existing order
    /// </summary>
    public async Task<Order> RemoveOrderLineAsync(
        Guid orderId,
        Guid orderLineId,
        [Service] IOrderService orderService,
        CancellationToken cancellationToken)
    {
        return await orderService.RemoveOrderLineAsync(orderId, orderLineId, cancellationToken);
    }
}
