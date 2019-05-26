using AutomationWithCSharp.Letters.Model;
using System.Collections.Generic;

namespace AutomationWithCSharp.Letters.Services.Interfaces
{
    public interface INotificationSender
    {
        void Send(string notification, IEnumerable<Person> receivers);
    }
}