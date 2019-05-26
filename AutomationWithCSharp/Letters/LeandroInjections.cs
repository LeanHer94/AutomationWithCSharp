using AutomationWithCSharp.Letters.Services;
using AutomationWithCSharp.Letters.Services.Interfaces;

namespace AutomationWithCSharp.Letters
{
    public class LeandroInjections
    {
        public IAgeValidator AgeValidator;
        public IBadWordsValidator BadWordsValidator;
        public INotificationSender NotificationSender;
        public ILettersDispatcher LettersDispatcher;

        public void Configure()
        {
            AgeValidator = new AgeValidator();
            BadWordsValidator = new BadWordsValidator();
            NotificationSender = new NotificationSender();

            LettersDispatcher = new LettersDispatcher(
                AgeValidator,
                BadWordsValidator,
                NotificationSender);
        }
    }
}