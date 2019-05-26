using AutomationWithCSharp.Letters.Model;
using AutomationWithCSharp.Letters.Services.Interfaces;
using System.Collections.Generic;

namespace AutomationWithCSharp.Letters.Services
{
    public class LettersDispatcher
    {
        private readonly IAgeValidator ageValidator;
        private readonly IBadWordsValidator badWordsValidator;
        private readonly INotificationSender notificationSender;


        public LettersDispatcher()
        {
            this.ageValidator = new AgeValidator();
            this.badWordsValidator = new BadWordsValidator();
            this.notificationSender = new NotificationSender();
        }

        public void Dispatch(IEnumerable<Letter> letters)
        {
            foreach (var letter in letters)
            {
                if (this.ageValidator.IsNotOlderEnough(letter.Sender))
                {
                    this.notificationSender.Send("", letter.Sender.Relatives);
                }

                if (this.badWordsValidator.ThereAreNotBadWords(letter.Body))
                {
                    //send
                }
            }
        }
    }
}