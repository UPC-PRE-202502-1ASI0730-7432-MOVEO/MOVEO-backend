using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using learningcenter.IAM.Infrastructure.Persistence.EFC.Configuration.Extensions;
using learningcenter.Profiles.Infrastruture.Persistence.EFC.Configuration.Extensions;
using learningcenter.Publishing.Infrastructure.Persistence.EFC.Configuration.Extensions;
using learningcenter.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using Microsoft.EntityFrameworkCore;
namespace learningcenter.Shared.Infrastructure.Persistence.EFC.Configuration;

/// <summary>
///     Application database context for the Learning Center Platform
/// </summary>
/// <param name="options">
///     The options for the database context
/// </param>
public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    
    /// <summary>
    ///     On configuring the database context
    /// </summary>
    /// <remarks>
    ///     This method is used to configure the database context.
    ///     It also adds the created and updated date interceptor to the database context.
    /// </remarks>
    /// <param name="builder">
    ///     The option builder for the database context
    /// </param>
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(builder);
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Publishing Context
        builder.ApplyPublishingConfiguration();

        // Profiles Context
        builder.ApplyProfilesConfiguration();
        
        // IAM Context
        builder.ApplyIamConfiguration();
        
        // General Naming Convention for the database objects
        builder.UseSnakeCaseNamingConvention();
    }
}