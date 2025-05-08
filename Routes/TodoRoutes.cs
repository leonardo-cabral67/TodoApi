using TodoApi.Models;
using TodoApi.Data;
using Microsoft.EntityFrameworkCore;

namespace TodoApi.Routes;

public static class TodoRoute
{
  public static void TodoRoutes(this WebApplication app)
  {
    var route = app.MapGroup("/api/todo");

    route.MapPost("", async (TodoItemRequest req, TodoContext context) =>
    {
      var todoItem = new TodoItem(req.title, req.description);
      await context.AddAsync(todoItem);
      await context.SaveChangesAsync();
      return Results.Created();
    });

    route.MapGet("", async (TodoContext context) =>
    {
      var todoList = await context.TodoList.ToListAsync();
      return Results.Ok(todoList);
    });

    route.MapPut("{id:guid}",
      async (Guid id, TodoItemRequest req, TodoContext context) =>
      {
        var todoItem = await context.TodoList.FirstOrDefaultAsync(x => x.Id == id);

        if (todoItem == null) return Results.NotFound();

        todoItem.ChangeTitle(req.title);
        todoItem.ChangeDescription(req.description);
        await context.SaveChangesAsync();
        return Results.Ok(todoItem);
      });

    route.MapDelete("{id:guid}",
      async (Guid id, TodoContext context) =>
      {
        var todoItem = await context.TodoList.FirstOrDefaultAsync(x => x.Id == id);
        if (todoItem == null) return Results.NotFound();
        context.TodoList.Remove(todoItem);
        await context.SaveChangesAsync();
        return Results.Ok(todoItem);
      });
  }
}