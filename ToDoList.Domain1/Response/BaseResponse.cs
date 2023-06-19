using ToDoList.Domain1.Enum;

namespace ToDoList.Domain1.Response;

public class BaseResponse<T>: IBaseResponse<T>
{
    public string Description { get; set; }
    public StatusCode StatusCode { get; set; }
    public T Data { get; set; }
}

public interface IBaseResponse<T>
{
    string Description { get;}
    StatusCode StatusCode { get;}
    T Data { get; }
}