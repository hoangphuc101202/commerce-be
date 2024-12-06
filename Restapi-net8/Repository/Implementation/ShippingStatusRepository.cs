using Restapi_net8.Data;
using Restapi_net8.Repository.Implementation;

public class ShippingStatusRepository: BaseRepository<ShippingStatus>, IShippingStatusRepository
{
    public ShippingStatusRepository(ApplicationDBContext context) : base(context)
    {
    }
}