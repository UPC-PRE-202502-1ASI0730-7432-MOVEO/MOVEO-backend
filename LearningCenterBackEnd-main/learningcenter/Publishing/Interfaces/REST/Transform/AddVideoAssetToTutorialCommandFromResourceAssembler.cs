using learningcenter.Publishing.Domain.Model.Commands;
using learningcenter.Publishing.Interfaces.REST.Resources;

namespace learningcenter.Publishing.Interfaces.REST.Transform;

public static class AddVideoAssetToTutorialCommandFromResourceAssembler
{
    public static AddVideoAssetToTutorialCommand ToCommandFromResource(AddVideoAssetToTutorialResource resource,
        int tutorialId)
    {
        return new AddVideoAssetToTutorialCommand(resource.VideoUrl, tutorialId);
    }
}