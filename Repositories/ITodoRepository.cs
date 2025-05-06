using TodoApi.Models;

namespace TodoApi.Repositories;

public interface ITodoRepository
{
  IEnumerable<TodoItem> GetAll();
  TodoItem? GetById(int id);
  TodoItem Create(TodoItem item);
  TodoItem? Update(TodoItem item);
  TodoItem? Delete(int Id);
}