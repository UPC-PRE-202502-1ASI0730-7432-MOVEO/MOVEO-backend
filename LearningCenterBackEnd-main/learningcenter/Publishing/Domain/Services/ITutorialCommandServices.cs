using learningcenter.Publishing.Domain.Model.Aggregate;
using learningcenter.Publishing.Domain.Model.Commands;
using learningcenter.Publishing.Domain.Model.Entities;

namespace learningcenter.Publishing.Domain.Services;

public interface ITutorialCommandServices
{
    Task<Tutorial?> Handle(CreateTutorialCommand command);
    
    Task<Tutorial?> Handle(AddVideoAssetToTutorialCommand command); 
}