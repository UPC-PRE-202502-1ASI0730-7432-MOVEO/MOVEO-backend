using learningcenter.Publishing.Domain.Model.ValueObjects;

namespace learningcenter.Publishing.Domain.Model.Entities;

public partial class Asset(EAssetType type) : IPublishable
{
    
    public int Id { get; }

    public AcmeAssetIdentifier AssetIdentifier { get; private set; } = new();
    public EPublishingStatus Status { get; private set; } = EPublishingStatus.Draft;

    public EAssetType Type { get; private set; } = type;

    public virtual bool Readable => false;
    
    public virtual bool Viewable => false;

    public void SendToEdit()
    {
        Status = EPublishingStatus.ReadyToEdit;
    }

    public void SendToApproval()
    {
        Status = EPublishingStatus.ReadyToApproval;
    }

    public void ApprovedAndLock()
    {
        Status = EPublishingStatus.ApprovedAndLocked;
    }

    public void Reject()
    {
        Status = EPublishingStatus.Draft;
    }

    public void ReturnToEdit()
    {
        Status = EPublishingStatus.ReadyToEdit;
    }


    public virtual object GetContent()
    {
        return string.Empty;
    }


}