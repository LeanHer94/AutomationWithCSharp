using AutomationWithCSharp.Letters.Model;
using System.Collections.Generic;

namespace AutomationWithCSharp.Letters.Services.Interfaces
{
    public interface ILettersDispatcher
    {
        void Dispatch(IEnumerable<Letter> letters);
    }
}