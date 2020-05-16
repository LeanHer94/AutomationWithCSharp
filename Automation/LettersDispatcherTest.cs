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
                .Setup(x => x.IsNotOlderEnough(It.IsAny<Person>())) // No need for an exact match. Don't over constrain.
                .Returns(!senderIsMinor);
        }

        [Fact]
        public void Should_NotSendLetters_When_ThereAreNot()
        {
            
        }

        [Fact]
        public void Should_NotSendLetter_When_SenderIsNotOlderEnough()
        {
            // Arrange 
            var receiver = new Person();

            // Don't actually care about the age of the person as the age validator
            //   is not under test and it is mocked
            var letters = new List<Letter> { new Letter { Sender = new Person {}, Receivers = new List<Person>{ receiver }, Body = "Some Message" } }; 

            this.ageValidator
                .Setup(x => x.IsNotOlderEnough(It.IsAny<Person>())) // No need for an exact match. Don't over constrain.
                .Returns(false);

            // Act
            this.lettersDispatcher.Dispatch(letters);

            // Assert
            receiver.ReceivedLetters.Should().HaveCount(0);
        }

        [Fact]
        public void Should_NotifyToTheSenderFamily_When_SenderIsNotOlderEnough()
        {
        }

        [Fact]
        public void Should_NotSendLetter_When_TheBodyOfItHasBadWords()
        {
            // Arrange 
            var receiver = new Person();

            var letters = new List<Letter> { new Letter { Sender = new Person {}, Receivers = new List<Person>{ receiver }, Body = "Some message" } }; 

            this.ageValidator
                .Setup(x => x.IsNotOlderEnough(It.IsAny<Person>())) // No need for an exact match. Don't over constrain.
                .Returns(false);

            this.badWordsValidator
                .Setup(x => x.ThereAreNotBadWords(It.IsAny<string>())) // No need for an exact match. Don't over constrain.
                .Returns(false); 

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

            var letters = new List<Letter> { new Letter { Sender = new Person {}, Receivers = new List<Person>{ receiver }, Body = "Some Message"  } }; 

            this.ageValidator
                .Setup(x => x.IsNotOlderEnough(It.IsAny<Person>())) // No need for an exact match. Don't over constrain.
                .Returns(false);

            this.badWordsValidator
                .Setup(x => x.ThereAreNotBadWords(It.IsAny<string>())) // No need for an exact match. Don't over constrain.
                .Returns(true); 

            // Act
            this.lettersDispatcher.Dispatch(letters);

            // Assert
            receiver.ReceivedLetters.Should().HaveCount(1);
        }
    }
}