using AutomationWithCSharp.Letters.Model;

namespace AutomationWithCSharp.Letters.Services.Interfaces
{
    public interface IAgeValidator
    {
        bool IsOlderEnough(Person guy);
    }
}