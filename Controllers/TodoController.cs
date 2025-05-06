using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Repositories;

namespace TodoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoController(ITodoRepository repository, ILogger<TodoController> logger) : ControllerBase
{
  private readonly ITodoRepository _repository = repository;
  private readonly ILogger<TodoController> _logger = logger;

  [HttpGet]
  public ActionResult<IEnumerable<TodoItem>> GetAll()
  {
    _logger.LogInformation("Loading all tasks");
    return Ok(_repository.GetAll());
  }

  [HttpGet("{id}")]
  public ActionResult<TodoItem?> GetById(int id)
  {
    var item = _repository.GetById(id);
    if (item == null)
      return NotFound();
    _logger.LogInformation($"Get task by id {id}");
    return Ok(item);
  }
  [HttpPost]
  public ActionResult<TodoItem> Create(TodoItem item)
  {
    _logger.LogInformation("Creating new Item");
    var createdItem = _repository.Create(item);
    return Created("createdItem", createdItem);
  }

  [HttpPut("{id}")]
  public ActionResult<TodoItem?> Update(int id, TodoItem item)
  {
    if (id != item.Id)
    {
      _logger.LogWarning($"Wrong Id: {item.Id}");
      return BadRequest("URL id is differente from the one in Body");
    }
    _logger.LogInformation($"Updating item with id: {item.Id}");
    var updatedItem = _repository.Update(item);
    if (updatedItem == null)
      return NotFound();
    return Created("updatedItem", updatedItem);
  }

  [HttpDelete("{id}")]
  public ActionResult<TodoItem?> Delete(int id)
  {
    var deletedItem = _repository.Delete(id);
    if (deletedItem == null)
      return NotFound();
    _logger.LogInformation($"Deleting item with id: {id}");
    return Ok(deletedItem);
  }
}