namespace Moveo_backend.Rental.Domain.Model.ValueObjects;

public record Location
{
    public string District { get; }
    public string Address { get; }
    public double? Lat { get; }
    public double? Lng { get; }
    
    protected Location()
    {
        District = string.Empty;
        Address = string.Empty;
    }
    
    public Location(string district, string address, double? lat = null, double? lng = null)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("Address cannot be blank");

        District = district ?? string.Empty;
        Address = address;
        Lat = lat;
        Lng = lng;
    }

    // Constructor simple para compatibilidad
    public Location(string address) : this(string.Empty, address, null, null) { }

    public override string ToString() =>
        string.IsNullOrWhiteSpace(District) ? Address : $"{Address}, {District}";
}