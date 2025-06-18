using GraphQLOrderExample.DomainModels;

namespace GraphQLOrderExample.Types;

/// <summary>
/// Base payload for order operations
/// </summary>
public abstract class OrderPayloadBase
{
    protected OrderPayloadBase(Order? order = null, IReadOnlyList<UserError>? errors = null)
    {
        Order = order;
        Errors = errors ?? Array.Empty<UserError>();
    }

    public Order? Order { get; }
    public IReadOnlyList<UserError> Errors { get; }
}

/// <summary>
/// Payload for create order mutation
/// </summary>
public class CreateOrderPayload : OrderPayloadBase
{
    public CreateOrderPayload(Order order) : base(order)
    {
    }

    public CreateOrderPayload(IReadOnlyList<UserError> errors) : base(errors: errors)
    {
    }

    public CreateOrderPayload(UserError error) : base(errors: new[] { error })
    {
    }
}

/// <summary>
/// Payload for update order mutation
/// </summary>
public class UpdateOrderPayload : OrderPayloadBase
{
    public UpdateOrderPayload(Order order) : base(order)
    {
    }

    public UpdateOrderPayload(IReadOnlyList<UserError> errors) : base(errors: errors)
    {
    }

    public UpdateOrderPayload(UserError error) : base(errors: new[] { error })
    {
    }
}

/// <summary>
/// Payload for delete order mutation
/// </summary>
public class DeleteOrderPayload
{
    public DeleteOrderPayload(bool success, IReadOnlyList<UserError>? errors = null)
    {
        Success = success;
        Errors = errors ?? Array.Empty<UserError>();
    }

    public DeleteOrderPayload(UserError error)
    {
        Success = false;
        Errors = new[] { error };
    }

    public bool Success { get; }
    public IReadOnlyList<UserError> Errors { get; }
}

/// <summary>
/// User error for GraphQL operations
/// </summary>
public class UserError
{
    public UserError(string message, string? code = null, string? path = null)
    {
        Message = message;
        Code = code;
        Path = path;
    }

    public string Message { get; }
    public string? Code { get; }
    public string? Path { get; }
}
