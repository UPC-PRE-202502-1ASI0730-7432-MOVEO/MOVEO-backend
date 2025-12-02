namespace learningcenter.Publishing.Domain.Model.Commands;

public record AddVideoAssetToTutorialCommand(string VideoUrl, int TutorialId);