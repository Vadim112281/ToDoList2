using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain1.Filters.Task;
using ToDoList.Domain1.ViewModels;
using ToDoList.Servicee.Interfaces;


namespace ToDoList.Controllers;

public class TaskController : Controller
{
    private readonly ITaskService _service;

    public TaskController(ITaskService service)
    {
        _service = service;
    }
    
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTaskViewModel model)
    {
        var response = await _service.Create(model);

        if (response.StatusCode == Domain1.Enum.StatusCode.OK)
        {
            return  Ok(new { description = response.Description });
        }

        return BadRequest(new {description = response.Description});
    }


    public async Task<IActionResult> TaskHandler(TaskFilter filter)
    {
        var response = await _service.GetTasks(filter);
        return Json(new {data = response.Data});
    }

    [HttpPost]
    public async Task<IActionResult> EndTask(long id)
    {
        var response = await _service.EndTask(id);

        if (response.StatusCode == Domain1.Enum.StatusCode.OK)
        {
            return  Ok(new { description = response.Description });
        }

        return BadRequest(new {description = response.Description});       
    }
    
    
}