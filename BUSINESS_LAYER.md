# Business Layer Implementation

This document describes the business layer implementation following Hot Chocolate 15 conventions.

## Architecture Overview

The business layer follows a clean architecture pattern with clear separation of concerns:

```
GraphQL Layer (Types) → Business Layer (Services) → Data Layer (EF Core)
```

## Business Layer Components

### 1. Interfaces (`Business/Interfaces/`)

- **`IOrderService`**: Main service interface for order operations
  - CRUD operations for orders
  - Business logic for state transitions
  - Order line management

### 2. Models (`Business/Models/`)

- **`PagedResult<T>`**: Generic pagination wrapper
- **`CreateOrderInput`**: Input model for creating orders
- **`UpdateOrderInput`**: Input model for updating orders
- **`CreatePartyInput`**: Input model for party information
- **`CreateOrderLineInput`**: Input model for order lines

### 3. Services (`Business/Services/`)

- **`OrderService`**: Implementation of `IOrderService`
  - Encapsulates business logic
  - Handles state transitions with validation
  - Manages entity relationships

## GraphQL Layer

### 1. Queries (`Types/OrderQueries.cs`)

- **`GetOrdersAsync`**: Get all orders with filtering and sorting
- **`GetPagedOrdersAsync`**: Get paginated orders
- **`GetOrderByIdAsync`**: Get specific order by ID

### 2. Mutations (`Types/OrderMutations.cs`)

- **`CreateOrderAsync`**: Create new order
- **`UpdateOrderAsync`**: Update existing order
- **`UpdateOrderStateAsync`**: Update order state with business rules
- **`DeleteOrderAsync`**: Delete order (only provisional)
- **`AddOrderLineAsync`**: Add order line to order
- **`RemoveOrderLineAsync`**: Remove order line from order

### 3. Payloads (`Types/OrderPayloads.cs`)

- **`CreateOrderPayload`**: Result payload for create operations
- **`UpdateOrderPayload`**: Result payload for update operations
- **`DeleteOrderPayload`**: Result payload for delete operations
- **`UserError`**: Error information for failed operations

## Business Rules

### Order State Transitions

The system enforces valid state transitions:

- **Provisional** → Confirmed, Cancelled
- **Confirmed** → Shipped, Cancelled
- **Shipped** → Delivered
- **Delivered** → (final state)
- **Cancelled** → (final state)

### Business Logic

1. **Order Deletion**: Only provisional orders can be deleted
2. **Order Line Modifications**: Only allowed for provisional orders
3. **State Validation**: Invalid state transitions throw exceptions
4. **Entity Relationships**: Proper handling of related entities

## Hot Chocolate 15 Features Used

1. **Attribute-based Configuration**: `[QueryType]`, `[MutationType]`
2. **Service Injection**: `[Service]` attribute for dependency injection
3. **Filtering & Sorting**: `[UseFiltering]`, `[UseSorting]`
4. **Pagination**: `[UsePaging]` with configuration
5. **Error Handling**: Structured error responses with payloads
6. **Documentation**: XML comments for GraphQL schema documentation

## Usage Examples

### Query Orders
```graphql
query {
  orders {
    id
    orderNumber
    state
    buyer {
      name
      email
    }
    orderLines {
      productCode
      quantity
      unitPrice
    }
  }
}
```

### Create Order
```graphql
mutation {
  createOrder(input: {
    orderNumber: "ORD-001"
    buyer: {
      name: "John Doe"
      email: "john@example.com"
      address: "123 Main St"
      city: "New York"
      postalCode: "10001"
      country: "USA"
      phone: "+1-555-0123"
    }
    supplier: {
      name: "Supplier Inc"
      email: "supplier@example.com"
      address: "456 Business Ave"
      city: "Chicago"
      postalCode: "60601"
      country: "USA"
      phone: "+1-555-0456"
    }
    pickupFrom: {
      name: "Warehouse A"
      email: "warehouse@example.com"
      address: "789 Industrial Blvd"
      city: "Detroit"
      postalCode: "48201"
      country: "USA"
      phone: "+1-555-0789"
    }
    deliveryTo: {
      name: "Customer Location"
      email: "delivery@example.com"
      address: "321 Customer St"
      city: "Boston"
      postalCode: "02101"
      country: "USA"
      phone: "+1-555-0321"
    }
    orderLines: [{
      productCode: "PROD-001"
      productDescription: "Sample Product"
      quantity: 10
      unitPrice: 25.99
    }]
  }) {
    order {
      id
      orderNumber
      state
    }
    errors {
      message
      code
    }
  }
}
```

### Update Order State
```graphql
mutation {
  updateOrderState(id: "guid-here", newState: CONFIRMED) {
    id
    state
  }
}
```

## Testing

The business layer can be tested independently of the GraphQL layer by directly testing the service implementations. Unit tests should cover:

1. Business logic validation
2. State transition rules
3. Error handling scenarios
4. Entity relationship management

## Future Enhancements

1. **Validation**: Add FluentValidation for input validation
2. **Caching**: Implement caching strategies for frequently accessed data
3. **Events**: Add domain events for order state changes
4. **Audit**: Implement audit logging for business operations
5. **Authorization**: Add role-based access control
