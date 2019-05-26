using System.Collections.Generic;

namespace AutomationWithCSharp.Letters.Model
{
    public class Person
    {
        public int Age { get; set; }
        public Country Country { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public User PlatformUser { get; set; }
        public IEnumerable<Person> Relatives { get; set; }
        public string Surname { get; set; }
    }
}