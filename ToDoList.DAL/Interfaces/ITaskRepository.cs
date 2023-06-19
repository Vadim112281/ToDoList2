using ToDoList.Domain1.ViewModels;

namespace ToDoList.DAL.Interfaces;

public interface ITaskRepository<T>
{
    Task Create(T entity);

    IQueryable<T> GetAll();

    Task Delete(T entity);

    Task<T> Update(T entity);
}