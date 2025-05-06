using Microsoft.VisualBasic;
using TodoApi.Models;
using TodoApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ITodoRepository, TodoRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var todoApi = app.MapGroup("api/todo");

todoApi.MapGet("/", (ITodoRepository repository, ILogger<Program> logger) =>
{
    logger.LogInformation("Obtendo todas as tarefas");
    return Results.Ok(repository.GetAll());
});


todoApi.MapGet("/{id}", (int id, ITodoRepository repository, ILogger<Program> logger) =>
{
    logger.LogInformation($"obtendo tarefa pelo ID {id}");
    var item = repository.GetById(id);
    if (item == null)
    {
        logger.LogWarning($"Task with id {id} not found");
        return Results.NotFound();

    }
    return Results.Ok(item);
});

todoApi.MapPost("/", (TodoItem item, ITodoRepository repository, ILogger<Program> logger) =>
{
    logger.LogInformation("Creating new task");
    TodoItem createdItem = repository.Create(item);
    return Results.Created($"api/todo/{createdItem.Id}", createdItem);
});

todoApi.MapPut("/{id}", (int id, TodoItem item, ITodoRepository repository, Logger<Program> logger) =>
{
    if (id != item.Id)
        return Results.BadRequest("Url ID is differente from id in body");
    logger.LogInformation($"Item with id {id} is being updated...");
    var updateItem = repository.Update(item);
    if (updateItem == null)
        return Results.NotFound();
    return Results.Created("result", updateItem);
});

todoApi.MapDelete("/{id}", (int id, ITodoRepository repository, ILogger<Program> logger) =>
{
    var deleteItem = repository.Delete(id);
    if (deleteItem == null)
        return Results.NotFound();
    logger.LogInformation($"Deleted item with id {id}");
    return Results.Ok();
});