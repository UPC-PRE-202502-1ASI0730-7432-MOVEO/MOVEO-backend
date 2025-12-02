using learningcenter.Profiles.Domain.Model.Commands;
using learningcenter.Profiles.Domain.Model.ValueObjects;

namespace learningcenter.Profiles.Domain.Model.Aggregates;

public partial class Profile
{

    public int Id { get; }
    public PersonName Name { get; private set; }

    public EmailAddress Email { get; private set; }
    
    public StreetAddress Address { get; private set; }

    public string FullName => Name.FullName;
    
    public string EmailAddress => Email.Address;
    
    public string StreetAddress => Address.FullAddress;

    public Profile(string firstName, string lastName, string email, string street, string city, string state, string postalCode, string country)
    {
        Name = new PersonName(firstName, lastName);
        Email = new EmailAddress();
        Address = new StreetAddress(street, city, state, postalCode, country);
    }

    public Profile()
    {
        Name = new PersonName();
        Email = new EmailAddress();
        Address = new StreetAddress();
    }

    public Profile(CreateProfileCommand command)
    {
        Name = new PersonName(command.FirstName, command.LastName);
        Email = new EmailAddress(command.Email);
        Address = new StreetAddress(command.Street, command.Number, command.City, command.PostalCode, command.Country);
    }
}