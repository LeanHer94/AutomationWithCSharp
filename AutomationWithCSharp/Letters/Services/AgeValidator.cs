using AutomationWithCSharp.Letters.Model;
using AutomationWithCSharp.Letters.Services.Interfaces;

namespace AutomationWithCSharp.Letters.Services
{
    public class AgeValidator : IAgeValidator
    {
        public const int MinimunAgeToSendLetters = 18;

        public bool IsNotOlderEnough(Person guy)
        {
            return guy.Age < MinimunAgeToSendLetters;
        }
    }
}