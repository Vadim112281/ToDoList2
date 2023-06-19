using Microsoft.EntityFrameworkCore;
using ToDoList.DAL;
using ToDoList.DAL.Interfaces;
using ToDoList.DAL.Repository;
using ToDoList.Domain1.Entity;
using ToDoList.Servicee.Implementations;
using ToDoList.Servicee.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddScoped<ITaskRepository<TaskEntity>, TaskRepository>();

builder.Services.AddScoped<ITaskService, TaskService>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b=>b.MigrationsAssembly("ToDoList"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Task}/{action=Index}/{id?}");

app.Run();