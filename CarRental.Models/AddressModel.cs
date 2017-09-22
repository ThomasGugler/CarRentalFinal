namespace CarRental.Models
{
    public class AddressModel
    {
        private AddressModel()
        {
        }
        public string Street { get; private set; }
        public string City { get; private set; }
        public string Postcode { get; private set; }

        public static AddressModel CreateObject(string street, string city, string postcode)
        {
            var address = new AddressModel();
            address.Street = street;
            address.City = city;
            address.Postcode = postcode;
            // TODO : PLZ validation...
            return address;
        }

        public void UpdatePostcode(string postcode)
        {
            this.Postcode = postcode;
            // TODO : Validation..
        }
    }
}
