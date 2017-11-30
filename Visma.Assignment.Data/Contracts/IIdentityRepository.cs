using System.Linq;
using Visma.Assignment.Data.Entities;

namespace Visma.Assignment.Data.Contracts
{
	public interface IIdentityRepository
	{
		IQueryable<UserEntity> GetUsers();

		bool Login(string user, string password);
		bool InsertTokenForUser(UserEntity user, AuthTokenEntity authToken);
	}
}