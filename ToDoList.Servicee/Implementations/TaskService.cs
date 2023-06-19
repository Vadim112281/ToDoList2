using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Logging;
using ToDoList.DAL.Interfaces;
using ToDoList.Domain1.Entity;
using ToDoList.Domain1.Enum;
using ToDoList.Domain1.Extenstions;
using ToDoList.Domain1.Filters.Task;
using ToDoList.Domain1.Response;
using ToDoList.Domain1.ViewModels;
using ToDoList.Servicee.Interfaces;

namespace ToDoList.Servicee.Implementations;

public class TaskService : ITaskService
{
    private readonly ITaskRepository<TaskEntity> _repository;
    private ILogger<TaskService> _logger;

    public TaskService(ITaskRepository<TaskEntity> repository, ILogger<TaskService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<IBaseResponse<TaskEntity>> Create(CreateTaskViewModel model)
    {
        try
        {
            model.Validate();
            _logger.LogInformation($"Запрос на создания задачи - {model.Name}");

            var task = await _repository.GetAll()
                .Where(x => x.Created.Date == DateTime.Today)
                .FirstOrDefaultAsync(x => x.Name == model.Name);

            if (task != null)
            {
                return new BaseResponse<TaskEntity>()
                {
                    Description = "Задача с таким названием уже есть",
                    StatusCode = StatusCode.TaskIsAlready
                };
            }

            task = new TaskEntity()
            {
                Name = model.Name,
                Description = model.Description,
                IsDone = false,
                Priority = model.Priority,
                Created = DateTime.Now
            };

            await _repository.Create(task);

            return new BaseResponse<TaskEntity>()
            {
                Description = "Задача создалась",
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"[TaskService.Create]: {ex.Message}");

            return new BaseResponse<TaskEntity>()
            {   
                Description = $"{ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponse<IEnumerable<TaskViewModel>>> GetTasks(TaskFilter filter)
    {
        try
        {
            var task = await _repository.GetAll()
                .Where(x=> x.IsDone == false)
                .WhereIf(!string.IsNullOrWhiteSpace(filter.Name), x => x.Name == filter.Name)
                .WhereIf(filter.Priority.HasValue, x => x.Priority == filter.Priority)
                .Select(x => new TaskViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    IsDone = x.IsDone == true ? "Ready" : "Not ready",
                    Priority = x.Priority.GetDisplayName(),
                    Created = x.Created.ToLongDateString()
                })
                .ToListAsync();

            return new BaseResponse<IEnumerable<TaskViewModel>>()
            {
                Data = task,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"[TaskService.Create]: {ex.Message}");

            return new BaseResponse<IEnumerable<TaskViewModel>>
            {   
                Description = $"{ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponse<bool>> EndTask(long id)
    {
        try
        {
            var task = await _repository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
            if (task == null)
            {
                return new BaseResponse<bool>()
                {
                    Description = "Задача не найдена",
                    StatusCode = StatusCode.TaskNotFound
                };
            }
            task.IsDone = true;
            await _repository.Update(task);
            
            return new BaseResponse<bool>()
            {
                Description = "Задача завершена",
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"[TaskService.EndTask]: {ex.Message}");

            return new BaseResponse<bool>()
            {   
                Description = $"{ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };            
        }
    }
}