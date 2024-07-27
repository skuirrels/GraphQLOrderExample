using GraphQLOrderExample.DataContracts;

namespace GraphQLOrderExample;

using GraphQLOrderExample.DomainModels;
using GraphQLOrderExample.DataContracts;
using Mapster;

public class MappingConfig
{
    public static void RegisterMappings()
    {
        TypeAdapterConfig<Order, OrderDto>.NewConfig()
            .Map(dest => dest.OrderLines, src => src.OrderLines)
            .Map(dest => dest.Buyer, src => src.Buyer)
            .Map(dest => dest.Supplier, src => src.Supplier)
            .Map(dest => dest.PickupFrom, src => src.PickupFrom)
            .Map(dest => dest.DeliveryTo, src => src.DeliveryTo);

        TypeAdapterConfig<OrderDto, Order>.NewConfig()
            .Map(dest => dest.OrderLines, src => src.OrderLines)
            .Map(dest => dest.Buyer, src => src.Buyer)
            .Map(dest => dest.Supplier, src => src.Supplier)
            .Map(dest => dest.PickupFrom, src => src.PickupFrom)
            .Map(dest => dest.DeliveryTo, src => src.DeliveryTo);

        TypeAdapterConfig<OrderLine, OrderLineDto>.NewConfig();
        TypeAdapterConfig<OrderLineDto, OrderLine>.NewConfig();

        TypeAdapterConfig<Party, PartyDto>.NewConfig();
        TypeAdapterConfig<PartyDto, Party>.NewConfig();
    }
}