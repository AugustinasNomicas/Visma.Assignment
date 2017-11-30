using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Visma.Assignment.Data.Contracts;
using Visma.Assignment.Data.Entities;

namespace Visma.Assignment.Data.Repositories
{
	public class IdentityRepository : IIdentityRepository
	{
		private static IList<UserEntity> Users { get; }

		static IdentityRepository()
		{
			var filePath = GetDatabaseFilePath();

			using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
			using (var reader = new StreamReader(stream))
			{
				Users = JsonConvert.DeserializeObject<IList<UserEntity>>(reader.ReadToEnd());
			}
		}

		public IQueryable<UserEntity> GetUsers()
		{
			return Users.AsQueryable();
		}

		public bool Login(string user, string password)
		{
			switch (user)
			{
				case "api_user":
					return "password" == password;
				case "api_user2":
					return "password2" == password;
				default:
					return false;
			}
		}

		private static string GetDatabaseFilePath()
		{
			var appDomain = AppDomain.CurrentDomain;
			var basePath = appDomain.RelativeSearchPath ?? appDomain.BaseDirectory;
			var filePath = Path.Combine(basePath, "Users.json");

			return filePath;
		}

		public bool InsertTokenForUser(UserEntity user, AuthTokenEntity authToken)
		{
			var existing = Users.SingleOrDefault(u => u.ApplicationKey == user.ApplicationKey);

			if (existing == null)
			{
				return false;
			}

			existing.AuthToken = authToken;

			File.WriteAllText(
				GetDatabaseFilePath(),
				JsonConvert.SerializeObject(Users, Formatting.Indented));

			return true;
		}
	}
}