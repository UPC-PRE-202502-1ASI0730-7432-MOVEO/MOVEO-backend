using learningcenter.Publishing.Domain.Model.Aggregate;
using learningcenter.Publishing.Domain.Model.Commands;
using learningcenter.Publishing.Domain.Repositories;
using learningcenter.Publishing.Domain.Services;
using learningcenter.Shared.Domain.Repositories;

namespace learningcenter.Publishing.Application.Internal.CommandServices;

public class TutorialCommandService(ITutorialRepository tutorialRepository,
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork) : ITutorialCommandServices
{
    public async Task<Tutorial?> Handle(AddVideoAssetToTutorialCommand command)
    {
        var tutorial = await tutorialRepository.FindByIdAsync(command.TutorialId);

        if (tutorial is null) throw new Exception("Tutorial not found");
        
        tutorial.AddVideo(command.VideoUrl);
        await unitOfWork.CompleteAsync();
        return tutorial;
        
    }

    public async Task<Tutorial?> Handle(CreateTutorialCommand command)
    {
        var category = await categoryRepository.FindByIdAsync(command.CategoryId);
        if (category is null) throw new Exception("Category not found");
        
        if (await tutorialRepository.ExistsByTitleAsync(command.Title))
            throw new Exception("Tutorial already exists");
        var tutorial = new Tutorial(command);
        await tutorialRepository.AddAsync(tutorial);
        await unitOfWork.CompleteAsync();
        tutorial.Category = category;
        return tutorial;
    }


}