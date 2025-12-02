namespace Moveo_backend.AdventureRoutes.Domain.Model.Aggregates;

public class AdventureRoute
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Difficulty { get; private set; }
    public double Distance { get; private set; }
    public string EstimatedTime { get; private set; }
    public string WaypointsJson { get; private set; } = "[]";
    public string ImagesJson { get; private set; } = "[]";
    public DateTime CreatedAt { get; private set; }
    public bool IsActive { get; private set; }

    protected AdventureRoute()
    {
        Name = string.Empty;
        Description = string.Empty;
        Difficulty = string.Empty;
        EstimatedTime = string.Empty;
    }

    public AdventureRoute(
        string name,
        string description,
        string difficulty,
        double distance,
        string estimatedTime,
        string waypointsJson,
        string imagesJson)
    {
        Name = name;
        Description = description;
        Difficulty = difficulty;
        Distance = distance;
        EstimatedTime = estimatedTime;
        WaypointsJson = waypointsJson;
        ImagesJson = imagesJson;
        CreatedAt = DateTime.UtcNow;
        IsActive = true;
    }

    public void UpdateDetails(
        string name,
        string description,
        string difficulty,
        double distance,
        string estimatedTime,
        string waypointsJson,
        string imagesJson)
    {
        Name = name;
        Description = description;
        Difficulty = difficulty;
        Distance = distance;
        EstimatedTime = estimatedTime;
        WaypointsJson = waypointsJson;
        ImagesJson = imagesJson;
    }

    public void Activate() => IsActive = true;
    public void Deactivate() => IsActive = false;
}
