using learningcenter.Publishing.Domain.Model.Entities;
using learningcenter.Publishing.Domain.Model.Commands;

namespace learningcenter.Publishing.Domain.Model.Aggregate;

public partial class Tutorial
{
    
    public Tutorial(string title, string summary, int categoryId) : this()
    {
        Title = title;
        Summary = summary;
        CategoryId = categoryId;
    }

    public Tutorial(CreateTutorialCommand command) : this(command.Title, command.Summary, command.CategoryId)
    {
    }
    public int Id { get; }
    public string Title { get; private set; }
    
    public string Summary { get; private set; }

    public int CategoryId { get; private set; }

    public Category Category { get; internal set; }
}