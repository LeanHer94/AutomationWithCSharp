using AutomationWithCSharp.Letters.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationWithCSharp.Letters.Services
{
    public static class PeopleInitializer
    {
        public static List<Person> Init()
        {
            return new List<Person>
            {
                new Person
                {
                    Age = 15,
                    Country = Country.Argentina,
                    Email = "person_one@at.com",
                    Name = "person",
                    Surname = "one",
                    PlatformUser = new User { Id = 1, NickName = "theNumberOne"},
                    Relatives = new List<Person>
                    {
                        new Person { Email = "relative_personOne" }
                    }
                },
                new Person
                {
                    Age = 19,
                    Country = Country.Argentina,
                    Email = "person_two@at.com",
                    Name = "person",
                    Surname = "two",
                    PlatformUser = new User { Id = 2, NickName = "theNumberTwo"}
                },
                new Person
                {
                    Age = 25,
                    Country = Country.Argentina,
                    Email = "person_three@at.com",
                    Name = "person",
                    Surname = "three",
                    PlatformUser = new User { Id = 3, NickName = "theNumberthree"}
                },
                new Person
                {
                    Age = 12,
                    Country = Country.Argentina,
                    Email = "person_four@at.com",
                    Name = "person",
                    Surname = "four",
                    PlatformUser = new User { Id = 4, NickName = "theNumberfour"},
                    Relatives = new List<Person>
                    {
                        new Person { Email = "relative_personfour" }
                    }
                }
            };
        }
    }
}
