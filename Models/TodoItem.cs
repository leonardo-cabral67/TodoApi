namespace TodoApi.Models;

public class TodoItem(string title, string description)
{
  public Guid Id { get; init; } = Guid.NewGuid();
  public string Title { get; private set; } = title;
  public string Description { get; private set; } = description;
  public bool IsComplete { get; private set; } = false;
  public DateTime CreatedAt { get; init; } = DateTime.Now;

  public void ChangeTitle(string newTitle)
  {
    Title = newTitle;
  }

  public void ChangeDescription(string newDescription)
  {
    Description = newDescription;
  }

  public void ChangeIsComplete()
  {
    IsComplete = !IsComplete;
  }
}