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
                // NOTE: Look down. Might be a good opportunity to introduce chain of responsibility pattern,
                //  or maybe list a list of Func<bool>

                if (string.IsNullOrEmpty(letter.Body))
                {
                   // TODO: Introduce a notifier that warns the sender against empty message.
                    
                    continue;
                }

                if (string.IsNullOrEmpty(letter.Title))
                {
                   // TODO: Introduce a notifier that warns the sender against no subject.
                    
                    continue;
                }

                if (this.ageValidator.IsNotOlderEnough(letter.Sender))
                {
                    this.notificationSender.Send("Your son is not older enough to send letters", letter.Sender.Relatives);

                    continue; // Good call avoiding cyclomatic complexity.
                }

                if (!this.badWordsValidator.ThereAreNotBadWords(letter.Body))
                {
                    // TODO: Introduce a notifier that warns the sender against the usage of inappropriate language.
                    
                    continue;
                }

                //send
                foreach (var receiver in letter.Receivers)
                {
                    receiver.ReceivedLetters.Add(letter);
                }
            }
        }
    }
}