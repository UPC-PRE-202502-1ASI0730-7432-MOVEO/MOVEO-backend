namespace learningcenter.Publishing.Domain.Model.Entities;

public record AcmeAssetIdentifier(Guid Identifier)
{

    public AcmeAssetIdentifier() : this(Guid.NewGuid())
    {
        
    }
}