using System.Linq;
using Visma.Assignment.Data.Entities;

namespace Visma.Assignment.Data.Contracts
{
    public interface ITasksRepository
    {
        IQueryable<TaskEntity> GetAllTasks();

	    TaskEntity GetTask(int id);

	    IQueryable<TaskEntity> GetAllTasksWithoutCompleted();

	    IQueryable<AssignmentEntity> GetAssignmentsForTask(int id);

	    IQueryable<TaskEntity> GetUserTasks(string userName);

	    TaskEntity GetUserTask(string userName, int taskId);

	    IQueryable<CommentEntity> GetCommentsForTask(int id);

	    CommentEntity AddTaskComment(int taskId, CommentEntity comment);

	    CommentEntity GetCommentForTask(int taskId, int commentId);

	    bool DeleteTaskComment(CommentEntity existing);

	    bool UpdateTaskComment(CommentEntity comment);

    }
}