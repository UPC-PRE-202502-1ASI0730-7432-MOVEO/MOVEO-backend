namespace Moveo_backend.Rental.Domain.Model.ValueObjects;

public record Location
{
    public string Address { get; }
    public string? City { get; }
    public string? Country { get; }
    
    protected Location()
    {
        Address = string.Empty;
        City = string.Empty;
        Country = string.Empty;
    }
    
    public Location(string address, string? city = null, string? country = null)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("Address cannot be blank");

        Address = address;
        City = city;
        Country = country;
    }

    public override string ToString() =>
        string.Join(", ", new[] { Address, City, Country }.Where(s => !string.IsNullOrWhiteSpace(s)));
}