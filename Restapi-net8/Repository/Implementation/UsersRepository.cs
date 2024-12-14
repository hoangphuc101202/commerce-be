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
            return await  _dbContext.Customers.FirstOrDefaultAsync(x => x.Email == email && !x.IsDeleted);
        }
        public async Task<IEnumerable<Customer>>GetAllUsers(GetAllUsers request)
        {
            var query = _dbContext.Customers.AsQueryable();
            if (!string.IsNullOrEmpty(request.fullName))
            {
                query = query.Where(x => x.FullName.Contains(request.fullName));
            }
            if (!string.IsNullOrEmpty(request.email))
            {
                query = query.Where(x => x.Email.Contains(request.email));
            }
            query = query.Where(x => !x.IsDeleted);
            return await query.ToListAsync();
        }
    }
}
