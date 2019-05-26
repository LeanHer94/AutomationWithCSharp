using AutomationWithCSharp.Letters.Model;
using AutomationWithCSharp.Letters.Services.Interfaces;

namespace AutomationWithCSharp.Letters.Services
{
    public class AgeValidator : IAgeValidator
    {
        public int MinimunAgeToSendLetters { get; } = 18;

        public bool IsNotOlderEnough(Person guy)
        {
            return guy.Age < this.MinimunAgeToSendLetters;
        }
    }
}