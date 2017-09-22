namespace CarRental.Models
{
    public class CarModel
    {
        public const string Small = "A";
        public const string Medium = "B";
        public const string Large = "C";
        public const string Luxury = "D";

        private CarModel()
        {
        }

        public int CarId { get; private set; }
        public string CarBrand { get; private set; }
        public string LicenceNumber { get; private set; }
        public float KilometerReading { get; private set; }
        public string Category { get; private set; }
        public OfficeModel Office { get; private set; }
        public int OfficeId { get; private set; }

        public static CarModel CreateObject()
        {
            var customer = new CarModel();
            return customer;
        }

        public static CarModel CreateObject(string carBrand, string licenceNumber, float kilometerReading, string category)
        {
            var customer = CreateObject();
            customer.CarBrand = carBrand;
            customer.LicenceNumber = licenceNumber;
            customer.KilometerReading = kilometerReading;
            customer.Category = category;
            return customer;
        }

        public void AddKilometers(float kilometersDriven)
        {
            this.KilometerReading += kilometersDriven;
        }
    }
}
