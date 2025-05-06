using TodoApi.Models;

namespace TodoApi.Repositories;

public class TodoRepository : ITodoRepository
{
  private readonly List<TodoItem> _todos = new();
  private int _nextId = 1;

  public TodoRepository()
  {
    _todos.Add(new TodoItem
    {
      Id = _nextId++,
      Title = "Learn ASP.NET CORE",
      Description = "Construir uma api REST com minimal API",
      IsComplete = false
    });

    _todos.Add(new TodoItem
    {
      Id = _nextId++,
      Title = "Practice C#",
      Description = "Resolve problens using LINQ",
      IsComplete = false
    });
  }

  public IEnumerable<TodoItem> GetAll()
  {
    return _todos;
  }

  public TodoItem? GetById(int id)
  {
    return _todos.FirstOrDefault(t => t.Id == id);
  }

  public TodoItem Create(TodoItem item)
  {
    item.Id = _nextId++;
    item.CreatedAt = DateTime.UtcNow;
    _todos.Add(item);
    return item;
  }

  public TodoItem? Update(TodoItem item)
  {
    var existingItem = _todos.FirstOrDefault(todo => todo.Id == item.Id);
    if (existingItem == null)
      return null;

    existingItem.Title = item.Title;
    existingItem.Description = item.Description;
    existingItem.IsComplete = item.IsComplete;

    return existingItem;
  }

  public TodoItem? Delete(int id)
  {
    var existingItem = _todos.FirstOrDefault(todo => todo.Id == id);
    if (existingItem != null)
    {
      _todos.Remove(existingItem);
    }
    return existingItem;
  }
}