namespace HighfieldQualifications.Contracts
{
    using System;

    public class Person
    {
        public uint Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
        public DateTimeOffset Dob { get; set; }
        public string FavouriteColour { get; set; }
    }
}
