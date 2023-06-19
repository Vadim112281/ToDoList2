using ToDoList.Domain1.Enum;

namespace ToDoList.Domain1.Filters.Task;

public class TaskFilter
{
    public string Name { get; set; }
    public Priority? Priority { get; set; }
}