using GraphQLOrderExample.Business.Interfaces;
using GraphQLOrderExample.Business.Models;
using GraphQLOrderExample.Data;
using GraphQLOrderExample.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace GraphQLOrderExample.Business.Services;

public class OrderService : IOrderService
{
    private readonly OrderContext _context;

    public OrderService(OrderContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Order>> GetOrdersAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Orders
            .Include(o => o.Buyer)
            .Include(o => o.Supplier)
            .Include(o => o.PickupFrom)
            .Include(o => o.DeliveryTo)
            .Include(o => o.OrderLines)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Order?> GetOrderByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Orders
            .Include(o => o.Buyer)
            .Include(o => o.Supplier)
            .Include(o => o.PickupFrom)
            .Include(o => o.DeliveryTo)
            .Include(o => o.OrderLines)
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    public async Task<PagedResult<Order>> GetPagedOrdersAsync(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var totalCount = await _context.Orders.CountAsync(cancellationToken);
        
        var orders = await _context.Orders
            .Include(o => o.Buyer)
            .Include(o => o.Supplier)
            .Include(o => o.PickupFrom)
            .Include(o => o.DeliveryTo)
            .Include(o => o.OrderLines)
            .AsNoTracking()
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<Order>
        {
            Items = orders,
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };
    }

    public async Task<Order> CreateOrderAsync(CreateOrderInput input, CancellationToken cancellationToken = default)
    {
        var order = new Order
        {
            OrderNumber = input.OrderNumber,
            Buyer = CreatePartyFromInput(input.Buyer),
            Supplier = CreatePartyFromInput(input.Supplier),
            PickupFrom = CreatePartyFromInput(input.PickupFrom),
            DeliveryTo = CreatePartyFromInput(input.DeliveryTo),
            State = OrderState.Provisional
        };

        if (input.OrderLines != null)
        {
            foreach (var lineInput in input.OrderLines)
            {
                order.OrderLines.Add(new OrderLine
                {
                    ProductCode = lineInput.ProductCode,
                    ProductDescription = lineInput.ProductDescription,
                    Quantity = lineInput.Quantity,
                    UnitPrice = lineInput.UnitPrice,
                    Order = order
                });
            }
        }

        _context.Orders.Add(order);
        await _context.SaveChangesAsync(cancellationToken);

        return order;
    }

    public async Task<Order> UpdateOrderAsync(Guid id, UpdateOrderInput input, CancellationToken cancellationToken = default)
    {
        var order = await _context.Orders
            .Include(o => o.Buyer)
            .Include(o => o.Supplier)
            .Include(o => o.PickupFrom)
            .Include(o => o.DeliveryTo)
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);

        if (order == null)
            throw new InvalidOperationException($"Order with ID {id} not found");

        if (input.OrderNumber != null)
            order.OrderNumber = input.OrderNumber;

        if (input.Buyer != null)
            UpdatePartyFromInput(order.Buyer, input.Buyer);

        if (input.Supplier != null)
            UpdatePartyFromInput(order.Supplier, input.Supplier);

        if (input.PickupFrom != null)
            UpdatePartyFromInput(order.PickupFrom, input.PickupFrom);

        if (input.DeliveryTo != null)
            UpdatePartyFromInput(order.DeliveryTo, input.DeliveryTo);

        await _context.SaveChangesAsync(cancellationToken);
        return order;
    }

    public async Task<Order> UpdateOrderStateAsync(Guid id, OrderState newState, CancellationToken cancellationToken = default)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        
        if (order == null)
            throw new InvalidOperationException($"Order with ID {id} not found");

        // Business logic for state transitions
        ValidateStateTransition(order.State, newState);
        
        order.State = newState;
        await _context.SaveChangesAsync(cancellationToken);
        
        return order;
    }

    public async Task<bool> DeleteOrderAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        
        if (order == null)
            return false;

        // Business rule: Only allow deletion of provisional orders
        if (order.State != OrderState.Provisional)
            throw new InvalidOperationException("Only provisional orders can be deleted");

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync(cancellationToken);
        
        return true;
    }

    public async Task<Order> AddOrderLineAsync(Guid orderId, CreateOrderLineInput input, CancellationToken cancellationToken = default)
    {
        var order = await _context.Orders
            .Include(o => o.OrderLines)
            .FirstOrDefaultAsync(o => o.Id == orderId, cancellationToken);

        if (order == null)
            throw new InvalidOperationException($"Order with ID {orderId} not found");

        // Business rule: Only allow modifications to provisional orders
        if (order.State != OrderState.Provisional)
            throw new InvalidOperationException("Order lines can only be modified for provisional orders");

        var orderLine = new OrderLine
        {
            ProductCode = input.ProductCode,
            ProductDescription = input.ProductDescription,
            Quantity = input.Quantity,
            UnitPrice = input.UnitPrice,
            Order = order
        };

        order.OrderLines.Add(orderLine);
        await _context.SaveChangesAsync(cancellationToken);

        return order;
    }

    public async Task<Order> RemoveOrderLineAsync(Guid orderId, Guid orderLineId, CancellationToken cancellationToken = default)
    {
        var order = await _context.Orders
            .Include(o => o.OrderLines)
            .FirstOrDefaultAsync(o => o.Id == orderId, cancellationToken);

        if (order == null)
            throw new InvalidOperationException($"Order with ID {orderId} not found");

        // Business rule: Only allow modifications to provisional orders
        if (order.State != OrderState.Provisional)
            throw new InvalidOperationException("Order lines can only be modified for provisional orders");

        var orderLine = order.OrderLines.FirstOrDefault(ol => ol.Id == orderLineId);
        if (orderLine == null)
            throw new InvalidOperationException($"Order line with ID {orderLineId} not found");

        order.OrderLines.Remove(orderLine);
        await _context.SaveChangesAsync(cancellationToken);

        return order;
    }

    private static Party CreatePartyFromInput(CreatePartyInput input)
    {
        return new Party
        {
            Name = input.Name,
            Address = input.Address,
            City = input.City,
            PostalCode = input.PostalCode,
            Country = input.Country,
            Email = input.Email,
            Phone = input.Phone
        };
    }

    private static void UpdatePartyFromInput(Party party, CreatePartyInput input)
    {
        party.Name = input.Name;
        party.Address = input.Address;
        party.City = input.City;
        party.PostalCode = input.PostalCode;
        party.Country = input.Country;
        party.Email = input.Email;
        party.Phone = input.Phone;
    }

    private static void ValidateStateTransition(OrderState currentState, OrderState newState)
    {
        // Business rules for valid state transitions
        var validTransitions = new Dictionary<OrderState, OrderState[]>
        {
            [OrderState.Provisional] = new[] { OrderState.Confirmed, OrderState.Cancelled },
            [OrderState.Confirmed] = new[] { OrderState.Shipped, OrderState.Cancelled },
            [OrderState.Shipped] = new[] { OrderState.Delivered },
            [OrderState.Delivered] = Array.Empty<OrderState>(),
            [OrderState.Cancelled] = Array.Empty<OrderState>()
        };

        if (!validTransitions[currentState].Contains(newState))
        {
            throw new InvalidOperationException(
                $"Invalid state transition from {currentState} to {newState}");
        }
    }
}
