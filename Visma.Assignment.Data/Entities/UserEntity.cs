namespace Visma.Assignment.Data.Entities
{
	public class UserEntity
	{
		public string ApplicationKey { get; set; }

		public string SharedSecret { get; set; }

		public AuthTokenEntity AuthToken { get; set; }
	}
}