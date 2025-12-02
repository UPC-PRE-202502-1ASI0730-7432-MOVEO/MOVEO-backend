
using System.Net.Mime;
using learningcenter.Publishing.Domain.Model.ValueObjects;

namespace learningcenter.Publishing.Domain.Model.Entities;

public class ImageAsset : Asset
{
    public Uri? ImageUri { get;  }
    public override bool Readable => false;
    public override bool Viewable => true;
    
    public ImageAsset() : base(EAssetType.Image)
    {
        
    }

    public ImageAsset(string imageUrl) : base(EAssetType.Image)
    {
        ImageUri = new Uri(imageUrl);
    }
}