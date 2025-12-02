using learningcenter.Publishing.Domain.Model.ValueObjects;

namespace learningcenter.Publishing.Domain.Model.Entities;

public class ReadableContentAsset : Asset
{
    public string ReadableContent { get; set; }
    public override bool Readable => true;
    public override bool Viewable => true;
    
    public ReadableContentAsset() : base(EAssetType.ReadableContentItem)
    {
        ReadableContent = string.Empty;
    }

    public ReadableContentAsset(string content) : base(EAssetType.ReadableContentItem)
    {
        ReadableContent = content;
    }

}