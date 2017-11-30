using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Visma.Assignment.Data.Contracts;
using Visma.Assignment.Data.Entities;

namespace Visma.Assignment.Data.Repositories
{
    public class TasksRepository : ITasksRepository
    {
        private static IList<TaskEntity> Tasks { get; }

        static TasksRepository()
        {
	        var filePath = GetDatabaseFilePath();

			using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
			using (var reader = new StreamReader(stream))
			{
				Tasks = JsonConvert.DeserializeObject<IList<TaskEntity>>(reader.ReadToEnd());
			}

	        foreach (var taskEntity in Tasks)
	        {
		        if (taskEntity.Assignments != null && taskEntity.Assignments.Any())
		        {
					foreach (var taskEntityAssignment in taskEntity.Assignments)
					{
						taskEntityAssignment.Task = taskEntity;
					}
				}

		        if (taskEntity.Comments != null && taskEntity.Comments.Any())
		        {
					foreach (var taskEntityComment in taskEntity.Comments)
					{
						taskEntityComment.Task = taskEntity;
					}
				}
	        }
        }

	    private static string GetDatabaseFilePath()
	    {
		    var appDomain = AppDomain.CurrentDomain;
		    var basePath = appDomain.RelativeSearchPath ?? appDomain.BaseDirectory;
		    var filePath = Path.Combine(basePath, "Assignments.json");

		    return filePath;
	    }

	    public IQueryable<TaskEntity> GetAllTasks()
        {
            return Tasks.AsQueryable();
        }

	    public IQueryable<AssignmentEntity> GetAssignmentsForTask(int id)
	    {
		    var task = Tasks.SingleOrDefault(t => t.Id == id);
		    return task?.Assignments?.AsQueryable();
	    }

	    public IQueryable<CommentEntity> GetCommentsForTask(int id)
	    {
			var task = Tasks.SingleOrDefault(t => t.Id == id);
		    return task?.Comments?.AsQueryable();
		}

	    public IQueryable<TaskEntity> GetAllTasksWithoutCompleted()
	    {
		    return Tasks.Where(t => t.Status != TaskStatus.Completed).AsQueryable();
	    }

	    public TaskEntity GetTask(int id)
	    {
		    var task = Tasks.SingleOrDefault(t => t.Id == id);
		    return task;
	    }

	    public CommentEntity GetCommentForTask(int taskId, int commentId)
	    {
		    return Tasks.SingleOrDefault(t => t.Id == taskId)?
				.Comments?.SingleOrDefault(c => c.Id == commentId);
	    }

	    public IQueryable<TaskEntity> GetUserTasks(string userName)
	    {
		    return Tasks.Where(t => t.UserName == userName).AsQueryable();
	    }

	    public TaskEntity GetUserTask(string userName, int taskId)
	    {
		    return Tasks.SingleOrDefault(t => t.UserName == userName
		                                      && t.Id == taskId);
	    }

		public CommentEntity AddTaskComment(int taskId, CommentEntity comment)
		{
			var task = Tasks.SingleOrDefault(t => t.Id == taskId);

			if (task == null)
			{
				return default(CommentEntity);
			}

			var existingId = Tasks
				.Where(t => t.Comments != null && t.Comments.Any())
				.SelectMany(t => t.Comments)
				.Max(c => c.Id);
			comment.Id = ++existingId;
			comment.Task = task;

			task.Comments.Add(comment);

			File.WriteAllText(
				GetDatabaseFilePath(),
				JsonConvert.SerializeObject(Tasks, Formatting.Indented));

			return comment;
		}

	    public bool UpdateTaskComment(CommentEntity comment)
	    {
		    var existing = GetCommentForTask(comment.Task.Id, comment.Id);

		    if (existing == null)
		    {
			    return false;
		    }

		    existing.Text = comment.Text;

		    File.WriteAllText(
			    GetDatabaseFilePath(),
			    JsonConvert.SerializeObject(Tasks, Formatting.Indented));
		    return true;
	    }

		public bool DeleteTaskComment(CommentEntity comment)
		{
			var task = Tasks
				.SingleOrDefault(c => c.Id == comment.Task.Id);

			var result = task?.Comments.Remove(comment) ?? false;

			File.WriteAllText(
				GetDatabaseFilePath(),
				JsonConvert.SerializeObject(Tasks, Formatting.Indented));

			return result;
		}
	}
}