using AutomationWithCSharp.Letters.Model;
using AutomationWithCSharp.Letters.Services.Interfaces;
using System.Collections.Generic;

namespace AutomationWithCSharp.Letters.Services
{
    public class LettersDispatcher : ILettersDispatcher
    {
        private readonly IAgeValidator ageValidator;
        private readonly IBadWordsValidator badWordsValidator;
        private readonly INotificationSender notificationSender;


        public LettersDispatcher(
            IAgeValidator ageValidator,
            IBadWordsValidator badWordsValidator,
            INotificationSender notificationSender)
        {
            this.ageValidator = ageValidator;
            this.badWordsValidator = badWordsValidator;
            this.notificationSender = notificationSender;
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
                    foreach (var receiver in letter.Receivers)
                    {
                        receiver.ReceivedLetters.Add(letter);
                    }
                }
            }
        }
    }
}