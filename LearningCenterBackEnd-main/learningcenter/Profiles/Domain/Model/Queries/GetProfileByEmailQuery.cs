using learningcenter.Profiles.Domain.Model.ValueObjects;

namespace learningcenter.Profiles.Domain.Model.Queries;

public record GetProfileByEmailQuery(EmailAddress Email);