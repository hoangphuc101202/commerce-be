using Microsoft.EntityFrameworkCore;
using Restapi_net8.Data;
using Restapi_net8.Model.Domain;
using Restapi_net8.Repository.Interface;

namespace Restapi_net8.Repository.Implementation
{
    public class UsersRepository : BaseRepository<Customer>, IUsersRepository
    {
        public UsersRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
        }

        public async Task<Customer> GetEmailUser(string email)
        {
            return await  _dbContext.Customers.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
