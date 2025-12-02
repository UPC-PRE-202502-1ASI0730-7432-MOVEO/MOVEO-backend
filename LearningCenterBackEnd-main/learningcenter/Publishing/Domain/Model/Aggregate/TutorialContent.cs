using learningcenter.Publishing.Domain.Model.Entities;
using learningcenter.Publishing.Domain.Model.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace learningcenter.Publishing.Domain.Model.Aggregate;

public partial class Tutorial : IPublishable
{
    public ICollection<Asset> Assets { get; }

    public EPublishingStatus Status { get; protected set; }

    public bool Readable => HasReadableAssets;
    public bool Viewable => HasViewableAssets;
    
    public bool HasReadableAssets => Assets.Any(asset => asset.Readable);
    
    public bool HasViewableAssets => Assets.Any(asset => asset.Viewable);

    public Tutorial()
    {
        Title = string.Empty;
        Summary = string.Empty;
        Assets = new List<Asset>();
        Status = EPublishingStatus.Draft;
    }

    public bool HasAllAssetsWithStatus(EPublishingStatus status)
    {
        return Assets.All(asset => asset.Status == status);
    }

    public void SendToEdit()
    {
        if (HasAllAssetsWithStatus(EPublishingStatus.ReadyToEdit))
            Status = EPublishingStatus.ReadyToEdit;
    }

    public void SendToApproval()
    {
        if (HasAllAssetsWithStatus(EPublishingStatus.ReadyToApproval))
            Status = EPublishingStatus.ReadyToApproval;
    }

    public void ApprovedAndLock()
    {
        if (HasAllAssetsWithStatus(EPublishingStatus.ApprovedAndLocked))
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

    private bool ExistsImageByUrl(string imageUrl)
    {
        return Assets.Any(asset => asset.Type == EAssetType.Image && 
                                   (string) asset.GetContent() == imageUrl);
    }

    private bool ExistsVideoByUrl(string videoUrl)
    {
        return Assets.Any(asset => asset.Type == EAssetType.Video && 
                                   (string) asset.GetContent() == videoUrl);
    }

    private bool ExistsReadableContent(string content)
    {
        return Assets.Any(asset => asset.Type == EAssetType.ReadableContentItem &&
                                   (string) asset.GetContent() == content);
    }

    public void AddImage(string imageUrl)
    {
        if (ExistsImageByUrl(imageUrl)) return;
        Assets.Add(new ImageAsset(imageUrl));
    }

    public void AddVideo(string videoUrl)
    {
        if (ExistsVideoByUrl(videoUrl)) return;
        Assets.Add(new VideoAsset(videoUrl));
    }

    public void AddReadableContent(string content)
    {
        if (ExistsReadableContent(content)) return;
        Assets.Add(new ReadableContentAsset(content));
    }

    public void RemoveAsset(AcmeAssetIdentifier identifier)
    {
        var asset = Assets.FirstOrDefault(a => a.AssetIdentifier == identifier);

        if (asset is null) return;

        Assets.Remove(asset);
    }

    public void ClearAssets()
    {
        Assets.Clear();
    }

    public List<ContentItem> GetContent()
    {
        var content = new List<ContentItem>();
        
        if (Assets.Count>0)
            content.AddRange(Assets.Select(asset => 
                new ContentItem(asset.Type.ToString(), asset.GetContent() as string?? string.Empty)
            ));
        return content;
    }
}