using Microsoft.SqlServer.Server;
using Newtonsoft.Json;

namespace Visma.Assignment.Data.Entities
{
	public class CommentEntity
	{
		public int Id { get; set; }

		public string Text { get; set; }

		public string UserName { get; set; }

		[JsonIgnore]
		public TaskEntity Task { get; set; }
	}
}