using ToDoList.DAL.Interfaces;
using ToDoList.Domain1.Entity;

namespace ToDoList.DAL.Repository;

public class TaskRepository: ITaskRepository<TaskEntity>
{
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }


    public async Task Create(TaskEntity entity)
    {
        await _context.Tasks.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public IQueryable<TaskEntity> GetAll()
    {
        return _context.Tasks;
    }

    public async Task Delete(TaskEntity entity)
    {
         _context.Tasks.Remove(entity);
         await _context.SaveChangesAsync();
    }

    public async Task<TaskEntity> Update(TaskEntity entity)
    {
        _context.Tasks.Update(entity);
        await _context.SaveChangesAsync();

        return entity;
    }
}