using AutomationWithCSharp.Letters.Services.Interfaces;

namespace AutomationWithCSharp.Letters.Services
{
    public class BadWordsValidator : IBadWordsValidator
    {
        public readonly string[] WordsToAvoid = new string[] {
            "shit"
        };

        public bool ThereAreNotBadWords(string textToLookOn)
        {
            foreach (var wordToAvoid in WordsToAvoid)
            {
                if (textToLookOn.Contains(wordToAvoid))
                {
                    return false;
                };
            }
            return true;
        }
    }
}