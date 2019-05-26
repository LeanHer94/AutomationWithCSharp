namespace AutomationWithCSharp.Letters.Services.Interfaces
{
    public interface IBadWordsValidator
    {
        bool ThereAreNotBadWords(string textToLookOn);
    }
}