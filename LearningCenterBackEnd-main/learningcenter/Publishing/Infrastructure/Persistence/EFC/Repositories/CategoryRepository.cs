using learningcenter.Publishing.Domain.Model.Entities;
using learningcenter.Publishing.Domain.Repositories;
using learningcenter.Shared.Infrastructure.Persistence.EFC.Configuration;
using learningcenter.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace learningcenter.Publishing.Infrastructure.Persistence.EFC.Repositories;

public class CategoryRepository(AppDbContext context) : BaseRepository<Category>(context), ICategoryRepository;