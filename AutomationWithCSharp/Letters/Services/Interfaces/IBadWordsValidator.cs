namespace AutomationWithCSharp.Letters.Services.Interfaces
{
    public interface IBadWordsValidator
    {
        bool AreThereBadWords(string textToLookOn);
    }
}