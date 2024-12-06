using Restapi_net8.Data;
using Restapi_net8.Model.Domain;
using Restapi_net8.Repository.Implementation;

public class StatusRepository : BaseRepository<Status>, IStatusRepository
{
    public StatusRepository(ApplicationDBContext context) : base(context)
    {
    }
}