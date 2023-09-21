using Core.DataAccess;
using Core.Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IUserRepository : IEntityRepository<User>
    {
        List<OperationClaim> GetClaims(int userId);
        List<Group> GetGroups(int userId);
        Task<User> GetByRefreshToken(string refreshToken);
    }
}