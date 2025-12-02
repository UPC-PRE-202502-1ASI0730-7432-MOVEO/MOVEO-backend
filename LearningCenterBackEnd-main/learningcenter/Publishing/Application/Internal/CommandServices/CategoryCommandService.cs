using Cortex.Mediator;
using learningcenter.Publishing.Domain.Model.Commands;
using learningcenter.Publishing.Domain.Model.Entities;
using learningcenter.Publishing.Domain.Model.Events;
using learningcenter.Publishing.Domain.Repositories;
using learningcenter.Publishing.Domain.Services;
using learningcenter.Shared.Domain.Repositories;
using Microsoft.EntityFrameworkCore.Metadata;

namespace learningcenter.Publishing.Application.Internal.CommandServices;

public class CategoryCommandService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IMediator domainEventPublisher)
    : ICategoryCommandService
{
    public async Task<Category?> Handle(CreateCategoryCommand command)
    {
        var category = new Category(command);
        await categoryRepository.AddAsync(category);
        await unitOfWork.CompleteAsync();

        await domainEventPublisher.PublishAsync(new CategoryCreatedEvent(category.Name));

        return category;
        
    }
    

}