using Microsoft.EntityFrameworkCore;
using ToDoList.Domain1.Entity;

namespace ToDoList.DAL;

public class AppDbContext:DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {}

    public DbSet<TaskEntity> Tasks { get; set; }
}