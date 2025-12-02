using learningcenter.Publishing.Domain.Model.ValueObjects;

namespace learningcenter.Publishing.Domain.Model.Entities;

public class VideoAsset : Asset
{
    public Uri? VideoUri { get; private set; }
    public override bool Readable => false;
    public override bool Viewable => true;
    
    public VideoAsset() : base(EAssetType.Video)
    {
    }

    public VideoAsset(string videoUrl) : base(EAssetType.Video)
    {
        VideoUri = new Uri(videoUrl);
    }

}