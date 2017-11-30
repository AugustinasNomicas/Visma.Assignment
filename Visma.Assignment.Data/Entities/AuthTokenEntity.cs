using System;

namespace Visma.Assignment.Data.Entities
{
	public class AuthTokenEntity
	{
		public string AuthToken { get; set; }

		public DateTime ExpirationDate { get; set; }
	}
}