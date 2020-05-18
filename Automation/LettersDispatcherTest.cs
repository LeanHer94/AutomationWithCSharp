using AutomationWithCSharp.Letters.Model;
using AutomationWithCSharp.Letters.Services;
using AutomationWithCSharp.Letters.Services.Interfaces;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Automation
{
    public class LettersDispatcherTest
    {
        private readonly Mock<IAgeValidator> ageValidator;
        private readonly Mock<IBadWordsValidator> badWordsValidator;
        private readonly Mock<INotificationSender> notificationSender;

        private readonly LettersDispatcher lettersDispatcher;

        public LettersDispatcherTest()
        {
            this.ageValidator = new Mock<IAgeValidator>();
            this.badWordsValidator = new Mock<IBadWordsValidator>();
            this.notificationSender = new Mock<INotificationSender>();

            this.lettersDispatcher = new LettersDispatcher(
                this.ageValidator.Object,
                this.badWordsValidator.Object,
                this.notificationSender.Object);
        }

        protected void SetupAgeValidator(bool senderIsMinor)
        {
            this.ageValidator
                .Setup(x => x.IsNotOlderEnough(It.IsAny<Person>()))
                .Returns(senderIsMinor);
        }

        protected void SetupLanguageValidator(bool cleanSpeach)
        {
            this.badWordsValidator
                .Setup(x => x.ThereAreNotBadWords(It.IsAny<string>()))
                .Returns(cleanSpeach); 
        }

        protected void BadWords () { SetupLanguageValidator(cleanSpeach:false); }

        protected void NoBadWords () { SetupLanguageValidator(cleanSpeach:true); }

        protected void MinorSender () { SetupAgeValidator(senderIsMinor:true);  }

        protected void AdultSender () { SetupAgeValidator(senderIsMinor:false); }

        [Fact]
        public void Should_NotSendLetter_When_SenderIsNotOlderEnough()
        {
            // Arrange 
            var receiver = new Person();

            var letters = new List<Letter> { new Letter { Title="A message", Sender = new Person {}, Receivers = new List<Person>{ receiver }, Body = "Some Message" } }; 

            MinorSender();

            // Act
            this.lettersDispatcher.Dispatch(letters);

            // Assert
            receiver.ReceivedLetters.Should().HaveCount(0);
        }

        [Fact]
        public void Should_NotifyToTheSenderFamily_When_SenderIsNotOlderEnough()
        {
            // Arrange 
            var receiver = new Person();

            var momName = "Liz";

            var mom = new Person{ Name=momName };

            var family = new List<Person>{mom};

            var letters = new List<Letter> { new Letter { Title="A message", Sender = new Person { Relatives = family }, Receivers = new List<Person>{ receiver }, Body = "Some Message" } }; 

            MinorSender();

            // Act
            this.lettersDispatcher.Dispatch(letters);

            // Assert
            notificationSender.Verify(n => n.Send(It.IsAny<string>(), family), Times.AtLeastOnce());
        }

        [Fact]
        public void Should_NotSendLetter_When_TheBodyOfItHasBadWords()
        {
            // Arrange 
            var receiver = new Person();

            var letters = new List<Letter> { new Letter { Title="A message", Sender = new Person {}, Receivers = new List<Person>{ receiver }, Body = "Some message" } }; 

            AdultSender();

            BadWords();

            // Act
            this.lettersDispatcher.Dispatch(letters);

            // Assert
            receiver.ReceivedLetters.Should().HaveCount(0);
        }

        [Fact]
        public void Should_SendLetterToReceivers_When_AllValidationsPass()
        {
            // Arrange 
            var receiver = new Person();

            var letters = new List<Letter> { new Letter { Title="A message", Sender = new Person {}, Receivers = new List<Person>{ receiver }, Body = "Some Message"  } }; 

            AdultSender();

            NoBadWords();

            // Act
            this.lettersDispatcher.Dispatch(letters);

            // Assert
            receiver.ReceivedLetters.Should().HaveCount(1);
        }

        [Fact]
        public void Should_NotSendLetterToReceivers_When_ThereIsNoMessage()
        {
            // Arrange 
            var receiver = new Person();

            var letters = new List<Letter> { new Letter { Title="A message", Sender = new Person {}, Receivers = new List<Person>{ receiver } } }; 

            AdultSender();

            NoBadWords();

            // Act
            this.lettersDispatcher.Dispatch(letters);

            // Assert
            receiver.ReceivedLetters.Should().HaveCount(0);
        }

        [Fact]
        public void Should_NotSendLetterToReceivers_When_ThereIsNoSubject()
        {
            // Arrange 
            var receiver = new Person();

            var letters = new List<Letter> { new Letter { Sender = new Person {}, Receivers = new List<Person>{ receiver }, Body="A message" } }; 

            AdultSender();

            NoBadWords();

            // Act
            this.lettersDispatcher.Dispatch(letters);

            // Assert
            receiver.ReceivedLetters.Should().HaveCount(0);
        }
    }
}
