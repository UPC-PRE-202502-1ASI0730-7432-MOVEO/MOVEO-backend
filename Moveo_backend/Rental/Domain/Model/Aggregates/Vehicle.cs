using Moveo_backend.Rental.Domain.Model.ValueObjects;


namespace Moveo_backend.Rental.Domain.Model.Aggregates;

    public class Vehicle
    {
        public Guid Id { get; private set; }
        public Guid OwnerId { get; private set; }
        
        public string Brand { get; private set; }
        public string Model { get; private set; }
        public int Year { get; private set; }
        public string Color { get; private set; }
        
        public string Transmission { get; private set; }
        public string FuelType { get; private set; }
        public int Seats { get; private set; }
        
        public Money DailyPrice { get; private set; }
        public Money DepositAmount { get; private set; }
        
        public Location Location { get; private set; }
        
        public List<string> Features { get; private set; } = new();
        public List<string> Restrictions { get; private set; } = new();
        public List<string> Photos { get; private set; } = new();

        public string Status { get; private set; } = "available";

        public bool IsAvailable { get; set; } = true;

        public Vehicle(
            Guid ownerId,
            string brand,
            string model,
            int year,
            string color,
            string transmission,
            string fuelType,
            int seats,
            Money dailyPrice,
            Money depositAmount,
            Location location,
            IEnumerable<string>? features,
            IEnumerable<string>? restrictions,
            IEnumerable<string>? photos)
        {
            Id = Guid.NewGuid();
            OwnerId = ownerId;
            Brand = brand;
            Model = model;
            Year = year;
            Color = color;
            Transmission = transmission;
            FuelType = fuelType;
            Seats = seats;
            DailyPrice = dailyPrice;
            DepositAmount = depositAmount;
            Location = location;
            Features = features?.ToList() ?? new List<string>();
            Restrictions = restrictions?.ToList() ?? new List<string>();
            Photos = photos?.ToList() ?? new List<string>();
            Status = "available";
        }

        public void UpdateDetails(
            string brand,
            string model,
            int year,
            string color,
            string transmission,
            string fuelType,
            int seats,
            Money dailyPrice,
            Money depositAmount,
            Location location,
            IEnumerable<string>? features,
            IEnumerable<string>? restrictions,
            IEnumerable<string>? photos)
        {
            Brand = brand;
            Model = model;
            Year = year;
            Color = color;
            Transmission = transmission;
            FuelType = fuelType;
            Seats = seats;
            DailyPrice = dailyPrice;
            DepositAmount = depositAmount;
            Location = location;
            Features = features?.ToList() ?? new List<string>();
            Restrictions = restrictions?.ToList() ?? new List<string>();
            Photos = photos?.ToList() ?? new List<string>();
        }

        public void ChangeStatus(string status)
        {
            Status = status;
        }
    }

