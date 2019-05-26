using System.Collections.Generic;

namespace AutomationWithCSharp.Letters.Model
{
    public class Person
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public Country Country { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public User PlatformUser { get; set; }
        public IEnumerable<Person> Relatives { get; set; }
        public ICollection<Letter> ReceivedLetters { get; set; } = new List<Letter>();
        public string Surname { get; set; }
    }
}