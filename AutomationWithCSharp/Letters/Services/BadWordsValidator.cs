using AutomationWithCSharp.Letters.Services.Interfaces;

namespace AutomationWithCSharp.Letters.Services
{
    public class BadWordsValidator : IBadWordsValidator
    {
        public string[] WordsToAvoid { get; } = new string[1];

        public bool ThereAreNotBadWords(string textToLookOn)
        {
            throw new System.NotImplementedException();
        }
    }
}