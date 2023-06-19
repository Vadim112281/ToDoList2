using ToDoList.Domain1.Enum;

namespace ToDoList.Domain1.Entity;

public class TaskEntity
{
    public long Id { get; set; }
    public string Name { get; set; }
    public bool IsDone { get; set; }
    public string Description { get; set; }
    public DateTime Created { get; set; }
    public Priority Priority { get; set; }
}