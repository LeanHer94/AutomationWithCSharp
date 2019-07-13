using AutomationWithCSharp.Letters;
using AutomationWithCSharp.Letters.Model;
using AutomationWithCSharp.Letters.Services;
using System.Collections.Generic;
using System.Linq;

namespace AutomationWithCSharp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var injectors = new InjectionsConfigurator();
            injectors.Configure();

            var people = PeopleInitializer.Init();

            var letters = new List<Letter>();

            people.ForEach(x =>
            {
                letters.Add(new Letter
                {
                    Title = "Letter 1",
                    Body = "shit dont like this letter. Shouldn't pass my validator",
                    Sender = x,
                    Receivers = people.Where(y => y.Id != x.Id)
                });
            });

            injectors.LettersDispatcher.Dispatch(letters);
        }
    }
}