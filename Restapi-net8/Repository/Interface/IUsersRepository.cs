using Restapi_net8.Model.Domain;

namespace Restapi_net8.Repository.Interface
{
    public interface IUsersRepository : IBaseRepository<Customer>
    {
        Task<Customer> GetEmailUser(string email);
    }
}
    