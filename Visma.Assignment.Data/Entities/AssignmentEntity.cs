using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;

namespace Visma.Assignment.Data.Entities
{
	public class AssignmentEntity
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public string Description { get; set; }

		public bool IsCompleted { get; set; }

		public IList<CommentEntity> Comments { get; set; }

		[JsonIgnore]
		public TaskEntity Task { get; set; }
	}
}