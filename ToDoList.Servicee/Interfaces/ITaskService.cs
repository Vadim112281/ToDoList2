using ToDoList.Domain1.Entity;
using ToDoList.Domain1.Filters.Task;
using ToDoList.Domain1.Response;
using ToDoList.Domain1.ViewModels;

namespace ToDoList.Servicee.Interfaces;

public interface ITaskService
{
    Task<IBaseResponse<TaskEntity>> Create(CreateTaskViewModel model);

    Task<IBaseResponse<IEnumerable<TaskViewModel>>> GetTasks(TaskFilter filter);

    Task<IBaseResponse<bool>> EndTask(long id);
}