﻿using AutomationWithCSharp.Letters.Model;
using AutomationWithCSharp.Letters.Services.Interfaces;
using System.Collections.Generic;

namespace AutomationWithCSharp.Letters.Services
{
    public class NotificationSender : INotificationSender
    {
        public void Send(string notification, IEnumerable<Person> receivers)
        {
            throw new System.NotImplementedException();
        }
    }
}