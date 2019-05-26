using System.Collections.Generic;

namespace AutomationWithCSharp.Letters.Model
{
    public class Letter
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public User Sender { get; set; }
        public IEnumerable<User> Receivers { get; set; }
    }
}