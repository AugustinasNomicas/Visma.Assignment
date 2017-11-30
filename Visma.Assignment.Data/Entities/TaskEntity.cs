using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Reflection;

namespace Visma.Assignment.Data.Entities
{
	public class TaskEntity
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public string Description { get; set; }

		public TaskType Type { get; set; }

		public TaskStatus Status { get; set; }

		public string Longitude { get; set; }

		public string Latitude { get; set; }

		public int Likes { get; set; }

		public int Points { get; set; }

		public DateTime CreatedDate { get; set; }

		public string UserName { get; set; }

		public IList<CommentEntity> Comments { get; set; }

		public IList<AssignmentEntity> Assignments { get; set; }
	}
}