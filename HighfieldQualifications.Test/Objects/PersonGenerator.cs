namespace HighfieldQualifications.Test.objects
{
    using Bogus;
    using System;
    using System.Collections.Generic;
    using Person = HighfieldQualifications.Contracts.Person;

    internal sealed class PersonGenerator
    {
        private readonly string[] colours = new string[] { "blue", "red", "orange", "pink", "green", "violet" };

        public IEnumerable<Person> CreatePersonsAllProperties(uint size)
        {
            var id = 0u;

            var personFaker = new Faker<Person>()
                .StrictMode(true)
                .CustomInstantiator(x => new Person())
                .RuleFor(o => o.Id, f => id++)
                .RuleFor(o => o.Dob, f => new DateTimeOffset(f.Person.DateOfBirth))
                .RuleFor(o => o.Email, f => f.Person.Email)
                .RuleFor(o => o.FirstName, f => f.Person.FirstName)
                .RuleFor(o => o.SecondName, f => f.Person.LastName)
                .RuleFor(o => o.FavouriteColour, f => f.PickRandom(colours));

            var persons = new List<Person>(personFaker.Generate((int)size));

            return persons as IEnumerable<Person>;
        }
    }
}
